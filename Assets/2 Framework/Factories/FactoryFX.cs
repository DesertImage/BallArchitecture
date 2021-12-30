﻿using System;using System.Collections.Generic;using DesertImage.Enums;using DesertImage.Pools;using Framework.FX;using UnityEngine;namespace DesertImage{    public class FactoryFx : Factory, IAwake    {        public readonly List<FXSpawnNode> Nodes = new List<FXSpawnNode>();        private MonoBehaviourPool<EffectBase> _effectPool;        public void OnAwake()        {            _effectPool = new EffectsPool(new GameObject("EffectsPool").transform);        }        public void Register(FXSpawnNode node)        {            Nodes.Add(node);        }        public void Register(ushort id, EffectBase effectBase, int preRegisterCount = 0)        {            Nodes.Add(new FXSpawnNode            {                Id = (EffectsId) id,                Prefab = effectBase,                RegisterCount = preRegisterCount            });        }        public void RegisterObjects()        {            if (_effectPool == null) return;            foreach (var spawnNode in Nodes)            {                if (!spawnNode.Prefab) continue;                _effectPool.Register(spawnNode.Prefab, spawnNode.RegisterCount);            }        }        public void ReturnInstance(EffectBase obj)        {            _effectPool.ReturnInstance(obj);        }        #region SPAWN        public EffectBase Spawn(EffectsId id, Vector3 position, Quaternion rotation, Transform parent)        {            EffectBase effect = null;            if (_effectPool == null) return null;            foreach (var spawnNode in Nodes)            {                if (spawnNode.Id != id) continue;                effect = _effectPool.GetInstance(spawnNode.Prefab);                if (!effect)                {#if UNITY_EDITOR                    Debug.LogError($"[FactoryFX] null instance: {id}");#endif                    continue;                }                var transform = effect.transform;                transform.SetParent(parent, false);//                transform.parent = parent;                transform.localScale = Vector3.one;                transform.position = position;                transform.rotation = rotation;                effect.Play();                break;            }            return effect;        }        public T Spawn<T>(EffectsId id, Transform parent)        {            return Spawn(id, parent).GetComponent<T>();        }        public T Spawn<T>(EffectsId id, Vector3 position)        {            return Spawn(id, position, Quaternion.identity, null).GetComponent<T>();        }        public T Spawn<T>(EffectsId id, Vector3 position, Transform parent)        {            return Spawn(id, position, Quaternion.identity, parent).GetComponent<T>();        }        public EffectBase Spawn(EffectsId id, Transform parent)        {            var transform = parent.transform;            return Spawn(                id,                parent != null ? transform.position : Vector3.zero,                parent != null ? transform.rotation : Quaternion.identity,                parent);        }        #endregion    }    [Serializable]    public class FXSpawnNode    {        [SerializeField] private string _name;        public EffectsId Id;        public EffectBase Prefab;        public int RegisterCount;    }}