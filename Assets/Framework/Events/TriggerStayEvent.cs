using DesertImage.Subjects;
using UnityEngine;

namespace BallArchitectureApp.Events
{
    public struct TriggerStayEvent
    {
        public ISubject Source;
        public Collider Other;
    }
}