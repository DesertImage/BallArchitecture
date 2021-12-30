using Entities;
using UnityEngine;

namespace DesertImage
{
    public class ParticleCollisionComponent : MonoBehaviour
    {
        private EntityMono _parent;

        private void Start()
        {
            _parent = GetComponentInParent<EntityMono>();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (!_parent) return;

            _parent.SendEvent(new ParticleCollisionEvent {Other = other});
        }
    }
}