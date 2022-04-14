using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void CancelButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        foreach (UnDestroy unDestroy in Resources.FindObjectsOfTypeAll(typeof(UnDestroy)) as UnDestroy[])
        if (unDestroy != null)
            Destroy(unDestroy.gameObject);
           
        SceneManager.LoadSceneAsync(1);
    }
}
