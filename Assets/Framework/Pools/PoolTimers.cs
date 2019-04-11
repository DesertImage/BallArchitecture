using DesertImage.Timers;

namespace DesertImage.Pools
{
    public class PoolTimers : Pool<Timer>
    {
        protected override Timer CreateInstance(Timer objInstance)
        {
            var timer = new Timer();
            
            timer.onCreate();
            
            return  timer;
        }

        protected override void GetStuff(Timer objInstance)
        {
            base.GetStuff(objInstance);
            
            objInstance.onCreate();
        }
    }
}