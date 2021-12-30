using System;
using System.Collections.Generic;
using System.Linq;
using DesertImage;
using BallArchitectureApp.Audio;
using UnityEditor;
using UnityEngine;

namespace BallArchitectureApp.Editor
{
    [CustomEditor(typeof(ScriptableSoundsLibrary))]
    public class SoundLibraryEditor : UnityEditor.Editor
    {
        private ScriptableSoundsLibrary _target;

        private void OnEnable()
        {
            _target = target as ScriptableSoundsLibrary;
        }

        public override void OnInspectorGUI()
        {
            _target.Nodes ??= new List<SoundNode>();

            var nodesCount = _target.Nodes.Count;

            if (nodesCount > 0)
            {
                for (var i = 0; i < nodesCount; i++)
                {
                    var node = _target.Nodes[i];

                    if (node == null) continue;

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField($"{(i).ToString()}.", GUILayout.Width(18));

                        GUI.color = node.Id > 0 ? Color.white : Color.red;
                        node.Id = (ushort)Convert.ToInt32(EditorGUILayout.EnumPopup((SoundId)node.Id));
                        GUI.color = Color.white;

                        GUI.color = node.SoundClip ? Color.white : Color.red;
                        node.SoundClip =
                            (AudioClip)EditorGUILayout.ObjectField(node.SoundClip, typeof(AudioClip), false);
                        GUI.color = Color.white;

                        GUILayout.Space(4);

                        node.RegisterCount = EditorGUILayout.IntField(node.RegisterCount);

                        GUILayout.Space(10);

                        GUI.color = Color.red;
                        if (GUILayout.Button("X"))
                        {
                            _target.Nodes.Remove(node);
                        }

                        GUI.color = Color.white;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(15);

            GUI.color = Color.green;
            if (GUILayout.Button("+"))
            {
                _target.Nodes.Add(new SoundNode());
            }

            GUI.color = Color.white;

            EditorUtility.SetDirty(target);

            EditorGUILayout.Space();

            if (GUILayout.Button("Auto refill ids"))
            {
                RefillIDs();
            }

            EditorGUILayout.Space();

            DropAreaGUI();
        }

        public void DropAreaGUI()
        {
            var evt = Event.current;

            var dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));

            GUI.Box(dropArea, "Drag multiple AudioClips");

            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                {
                    if (!dropArea.Contains(evt.mousePosition)) return;

                    UnityEditor.DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        UnityEditor.DragAndDrop.AcceptDrag();

                        if (!_target) return;

                        foreach (var draggedObject in UnityEditor.DragAndDrop.objectReferences)
                        {
                            var audioClip = draggedObject as AudioClip;

                            if (!audioClip || _target.Nodes.FirstOrDefault(x => x.SoundClip == audioClip) != null)
                                continue;

                            var ids = (SoundId[])Enum.GetValues(typeof(SoundId));

                            var id = (from soundId in ids
                                where soundId.ToString().Contains(audioClip.name)
                                select (ushort)soundId).FirstOrDefault();

                            _target.Nodes.Add(new SoundNode()
                            {
                                Id = id,
                                SoundClip = audioClip
                            });
                        }
                    }

                    break;
                }
            }
        }

        private void RefillIDs()
        {
            var nodesCount = _target.Nodes.Count;

            if (nodesCount == 0) return;

            for (var i = 0; i < nodesCount; i++)
            {
                var node = _target.Nodes[i];

                if (node == null) continue;

                var ids = (SoundId[])Enum.GetValues(typeof(SoundId));

                var targetId =
                (
                    from soundId in ids
                    where soundId.ToString().Contains(node.SoundClip.name)
                    select (ushort)soundId
                ).FirstOrDefault();

                if (targetId == default) continue;

                node.Id = targetId;
            }

            EditorUtility.SetDirty(target);
        }
    }
}