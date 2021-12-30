using System;
using System.Collections.Generic;
using System.Linq;
using BallArchitectureApp.Spawning;
using DesertImage;
using UnityEditor;
using UnityEngine;

namespace BallArchitectureApp.Editor
{
    [CustomEditor(typeof(ScriptableObjectsLibrary))]
    public class ObjectsLibraryEditor : UnityEditor.Editor
    {
        private ScriptableObjectsLibrary _target;

        private void OnEnable()
        {
            _target = target as ScriptableObjectsLibrary;
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();

            _target.Nodes ??= new List<ObjectsSpawnNode>();

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

                        var serializedNode = serializedObject.FindProperty("Nodes").GetArrayElementAtIndex(i);

                        // EditorGUILayout.PropertyField(serializedNode, false);
                        // var drawer = new SearchableAttributeDrawer();

                        // EditorGUI.PropertyField(serializedNode);
                        // var drawerRect = EditorGUILayout.GetControlRect(true,
                        // drawer.GetPropertyHeight(serializedNode, GUIContent.none));

                        // drawer.OnGUI(drawerRect, serializedNode, GUIContent.none);
                        // serializedNode.dra

                        node.Id = (ushort)Convert.ToInt32(EditorGUILayout.EnumPopup((ObjectsId)node.Id));
                        GUI.color = Color.white;

                        GUI.color = node.Prefab ? Color.white : Color.red;
                        node.Prefab = (GameObject)EditorGUILayout.ObjectField(node.Prefab, typeof(GameObject), false);
                        GUI.color = Color.white;

                        GUILayout.Space(4);

                        node.RegisterCount = EditorGUILayout.IntField(node.RegisterCount, GUILayout.Width(30f));

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
                _target.Nodes.Add(new ObjectsSpawnNode());
            }

            GUI.color = Color.white;

            EditorUtility.SetDirty(target);

            EditorGUILayout.Space();

            DropAreaGUI();
        }

        public void DropAreaGUI()
        {
            var evt = Event.current;

            var dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));

            GUI.Box(dropArea, "Drag multiple prefabs");

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
                            var gameObject = draggedObject as GameObject;

                            if (!gameObject) continue;

                            var ids = (ObjectsId[])Enum.GetValues(typeof(ObjectsId));

                            var id = (from objId in ids
                                where objId.ToString().Contains(gameObject.name)
                                select (ushort)objId).FirstOrDefault();

                            _target.Nodes.Add(new ObjectsSpawnNode
                            {
                                Id = id,
                                Prefab = gameObject
                            });
                        }
                    }

                    break;
                }
            }
        }
    }
}