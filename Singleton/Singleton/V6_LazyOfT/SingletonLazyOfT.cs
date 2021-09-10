using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.V6_LazyOfT
{
    // Source: https://csharpindepth.com/articles/singleton
    public sealed class SingletonLazyOfT
    {
        // reading this will initialize the instance
        public static readonly Lazy<SingletonLazyOfT> _lazy = new Lazy<SingletonLazyOfT>(() => new SingletonLazyOfT());

        public static SingletonLazyOfT Instance
        {
            get
            {
                Logger.Log("Instance called.");
                return _lazy.Value;
            }
        }

        private SingletonLazyOfT()
        {
            // cannot be created except within this class
            Logger.Log("Constructor invoked.");
        }
    }
}
