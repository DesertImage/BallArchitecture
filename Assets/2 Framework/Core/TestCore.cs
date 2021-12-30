using DesertImage.Entities;
using DesertImage.Managers;
using DesertImage.Pools;
using Framework.Managers;
using Managers;

namespace DesertImage
{
    public class TestCore : Core
    {
        public TestCore()
        {
            Add(new ManagerUpdate());
            Add(new ManagerEvents());
            Add(new EntitiesManager());
            Add(new ManagerTimers());
            Add(new TimersUpdater());
            Add(new PoolComponents());
        }
    }
}