using System;
using Components;
using UniRx;

namespace BallArchitectureApp
{
    [Serializable]
    public class DataHealth : DataComponent<DataHealth>
    {
        public ReactiveProperty<float> Health;

        public float StartHealth;
    }
}