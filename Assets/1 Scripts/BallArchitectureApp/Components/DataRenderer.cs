using System;
using Components;
using UnityEngine;

namespace BallArchitectureApp
{
    [Serializable]
    public class DataRenderer : DataComponent<DataRenderer>
    {
        public Renderer Value;
    }
}