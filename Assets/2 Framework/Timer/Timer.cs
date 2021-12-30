using System;
using DesertImage.Managers;
using Managers;

namespace DesertImage.Timers
{
    public class Timer : IComponent, ITick, IPoolable
    {
        public Timer()
        {
            Id = 0;
        }

        public Timer(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public float Time { get; private set; }

        private float _targetTime;
        private Action _action;

        private bool _isPlaying;

        private bool _isIgnoreTimescale;

        private bool _isLogged;


        #region PUBLIC METHODS

        public void Play(Action action, float timeDelay = 1f, bool ignoreTimeScale = false)
        {
            _isPlaying = true;

            _action = action;

            _targetTime = timeDelay;

            _isIgnoreTimescale = ignoreTimeScale;
        }

        public void Stop()
        {
            _isPlaying = false;

            Reset();
        }

        public void PlayAndReturnToPool()
        {
            if (!_isPlaying) return;

            _action?.Invoke();

            ReturnToPool();
        }

        #endregion

        public void Tick()
        {
            Count();
        }

        #region PLAY / STOP / RESET

        private void Reset()
        {
            Time = 0f;

            _isLogged = false;

            _action = null;

            _targetTime = 0.3f;
        }

        #endregion

        private void Count()
        {
            if (!_isPlaying) return;

            Time += _isIgnoreTimescale ? UnityEngine.Time.unscaledDeltaTime : UnityEngine.Time.deltaTime;

            if (Time < _targetTime) return;

            _isPlaying = false;

            _action.Invoke();

            _action = null;

            ReturnToPool();
        }

        #region POOL STUFF

        public void OnCreate()
        {
            Reset();

            Core.Instance?.Get<TimersUpdater>()?.Add(this);
        }

        public void ReturnToPool()
        {
            Stop();

            Core.Instance?.Get<TimersUpdater>().Remove(this);

            Core.Instance?.Get<ManagerTimers>().ReturnInstance(this);
        }

        #endregion
    }
}