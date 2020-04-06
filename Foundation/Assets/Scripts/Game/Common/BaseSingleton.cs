using System;

namespace Game.Common
{
    public abstract class BaseSingleton<T> where T : class, new()
    {
        public static T Instance
        {
            get { return lazyInstance.Value; }
        }

        protected BaseSingleton()
        {
        }

        private static readonly Lazy<T> lazyInstance = new Lazy<T>(() => new T());
    }
}