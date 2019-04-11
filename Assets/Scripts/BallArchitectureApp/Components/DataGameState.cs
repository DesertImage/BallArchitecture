using BallArchitectureApp.Enums;
using DesertImage;
using UniRx;

namespace BallArchitectureApp.Components
{
    public class DataGameState : IDataComponent
    {
        public ReactiveProperty<GameStates> State;
    }
}