using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace i4o
{
    public class UpdateableIndexSet<T> : IndexSet<T>
    {
        public UpdateableIndexSet(IndexSpecification<T> indexSpecification)
            : base(indexSpecification)
        {
        }

        public UpdateableIndexSet(IEnumerable<T> source, IndexSpecification<T> indexSpecification)
            : base(source, indexSpecification)
        {
        }

        public void Remove(T item)
        {
            IndexDictionary.Values.ToList().ForEach(index => index.Remove(item));
        }

        public void Add(T item)
        {
            IndexDictionary.Values.ToList().ForEach(index => index.Add(item));
        }

        public void Update(T item)
        {
            IndexDictionary.Values.ToList().ForEach(index => index.Reset(item));
        }

    }
}
