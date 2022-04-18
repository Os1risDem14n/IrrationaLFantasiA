using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public enum EnemyID
    {
        Giant,
        Boar,
        Bat,
        Dino,
        Dragon,
        Ghost
    }

    public EnemyID enemyID;
    public GameObject enemyPrefab;
    public bool isAlive = true;
    public int EXP;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            isAlive = false;
            IdentifyFightingCharacter.Instance.SetEnemy(enemyPrefab);
            //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            //if (asyncOperation.isDone)
            //    SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            UnDestroy.Instance.gameObject.SetActive(false);
            SceneManager.LoadScene(3);
        }
    }
}
