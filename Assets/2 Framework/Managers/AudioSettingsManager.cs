using DesertImage.Starters;
using DesertImage.Settings;
using UniRx;
using UnityEngine;

namespace DesertImage.Managers
{
    public class AudioSettingsManager : SystemBase, IAwake
    {
        private const string SoundVolumeKey = "SoundVolume";
        private const string SoundEnabledKey = "SoundEnabled";
        private const string MusicVolumeKey = "MusicVolume";
        private const string MusicEnabledKey = "MusicEnabled";

        public void OnAwake()
        {
            Load();

            GameSettings.SoundEnabled.Subscribe(OnSoundEnabledChanged).AddTo(Starter.Instance);
            GameSettings.SoundVolume.Subscribe(OnSoundVolumeChanged).AddTo(Starter.Instance);

            GameSettings.MusicEnabled.Subscribe(OnMusicEnabledChanged).AddTo(Starter.Instance);
            GameSettings.MusicVolume.Subscribe(OnMusicVolumeChanged).AddTo(Starter.Instance);
        }

        private void Load()
        {
            GameSettings.SoundEnabled.Value = PlayerPrefs.GetInt(SoundEnabledKey, 1) == 1;
            GameSettings.MusicEnabled.Value = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;

            GameSettings.SoundVolume.Value = PlayerPrefs.GetFloat(SoundVolumeKey, .5f);
            GameSettings.MusicVolume.Value = PlayerPrefs.GetFloat(MusicVolumeKey, .5f);
        }

        #region SOUND

        private void OnSoundVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat(SoundVolumeKey, value);
        }

        private void OnSoundEnabledChanged(bool value)
        {
            PlayerPrefs.SetInt(SoundEnabledKey, value ? 1 : 0);
        }

        #endregion

        #region MUSIC

        private void OnMusicVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, value);
        }

        private void OnMusicEnabledChanged(bool value)
        {
            PlayerPrefs.SetInt(MusicEnabledKey, value ? 1 : 0);
        }

        #endregion
    }
}