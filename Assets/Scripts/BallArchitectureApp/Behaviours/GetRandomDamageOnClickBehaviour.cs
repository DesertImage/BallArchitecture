using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Behaviours;
using DesertImage.Subjects;
using UnityEngine;

namespace BallArchitectureApp.Behaviours
{
    public class GetRandomDamageOnClickBehaviour : BehaviourBase, IListen<ClickEvent>
    {
        protected override void Link(ISubject subject)
        {
            base.Link(subject);
            
            subject.listen<ClickEvent>(this);
        }

        public void handleCallback(ClickEvent arguments)
        {
            Subject.send(new GetDamageEvent{Value = Random.Range(1f, 3f)});
        }
    }
}