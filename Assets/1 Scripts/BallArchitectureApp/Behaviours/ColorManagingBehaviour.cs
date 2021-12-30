using BallArchitectureApp;
using DesertImage;
using DesertImage.Behaviours;
using DesertImage.Extensions;

namespace BallArchitectureApp
{
    public class ColorManagingBehaviour : Behaviour, IListen<ColorSetEvent>
    {
        private DataColor _dataColor;
        private DataRenderer _dataRenderer;

        public override void Activate()
        {
            base.Activate();

            _dataColor = Entity.Get<DataColor>();
            _dataRenderer = Entity.Get<DataRenderer>();

            Entity.ListenEvent<ColorSetEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Entity.UnlistenEvent<ColorSetEvent>(this);
        }

        public void handleCallback(ColorSetEvent arguments)
        {
            _dataColor.Value = arguments.Value;
            _dataRenderer.Value.SetColor(arguments.Value);
        }
    }
}