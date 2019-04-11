using BallArchitectureApp.Components;
using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Behaviours;
using DesertImage.Subjects;

namespace BallArchitectureApp.Behaviours
{
    public class GetDamageBehaviour : BehaviourBase, IListen<GetDamageEvent>
    {
        private DataHealth _dataHealth;

        protected override void Link(ISubject subject)
        {
            base.Link(subject);
            
            subject.listen<GetDamageEvent>(this);

            _dataHealth = subject.get<DataHealth>();
        }

        public void handleCallback(GetDamageEvent arguments)
        {
            _dataHealth.Health.Value -= arguments.Value;
            
            if (_dataHealth.Health.Value > 0.2f) return;
            
            Subject.send(new DieEvent());
        }
    }
}