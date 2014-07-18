using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace i4o
{
    public class UpdateableIndexSet<T> : IndexSet<T>
    {
        private IList<T> _list;

        public UpdateableIndexSet(IList<T> source, IndexSpecification<T> indexSpecification)
            : base(source, indexSpecification)
        {
            _list = source;
        }

        public void Remove(T item)
        {
            _list.Remove(item);
            IndexDictionary.Values.ToList().ForEach(index => index.Remove(item));
        }

        public void Add(T item)
        {
            _list.Add(item);
            IndexDictionary.Values.ToList().ForEach(index => index.Add(item));
        }

        public void Update(T item)
        {
            IndexDictionary.Values.ToList().ForEach(index => index.Reset(item));
        }

    }
}
