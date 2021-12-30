using BallArchitectureApp.Spawning;
using DesertImage.UI;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BallArchitectureApp.UI
{
    public class GameOverPopup : Window<GameOverPopupSettings>
    {
        public override ushort Id => (ushort)UIIDs.GameOverPopup;

        public override bool IsPopup => true;

        [SerializeField] private Text descriptionLabel;
        [SerializeField] private Button restartButton;

        public override void Initialize()
        {
            base.Initialize();

            restartButton.SetOnClickWithSound(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }

        public override void Setup(GameOverPopupSettings settings)
        {
            base.Setup(settings);

            descriptionLabel.text = settings.Description;
        }
    }

    public class GameOverPopupSettings : IWindowSettings
    {
        public bool DontHideIfNotForeground { get; }
        public WindowPriority Priority { get; }
        public bool IsPopup { get; }

        public string Description;
    }
}