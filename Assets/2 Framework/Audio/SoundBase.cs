using DesertImage.Extensions;
using UnityEngine;

namespace DesertImage.Audio
{
    public class SoundBase : MonoBehaviour, IPoolable
    {
        public AudioClip Clip => AudioSource.clip;

        public AudioSource AudioSource;

        public float Delay { get; private set; }

        private bool _isPlayed;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.playOnAwake = false;
        }

        private void Reset()
        {
            _isPlayed = false;

            if (AudioSource)
            {
                AudioSource.Stop();
                AudioSource.clip = null;
                AudioSource.pitch = 1f;
            }

            Delay = 0f;
        }

        public void Play(AudioClip audioClip, float volume, bool isLooped = false)
        {
            AudioSource.clip = audioClip;
            AudioSource.volume = volume;
            AudioSource.loop = isLooped;

            _isPlayed = true;

            Delay = 0f;

            AudioSource.Play();
        }

        public void Play(AudioClip audioClip, float volume, float delay = 0f, bool isLooped = false)
        {
            AudioSource.clip = audioClip;
            AudioSource.volume = volume;
            AudioSource.loop = isLooped;

            AudioSource.playOnAwake = false;

            Delay = delay;

            AudioSource.PlayDelayed(delay);

            this.DoActionWithDelay
            (
                () => { _isPlayed = true; },
                delay
            );
        }

        private void Update()
        {
            if (!_isPlayed) return;

            if (AudioSource.isPlaying) return;

            ReturnToPool();
        }

        public void OnCreate()
        {
            Reset();
        }

        public void ReturnToPool()
        {
            Core.Instance?.Get<FactorySound>()?.ReturnInstance(this);
        }
    }
}