using DesertImage;
using Interaction;
using UnityEngine;
using Behaviour = DesertImage.Behaviours.Behaviour;

namespace BallArchitectureApp
{
    public class GetRandomDamageOnClickBehaviour : Behaviour, IListen<ClickedEvent>
    {
        public override void Activate()
        {
            base.Activate();

            Entity.ListenEvent<ClickedEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Entity.UnlistenEvent<ClickedEvent>(this);
        }

        public void handleCallback(ClickedEvent arguments)
        {
            Entity.SendEvent(new DamageGetEvent { Value = Random.Range(1f, 3f) });
        }
    }
}