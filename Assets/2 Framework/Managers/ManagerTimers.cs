using System;
using System.Collections;
using DesertImage.Pools;
using DesertImage.Timers;

namespace DesertImage.Managers
{
    public class ManagerTimers : SystemBase, IAsyncAwake
    {
        private readonly Pool<Timer> _pool = new TimersPool();

        public IEnumerator OnAsyncAwake()
        {
            var process = _pool.AsyncRegister(35);
            
            while (process.MoveNext())
            {
                yield return null;
            }
        }
        
        public Timer PlayAction(Action action, float delay = 1f, bool ignoreTimescale = false)
        {
            var timer = _pool.GetInstance();

            timer.Play(action, delay, ignoreTimescale);

            return timer;
        }

        public void ReturnInstance(Timer instance)
        {
            _pool.ReturnInstance(instance);
        }

        
    }
}