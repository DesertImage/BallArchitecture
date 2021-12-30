using Components;
using DesertImage;
using DesertImage.Behaviours;
using DesertImage.Entities;
using DesertImage.Extensions;

namespace Coloring
{
    public class RendererColorBehaviour : Behaviour, IListen<ChangeColorEvent>
    {
        private DataRenderer _dataRenderer;

        public override void Activate()
        {
            base.Activate();

            Entity.ListenEvent<ChangeColorEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Entity.UnlistenEvent<ChangeColorEvent>(this);
        }

        public override void Link(IEntity entity)
        {
            base.Link(entity);

            _dataRenderer = entity.Get<DataRenderer>();
        }

        public void handleCallback(ChangeColorEvent arguments)
        {
            _dataRenderer.Value.SetColor(arguments.Value);
        }
    }
}