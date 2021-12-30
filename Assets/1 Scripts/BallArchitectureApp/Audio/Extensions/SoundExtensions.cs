using BallArchitectureApp.Audio;

namespace BallArchitectureApp.Spawning
{
    public static class SoundExtensions
    {
        public static SoundId ToSoundId(this object obj)
        {
            return (SoundId)obj;
        }

        public static SoundId ToSoundId(this ushort uId)
        {
            return (SoundId)uId;
        }
    }
}