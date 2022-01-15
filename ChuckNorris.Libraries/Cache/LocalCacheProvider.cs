using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorris.Libraries.Cache
{
    public class LocalCacheProvider : ICacheProvider
    {

        LinkedList<object> _cache;
        LinkedListNode<object> _currentObject;

        public LocalCacheProvider()
        {
            _cache = new LinkedList<object>();

        }

        public void AddObjectToCache(object result)
        {

            if (_cache.Count == 0)
            {
                _cache.AddFirst(result);
                _currentObject = _cache.First;
                return;
            }

            else
            {
                var alreadyExists = _cache.Find(result) == null ? false : true;

                if (!alreadyExists)
                {
                    _cache.AddAfter(_cache.Last, result);
                    _currentObject = _cache.Last;
                }
            }
        }
        public T GetNextObject<T>()
        {
            if (_cache.Count == 0 || _currentObject == null || _currentObject.Next == null)
                return default(T);

            _currentObject = _currentObject.Next;
            return (T)_currentObject.Value;

        }
        public T GetPreviousObject<T>()
        {
            if (_cache.Count == 0 || _currentObject == null || _currentObject.Previous == null)
                return default(T);

            _currentObject = _currentObject.Previous;
            return (T)_currentObject.Value;

        }


    }
}
