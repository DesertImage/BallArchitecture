using System;
using DesertImage.Pools;
using DesertImage.Timers;
using UnityEngine;

namespace DesertImage.Managers
{
    public class ManagerTimers : ManagerBase, IAwake
    {
        private Pool<Timer> _pool = new PoolTimers();
        
        public void onAwake()
        {
            _pool.register(new Timer(), 1);
        }

        public Timer playAction(Action action, float delay = 1f)
        {
            var timer = _pool.getInstance();

            timer.play(action, delay);

            return timer;
        }

        public void returnInstance(Timer instance)
        {
            _pool.returnInstance(instance);
        }
        
        public void stopTimer(Timer timer)
        {
            timer.stop();
        }
    }
}