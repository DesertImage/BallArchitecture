using System;
using System.Reflection;
using DesertImage.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [CustomEditor(typeof(UISetup))]
    public class UISetupEditor : Editor
    {
        private static UISetup _target;

        private void OnEnable()
        {
            _target = (UISetup)target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Build"))
            {
                Build(_target);
            }

            GUILayout.Space(15);

            base.OnInspectorGUI();
        }

        public static void Build(UISetup uiSetup)
        {
            static T GetPrivateField<T>(object targetObj, string fieldName) where T : class
            {
                return targetObj
                    .GetType()
                    .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.GetValue(targetObj) as T;
            }

            var manager = GetPrivateField<UIManager>(uiSetup, "uiManagerPrefab");
            var screens = GetPrivateField<GameObject[]>(uiSetup, "screens");

            var uiManager = PrefabUtility.InstantiatePrefab(manager) as UIManager;

            foreach (var screen in screens)
            {
                var obj = PrefabUtility.InstantiatePrefab(screen, uiManager.transform) as GameObject;

                if (!obj) continue;

                var newScreen = obj.GetComponent<IScreen<ushort>>();

                uiManager.Register(newScreen.Id, newScreen);
            }

            try
            {
                uiManager.ShowAll();
            }
            catch (Exception e)
            {
#if DEBUG
                UnityEngine.Debug.LogError("[UISetupEditor] exception " + e);
#endif
            }

            var parent = uiManager.transform as RectTransform;

            #region MAKET

            var maket = new GameObject("Maket", typeof(RectTransform))
            {
                transform =
                {
                    parent = parent
                }
            };

            var maketImage = maket.AddComponent<Image>();
            maketImage.sprite = GetPrivateField<Sprite>(uiSetup, "maket");

            var maketImageColor = maketImage.color;
            maketImageColor.a = .5f;
            maketImage.color = maketImageColor;


            var maketRect = maket.transform as RectTransform;

            maketRect.transform.localScale = Vector3.one;
            maketRect.transform.SetParent(parent);

            maketRect.anchoredPosition = new Vector2();

            maketRect.anchorMin = new Vector2(0f, 0f);
            maketRect.anchorMax = new Vector2(1f, 1f);

            maketRect.pivot = new Vector2(0.5f, 0.5f);

            // maketRect.sizeDelta = parent.sizeDelta * .5f;

            maketRect.offsetMin = maketRect.offsetMax = new Vector2(0, 0);
            // maketRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
            // maketRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            // maketRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            // maketRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);

            maket.SetActive(false);

            #endregion

            // var adaptiveSafeArea = uiManager.GetComponentInChildren<AdaptiveSafeArea>();
            // if (adaptiveSafeArea)
            // {
            //     adaptiveSafeArea.Resize(new Rect(132, 63, 2172, 1062));
            // }

            uiManager.gameObject.tag = "EditorOnly";
        }
    }
}