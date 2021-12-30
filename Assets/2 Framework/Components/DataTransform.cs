using Components;
using Framework.Components;
using UnityEngine;

namespace DesertImage.Components
{
    public class DataTransform : DataComponent<DataTransform>
    {
        public Transform Value;
    }
    
    public class DataTransformWrapper : ComponentWrapper<DataTransform>
    {
    }
}