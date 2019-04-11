using BallArchitectureApp.Enums;
using UnityEngine;

namespace BallArchitectureApp.UI
{
    [RequireComponent(typeof(Canvas))]
    public class GameOverScreen : GameScreen
    {
        protected override GameStates State => GameStates.Lose;
    }
}