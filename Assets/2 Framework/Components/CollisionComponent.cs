using DesertImage.Entities;
using DesertImage.Extensions;
using Entities;
using UnityEngine;

namespace DesertImage
{
    public class CollisionComponent : MonoBehaviour
    {
        private EntityMono _parent;

        private void Start()
        {
            _parent = GetComponentInParent<EntityMono>();
        }

        private void OnTriggerEnter(Collider other)
        {
            this.SendGlobalEvent(new TriggerEnterEvent {Source = this._parent, Other = other});

            var entity = other.GetComponentInParent<EntityMono>();

            if (!entity) return;

            if (!_parent) return;

            _parent.SendEvent(new TriggerEnterEvent {Source = entity.LocalEntity, Other = other});
            entity.SendEvent(new TriggerEnterEvent {Source = _parent.LocalEntity});
        }

        /*private void OnTriggerStay(Collider other)
        {
           this.SendGlobalEvent(new TriggerStayEvent {Source = _subject, Other = other});

            var subject = other.GetComponentInParent<ISubject>();

            if (subject == null) return;

            _subject.send(new TriggerStayEvent() {Source = subject, Other = other});
        }*/

        private void OnTriggerExit(Collider other)
        {
            this.SendGlobalEvent(new TriggerExitEvent {Source = this._parent, Other = other});

            var entity = other.GetComponentInParent<EntityMono>();

            if (!entity) return;

            if (!_parent) return;

            _parent.SendEvent(new TriggerExitEvent {Source = entity.LocalEntity, Other = other});
            entity.SendEvent(new TriggerExitEvent {Source = _parent.LocalEntity});
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.isTrigger) return;

            this.SendGlobalEvent(new CollisionEnterEvent() {Source = this._parent});

            var entity = other.collider.GetComponentInParent<EntityMono>();

            if (!entity) return;

            if (!_parent) return;

            _parent.SendEvent(new CollisionEnterEvent {Source = entity.LocalEntity});
            // entity.SendEvent(new TriggerExitEvent {Source = parentEntity.LocalEntity});          
        }

        /*private void OnCollisionStay(Collision other)
        {
           this.SendGlobalEvent(new CollisionStayEvent() {Source = _subject});

            var subject = other.collider.GetComponentInParent<ISubject>();
            
            if (subject == null) return;
            
            _subject.send(new CollisionStayEvent() {Source = subject});  
        }*/
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            this.SendGlobalEvent(new TriggerEnterEvent {Source = this._parent, Other2D = other});

            var entity = other.GetComponentInParent<EntityMono>();

            if (!entity) return;

            if (!_parent) return;

            _parent.SendEvent(new TriggerEnterEvent {Source = entity.LocalEntity, Other2D = other});
            entity.SendEvent(new TriggerEnterEvent {Source = _parent.LocalEntity});
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            this.SendGlobalEvent(new TriggerExitEvent {Source = this._parent, Other2D = other});

            var entity = other.GetComponentInParent<EntityMono>();

            if (!entity) return;

            if (!_parent) return;

            _parent.SendEvent(new TriggerExitEvent {Source = entity.LocalEntity, Other2D = other});
            entity.SendEvent(new TriggerExitEvent {Source = _parent.LocalEntity});
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.isTrigger) return;

            this.SendGlobalEvent(new CollisionEnterEvent() {Source = this._parent});

            var entity = other.collider.GetComponentInParent<EntityMono>();

            if (!entity) return;

            if (!_parent) return;

            _parent.SendEvent(new CollisionEnterEvent {Source = entity.LocalEntity});
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.isTrigger) return;

            this.SendGlobalEvent(new CollisionExitEvent {Source = _parent});

            var entity = other.collider.GetComponentInParent<EntityMono>();

            if (!entity) return;

            if (!_parent) return;

            _parent.SendEvent(new CollisionExitEvent {Source = entity.LocalEntity});
        }
    }
}