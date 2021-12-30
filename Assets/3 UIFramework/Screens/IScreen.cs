using System;

namespace DesertImage.UI
{
    public interface IScreen
    {
        void Initialize();

        bool IsEnabled { get; }
    }

    public interface IScreen<TId> : IScreen
    {
        Action<IScreen<TId>> OnShowFinished { get; set; }
        Action<IScreen<TId>> OnHideFinished { get; set; }

        Action<IScreen<TId>> OnCloseRequest { get; set; }

        Action<IScreen<TId>> OnDestroyed { get; set; }

        TId Id { get; }

        
        void Show<TSettings>(TSettings settings = default, bool animate = true) where TSettings : IScreenSettings; 

        void Show();
        void Hide(bool animate = true);
    }

    public interface IScreen<TId, TSettings> : IScreen<TId>
    {
        TSettings Settings { get; }
        
        void Show(TSettings settings = default, bool animate = true);
    }
}