using DesertImage;
using DesertImage.Extensions;
using DesertImage.UI;
using BallArchitectureApp.UI;

namespace UI.Extensions
{
    public static class UIExtensions
    {
        public static void ShowScreen(this object sender, UIIDs id, IScreenSettings settings = null)
        {
            sender.SendGlobalEvent(new ShowWindowEvent
            {
                Id = (ushort) id,
                Settings = settings
            });
        }

        public static void HideScreen(this object sender, UIIDs id, bool dontAnimate = false)
        {
            sender.SendGlobalEvent(new HideWindowEvent
            {
                Id = (ushort) id,
                WithoutAnimation = dontAnimate
            });
        }

        public static IScreen GetScreen(this object sender, UIIDs id)
        {
            return Core.Instance.Get<DataUIManager>().Value.Get((ushort) id);
        }

        public static TScreen GetScreen<TScreen>(this object sender, UIIDs id) where TScreen : IScreen
        {
            return (TScreen) GetScreen(sender, id);
        }

        public static TSettings GetScreenSettings<TSettings>(this object sender, UIIDs id)
            where TSettings : IScreenSettings
        {
            return GetScreen<IScreen<ushort, TSettings>>(sender, id).Settings;
        }
    }
}