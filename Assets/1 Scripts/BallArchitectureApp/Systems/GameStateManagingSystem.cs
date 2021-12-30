using DesertImage;
using DesertImage.Managers;
using DesertImage.Extensions;

namespace BallArchitectureApp
{
    public class GameStateManagingSystem : SystemBase, IListen<GameStateSetEvent>, IListen<GameOverEvent>
    {
        private DataGameState _dataGameState;

        public override void Activate()
        {
            base.Activate();

            _dataGameState = Core.Instance.Get<DataGameState>();

            this.ListenGlobalEvent<GameOverEvent>();
            this.ListenGlobalEvent<GameStateSetEvent>();
        }

        public override void Deactivate()
        {
            base.Deactivate();

            this.UnlistenGlobalEvent<GameOverEvent>();
            this.UnlistenGlobalEvent<GameStateSetEvent>();
        }

        private void SetState(GameState state)
        {
            if (state == _dataGameState.State.Value) return;

            _dataGameState.State.Value = state;
        }

        public void handleCallback(GameOverEvent arguments)
        {
            this.SendGlobalEvent(new GameStateSetEvent { Value = GameState.GameOver });
        }

        public void handleCallback(GameStateSetEvent arguments)
        {
            SetState(arguments.Value);
        }
    }
}