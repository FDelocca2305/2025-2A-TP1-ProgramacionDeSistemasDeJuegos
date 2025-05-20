using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Excercise1
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<SceneRef> scenes = new();

        private async void Start()
        {
            try
            {
                if (scenes == null || scenes.Count == 0) {
                    Debug.LogWarning("[GameManager] No hay escenas para cargar.");
                    return;
                }
            
                foreach (var scene in scenes)
                {
                    if (scene == null) continue;
                    if (scene.Index < 0 || scene.Index >= SceneManager.sceneCountInBuildSettings) {
                        Debug.LogError($"[GameManager] Scene index inválido: {scene.Index}");
                        continue;
                    }
                
                    var loadSceneAsync = SceneManager.LoadSceneAsync(scene.Index, LoadSceneMode.Additive);
                    if (loadSceneAsync == null)
                        continue;
                    await loadSceneAsync;
                }
            }
            catch (Exception ex) {
                Debug.LogError($"[GameManager] Error cargando escenas: {ex}");
            }
        }
    }
}
