using UniRx;

namespace DesertImage.Settings
{
    public static class GameSettings
    {
        #region MUSIC/SOUND

        public static ReactiveProperty<bool> SoundEnabled = new BoolReactiveProperty(true);
        public static ReactiveProperty<float> SoundVolume = new FloatReactiveProperty(.5f);

        public static ReactiveProperty<bool> MusicEnabled = new BoolReactiveProperty(true);
        public static ReactiveProperty<float> MusicVolume = new FloatReactiveProperty(.5f);

        #endregion
    }
}