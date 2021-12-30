namespace DesertImage.UI
{
    public interface IWindow : IScreen<ushort>
    {
        bool DontHideIfNotForeground { get; }
        WindowPriority Priority { get; }
        bool IsPopup { get; }
    }
}