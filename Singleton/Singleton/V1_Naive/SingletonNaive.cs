namespace Singleton.V1_Naive
{
    // Bad code
#nullable enable

    public sealed class SingletonNaive
    {
        private static SingletonNaive? _instance;

        public static SingletonNaive Instance
        {
            get
            {
                Logger.Log("Instance called.");
                return _instance ??= new SingletonNaive();
            }
        }

        private SingletonNaive()
        {
            // cannot be created except withing this class
            Logger.Log("Constructor invoked.");
        }
    }
}
