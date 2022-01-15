using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorris.Libraries.Cache
{
    public class CacheManager : IDisposable, ICacheProvider
    {

        #region "Properties & Variables"

        private static CacheManager singletonCacher;
        private static object lockObj = new object();
        private ICacheProvider _cache = null;
        private ICacheProvider Cache
        {
            get
            {
                if (_cache == null)
                    InitialiseCache();
                return _cache;
            }
        }

        #endregion

        #region "Methods"    
        public static CacheManager Instance()
        {

            if (singletonCacher == null)
                lock (lockObj)
                {
                    if (singletonCacher == null)
                    {
                        try
                        {
                            singletonCacher = new CacheManager();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

            return singletonCacher;

        }
        private void InitialiseCache()
        {
            var cacheType = ConfigurationManager.AppSettings["CacheType"] ?? String.Empty;
            var implementationType = cacheType ?? "LocalCache";

            if (String.IsNullOrWhiteSpace(implementationType))
            {
                implementationType = "LocalCache";
            }

            switch (implementationType)
            {
                //case "MemoryCache":
                //case "NullCache":
                //case Other types of cache
                case "LocalCache":
                default:
                    _cache = new LocalCacheProvider(); break;
            }
        }
        public void AddObjectToCache(object result)
        {
            Cache.AddObjectToCache(result);
        }
        public T GetNextObject<T>()
        {
            return Cache.GetNextObject<T>();
        }
        public T GetPreviousObject<T>()
        {
            return Cache.GetPreviousObject<T>();
        }
        public void Dispose()
        {
            _cache = null;
            singletonCacher = null;
            lockObj = null;
        }

        #endregion
    }
}
