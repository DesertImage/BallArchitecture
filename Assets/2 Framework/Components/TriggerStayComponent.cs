using DesertImage.Entities;
using DesertImage.Extensions;
using Entities;
using UnityEngine;

namespace DesertImage
{
    public class TriggerStayComponent : MonoBehaviour
    {
        private EntityMono _parent;

        private void Start()
        {
            _parent = GetComponentInParent<EntityMono>();
        }

        private void OnTriggerStay(Collider other)
        {
            this.SendGlobalEvent(new TriggerStayEvent
            {
                Other = other,
                Source = _parent
            });

            var entity = other.GetComponentInParent<IEntity>();

            if (entity == null) return;

            _parent.SendEvent(new TriggerStayEvent
            {
                Other = other,
                Source = entity
            });
        }
    }
}