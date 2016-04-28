using System;

namespace TestEntities
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TestEntity game = new TestEntity())
            {
                game.Run();
            }
        }
    }
#endif
}

