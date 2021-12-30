using System.Collections.Generic;
using DesertImage;
using DesertImage.Managers;
using DesertImage.Timers;
using Framework.Managers;
using UnityEngine;

namespace Managers
{
    public class TimersUpdater : SystemBase, ITick
    {
        private readonly List<Timer> _timers = new List<Timer>();

        private bool _isActive;

        public TimersUpdater()
        {
            _isActive = true;
        }

        public override void Activate()
        {
            base.Activate();

            _isActive = true;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            _isActive = false;
        }

        public void Add(Timer timer)
        {
            _timers.Add(timer);
        }

        public void Remove(Timer timer)
        {
            _timers.Remove(timer);
        }

        public void Tick()
        {
            if (!_isActive) return;
            
            for (var i = 0; i < _timers.Count; i++)
            {
                if (!_isActive) return;                
                
                _timers[i].Tick();
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            _isActive = false;
        }
    }
}