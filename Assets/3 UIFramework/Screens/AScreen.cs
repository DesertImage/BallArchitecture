using System;
using System.Diagnostics.SymbolStore;
using UnityEngine;

namespace DesertImage.UI
{
    public abstract class AScreen<TId, TSettings> : MonoBehaviour, IScreen<TId, TSettings>
        where TSettings : IScreenSettings, new()
    {
        public Action<IScreen<TId>> OnShowFinished { get; set; }
        public Action<IScreen<TId>> OnHideFinished { get; set; }
        public Action<IScreen<TId>> OnCloseRequest { get; set; }
        public Action<IScreen<TId>> OnDestroyed { get; set; }

        public virtual bool IsEnabled => gameObject.activeSelf;

        public virtual TId Id => default;

        public TSettings Settings => settings;

        [SerializeField] protected ATransition enterTransition;
        [SerializeField] protected ATransition exitTransition;

        [SerializeField] [Space(10)] protected TSettings settings;

        /// <summary>
        /// Calls automatically after ALayer.Register() by AUIManager
        /// </summary>
        public virtual void Initialize()
        {
            if (enterTransition)
            {
                enterTransition.Init(enterTransition.transform);
            }

            if (exitTransition)
            {
                exitTransition.Init(exitTransition.transform);
            }

            settings = new TSettings();
        }

        protected virtual void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        public void Show<TSettings1>(TSettings1 settings = default, bool animate = true)
            where TSettings1 : IScreenSettings
        {
            Show(settings is TSettings screenSettings ? screenSettings : default, animate);
        }

        public void Show()
        {
            Show(default);
        }

        public virtual void Show(TSettings settings = default, bool animate = true)
        {
            if (animate)
            {
                if ( /*!Equals(settings, Settings) &&*/ settings != null) Setup(settings);
            }

            // if (!IsEnabled)
            // {
            Animate
            (
                enterTransition,
                OnEnterAnimationFinished,
                animate && (!IsEnabled || exitTransition && exitTransition.IsInProcess)
            );
            // }
            // else
            // {
            // OnShowFinished?.Invoke(this);
            // }

            UpdateHierarchy();
        }

        public virtual void Hide(bool animate = true)
        {
            Animate
            (
                exitTransition,
                OnExitAnimationFinished,
                animate && (IsEnabled || enterTransition && enterTransition.IsInProcess),
                false
            );
        }

        public virtual void Setup(TSettings settings)
        {
            this.settings = settings;
        }

        protected virtual void Enable()
        {
            gameObject.SetActive(true);
        }

        protected virtual void Disable()
        {
            gameObject.SetActive(false);
        }

        protected virtual void UpdateHierarchy()
        {
            transform.SetAsLastSibling();
        }

        private void Animate(ITransition transition, Action callback = null, bool animate = true, bool isVisible = true)
        {
            //TODO: refactor this. Don't like it
            if (enterTransition && enterTransition.IsInProcess && enterTransition != (ATransition) transition)
            {
                enterTransition.Cancel();
            }

            if (exitTransition && exitTransition.IsInProcess && exitTransition != (ATransition) transition)
            {
                exitTransition.Cancel();
            }

            if (transition == null || !animate)
            {
                callback?.Invoke();

                if (isVisible)
                {
                    Enable();
                }
                else
                {
                    Disable();
                }
            }
            else
            {
                Enable();

                transition.Play(transform, () =>
                {
                    callback?.Invoke();

                    if (isVisible) return;

                    Disable();
                });
            }
        }

        protected virtual void OnEnterAnimationFinished()
        {
            OnShowFinished?.Invoke(this);
        }

        protected virtual void OnExitAnimationFinished()
        {
            OnHideFinished?.Invoke(this);
        }
    }
}