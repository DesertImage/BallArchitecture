using DesertImage;
using DesertImage.Behaviours;

namespace BallArchitectureApp
{
    public class GetDamageBehaviour : Behaviour, IListen<DamageGetEvent>
    {
        private DataHealth _dataHealth;

        public override void Activate()
        {
            base.Activate();

            _dataHealth = Entity.Get<DataHealth>();

            Entity.ListenEvent<DamageGetEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Entity.UnlistenEvent<DamageGetEvent>(this);
        }

        public void handleCallback(DamageGetEvent arguments)
        {
            _dataHealth.Health.Value -= arguments.Value;

            if (_dataHealth.Health.Value > 0.2f) return;

            Entity.SendEvent(new DieEvent());
        }
    }
}