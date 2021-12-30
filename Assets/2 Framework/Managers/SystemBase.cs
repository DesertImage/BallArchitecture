using System;

namespace DesertImage.Managers
{
    public interface ISystem : ISwitchable, IDisposable
    {

    }
    
    public class SystemBase : ISystem
    {
        public virtual void Activate()
        {
        }

        public virtual void Deactivate()
        {
        }

        public virtual void Dispose()
        {
            Deactivate();
        }
    }
}