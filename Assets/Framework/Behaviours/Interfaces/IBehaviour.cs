using System;
using DesertImage.Subjects;

namespace BallArchitectureApp.Behaviours.Interfaces
{
    public interface IBehaviour : IDisposable
    {
        void link(ISubject subject);
    }
}