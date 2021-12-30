using DefaultNamespace;
using DesertImage.Extensions;
using BallArchitectureApp.UI;
using BallArchitectureApp.UI.Wrappers;
using UnityEditor;
using UnityEngine;

namespace BallArchitectureApp.Editor
{
    [CustomEditor(typeof(DataUISetupWrapper))]
    public class DataUISetupWrapperEditor : UnityEditor.Editor
    {
        private DataUISetupWrapper _target;

        private void OnEnable()
        {
            _target = (DataUISetupWrapper)target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Build"))
            {
                var data = _target.GetPrivateFiled<DataUISetup>("data");

                UISetupEditor.Build(data.Value);
            }

            GUILayout.Space(15);

            base.OnInspectorGUI();
        }
    }
}