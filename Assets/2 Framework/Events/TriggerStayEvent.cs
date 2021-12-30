using DesertImage.Entities;
using UnityEngine;

namespace DesertImage
{
    public struct TriggerStayEvent
    {
        public IEntity Source;
        public Collider Other;
    }
}