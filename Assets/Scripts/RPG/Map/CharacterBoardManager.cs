using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterBoardManager : MonoBehaviour
{
    [Header("Character Info Text")]
    public TMP_Text characterName;
    public TMP_Text characterLevel;
    public TMP_Text characterHP;
    public TMP_Text characterMP;
    public TMP_Text characterAttack;
    public TMP_Text characterMagic;
    public TMP_Text characterDefense;
    public TMP_Text characterPoint;

    [Header("Plus Point Button")]
    public Button plusHPButton;
    public Button plusMPButton;
    public Button plusAttackButton;
    public Button plusMagicButton;
    public Button plusDefenseButotn;

    [Header("Stat per Point")]
    public int HP;
    public int MP;
    public int Attack;
    public int Magic;
    public int Defense;

    private enum charText
    {
        HP,
        MP,
        Attack,
        Magic,
        Defense,
        Point
    }
    private CharacterStats character;
    private CharacterLevel level;
    private int hp;
    private int mp;
    private int attack;
    private int magic;
    private int defense;
    private int point;

    private void OnEnable()
    {
        GetCharacterData();
        characterName.text = character.Name;
        characterLevel.text = "Level " + level.Level + "     " + level.levelEXP + " / " + level.levelupEXP[level.Level];

        plusAttackButton.GetComponent<Button>().onClick.AddListener(() => PlusButton(charText.Attack));
        plusDefenseButotn.GetComponent<Button>().onClick.AddListener(() => PlusButton(charText.Defense));
        plusHPButton.GetComponent<Button>().onClick.AddListener(() => PlusButton(charText.HP));
        plusMagicButton.GetComponent<Button>().onClick.AddListener(() => PlusButton(charText.Magic));
        plusMPButton.GetComponent<Button>().onClick.AddListener(() => PlusButton(charText.MP));
    }

    private void SetText(charText charText,int stat)
    {
        switch (charText)
        {
            case charText.HP:
                characterHP.text = "" + stat;
                break;
            case charText.MP:
                characterMP.text = "" + stat;
                break;
            case charText.Attack:
                characterAttack.text = "" + stat;
                break;
            case charText.Magic:
                characterMagic.text = "" + stat;
                break;
            case charText.Defense:
                characterDefense.text = "" + stat;
                break;
            case charText.Point:
                characterPoint.text = "Remaining " + stat + " pts";
                break;
        }
    }

    private void PlusButton(charText charText)
    {
        if (point < 1)
            return;
        switch (charText)
        {
            case charText.HP:
                hp += HP;
                point--;
                SetText(charText.HP, hp);
                SetText(charText.Point, point);
                break;
            case charText.MP:
                mp += MP;
                point--;
                SetText(charText.MP, mp);
                SetText(charText.Point, point);
                break;
            case charText.Attack:
                attack += Attack;
                point--;
                SetText(charText.Attack, attack);
                SetText(charText.Point, point);
                break;
            case charText.Magic:
                magic += Magic;
                point--;
                SetText(charText.Magic, magic);
                SetText(charText.Point, point);
                break;
            case charText.Defense:
                defense += Defense;
                point--;
                SetText(charText.Defense, defense);
                SetText(charText.Point, point);
                break;
        }
    }

    public void GetCharacterData()
    {
        if (IdentifyFightingCharacter.Instance.playerPrefab != null)
        {
            character = IdentifyFightingCharacter.Instance.playerPrefab.GetComponent<CharacterStats>();
            level = IdentifyFightingCharacter.Instance.playerPrefab.GetComponent<CharacterLevel>();
        }
            

        hp = character.maxHP;
        mp = character.maxMP;
        attack = character.attack;
        magic = character.magic;
        defense = character.defense;
        point = level.Point;

        SetText(charText.HP, hp);
        SetText(charText.MP, mp);
        SetText(charText.Attack, attack);
        SetText(charText.Magic, magic);
        SetText(charText.Defense, defense);
        SetText(charText.Point, point);
    }

    public void SaveCharacterData()
    {
        character.maxHP = hp;
        character.maxMP = mp;
        character.attack = attack;
        character.magic = magic;
        character.defense = defense;
        level.Point = point;
        UserData.Instance.SaveUserLevel(level);
        UserData.Instance.SaveUserStats(character);
        DatabaseManager.Instance.SaveData();
    }
}
