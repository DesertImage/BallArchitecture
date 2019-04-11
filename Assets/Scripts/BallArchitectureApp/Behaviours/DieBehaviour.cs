using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Behaviours;
using DesertImage.Subjects;

namespace BallArchitectureApp.Behaviours
{
    public class DieBehaviour : BehaviourBase, IListen<DieEvent>
    {
        protected override void Link(ISubject subject)
        {
            base.Link(subject);

            subject.listen<DieEvent>(this);
        }

        public void handleCallback(DieEvent arguments)
        {
            this.Send(new DieEvent {Value = Subject}); //sending global event with parent subject in arguments
            
            Subject.returnToPool();
        }
    }
}