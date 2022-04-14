using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(1);
            SceneManager.UnloadSceneAsync(2);
        }
        UnDestroy.Instance.gameObject.SetActive(true);
    }
}
