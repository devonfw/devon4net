using System.Collections.Generic;

namespace Excalibur.Shared.Comparers
{
    public abstract class BaseComparer<TObservable> : IComparer<TObservable>
    {
        public abstract int Compare(TObservable x, TObservable y);
    }
}
