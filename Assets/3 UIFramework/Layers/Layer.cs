using System.Collections.Generic;
using UnityEngine;

namespace DesertImage.UI
{
    public class Layer<TId, TScreen> : MonoBehaviour, ILayer<TId, TScreen> where TScreen : IScreen<TId>
    {
        public TScreen Current { get; protected set; }

        protected readonly Dictionary<TId, TScreen> Screens = new Dictionary<TId, TScreen>();

        public virtual void Show<TSettings>(TScreen screen, TSettings settings = default)
            where TSettings : IScreenSettings
        {
            if (!Screens.ContainsValue(screen))
            {
#if UNITY_EDITOR
                Debug.LogError($"[UILayer] there is no registered screen {screen}");
#endif
                return;
            }

            screen.Show(settings);
        }

        public void Show<TSettings>(TId id, TSettings settings = default) where TSettings : IScreenSettings
        {
            if (!(Screens.TryGetValue(id, out var screen))) return;

            Show(screen, settings);
        }

        public virtual void HIde(TScreen screen, bool animate = true)
        {
            if (!Screens.ContainsValue(screen)) return;

            screen.Hide();
        }

        public void HIde(TId id)
        {
            if (!(Screens.TryGetValue(id, out var screen))) return;

            HIde(screen);
        }

        public IScreen Get(TId id)
        {
            Screens.TryGetValue(id, out var screen);

            return screen;
        }

        public virtual void HideAll(bool animate = true)
        {
            foreach (var screensValue in Screens.Values)
            {
                HIde(screensValue, animate);
            }
        }

        public void Register(TId id, TScreen screen)
        {
            if (Screens.ContainsKey(id))
            {
#if DEBUG
                UnityEngine.Debug.LogError(
                    $"[Layer] Screen with id {id} already contains in {this}. Screen is {screen}");
#endif

                return;
            }

            RegisterProcess(id, screen);
        }

        public void Unregister(TId id, TScreen screen)
        {
            if (!Screens.ContainsKey(id)) return;

            screen.OnDestroyed -= OnScreenDestroyed;

            Screens.Remove(id);
        }

        // public TScreen Get(TId id)
        // {
        //     return !Screens.TryGetValue(id, out var screen) ? default : screen;
        // }

        protected virtual void InitParent(TScreen screen)
        {
            (screen as MonoBehaviour).transform.SetParent(transform, false);
        }

        protected virtual void RegisterProcess(TId id, TScreen screen)
        {
            Screens.Add(id, screen);

            InitParent(screen);

            screen.Hide(false);

            screen.OnCloseRequest += OnScreenCloseRequest;
            screen.OnDestroyed += OnScreenDestroyed;
        }

        protected virtual void UnregisterProcess(TId id, TScreen screen)
        {
            screen.OnCloseRequest -= OnScreenCloseRequest;
            screen.OnDestroyed -= OnScreenDestroyed;
        }

        protected virtual void OnScreenCloseRequest(IScreen<TId> screen)
        {
            HIde((TScreen) screen);
        }

        protected virtual void OnScreenDestroyed(IScreen<TId> screen)
        {
            Unregister(screen.Id, (TScreen) screen);
        }
    }
}