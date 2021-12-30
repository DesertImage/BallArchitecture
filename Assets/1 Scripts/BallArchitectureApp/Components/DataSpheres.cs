using System;
using System.Collections.Generic;
using Components;
using DesertImage.Entities;

namespace BallArchitectureApp
{
    [Serializable]
    public class DataSpheres : DataComponent<DataSpheres>
    {
        public List<IEntity> Values = new List<IEntity>();
    }
}