using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject clone;

    private void Awake()
    {
        transform.position = UnDestroy.Instance.playerPosition;
        IdentifyFightingCharacter.Instance.SetPlayer(playerPrefab);
    }

    public void ResetPlayer()
    {
        playerPrefab.GetComponent<CharacterLevel>().ResetLevel();
        playerPrefab.GetComponent<CharacterStats>().ResetData();
    }

    public void SetPlayerData()
    {
        playerPrefab.GetComponent<CharacterStats>().SetData();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            UnDestroy.Instance.UpdatePosition(transform.position);
        }
    }
}
