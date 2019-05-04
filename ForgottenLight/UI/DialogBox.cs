using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenLight.Animations;
using ForgottenLight.Events;
using ForgottenLight.Primitives;

namespace ForgottenLight.UI {
    class DialogBox : UIComponent {

        private SpriteFont font;
        private Label messageLabel;

        private DialogMessage currentMessage;

        int currentCharacterIndex = 0;

        private Animation defaultAnimation;
        private Animation watingAnimation;
        private AnimationPlayer animationPlayer;

        private AnimationState animationState = AnimationState.DEFAULT;
        private AnimationState prevAnimationState;


        private Timer charDelay = new Timer(25);
        private Timer messageDelay = new Timer(1000);


        public Color TextColor {
            get; set;
        } = CustomColor.DarkBlue;

        public bool IsDialogRunning {
            get; private set;
        }

        private Queue<DialogMessage> Messages {
            get; set;
        }

        private bool MoreCharacters {
            get {
                if (currentMessage == null) return false;
                return currentCharacterIndex < currentMessage.Length;
            }
        }

        public DialogBox(SpriteFont font, ContentManager content) {
            this.font = font;
            this.Messages = new Queue<DialogMessage>();

            LoadContent(content);

            this.messageLabel = new Label(font) {
                MaxWidth = 360,
                Position = new Vector2(20, 15),
                Scale = Vector2.One * 1,
                Pivot = new Vector2(0,0),
                Color = TextColor,
                Parent = this
            };

            Input.Instance.RegisterOnKeyDownEvent(Keys.E, new Input.KeyboardEvent(this.OnNextKey));
        }

        private void LoadContent(ContentManager content) {
            Texture2D textureAtlas = content.Load<Texture2D>("ui/ui_atlas");

            this.defaultAnimation = new Animation(textureAtlas, 100, 400, Vector2.Zero, 1, 0, false);
            this.watingAnimation = new Animation(textureAtlas, 100, 400, new Vector2(0, 100), 4, .2f, true);

            this.animationPlayer = new AnimationPlayer(Vector2.Zero);
            this.animationPlayer.PlayAnimation(defaultAnimation);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState) {
            base.Update(gameTime, keyboardState, mouseState);
            
            if (currentMessage == null && Messages.Count > 0) { // Change message if done and new message waiting
                NextMessage();
            }

            PerformCurrentMessage(gameTime); // write current message to output and wait for next one

            // Handle Animation changes
            if(animationState != prevAnimationState) {
                switch (animationState) {
                    case AnimationState.DEFAULT:
                        this.animationPlayer.PlayAnimation(this.defaultAnimation);
                        break;
                    case AnimationState.WAITING:
                        this.animationPlayer.PlayAnimation(this.watingAnimation);
                        break;
                }
            }
            prevAnimationState = animationState;

            // Set visablity if currently dialog is running
            Visible = IsDialogRunning;
        }

        private void PerformCurrentMessage(GameTime gameTime) {
            if (MoreCharacters) { // Show next character if there are more
                NextChar(gameTime);
                return;
            }

            if (currentMessage == null || !currentMessage.AutoContinue) { // Wait for input if no autocontinue
                return;
            }

            if (!messageDelay.Done) { // If delay not over; wait for delay completion
                messageDelay.Update(gameTime);
                return;
            }

            // If current message done; remove it
            currentMessage = null;
            messageDelay.Restart(); // restart delay timer for next time
            if (Messages.Count <= 0) IsDialogRunning = false; // if no more messages; finish dialog
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) {
            animationPlayer.Draw(spriteBatch, gameTime, AbsolutePosition, Scale);
        }
        
        private void NextMessage() {
            // Reset message and counters
            currentCharacterIndex = 0;
            messageLabel.Text = "";

            // get new message
            currentMessage = Messages.Dequeue();

            // Show animation
            animationState = currentMessage.AutoContinue ? AnimationState.DEFAULT : AnimationState.WAITING;
            IsDialogRunning = true;
        }

        private void NextChar(GameTime gameTime) {
            if(!charDelay.Done) { // if delay not over; wait for completion
                charDelay.Update(gameTime);
                return;
            }
            messageLabel.Text += currentMessage.Text[currentCharacterIndex++]; // append next char
            this.charDelay.Restart(); // restart delay for next char
        }
        
        private void OnNextKey() {
            if (!IsDialogRunning) return;

            if(this.Messages.Count > 0) { // If more messages; show next one
                NextMessage();
                return;
            }
            
            if(currentCharacterIndex < currentMessage.Length) { // if last message -> complete message
                currentCharacterIndex = currentMessage.Length; // set index to end
                messageLabel.Text = currentMessage.Text; // set whole message to label
                return;
            }

            if(currentCharacterIndex >= currentMessage.Length && this.Messages.Count == 0) { // If last message and next hit -> clear dialogbox
                messageLabel.Text = "";
                this.currentCharacterIndex = 0;
                this.currentMessage = null;
                this.IsDialogRunning = false;
            }
        }
        
        public void Enqueue(DialogMessage message) {
            if (IsDialogRunning) return;
            this.Messages.Enqueue(message);
        }

        public void Enqueue(string message, bool autoContinue=false) {
            Enqueue(new DialogMessage(message, autoContinue));
        }
        
        private enum AnimationState {
            DEFAULT, WAITING 
        }

    }
}
