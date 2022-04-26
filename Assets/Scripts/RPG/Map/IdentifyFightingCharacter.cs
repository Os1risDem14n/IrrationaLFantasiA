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
        LoadData();
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
        UserData.Instance.SaveUserLevel(characterLevel);
    }

    public void LoadData()
    {
        CharacterStats character = playerPrefab.GetComponent<CharacterStats>();
        CharacterLevel characterLevel = playerPrefab.GetComponent<CharacterLevel>();
        
        character.attack = UserData.Instance.userAttack;
        character.defense = UserData.Instance.userDefense;
        character.magic = UserData.Instance.userMagic;
        character.maxMP = UserData.Instance.userMp;
        character.maxHP = UserData.Instance.userHp;
        character.mpCost = UserData.Instance.userMpCost;
        character.transform.position = UserData.Instance.userPosition;
        characterLevel.Level = UserData.Instance.userLevel;
        characterLevel.levelEXP = UserData.Instance.userExp;
        characterLevel.Point = UserData.Instance.userPoint;
    }
}
