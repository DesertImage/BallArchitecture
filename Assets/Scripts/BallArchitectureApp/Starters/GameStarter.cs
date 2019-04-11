using DesertImage.Starters;

namespace BallArchitectureApp
{
    public class GameStarter : Starter
    {
        protected override void InitCore()
        {
            Core = new GameCore();
        }
    }
}