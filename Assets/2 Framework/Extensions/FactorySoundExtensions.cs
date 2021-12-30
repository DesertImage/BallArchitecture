using DesertImage.Audio;
using DesertImage.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DesertImage.Extensions
{
    public static class FactorySoundExtensions
    {
        private static FactorySound Factory => _factory ??= Core.Instance?.Get<FactorySound>();

        private static FactorySound _factory;

        static FactorySoundExtensions()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += SceneManagerOnsceneUnloaded;
        }

        private static void SceneManagerOnsceneUnloaded(Scene arg0)
        {
            _factory = null;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _factory = null;
        }

        public static SoundBase PlaySound(this object sender, ushort id, float volume = 1f)
        {
            return Factory?.Spawn2D(id, volume);
        }

        public static SoundBase PlaySound(this object sender, ushort id, bool isLooped, float volume = 1f)
        {
            return Factory?.Spawn2D(id, volume, isLooped);
        }

        public static SoundBase PlaySound(this object sender, AudioClip audioClip, float volume = 1f)
        {
            return Factory?.Spawn2D(audioClip, volume);
        }

        public static AudioSource GetAudioSource(this object sender)
        {
            return Factory?.GetAudioSource();
        }

        public static AudioClip GetTrack(this object sender, ushort id)
        {
            return Factory.GetTrack(id);
        }

        public static float GetLength(this object sender, ushort id)
        {
            return Factory.GetTrackLength(id);
        }

        public static SoundBase GetReadySoundBase(this object sender, ushort id)
        {
            return Factory.GetReadySoundBase(id);
        }
    }
}