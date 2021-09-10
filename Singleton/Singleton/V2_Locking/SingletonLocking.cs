namespace Singleton.V2_Locking
{
    // Bad code
    // Source: https://csharpindepth.com/articles/singleton
    public sealed class SingletonLocking
    {
        private static SingletonLocking _instance;
        private static readonly object padlock = new object();

        public static SingletonLocking Instance
        {
            get
            {
                Logger.Log("Instance called.");
                lock (padlock) // this lock is used on *every* reference to SingletonLocking
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonLocking();
                    }
                    return _instance;
                }
            }
        }

        private SingletonLocking()
        {
            // cannot be created except withing this class
            Logger.Log("Constructor invoked");
        }

    }
}
