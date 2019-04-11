using BallArchitectureApp.Behaviours.Interfaces;
using DesertImage.Subjects;

namespace DesertImage.Behaviours
{
    public class BehaviourBase : IBehaviour
    {
        protected ISubject Subject;
        
        protected virtual void Link(ISubject subject)
        {
            Subject = subject;
        }

        public void link(ISubject subject)
        {
            Link(subject);
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }
}