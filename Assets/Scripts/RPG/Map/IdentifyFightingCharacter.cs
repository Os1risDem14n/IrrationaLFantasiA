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
        CharacterStats character = playerPrefab.GetComponent<CharacterStats>();
        UserData.Instance.userAttack = character.attack;
        UserData.Instance.userDefense = character.defense;
        UserData.Instance.userMagic = character.magic;
        UserData.Instance.userMp = character.maxMP;
        UserData.Instance.userHp = character.maxHP;
        UserData.Instance.userMpCost = character.mpCost;
        UserData.Instance.userPosition = player.transform.position;
    }

    public void SetEnemy(GameObject enemy)
    {
        enemyPrefab = enemy;
    }

    public void UpdateEXP(int exp)
    {
        CharacterLevel characterLevel = playerPrefab.GetComponent<CharacterLevel>();
        characterLevel.levelEXP += exp;
        characterLevel.UpdateLevel();
        UserData.Instance.userLevel = characterLevel.Level;
        UserData.Instance.userExp = characterLevel.levelEXP;
        UserData.Instance.userPoint = characterLevel.Point;
    }
}
