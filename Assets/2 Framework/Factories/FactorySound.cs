using System;
using System.Collections.Generic;
using System.Linq;
using DesertImage.Audio;
using DesertImage.Pools;
using DesertImage.Settings;
using Framework.External;
using UnityEngine;

namespace DesertImage
{
    public class FactorySound : Factory, IAwake
    {
        private const string PrefabName = "SoundEffect";

        private const int MaxDuplicateSoundsCount = 70;

        public readonly List<SoundNode> Nodes = new List<SoundNode>();

        private PoolSoundBase _pool;

        private GameObject _objPrefab;

        private Transform _soundsTransform;

        public Dictionary<int, CustomList<SoundBase>> PlayingSounds = new Dictionary<int, CustomList<SoundBase>>();

        public void OnAwake()
        {
            _pool = new PoolSoundBase(new GameObject("SoundPool").transform);

            _pool.Register(50);

            _soundsTransform = new GameObject("Sounds").transform;
        }

        public SoundBase Spawn2D(ushort id, float volume = 1f, bool isLooped = false)
        {
            foreach (var node in Nodes)
            {
                if (node.Id != id) continue;

                return Spawn2D(node.SoundClip, volume, isLooped);
            }

            return null;
        }

        public SoundBase Spawn2D(AudioClip audioClip, float volume = 1f, bool isLooped = false)
        {
            if (_pool == null) return null;

            var soundId = audioClip.GetInstanceID();

            if (PlayingSounds.TryGetValue(soundId, out var instances))
            {
                if (instances.Count >= MaxDuplicateSoundsCount)
                {
                    ReturnInstance(instances[0]);
                }
            }

            var soundBase = _pool.GetInstance();

            var finalVolume = GameSettings.SoundEnabled.Value ? volume * GameSettings.SoundVolume.Value : 0f;

            soundBase.transform.parent = _soundsTransform;

            soundBase.Play(audioClip, finalVolume, isLooped);

            if (PlayingSounds.TryGetValue(soundId, out instances))
            {
                instances.Add(soundBase);
            }
            else
            {
                PlayingSounds.Add(soundId, new CustomList<SoundBase>(MaxDuplicateSoundsCount) { soundBase });
            }

            return soundBase;
        }

        public AudioClip GetTrack(ushort id)
        {
            AudioClip track = null;

            foreach (var soundNode in Nodes)
            {
                if (soundNode.Id != id) continue;

                track = soundNode.SoundClip;

                break;
            }

            return track;
        }

        public void Register(SoundNode node)
        {
            Nodes.Add(node);
        }

        public void Register(ushort id, AudioClip audioClip, int preRegisterCount = 0)
        {
            Nodes.Add(new SoundNode
            {
                Id = id,
                SoundClip = audioClip,
                RegisterCount = preRegisterCount
            });
        }

        public AudioSource GetAudioSource()
        {
            return _pool.GetInstance().AudioSource;
        }

        public SoundBase GetReadySoundBase(ushort soundId)
        {
            var soundBase = _pool.GetInstance();

            soundBase.transform.parent = _soundsTransform;

            var node = Nodes.FirstOrDefault(x => x.Id == soundId);

            soundBase.AudioSource.clip = node?.SoundClip;

            return soundBase;
        }

        public float GetTrackLength(ushort id)
        {
            var node = Nodes.FirstOrDefault(x => x.Id == id);

            if (node == null) return -1f;

            return node.SoundClip ? node.SoundClip.length : -1f;
        }

        public void ReturnInstance(SoundBase soundBase)
        {
            if (!soundBase) return;

            var idHash = 0;

            foreach (var soundNode in Nodes)
            {
                if (soundNode.SoundClip != soundBase.Clip) continue;

                idHash = soundNode.SoundClip.GetInstanceID();
            }

            if (PlayingSounds.TryGetValue(idHash, out var instances))
            {
                instances.Remove(soundBase);
            }

            _pool.ReturnInstance(soundBase);
        }
    }

    [Serializable]
    public class SoundNode
    {
        [SerializeField] private string _name;

        public ushort Id;
        public AudioClip SoundClip;

        public int RegisterCount;
    }
}