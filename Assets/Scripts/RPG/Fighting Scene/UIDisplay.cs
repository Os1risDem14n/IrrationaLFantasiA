using UnityEngine.UI;
using UnityEngine;

public class UIDisplay : MonoBehaviour
{
    public BattleManager battleManager;

    [Header("Player UI")]
    public Slider playerHPSlider;
    public Text playerHPCount;
    public Slider playerMPSlider;
    public Text playerMPCount;

    [Header("Enemy UI")]
    public Slider enemyHPSlider;
    public Text enemyHPCount;
    public Slider enemyMPSlider;
    public Text enemyMPCount;

    private CharacterStats enemy;
    private CharacterStats player;

    public void InitUI()
    {
        enemy = battleManager.enemyStats;
        player = battleManager.playerStats;

        playerHPSlider.maxValue = player.maxHP;
        playerHPSlider.value = player.currentHP;
        playerMPSlider.maxValue = player.maxMP;
        playerMPSlider.value = player.currentMP;
        playerHPCount.text = player.currentHP + "";
        playerMPCount.text = player.currentMP + "";

        enemyHPSlider.maxValue = enemy.maxHP;
        enemyHPSlider.value = enemy.currentHP;
        enemyMPSlider.maxValue = enemy.maxMP;
        enemyMPSlider.value = enemy.currentMP;
        enemyHPCount.text = enemy.currentHP + "";
        enemyMPCount.text = enemy.currentMP + "";
    }

    private void Update()
    {
        playerHPSlider.value = player.currentHP;
        playerHPCount.text = player.currentHP + "";
        playerMPSlider.value = player.currentMP;
        playerMPCount.text = player.currentMP + "";

        enemyHPSlider.value = enemy.currentHP;
        enemyMPSlider.value = enemy.currentMP;
        enemyHPCount.text = enemy.currentHP + "";
        enemyMPCount.text = enemy.currentMP + "";
    }
}
