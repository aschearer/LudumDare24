using System;

namespace LudumDare24
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (LudumDareMain game = new LudumDareMain())
            {
                game.Run();
            }
        }
    }
#endif
}

