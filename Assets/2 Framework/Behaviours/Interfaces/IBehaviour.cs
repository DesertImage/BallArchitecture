using System;
using DesertImage.Entities;

namespace DesertImage.Behaviours
{
    public interface IBehaviour : ISwitchable, IDisposable
    {
        void Link(IEntity entity);
    }
}