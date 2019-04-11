using DesertImage.Subjects;
using UnityEngine;

namespace BallArchitectureApp.Events
{
    public struct TriggerEnterEvent
    {
        public ISubject Source;
        public Collider Other;
    }
}