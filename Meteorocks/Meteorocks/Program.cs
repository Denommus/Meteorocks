using System;

namespace Meteorocks
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Meteorocks game = new Meteorocks())
            {
                game.Run();
            }
        }
    }
#endif
}

