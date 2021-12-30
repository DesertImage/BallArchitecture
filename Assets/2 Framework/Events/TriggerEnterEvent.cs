using DesertImage.Entities;
using UnityEngine;

namespace DesertImage
{
    public struct TriggerEnterEvent
    {
        public IEntity Source;
        public Collider Other;
        public Collider2D Other2D;
    }
}