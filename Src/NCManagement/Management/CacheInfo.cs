// Copyright (c) 2015 Alachisoft
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Collections;
using Alachisoft.NCache.Caching;
using Alachisoft.NCache.Config;
using Alachisoft.NCache.Config.Dom;
using Alachisoft.NCache.Runtime.Serialization;

namespace Alachisoft.NCache.Management
{
    [Serializable]
    public class CacheInfo : ICompactSerializable
    {
           
        private LeasedCache _cache;

        public CacheServerConfig CacheProps
        {
            get
            {
                if (this._cache != null)
                {
                    return this._cache.Configuration;
                }
                return null;
            }
            set
            {
                if (this._cache != null)
                {
                    this._cache.Configuration = value;
                }
            }
        }

        internal LeasedCache Cache
        {
            get
            {
                return _cache;
            }

            set
            {
                _cache = value;
            }
        }

        public void SyncConfiguration()
        {
            //_cache.Configuration = _cacheProps;
            if (this._cache != null)
            {
                Hashtable config = ConfigConverter.ToHashtable(this.CacheProps);
                this._cache.ConfigString = ConfigReader.ToPropertiesString(config);
            }
        }

        #region ICompactSerializable Members

        public void Deserialize(Runtime.Serialization.IO.CompactReader reader)
        {
            this._cache.Configuration = reader.ReadObject() as CacheServerConfig;
        }

        public void Serialize(Runtime.Serialization.IO.CompactWriter writer)
        {
            writer.WriteObject(this._cache.Configuration);
        }

        #endregion
    }
}
