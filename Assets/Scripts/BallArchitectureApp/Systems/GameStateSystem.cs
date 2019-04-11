using DesertImage;
using DesertImage.Managers;
using BallArchitectureApp.Components;
using BallArchitectureApp.Enums;
using BallArchitectureApp.Events;

namespace BallArchitectureApp.Managers
{
    public class GameStateSystem : ManagerBase, IAwake, IListen<ChangeGameStateEvent>, IListen<GameOverEvent>
    {
        private DataGameState _dataGameState;

        public void onAwake()
        {
            this.Listen<GameOverEvent>();
            this.Listen<ChangeGameStateEvent>();

            _dataGameState = Core.Instance.get<DataGameState>();
        }

        private void SetState(GameStates state)
        {
            if (state == _dataGameState.State.Value) return;

            _dataGameState.State.Value = state;
        }

        public void handleCallback(GameOverEvent arguments)
        {
            SetState(arguments.IsWin ? GameStates.Win : GameStates.Lose);
        }

        public void handleCallback(ChangeGameStateEvent arguments)
        {
            SetState(arguments.Value);
        }
    }
}