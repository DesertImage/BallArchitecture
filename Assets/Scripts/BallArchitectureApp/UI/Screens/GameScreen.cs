using BallArchitectureApp.Components;
using BallArchitectureApp.Enums;
using DesertImage;
using UniRx;
using UnityEngine;

namespace BallArchitectureApp.UI
{
    [RequireComponent(typeof(Canvas))]
    public class GameScreen : MonoBehaviour
    {
        protected virtual GameStates State => GameStates.Game;

        private DataGameState _dataGameState;
        
        private Canvas _canvas;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _canvas = GetComponent<Canvas>();

            _dataGameState = Core.Instance.get<DataGameState>();

            _dataGameState.State.Subscribe(OnGameStateChanged); 
        }
        
        private void OnGameStateChanged(GameStates state)
        {
            Show(state == State);
        }

        protected virtual void Show(bool show = true)
        {
            _canvas.enabled = show;
        }
    }
}