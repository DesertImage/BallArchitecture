using System.Collections.Generic;
using UnityEngine;

namespace AssetLibraries
{
    public class AssetsLibrary<T> : IAssetsLibrary<T> where T : Object
    {
        private readonly Dictionary<ushort, T> _assets = new Dictionary<ushort,T>();
        
        public void Register(ushort id, T asset)
        {
            if (_assets.TryGetValue(id, out _)) return;
            
            _assets.Add(id, asset);
        }

        public T Get(ushort id)
        {
            _assets.TryGetValue(id, out var obj);

            return obj;
        }
    }
}