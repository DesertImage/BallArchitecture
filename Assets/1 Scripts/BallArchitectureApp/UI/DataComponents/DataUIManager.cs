using Components;
using DesertImage.UI;
using UnityEngine;

namespace BallArchitectureApp.UI
{
    public class DataUIManager : DataComponent<DataUIManager>
    {
        public IUIManager<ushort> Value;

        public Canvas Canvas;
        
        public PopupsLayer PopupsLayer;
    }
}