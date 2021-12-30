﻿using System.Collections.Generic;using DesertImage.Extensions;using UnityEngine;namespace DesertImage.Managers{    public class ManagerEvents : SystemBase, IAwake    {        private readonly Dictionary<int, List<IListen>> _eventList = new Dictionary<int, List<IListen>>();        private readonly Queue<RemoveQueueInstance> _removeQueue = new Queue<RemoveQueueInstance>();        private bool _isSendingInProcess;        struct RemoveQueueInstance        {            public readonly List<IListen> Listeners;            public readonly IListen Listener;            public RemoveQueueInstance(List<IListen> listeners, IListen listener)            {                Listeners = listeners;                Listener = listener;            }        }        public void OnAwake()        {            Clear();        }        public void Clear()        {            _eventList.Clear();        }        #region ADD / REMOVE        public void Add<T>(IListen newListener)        {            var hash = HashCodeTypeTool.GetCachedHashCode<T>();            if (_eventList.TryGetValue(hash, out var cachedListeners))            {                cachedListeners.Add(newListener);                _eventList[hash] = cachedListeners;                return;            }            _eventList.Add(hash, new List<IListen> {newListener});        }        public void Remove<T>(IListen listener)        {            if (listener == null) return;            if (!_eventList.TryGetValue(HashCodeTypeTool.GetCachedHashCode<T>(), out var cachedListeners)) return;            // if (_isSendingInProcess)            // {            //     _removeQueue.Enqueue(new RemoveQueueInstance(cachedListeners, listener));            // }            // else            // {                cachedListeners.Remove(listener);            // }        }        #endregion        private void ExecuteRemoveQueue()        {            while (_removeQueue.Count > 0)            {                var instance = _removeQueue.Dequeue();                instance.Listeners.Remove(instance.Listener);            }        }        #region SEND EVENT        public void Send<T>(T arguments = default)        {            if (!_eventList.TryGetValue(HashCodeTypeTool.GetCachedHashCode<T>(), out var cachedListeners)) return;            if (cachedListeners == null) return;            for (var i = 0; i < cachedListeners.Count; i++)            {                _isSendingInProcess = true;                var cachedListener = cachedListeners[i];                if (cachedListener == null) continue;                if (!(cachedListener is IListen<T> listener))                {#if UNITY_EDITOR                    Debug.LogError($"WRONG LISTENER <color=red>{typeof(T)}</color> in {cachedListener}");#endif                    continue;                }                listener.handleCallback(arguments);                _isSendingInProcess = true;            }            ExecuteRemoveQueue();            _isSendingInProcess = false;        }        #endregion    }}