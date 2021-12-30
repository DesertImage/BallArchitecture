using System;
using Components;
using DesertImage.UI;

namespace BallArchitectureApp.UI
{
    [Serializable]
    public class DataUISetup : DataComponent<DataUISetup>
    {
        public UISetup Value;
    }
}