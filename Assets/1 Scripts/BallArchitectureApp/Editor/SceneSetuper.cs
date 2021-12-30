using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BallArchitectureApp.Editor
{
    [InitializeOnLoad]
    public static class SceneSetuper 
    {
        static SceneSetuper()
        {
            EditorSceneManager.newSceneCreated += EditorSceneManagerOnNewSceneCreated;                
        }

        private static void EditorSceneManagerOnNewSceneCreated(Scene scene, NewSceneSetup setup, NewSceneMode mode)
        {
            SetupCameras();
            SetupLights();
            // SetupUI();
            SetupWorld();
        }

        private static void SetupCameras()
        {
            var cameras = new GameObject("[CAMERAS]");

            var cameraMain = Camera.main;

            if (cameraMain)
            {
                cameraMain.transform.SetParent(cameras.transform);
            }
        }

        private static void SetupLights()
        {
            var lights = new GameObject("[LIGHTS]");
 
            var light = GameObject.Find("Directional Light");

            if (light)
            {
                light.transform.SetParent(lights.transform);
            }
        }

        private static void SetupUI()
        {
            var ui = new GameObject("[UI]");
        }

        private static void SetupWorld()
        {
            var world = new GameObject("[WORLD]");
            
            var staticWorld = new GameObject("Static");
            var dynamic = new GameObject("Dynamic");

            if (staticWorld)
            {
                staticWorld.transform.SetParent(world.transform);
            }

            if (dynamic)
            {
                dynamic.transform.SetParent(world.transform);
            }
        }
    }
}