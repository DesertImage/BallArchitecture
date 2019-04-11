using DesertImage.Subjects;
using UnityEngine;

namespace Framework.Events
{
    public struct TriggerExitEvent
    {
        public ISubject Source;
        public Collider Other;
    }
}