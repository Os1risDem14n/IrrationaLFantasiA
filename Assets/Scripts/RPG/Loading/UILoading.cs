using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    [SerializeField] private Text enemyName;
    [SerializeField] private SpriteRenderer enemyImage;

    private void Awake()
    {
        CharacterStats enemy = IdentifyFightingCharacter.Instance.enemyPrefab.GetComponent<CharacterStats>();
        enemyName.text = enemy.Name;
        enemyImage.sprite = enemy.iconImage;
    }
}
