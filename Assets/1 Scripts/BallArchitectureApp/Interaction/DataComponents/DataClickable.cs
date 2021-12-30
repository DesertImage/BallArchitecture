using System;
using Components;
using DesertImage.Entities;

namespace Interaction
{
    [Serializable]
    public class DataClickable : DataComponent<DataClickable>
    {
        public IEntity ClickTarget;
    }
}