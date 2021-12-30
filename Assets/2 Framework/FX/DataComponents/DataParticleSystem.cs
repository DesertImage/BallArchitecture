using System;
using Components;
using UnityEngine;

namespace Framework.FX.DataComponents
{
    [Serializable]
    public class DataParticleSystem : DataComponent<DataParticleSystem>
    {
        public ParticleSystem Value;
    }
}