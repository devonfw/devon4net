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

        public SafeEnumerator(object @lock)
        {
            // entering lock in constructor
            _mLock = @lock;
            Monitor.Enter(_mLock);
        }

        public void SetList(IEnumerator<T> inner)
        {
            _mInner = inner;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            // .. and exiting lock on Dispose()
            // This will be called when for each loop finishes
            Monitor.Exit(_mLock);
        }

        #endregion

        #region Implementation of IEnumerator

        // we just delegate actual implementation
        // to the inner enumerator, that actually iterates
        // over some collection

        public bool MoveNext()
        {
            return _mInner.MoveNext();
        }

        public void Reset()
        {
            _mInner.Reset();
        }

        public T Current
        {
            get { return _mInner.Current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }
}
