using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevel : MonoBehaviour
{
    [Header("Exp to level up")]
    public List<int> levelupEXP = new List<int> { 0, 100 };
    [Header("Level")]
    public int Level;
    public int levelEXP;
    public int Point = 0;

    public void UpdateLevel()
    {
        if (levelEXP >= levelupEXP[Level])
        {
            levelEXP -= levelupEXP[Level];
            Level++;
            Point += 5;
        }
    }

    public void ResetLevel()
    {
        Level = 0;
        levelEXP = 0;
    }
}
