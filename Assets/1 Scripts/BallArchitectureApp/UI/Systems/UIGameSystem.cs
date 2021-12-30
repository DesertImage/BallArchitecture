using DesertImage;
using DesertImage.Extensions;
using DesertImage.Managers;
using UI;
using UI.Extensions;
using UnityEngine;

namespace BallArchitectureApp.UI
{
    public class UIGameSystem : SystemBase, IListen<GameStateSetEvent>
    {
        private readonly GameOverPopupSettings _settings = new GameOverPopupSettings();

        public override void Activate()
        {
            base.Activate();

            this.ListenGlobalEvent<GameStateSetEvent>();
        }

        public override void Deactivate()
        {
            base.Deactivate();

            this.UnlistenGlobalEvent<GameStateSetEvent>();
        }

        public void handleCallback(GameStateSetEvent arguments)
        {
            if (arguments.Value != GameState.GameOver) return;

            _settings.Description = $"Random Text {Random.Range(0, 10)}";

            this.ShowScreen(UIIDs.GameOverPopup, _settings);
        }
    }
}