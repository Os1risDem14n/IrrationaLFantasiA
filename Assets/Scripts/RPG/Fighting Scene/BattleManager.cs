using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState
{
    START,
    PLAYER,
    ENEMY,
    WIN,
    LOSE
}
public class BattleManager : Singleton<BattleManager>
{
    [HideInInspector] public GameObject Player;
    [HideInInspector] public GameObject Enemy;
    [HideInInspector] public CharacterStats playerStats;
    [HideInInspector] public CharacterStats enemyStats;
    [HideInInspector] public BattleState state;
    [HideInInspector] public bool isTransfered;

    [Header("Character Position")] 
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private GameObject enemyPosition;

    [Header("Value of tiles")]
    [SerializeField] private int defensePerMatch;
    [SerializeField] private int hpPerMatch;
    [SerializeField] private int mpPerMatch;
    [SerializeField] private int attackMulPerMatch;
    [SerializeField] private int skillMulPerMatch;


    [Header("UI")]
    public UIDisplay UIDisplay;
    public Text playerDamaged;
    public Text enemyDamaged;
    public Text dialogueText;

    private CharacterAction playerAction;
    private CharacterAction enemyAction;
    private GameObject playerPrefab;
    private GameObject enemyPrefab;
    private List<Board.ItemID> enemyActions = new List<Board.ItemID>();

    private void Awake()
    {

        playerPrefab = IdentifyFightingCharacter.Instance.playerPrefab;
        enemyPrefab = IdentifyFightingCharacter.Instance.enemyPrefab;

        state = BattleState.START;
        Init();
        //DontDestroyOnLoad(this);

        playerDamaged.gameObject.SetActive(false);
        playerDamaged.gameObject.SetActive(false);
    }

    void Init()
    {
        Player = Instantiate(playerPrefab, playerPosition.transform.position, Quaternion.identity);
        Enemy = Instantiate(enemyPrefab, enemyPosition.transform.position, Quaternion.identity);

        playerStats = Player.GetComponent<CharacterStats>();
        enemyStats = Enemy.GetComponent<CharacterStats>();

        playerAction = Player.GetComponent<CharacterAction>();
        enemyAction = Enemy.GetComponent<CharacterAction>();

        UIDisplay.InitUI();
        state = BattleState.ENEMY;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        updateEnemyActions();
        enemyDamaged.gameObject.SetActive(false);
        Board.ItemID rand = enemyActions[Random.Range(0, enemyActions.Count)];
        int tempHP = playerStats.currentHP;

        switch (rand)
        {
            case Board.ItemID.Attack:
                {     
                    playerDamaged.gameObject.SetActive(true);
                    StartCoroutine(enemyAction.Attack(Player,false));

                    bool isDead = playerStats.TakeDamage(enemyStats.attack*attackMulPerMatch, true);
                    playerDamaged.text = "-" + (tempHP - playerStats.currentHP);
                    dialogueText.text = "Enemy attack you deal "+ (tempHP - playerStats.currentHP)+" damage";

                    if (isDead)
                    {
                        state = BattleState.LOSE;
                        yield return new WaitForSeconds(2f);
                        StartCoroutine(EndBattle());
                    }
                }
                break;
            case Board.ItemID.Shield:
                dialogueText.text = "Enemy defend +"+defensePerMatch;
                enemyStats.defense += defensePerMatch;
                break;
            case Board.ItemID.HP:
                dialogueText.text = "Enemy HP + "+hpPerMatch;

                if ((enemyStats.maxHP - enemyStats.currentHP) > hpPerMatch)
                {
                    enemyStats.currentHP += hpPerMatch;
                }
                else enemyStats.currentHP = enemyStats.maxHP;
                
                break;
            case Board.ItemID.MP:
                dialogueText.text = "Enemy MP + "+mpPerMatch;

                if ((enemyStats.maxMP - enemyStats.currentMP) > mpPerMatch)
                {
                    enemyStats.currentMP += mpPerMatch;
                }
                else enemyStats.currentMP = enemyStats.maxMP;
                break;
            case Board.ItemID.Magic:                
                {
                    playerDamaged.gameObject.SetActive(true);
                    StartCoroutine(enemyAction.Skill(Player,false));

                    bool isDead = false;
                    if (enemyStats.currentMP >= enemyStats.mpCost)
                    {
                        isDead = playerStats.TakeDamage(enemyStats.magic*skillMulPerMatch, false);
                        enemyStats.currentMP -= enemyStats.mpCost;
                    }

                    dialogueText.text = "Enemy use skill deal " + (tempHP - playerStats.currentHP) + " damage";
                    playerDamaged.text = "-" + (tempHP - playerStats.currentHP);

                    if (isDead)
                    {
                        state = BattleState.LOSE;
                        yield return new WaitForSeconds(2f);
                        StartCoroutine(EndBattle());
                    }
                }
                break;
        }
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYER;

        enemyStats.animator.Play("Idle");
        playerStats.animator.Play("Idle");
    }

    IEnumerator EndBattle()
    {
        Time.timeScale = 0;
        if (state == BattleState.WIN)
        {
            dialogueText.text = "Congratz, you won the battle";
            IdentifyFightingCharacter.Instance.UpdateEXP(enemyStats.EXP);
            yield return new WaitForSecondsRealtime(5f);
            Time.timeScale = 1;
            //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            SceneManager.LoadScene(2);

        }    
        else if (state == BattleState.LOSE)
        {
            dialogueText.text = "Noob, you lost the battle";
            playerStats.ResetData();
            Player.GetComponent<CharacterLevel>().ResetLevel();
            IdentifyFightingCharacter.Instance.playerPrefab.GetComponent<CharacterStats>().ResetData();
            IdentifyFightingCharacter.Instance.playerPrefab.GetComponent<CharacterLevel>().ResetLevel();
            yield return new WaitForSecondsRealtime(5f);
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
            
    }

    public IEnumerator PlayerTurn(Board.ItemID itemID, int count)
    {
        int mul = count;
        playerDamaged.gameObject.SetActive(false);
        int tempHP = enemyStats.currentHP;
        switch (itemID)
        {
            case Board.ItemID.Attack:
                {
                    StartCoroutine(playerAction.Attack(Enemy,true));
                    bool isDead = enemyStats.TakeDamage(playerStats.attack * mul * attackMulPerMatch, true);

                    enemyDamaged.gameObject.SetActive(true);
                    enemyDamaged.text = "-" + (tempHP - enemyStats.currentHP);
                    dialogueText.text = "You attack enemy deal " + (tempHP - enemyStats.currentHP) + " damage";

                    if (isDead)
                    {
                        yield return new WaitForSeconds(2f);
                        state = BattleState.WIN;
                        StartCoroutine(EndBattle());
                    }
                }
                break;
            case Board.ItemID.Shield:
                dialogueText.text = "Your defend + "+defensePerMatch*mul;
                playerStats.defense += defensePerMatch*mul;
                break;
            case Board.ItemID.HP:
                dialogueText.text = "Your HP + "+hpPerMatch*mul;

                if ((playerStats.maxHP - playerStats.currentHP) > hpPerMatch*mul)
                {
                    playerStats.currentHP += hpPerMatch*mul;
                }
                else playerStats.currentHP = playerStats.maxHP;
                break;
            case Board.ItemID.MP:
                dialogueText.text = "Your MP + " +mpPerMatch*mul;

                if ((playerStats.maxMP - playerStats.currentMP) > mpPerMatch*mul)
                {
                    playerStats.currentMP += mpPerMatch*mul;
                }
                else playerStats.currentMP = playerStats.maxMP;
                break;
            case Board.ItemID.Magic:
                {
                    StartCoroutine(playerAction.Skill(Enemy, true));         
                    bool isDead = false;

                    if (playerStats.currentMP >= playerStats.mpCost)
                    {
                        isDead = enemyStats.TakeDamage(playerStats.magic*mul*skillMulPerMatch, false);
                        playerStats.currentMP -= playerStats.mpCost;
                    }

                    enemyDamaged.gameObject.SetActive(true);
                    dialogueText.text = "You use skill deal " + (tempHP - enemyStats.currentHP) + " damage";
                    enemyDamaged.text = "-" + (tempHP - enemyStats.currentHP);

                    if (isDead)
                    {
                        yield return new WaitForSeconds(2f);
                        state = BattleState.WIN;
                        StartCoroutine(EndBattle());
                    }
                }
                break;
        }
        yield return new WaitForSeconds(2f);

        enemyStats.animator.Play("Idle");
        playerStats.animator.Play("Idle");

        state = BattleState.ENEMY;
        StartCoroutine(EnemyTurn());
    }

    void updateEnemyActions()
    {
        enemyActions.Clear();
        if (enemyStats.currentHP < enemyStats.maxHP)
        {
            enemyActions.Add(Board.ItemID.HP);
        }
        if (enemyStats.currentMP < enemyStats.maxMP)
        {
            enemyActions.Add(Board.ItemID.MP);
        }
        if (enemyStats.currentMP > enemyStats.mpCost)
        {
            enemyActions.Add(Board.ItemID.Magic);
        }
        if (enemyStats.defense < playerStats.attack)
        {
            enemyActions.Add(Board.ItemID.Shield);
        }
        if (enemyStats.attack > playerStats.defense)
        {
            enemyActions.Add(Board.ItemID.Attack);
        }
    }
}
