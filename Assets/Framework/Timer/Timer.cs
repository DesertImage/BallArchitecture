using System;
using DesertImage.Managers;
using UnityEngine;

namespace DesertImage.Timers
{
    public class Timer : IComponent, ITick, IPoolable
    {
        public float Time { get; private set; }

        private float _targetTime;
        private Action _action;

        private bool _isPlaying;

        #region PUBLIC METHODS

        public void play(Action action, float timeDelay = 1f)
        {
            Play(action, timeDelay);
        }

        public void stop()
        {
            Stop();
        }

        #endregion

        public void tick()
        {
            Count();
        }

        #region PLAY / STOP / RESET

        private void Play(Action action, float timeDelay)
        {
            _isPlaying = true;

            _action = action;

            _targetTime = timeDelay;
        }

        private void Stop()
        {
            _isPlaying = false;

            Reset();
        }

        private void Reset()
        {
            Time = 0f;

            _action = null;

            _targetTime = 0.1f;
        }

        #endregion

        private void Count()
        {
            if (!_isPlaying) return;

            Time += UnityEngine.Time.deltaTime;

            if (Time < _targetTime) return;

            _isPlaying = false;
            
            _action();

            returnToPool();
        }

        #region POOL STUFF

        public void onCreate()
        {
            Core.Instance.get<ManagerUpdate>().add(this);

            Reset();
        }

        public void returnToPool()
        {
            Core.Instance.get<ManagerUpdate>().remove(this);

            Stop();

            Core.Instance.get<ManagerTimers>().returnInstance(this);
        }

        #endregion
    }
}