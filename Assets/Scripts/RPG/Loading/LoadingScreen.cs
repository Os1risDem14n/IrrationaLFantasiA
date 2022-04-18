using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] public Image progressBar;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(LoadSceneAsyncOperation(3f));
    }

    private IEnumerator LoadSceneAsyncOperation(float timeDelay)
    {
        float time = 0f;
        while (time <= timeDelay)
        {
            time += Time.deltaTime;
            progressBar.fillAmount = time / timeDelay;
            yield return null;
        }
        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(4);

        if (!asyncOperation.isDone)
        {
            yield return null;
        }

    }

    private IEnumerator FadeOut(float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}
