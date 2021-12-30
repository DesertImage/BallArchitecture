using System;
using Components;
using UnityEngine;

namespace BallArchitectureApp
{
    [Serializable]
    public class DataCanvas : DataComponent<DataCanvas>
    {
        public Canvas Value;
    }
}