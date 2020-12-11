using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Excalibur.Shared.Collections
{
    /// <summary>
    /// Credits for this solution go to: 
    /// http://www.codeproject.com/Articles/56575/Thread-safe-enumeration-in-C
    /// </summary>
    public class SafeEnumerator<T> : IEnumerator<T>
    {
        // this is the (thread-unsafe)
        // enumerator of the underlying collection
        private IEnumerator<T> _mInner;
        // this is the object we shall lock on. 
        private readonly object _mLock;

        /// <inheritdoc />
        public SafeEnumerator(object @lock)
        {
            // entering lock in constructor
            _mLock = @lock;
            Monitor.Enter(_mLock);
        }

        /// <summary>
        /// Just a method that is used for actually setting the list to enumerate on.
        /// </summary>
        /// <param name="inner"></param>
        public void SetList(IEnumerator<T> inner)
        {
            _mInner = inner;
        }

        #region Implementation of IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            // .. and exiting lock on Dispose()
            // This will be called when for each loop finishes
            Monitor.Exit(_mLock);
        }

        #endregion

        #region Implementation of IEnumerator

        /// <inheritdoc />
        public bool MoveNext()
        {
            return _mInner.MoveNext();
        }

        /// <inheritdoc />
        public void Reset()
        {
            _mInner.Reset();
        }

        /// <inheritdoc />
        public T Current => _mInner.Current;

        object IEnumerator.Current => Current;

        #endregion
    }
}
