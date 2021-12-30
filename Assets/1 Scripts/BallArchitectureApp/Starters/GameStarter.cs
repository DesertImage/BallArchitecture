using System.Collections;
using BallArchitectureApp.Managers;
using BallArchitectureApp.UI;
using DesertImage.Starters;

namespace BallArchitectureApp
{
    public class GameStarter : Starter
    {
        protected override IEnumerator InitData()
        {
            var process = base.InitData();

            while (process.MoveNext())
            {
                yield return null;
            }

            Core.Add<DataGameState>();

            Core.Add<DataSpheres>();

            Core.Add<DataUIManager>();
        }

        protected override IEnumerator InitSystems()
        {
            var process = base.InitData();

            while (process.MoveNext())
            {
                yield return null;
            }

            Core.Add<GameStateManagingSystem>();

            Core.Add<SpheresSpawnSystem>();

            Core.Add<SpheresManagingSystem>();
            Core.Add<SpawnRandomCountSpheresSystem>();
            Core.Add<SpheresCountGameOverSystem>();

            Core.Add<UIControllerSystem>();
            Core.Add<UIGameSystem>();
        }

        protected override IEnumerator InitManagers()
        {
            var process = base.InitManagers();

            while (process.MoveNext())
            {
                yield return null;
            }
        }
    }
}