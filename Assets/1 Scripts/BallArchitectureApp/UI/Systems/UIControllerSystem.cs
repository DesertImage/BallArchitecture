using DesertImage;
using DesertImage.Extensions;
using DesertImage.Managers;
using DesertImage.UI;
using UnityEngine;

namespace BallArchitectureApp.UI
{
    public class UIControllerSystem : SystemBase, IAwake, IListen<ShowWindowEvent>, IListen<HideWindowEvent>,
        IListen<HideAllWindowsEvent>
    {
        private DataUISetup _dataUiSetup;
        private DataUIManager _dataUiManager;

        public void OnAwake()
        {
            _dataUiSetup = Core.Instance.Get<DataUISetup>();
            _dataUiManager = Core.Instance.Get<DataUIManager>();

            var isDebug = false;

#if DEBUG
            isDebug = true;
#endif

            var uiManager = _dataUiSetup.Value.Setup(isDebug);

            _dataUiManager.Value = uiManager;

            _dataUiManager.Canvas = uiManager.GetComponent<Canvas>();

            _dataUiManager.PopupsLayer = uiManager.GetComponentInChildren<PopupsLayer>();

            this.ListenGlobalEvent<ShowWindowEvent>();
            this.ListenGlobalEvent<HideWindowEvent>();
            this.ListenGlobalEvent<HideAllWindowsEvent>();
        }

        public void handleCallback(ShowWindowEvent arguments)
        {
            _dataUiManager.Value.Show(arguments.Id, arguments.Settings);
        }

        public void handleCallback(HideWindowEvent arguments)
        {
            _dataUiManager.Value.Hide(arguments.Id);
        }

        public void handleCallback(HideAllWindowsEvent arguments)
        {
            _dataUiManager.Value.HideAll(!arguments.IsWithoutAnimation);
        }
    }
}