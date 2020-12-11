using System;

namespace Excalibur.Shared.Core
{
    /// <summary>
    /// A base class for the singleton design pattern.
    /// </summary>
    /// <typeparam name="T">Class type of the singleton</typeparam>
    public abstract class ExSingletonBase<T> : IDisposable
        where T : class
    {
        /// <inheritdoc />
        ~ExSingletonBase()
        {
            this.Dispose(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose method that will dispose the singleton instance.
        /// </summary>
        /// <param name="isDisposing">Indicates if we want to dispose or not</param>
        protected void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                var instanceAsDisposable = Instance as IDisposable;
                instanceAsDisposable?.Dispose();
            }
        }
        
        #region Members

        /// <summary>
        /// Static instance. Needs to use lambda expression
        /// to construct an instance (since constructor is private).
        /// </summary>
        private static readonly Lazy<T> SInstance = new Lazy<T>(CreateInstanceOfT);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance of this singleton.
        /// </summary>
        public static T Instance => SInstance.Value;

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of T via reflection since T's constructor is expected to be private.
        /// </summary>
        /// <returns></returns>
        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T)) as T;
        }

        #endregion
    }
}
