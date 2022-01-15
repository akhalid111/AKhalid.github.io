using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorris.Libraries.Cache
{
    public interface ICacheProvider
    {
        void AddObjectToCache(object result);
        T GetNextObject<T>();
        T GetPreviousObject<T>();
       
        //To Do -Retreive Object from Cache by Key Identifier
        // T GetObjectFromCacheByKey<T>();
    }
}
