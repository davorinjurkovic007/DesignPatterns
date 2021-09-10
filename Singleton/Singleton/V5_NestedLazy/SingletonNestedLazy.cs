namespace Singleton.V5_NestedLazy
{
    // Source: https://csharpindepth.com/articles/singleton
    public sealed class SingletonNestedLazy
    {
        // reading this will initialize the instance
        public static readonly string GREETING = "Hi!";
        public static SingletonNestedLazy Instance
        {
            get
            {
                Logger.Log("Instance called.");
                return Nested._instance;
            }
        }

        private class Nested
        {
            // Tell C# compiler not to mark type as beforefieldinit (https://csharpindepth.com/articles/BeforeFieldInit)
            static Nested()
            {
            }
            internal static readonly SingletonNestedLazy _instance = new SingletonNestedLazy();
        }

        private SingletonNestedLazy()
        {
            // cannot be created except within this class
            Logger.Log("Constructor invoked.");
        }
    }
}
