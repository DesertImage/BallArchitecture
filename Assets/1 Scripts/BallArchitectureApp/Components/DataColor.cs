using System;
using Components;
using UnityEngine;

namespace BallArchitectureApp
{
    [Serializable]
    public class DataColor : DataComponent<DataColor>
    {
        public Color Value;
    }
}