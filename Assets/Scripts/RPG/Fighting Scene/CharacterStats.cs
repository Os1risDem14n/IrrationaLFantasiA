using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Information")]
    public string Name;
    public Sprite iconImage;
    [Header("Stats")]
    public int maxHP;
    [HideInInspector] public int currentHP;
    public int maxMP;
    [HideInInspector] public int currentMP;
    public int attack;
    public int magic;
    public int mpCost;
    public int defense;
    [Header("[Only Enemy] EXP")]
    public int EXP;

    [HideInInspector] public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        currentMP = maxMP;
    }

    public void ResetData()
    {
        attack = PlayerPrefs.GetInt("attack");
        magic = PlayerPrefs.GetInt("magic");
        maxHP = PlayerPrefs.GetInt("maxHP");
        maxMP = PlayerPrefs.GetInt("maxMP");
        mpCost = PlayerPrefs.GetInt("mpCost");
        defense = PlayerPrefs.GetInt("defense");
    }

    public void SetData()
    {
        PlayerPrefs.SetInt("attack", attack);
        PlayerPrefs.SetInt("magic", magic);
        PlayerPrefs.SetInt("maxHP", maxHP);
        PlayerPrefs.SetInt("maxMP", maxMP);
        PlayerPrefs.SetInt("mpCost", mpCost);
        PlayerPrefs.SetInt("defense", defense);
    }

    public bool TakeDamage(int dmg, bool type) //true if physics, false if magic
    {
        int damage = 0;
        if (type && dmg > defense)
        {
            damage = dmg - defense;
            currentHP -= damage;
        }
        else if (!type)
        {
            damage = dmg;
            currentHP -= dmg;
        }

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
