namespace Singleton.V3_BetterLocking
{
    // Bad code
    // Source: https://csharpindepth.com/articles/singleton
    public sealed class SingletonBetterLocking
    {
        private static SingletonBetterLocking _instance = null;
        private static readonly object padlock = new object();

        public static SingletonBetterLocking Instance
        {
            get
            {
                Logger.Log("Instance called.");
                if(_instance == null) // only get a lock if the instance is null
                {
                    lock(padlock)
                    {
                        if(_instance == null)
                        {
                            _instance = new SingletonBetterLocking();
                        }
                    }
                }
                return _instance;
            }
        }

        private SingletonBetterLocking()
        {
            // cannot be created except within this class
            Logger.Log("Constructor invoked.");
        }
    }
}
