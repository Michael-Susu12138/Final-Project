using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class CameraSwitcher : MonoBehaviour
{
    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        // Find all main cameras in the new scene
        var mainCameras = newScene.GetRootGameObjects()
            .Where(go => go.CompareTag("MainCamera"))
            .ToArray();

        // Destroy the main camera of the previous scene
        if (Camera.main != null)
        {
            Destroy(Camera.main.gameObject);
        }

        // Enable the main camera(s) of the new scene
        foreach (var mainCamera in mainCameras)
        {
            mainCamera.SetActive(true);
        }
    }
}
