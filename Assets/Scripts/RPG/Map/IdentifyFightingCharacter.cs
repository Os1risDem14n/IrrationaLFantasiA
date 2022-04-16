using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentifyFightingCharacter : Singleton<IdentifyFightingCharacter>
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public void SetPlayer(GameObject player)
    {
        playerPrefab = player;
    }

    public void SetEnemy(GameObject enemy)
    {
        enemyPrefab = enemy;
    }

    public void UpdateEXP(int exp)
    {
        playerPrefab.GetComponent<CharacterLevel>().levelEXP += exp;
        playerPrefab.GetComponent<CharacterLevel>().UpdateLevel();
    }
}
