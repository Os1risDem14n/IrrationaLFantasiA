using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterAction : MonoBehaviour
{
    [SerializeField] private GameObject bulletAttack;
    [SerializeField] private GameObject bulletSkill;
    [SerializeField] private bool isRangeAttack;
    [SerializeField] private bool isRangeSkill;

    private Animator Animator;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public IEnumerator Attack(GameObject enemy, bool isPlayer)
    {
        Vector3 enemySize = enemy.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 currentSize = this.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 enemyPosition = enemy.transform.position;
        Vector3 currentPosition = this.transform.position;
        Vector3 target;
        Vector3 current;
        float enemyDistance;
        float currentDistance; 

        if (isRangeAttack)
        {
            enemyDistance = enemySize.x / 2;
            currentDistance = currentSize.x / 2;
        }
        else
        {
            enemyDistance = enemySize.x;
            currentDistance = currentSize.x;
        }

        if (isPlayer)
        {
            target = new Vector3(enemyPosition.x + enemyDistance, enemyPosition.y, enemyPosition.z);
            current = new Vector3(currentPosition.x - currentDistance, currentPosition.y, currentPosition.z);
        }
        else
        {
            target = new Vector3(enemyPosition.x - enemyDistance, enemyPosition.y, enemyPosition.z);
            current = new Vector3(currentPosition.x + currentDistance, currentPosition.y, currentPosition.z);
        }

        if (isRangeAttack)
        {
            this.Animator.Play("Attack");
            yield return new WaitForSeconds(0.2f);
            GameObject tempBullet = Instantiate(bulletAttack, current, Quaternion.identity);
            yield return StartCoroutine(MovePositionTo(target, tempBullet, 0.4f));
            Destroy(tempBullet);
            enemy.GetComponent<Animator>().Play("Damaged");
        }
        else
        {
            yield return StartCoroutine(MovePositionTo(target, gameObject, 0.2f));
            Animator.Play("Attack");
            yield return new WaitForSeconds(0.3f);
            enemy.GetComponent<Animator>().Play("Damaged");
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MovePositionTo(currentPosition, gameObject, 0.2f));
        }
    }

    public IEnumerator Skill(GameObject enemy, bool isPlayer)
    {
        Vector3 enemySize = enemy.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 currentSize = this.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 enemyPosition = enemy.transform.position;
        Vector3 currentPosition = this.transform.position;
        Vector3 target;
        Vector3 current;
        float enemyDistance;
        float currentDistance;

        if (isRangeAttack)
        {
            enemyDistance = enemySize.x / 2;
            currentDistance = currentSize.x / 2;
        }
        else
        {
            enemyDistance = enemySize.x;
            currentDistance = currentSize.x;
        }

        if (isPlayer)
        {
            target = new Vector3(enemyPosition.x + enemyDistance, enemyPosition.y, enemyPosition.z);
            current = new Vector3(currentPosition.x - currentDistance, currentPosition.y, currentPosition.z);
        }
        else
        {
            target = new Vector3(enemyPosition.x - enemyDistance, enemyPosition.y, enemyPosition.z);
            current = new Vector3(currentPosition.x + currentDistance, currentPosition.y, currentPosition.z);
        }

        if (isRangeSkill)
        {
            this.Animator.Play("Skill");
            yield return new WaitForSeconds(0.2f);
            GameObject tempBullet = Instantiate(bulletSkill, current, Quaternion.identity);
            yield return StartCoroutine(MovePositionTo(target, tempBullet, 0.4f));
            Destroy(tempBullet);
            enemy.GetComponent<Animator>().Play("Damaged");
        }
        else
        {
            yield return StartCoroutine(MovePositionTo(target, gameObject, 0.2f));
            Animator.Play("Skill");
            yield return new WaitForSeconds(0.3f);
            enemy.GetComponent<Animator>().Play("Damaged");
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MovePositionTo(currentPosition, gameObject, 0.2f));
        }
    }

    IEnumerator MovePositionTo(Vector3 target, GameObject current, float time)
    {
        var currentPos = current.transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            current.transform.position = Vector3.Lerp(currentPos, target, t);
            yield return null;
        }
    }
}
