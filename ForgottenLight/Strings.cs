
namespace ForgottenLight {
    class Strings {

        public static readonly string GAME_VERSION = "v1.1.0";

        public static readonly string CONTENT_ROOT_DIRECTORY = "Content";

        public static readonly string CONTENT_LIGHTNING_EFFECT = "lightning";
        public static readonly string CONTENT_SPRITE_ATLAS = "sprites/sprite_atlas";
        public static readonly string CONTENT_SPRITE_LIGHT = "light";
        public static readonly string CONTENT_SPRITE_PLAYER = "sprites/player";
        public static readonly string CONTENT_SPRITE_BRICKS = "sprites/bricks";
        public static readonly string CONTENT_SPRITE_UI_ATLAS = "ui/ui_atlas";
        public static readonly string CONTENT_SPRITE_LOGO = "logo";
        public static readonly string CONTENT_FONT_MAIN = "fonts/Main";
        public static readonly string CONTENT_LEVEL_PATH = "Content/levels/{0}.json";
        public static readonly string CONTENT_LEVEL_FIRST = "level_1";

        public static readonly string SHADER_PARAM_LIGHTMASK = "lightMask";

        public static readonly string UI_MESSAGE_OPEN = "press e to open";
        public static readonly string UI_MESSAGE_SEARCH = "Press E to search";
        public static readonly string UI_FOUND_MESSAGE = "You found {0}!\n{1}";
        public static readonly string UI_EMPTY_MESSAGE = "This storage is empty!";
        public static readonly string UI_DOOR_OPENED1 = "You opened the door!";
        public static readonly string UI_DOOR_OPENED2 = "Let's see what's inside!";
        public static readonly string UI_DOOR_NEXT_LEVEL = "Press E to get into next level!";
        public static readonly string UI_DOOR_FIND_KEY = "You must find the key first!";
        public static readonly string UI_DEATH_MESSAGE1 = "You got caught by the ghosts!";
        public static readonly string UI_DEATH_MESSAGE2 = "Maybe you have more luck next time...";
        public static readonly string UI_WON_MESSAGE1 = "You managed to escape the dungeon!";
        public static readonly string UI_WON_MESSAGE2 = "Congratulations! You won the game!";
        public static readonly string UI_WON_MESSAGE3 = "Thank you for playing The Forgotten Light! :)";
        public static readonly string UI_MENU_MOVEMENT = "Movement";
        public static readonly string UI_MENU_INTERACT = "Interact";
        public static readonly string UI_MENU_MOUSE = "Use mouse to move light";
        public static readonly string UI_MENU_PLAY = "Play";
        public static readonly string UI_MENU_CONTROLS = "Controls";
        public static readonly string UI_MENU_EXIT = "Exit";
        public static readonly string UI_MENU_MMT = "MultiMediaTechnology - FH Salzburg";
        public static readonly string UI_MENU_CREDITS = "\u00A9 Fabian Friedl 2019 | {0}"; // \u00A9 == (c)

        public static readonly string ERROR_LOG_PATH = @"error.log";
        public static readonly string ERROR_LOG_MESSAGE = "An error occurred during the execution of TheForgottenLight!";
        public static readonly string ERROR_LEVEL_READ = "Level json file could not be read!";

        public static readonly string TEST_KEY_DESCRIPTION = "This Key could be useful for something.";
        public static readonly string TEST_KEY_NAME = "Key";
    }
}
