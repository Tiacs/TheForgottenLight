/*
 * Fabian Friedl MMP1
 * MultiMediaTechnology FH-Salzburg
 * 2019
 */

using System;

namespace ForgottenLight {
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            try
            {
                using (var game = new Game1())
                    game.Run();
            }
            catch(Exception e)
            {
                WriteToLog(e);
                throw;
            }
        }

        public static void WriteToLog(Exception e) {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Strings.ERROR_LOG_PATH, false)) {
                file.WriteLine(Strings.ERROR_LOG_MESSAGE);
                file.WriteLine(e.ToString());
            }
        }
    }
}
