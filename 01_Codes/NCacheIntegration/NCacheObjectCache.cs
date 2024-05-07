using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Alachisoft.NCache.Web.Caching;

namespace NCacheIntegration
{
    public class NCacheObjectCache : ObjectCache
    {
        string cacheName;
        Cache _nCache;
        Cache nCache
        {
            get
            {
                if (_nCache == null)
                    _nCache = NCache.InitializeCache(cacheName);
                return _nCache;
            }
        }

        /// <summary>
        /// Cache name as in client.ncconf
        /// </summary>
        /// <param name="cacheName"></param>
        public NCacheObjectCache(string cacheName)
        {
            this.cacheName = cacheName;
        }

        object GetNCache(string key)
        {
            try
            {
                return nCache.Get(key);
            }
            catch
            {
                if (_nCache != null)
                    _nCache.Dispose();
                _nCache = null;
                throw;
            }
        }

        bool ContainNCache(string key)
        {
            try
            {
                return nCache.Contains(key);
            }
            catch
            {
                if (_nCache != null)
                    _nCache.Dispose();
                _nCache = null;
                throw;
            }
        }

        object RemoveNCache(string key)
        {
            try
            {
                return nCache.Remove(key);
            }
            catch
            {
                if (_nCache != null)
                    _nCache.Dispose();
                _nCache = null;
                throw;
            }
        }

        void InsertNCache(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration, Alachisoft.NCache.Runtime.CacheItemPriority priority)
        {
            try
            {
                nCache.Insert(key, value, absoluteExpiration, slidingExpiration, priority);
            }
            catch
            {
                if (_nCache != null)
                    _nCache.Dispose();
                _nCache = null;
                throw;
            }
        }

        void InsertNCache(string key, object value)
        {
            try
            {
                nCache.Insert(key, value);
            }
            catch
            {
                if (_nCache != null)
                    _nCache.Dispose();
                _nCache = null;
                throw;
            }
        }

        long CountNCache()
        {
            try
            {
                return nCache.Count;
            }
            catch
            {
                if (_nCache != null)
                    _nCache.Dispose();
                _nCache = null;
                throw;
            }
        }

        public override object this[string key]
        {
            get
            {
                return GetNCache(key);
            }

            set
            {
                InsertNCache(key, value);
            }
        }

        public override DefaultCacheCapabilities DefaultCacheCapabilities
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override System.Runtime.Caching.CacheItem AddOrGetExisting(System.Runtime.Caching.CacheItem value, CacheItemPolicy policy)
        {
            if (value.RegionName != null) throw new NotSupportedException();

            var obj = GetCacheItem(value.Key, value.RegionName);
            if (obj != null) return obj;

            Set(value, policy);
            return value;
        }

        public override object AddOrGetExisting(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
            if (regionName != null) throw new NotSupportedException();

            var obj = GetNCache(key);
            if (obj != null) return obj;

            Set(key, value, policy, regionName);

            return value;
        }

        public override object AddOrGetExisting(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
            if (regionName != null) throw new NotSupportedException();

            var obj = GetNCache(key);
            if (obj != null) return obj;

            Set(key, value, absoluteExpiration, regionName);

            return value;
        }

        public override bool Contains(string key, string regionName = null)
        {
            if (regionName != null) throw new NotSupportedException();

            return ContainNCache(key);
        }

        public override CacheEntryChangeMonitor CreateCacheEntryChangeMonitor(IEnumerable<string> keys, string regionName = null)
        {
            throw new NotImplementedException();
        }

        public override object Get(string key, string regionName = null)
        {
            if (regionName != null) throw new NotSupportedException();

            return GetNCache(key);
        }

        public override System.Runtime.Caching.CacheItem GetCacheItem(string key, string regionName = null)
        {
            var obj = GetNCache(key);

            if (obj == null) return null;

            return new System.Runtime.Caching.CacheItem(key, obj, regionName);
        }

        public override long GetCount(string regionName = null)
        {
            return CountNCache();
        }

        public override IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName = null)
        {
            throw new NotImplementedException();
        }

        public override object Remove(string key, string regionName = null)
        {
            if (regionName != null) throw new NotSupportedException();

            return RemoveNCache(key);
        }

        public override void Set(System.Runtime.Caching.CacheItem item, CacheItemPolicy policy)
        {
            Set(item.Key, item.Value, policy, item.RegionName);
        }


        Alachisoft.NCache.Runtime.CacheItemPriority ConvertToCacheItemPriority(System.Runtime.Caching.CacheItemPriority prioirty)
        {
            if (prioirty == CacheItemPriority.NotRemovable) return Alachisoft.NCache.Runtime.CacheItemPriority.NotRemovable;
            else return Alachisoft.NCache.Runtime.CacheItemPriority.Default;
        }

        public override void Set(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
            if (regionName != null) throw new NotSupportedException();

            if (policy.AbsoluteExpiration != ObjectCache.InfiniteAbsoluteExpiration)
                InsertNCache(key, value, policy.AbsoluteExpiration.DateTime, Cache.NoSlidingExpiration, ConvertToCacheItemPriority(policy.Priority));
            else
                InsertNCache(key, value, Cache.NoAbsoluteExpiration, policy.SlidingExpiration, ConvertToCacheItemPriority(policy.Priority));
        }

        public override void Set(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
            if (regionName != null) throw new NotSupportedException();

            InsertNCache(key, value, absoluteExpiration.DateTime, Cache.NoSlidingExpiration, Alachisoft.NCache.Runtime.CacheItemPriority.Default);
        }

        protected override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
