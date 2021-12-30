using UnityEngine;

namespace AssetLibraries
{
    public interface IAssetsLibrary
    {
    }

    public interface IAssetsLibrary<T> : IAssetsLibrary where T : Object
    {
        void Register(ushort id, T asset);
        T Get(ushort id);
    }
}