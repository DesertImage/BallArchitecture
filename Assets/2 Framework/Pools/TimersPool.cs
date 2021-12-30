using DesertImage.Timers;

namespace DesertImage.Pools
{
    public class TimersPool : Pool<Timer>
    {
        private int _timersCount;
        
        protected override Timer CreateInstance()
        {
            var timer = new Timer(_timersCount);

            _timersCount++;
            
            return timer;
        }
    }
}