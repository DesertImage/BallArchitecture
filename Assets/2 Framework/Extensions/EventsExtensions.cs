using DesertImage.Managers;
using UnityEngine;

namespace DesertImage.Extensions
{
    public static class EventsExtensions
    {
        public static void SendGlobalEvent<T>(this object sender, T arguments = default(T))
        {
            var core = Core.Instance;

            if (core == null)
            {
#if DEBUG
                Debug.LogError("THERE IS NO CORE");
#endif
                return;
            }

            var manager = core.Get<ManagerEvents>();

            if (manager == null)
            {
#if DEBUG
                Debug.LogError("THERE IS NO MANAGER EVENT");
#endif
                return;
            }

            manager.Send(arguments);
        }

        public static void ListenGlobalEvent<T>(this IListen listener)
        {
            var core = Core.Instance;

            if (core == null)
            {
#if DEBUG
                Debug.LogError("THERE IS NO CORE");
#endif
                return;
            }

            if (!typeof(T).IsValueType)
            {
#if DEBUG
                Debug.LogError($"MAY BE WRONG LISTENER {typeof(T)} in {listener}");
#endif
            }

            var manager = core.Get<ManagerEvents>();

            if (manager == null)
            {
#if DEBUG
                Debug.LogError("THERE IS NO MANAGER EVENT");
#endif
                return;
            }

            manager.Add<T>(listener);
        }

        public static void UnlistenGlobalEvent<T>(this IListen listener)
        {
            var core = Core.Instance;

            if (core == null)
            {
#if DEBUG
                Debug.LogError("THERE IS NO CORE");
#endif
                return;
            }

            var manager = core.Get<ManagerEvents>();

            if (manager == null)
            {
#if DEBUG
                Debug.LogError("THERE IS NO MANAGER EVENT");
#endif
                return;
            }

            manager.Remove<T>(listener);
        }
    }
}