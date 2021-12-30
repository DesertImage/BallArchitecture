using Components;
using UniRx;

namespace BallArchitectureApp
{
    public class DataGameState : DataComponent<DataGameState>
    {
        public readonly ReactiveProperty<GameState> State = new ReactiveProperty<GameState>();
    }
}