using BallArchitectureApp.Components;
using BallArchitectureApp.Enums;
using BallArchitectureApp.Managers;
using DesertImage;
using DesertImage.Pools;
using UniRx;
using UnityEngine;

namespace BallArchitectureApp
{
    public class GameCore : Core
    {
        protected override void InitData()
        {
            base.InitData();

            var poolsObj = GameObject.Find("Pools");
            if (!poolsObj)
            {
                poolsObj = new GameObject("Pools");
            }

            Add(new PoolGameObject(poolsObj.transform));
            
            
            Add(new DataSpheres());

            Add(new DataGameState {State = new ReactiveProperty<GameStates>(GameStates.Game)});

            Add(new DataCanvas {Value = GameObject.Find("UI").GetComponent<Canvas>()});
        }

        protected override void InitSystems()
        {
            base.InitSystems();

            Add(new GameStateSystem());
            
            Add(new SpheresCollectionSystem());
            
            Add(new SpheresCountGameOverSystem());
            
            Add(new BindCountHealthViewSystem());
            Add(new BindSliderHealthViewSystem());
            
            Add(new SpawnSpheresSystem());
            Add(new SpawnRandomCountSpheresSystem());
        }
    }
}