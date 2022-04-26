using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UserData : Singleton<UserData>
{
    [Header("User Data")]
    public int userLevel;
    public int userExp;
    public int userPoint;
    public int userCoin;
    public Vector2 userPosition;
    public int userHp;
    public int userMp;
    public int userAttack;
    public int userMagic;
    public int userMpCost;
    public int userDefense;

    public void SaveUserStats(CharacterStats character)
    {
        userAttack = character.attack;
        userDefense = character.defense;
        userMagic = character.magic;
        userMp = character.maxMP;
        userHp = character.maxHP;
        userMpCost = character.mpCost;
        userPosition = character.transform.position;
    }

    public void SaveUserLevel(CharacterLevel characterLevel)
    {
        userLevel = characterLevel.Level;
        userExp = characterLevel.levelEXP;
        userPoint = characterLevel.Point;
    }

    public void InitData()
    {
        userAttack = PlayerPrefs.GetInt("attack");
        userMagic = PlayerPrefs.GetInt("magic");
        userHp = PlayerPrefs.GetInt("maxHP");
        userMp = PlayerPrefs.GetInt("maxMP");
        userMpCost = PlayerPrefs.GetInt("mpCost");
        userDefense = PlayerPrefs.GetInt("defense");
        userLevel = 0;
        userExp = 0;
        userPoint = 0;
        userCoin = 0;
    }

    public void LoadFromJSON(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }
 
    public string SaveToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}