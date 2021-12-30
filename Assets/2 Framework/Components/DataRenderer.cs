using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class DataRenderer : DataComponent<DataRenderer>
    {
        public Renderer Value;
    }
}