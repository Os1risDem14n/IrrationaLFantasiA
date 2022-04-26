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

    public void LoadData()
    {
    }

    public void ResetData()
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
}