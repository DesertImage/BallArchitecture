using UnityEngine;
using UnityEngine.UI;

namespace DesertImage.UI
{
    [CreateAssetMenu(fileName = "UISetup", menuName = "UI/Setup")]
    public class UISetup : ScriptableObject
    {
        [SerializeField] private UIManager uiManagerPrefab;

#if UNITY_EDITOR
        [SerializeField] [Space(10)] private Sprite maket;
#endif

        [SerializeField] [Space(15)] private GameObject[] screens;
        [SerializeField] [Space(10)] private GameObject[] debugScreens;


        public UIManager Setup(bool isDebugMode = false)
        {
            var uiManager = Instantiate(uiManagerPrefab);

            foreach (var screen in screens)
            {
                var screenObj = Instantiate(screen, uiManager.transform);

                var newScreen = screenObj.GetComponent<IScreen<ushort>>();

                if (newScreen == null)
                {
#if DEBUG
                    UnityEngine.Debug.LogError("[UISetup] screen is null " + screenObj);
#endif

                    continue;
                }

                uiManager.Register(newScreen.Id, newScreen);
            }

            if (isDebugMode && debugScreens != null)
            {
                foreach (var debugScreen in debugScreens)
                {
                    var screenObj = Instantiate(debugScreen);

                    var screen = screenObj.GetComponent<IScreen<ushort>>();

                    uiManager.Register(screen.Id, screen);

                    uiManager.Unregister(screen.Id, screen);

                    screen.Show();
                }
            }

            return uiManager;
        }
    }
}