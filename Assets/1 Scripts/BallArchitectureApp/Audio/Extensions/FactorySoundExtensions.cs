using System;
using BallArchitectureApp.Audio;
using DesertImage;
using DesertImage.Audio;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace BallArchitectureApp.Spawning
{
    public static class FactorySoundExtensions
    {
        public static SoundBase Play2D(this SoundId id, float volume = 1f)
        {
            return DesertImage.Extensions.FactorySoundExtensions.PlaySound(null, (ushort)id, volume);
        }

        public static SoundBase Play2D(this SoundId id, bool isLooped, float volume = 1f)
        {
            return Play2D((ushort)id, volume, isLooped);
        }

        public static SoundBase Play2D(this ushort id, float volume = 1f, bool isLooped = false)
        {
            return DesertImage.Extensions.FactorySoundExtensions.PlaySound(null, id, isLooped, volume);
        }

        public static float GetTrackLength(this SoundId id)
        {
            return DesertImage.Extensions.FactorySoundExtensions.GetLength(null, (ushort)id);
        }

        public static void SetOnClickWithSound(this Button button, Action action, SoundId id = SoundId.ClickSound)
        {
            if (!button) return;

            button.SetOnClick(action);

            button.onClick.AddListener(() => { id.Play2D(); });
        }

        public static AudioClip GetTrack(this SoundId id)
        {
            return DesertImage.Extensions.FactorySoundExtensions.GetTrack(null, (ushort)id);
        }

        public static SoundBase GetReadySoundBase(this SoundId id)
        {
            return DesertImage.Extensions.FactorySoundExtensions.GetReadySoundBase(null, (ushort)id);
        }

        public static SoundBase PlayAfter(this SoundBase soundBase, SoundId id)
        {
            if (!soundBase) return soundBase;

            var previousLength = soundBase.Clip.length;

            var newSoundBase = GetReadySoundBase(id);

            var track = GetTrack(id);

            newSoundBase.Play(track, 1f, soundBase.Delay + previousLength);

            return newSoundBase;
        }
    }
}