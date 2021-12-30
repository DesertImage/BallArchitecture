namespace DesertImage.UI.Panel
{
    public interface IPanel : IScreen<ushort>
    {
        bool IsShowing { get; }
        
        PanelPriority Priority { get; }
    }
}