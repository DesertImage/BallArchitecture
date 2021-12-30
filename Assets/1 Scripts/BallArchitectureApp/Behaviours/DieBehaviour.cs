using DesertImage;
using DesertImage.Behaviours;
using DesertImage.Entities;
using DesertImage.Extensions;

namespace BallArchitectureApp
{
    public class DieBehaviour : Behaviour, IListen<DieEvent>
    {
        public override void Link(IEntity entity)
        {
            base.Link(entity);

            entity.ListenEvent<DieEvent>(this);
        }

        public void handleCallback(DieEvent arguments)
        {
            this.SendGlobalEvent(new DieEvent
            {
                Value = Entity
            }); //sending global event with parent subject in arguments

            Entity.ReturnToPool();
        }
    }
}