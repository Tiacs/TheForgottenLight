using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgottenLight.Animations;
using ForgottenLight.Events;
using ForgottenLight.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForgottenLight.UI {
    class DialogBox : UIComponent {

        private Label messageLabel;

        private SpriteFont font;

        public Texture2D Background {
            get; set;
        }

        public Queue<DialogMessage> Messages {
            get; set;
        }
        
        private DialogMessage currentMessage;
        
        int currentIndex = 0;

        private Timer charDelay = new Timer(25);
        private Timer messageDelay = new Timer(1000);

        public bool IsDialogRunning {
            get; private set;
        } 

        private bool MoreCharacters {
            get {
                if (currentMessage == null) return false;
                return currentIndex < currentMessage.Length;
            }
        }

        public Color TextColor {
            get; set;
        } = CustomColor.DarkBlue;

        private Animation defaultAnimation;
        private Animation watingAnimation;
        private AnimationPlayer animationPlayer;

        private AnimationState animationState = AnimationState.DEFAULT;
        private AnimationState prevAnimationState;

        public DialogBox(SpriteFont font, ContentManager content) {
            this.font = font;

            LoadContent(content);

            this.Messages = new Queue<DialogMessage>();

            this.messageLabel = new Label(font) {
                MaxWidth = 360,
                Position = new Vector2(20, 15),
                Scale = Vector2.One * 1,
                Pivot = new Vector2(0,0),
                Color = TextColor,
                Parent = this
            };

            Input.Instance.RegisterOnKeyDownEvent(Keys.E, new Input.KeyboardEvent(this.OnEnter));
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
            
            if (currentMessage == null && Messages.Count > 0) {
                NextMessage();
            }

            PerformCurrentMessage(gameTime);

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

            if (currentMessage == null) {
                Visible = false;
            } else {
                Visible = true;
            }
        }

        private void PerformCurrentMessage(GameTime gameTime) {
            if (MoreCharacters) { // Show next character if there are more
                NextChar(gameTime);
                return;
            }

            if (currentMessage == null || !currentMessage.AutoContinue) {
                return;
            }

            if (!messageDelay.Done) {
                messageDelay.Update(gameTime);
                return;
            }

            currentMessage = null;
            messageDelay.Restart();
            if (Messages.Count <= 0) IsDialogRunning = false;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) {

            if (this.Background == null) this.Background = GetColor(spriteBatch);
            //spriteBatch.Draw(Background, new Rectangle((int)AbsolutePosition.X, (int)AbsolutePosition.Y, (int)Width, (int)Height), new Color(73, 77, 126));
            animationPlayer.Draw(spriteBatch, gameTime, AbsolutePosition, Scale);
        }
        
        private void NextMessage() { // TODO: Some delay before next message
            currentIndex = 0;
            messageLabel.Text = "";
            currentMessage = Messages.Dequeue();
            animationState = currentMessage.AutoContinue ? AnimationState.DEFAULT : AnimationState.WAITING;
            IsDialogRunning = true;
        }

        private void NextChar(GameTime gameTime) {
            if(!charDelay.Done) {
                charDelay.Update(gameTime);
                return;
            }
            messageLabel.Text += currentMessage.Text[currentIndex++];
            this.charDelay.Restart();
        }
        
        private void OnEnter() {
            if (!IsDialogRunning) return;

            if(this.Messages.Count > 0) {
                NextMessage();
                return;
            }

            if (currentMessage == null) return;

            if(currentIndex < currentMessage.Length) { // if last message -> complete message
                currentIndex = currentMessage.Length; // set index to end
                messageLabel.Text = currentMessage.Text; // set whole message to label
                return;
            }

            if(currentIndex >= currentMessage.Length && this.Messages.Count == 0) { // If last message and enter hit -> clear dialogbox
                messageLabel.Text = "";
                this.currentIndex = 0;
                this.currentMessage = null;
                this.IsDialogRunning = false;
            }
        }
        
        public void Enqueue(DialogMessage message) {
            if (IsDialogRunning) return;

            if (this.Messages.Count == 0 && currentMessage != null && currentIndex >= currentMessage.Length) {
                this.currentMessage = null;
            }
            
            this.Messages.Enqueue(message);
        }

        public void Enqueue(string message, bool autoContinue=false) {
            Enqueue(new DialogMessage(message, autoContinue));
        }

        public Texture2D GetColor(SpriteBatch spriteBatch) {
            Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            t.SetData<Color>(new Color[] { Color.White });
            return t;
        }

        private enum AnimationState {
            DEFAULT, WAITING 
        }

    }
}
