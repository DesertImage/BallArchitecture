using DesertImage.Events;
using DesertImage.Subjects;
using Framework.Events;
using BallArchitectureApp.Events;
using UnityEngine;
using CollisionExitEvent = DesertImage.Events.CollisionExitEvent;

namespace DesertImage
{
    public class CollisionComponent : MonoBehaviour
    {
        private ISubject _subject;

        private void Awake()
        {
            _subject = GetComponentInParent<ISubject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            this.Send(new TriggerEnterEvent {Source = _subject, Other = other});

            var subject = other.GetComponentInParent<ISubject>();

            if (subject == null) return;

            _subject.send(new TriggerEnterEvent() {Source = subject, Other = other});
        }
        
        
        /*private void OnTriggerStay(Collider other)
        {
            this.Send(new TriggerStayEvent {Source = _subject, Other = other});

            var subject = other.GetComponentInParent<ISubject>();

            if (subject == null) return;

            _subject.send(new TriggerStayEvent() {Source = subject, Other = other});
        }*/

        private void OnTriggerExit(Collider other)
        {
            this.Send(new TriggerExitEvent {Source = _subject, Other = other});

            var subject = other.GetComponentInParent<ISubject>();

            if (subject == null) return;

            _subject.send(new TriggerExitEvent() {Source = subject, Other = other});
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.isTrigger) return;

            this.Send(new CollisionEnterEvent() {Source = _subject});

            var subject = other.collider.GetComponentInParent<ISubject>();

            if (subject == null) return;

            _subject.send(new CollisionEnterEvent() {Source = subject});
//            subject.send(new CollisionEnterEvent() {Source = _subject, Other = other});            
        }

        /*private void OnCollisionStay(Collision other)
        {
            this.Send(new CollisionStayEvent() {Source = _subject});

            var subject = other.collider.GetComponentInParent<ISubject>();
            
            if (subject == null) return;
            
            _subject.send(new CollisionStayEvent() {Source = subject});  
        }*/

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.isTrigger) return;

            this.Send(new CollisionExitEvent() {Source = _subject});

            var subject = other.collider.GetComponentInParent<ISubject>();

            if (subject == null) return;

            _subject.send(new CollisionExitEvent() {Source = subject});
        }
    }
}