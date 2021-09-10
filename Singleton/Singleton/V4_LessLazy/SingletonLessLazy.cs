using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.V4_LessLazy
{
    // Source: https://csharpindepth.com/articles/singleton
    public sealed class SingletonLessLazy
    {
        private static readonly SingletonLessLazy _instance = new SingletonLessLazy();

        // reading this will initialize the _instance
        public static readonly string GREETING = "Hi!";

        // Tell C# compiler not to mark type as beforefieldinit
        // (https://csharpindepth.com/articles/BeforeFieldInit)
        static SingletonLessLazy()
        {
        }

        public static SingletonLessLazy Instance
        {
            get
            {
                Logger.Log("Instance called.");
                return _instance;
            }
        }

        private SingletonLessLazy()
        {
            // cannot be created except within this class
            Logger.Log("Constructor invoked.");
        }
    }
}
