using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapData : Singleton<MapData>
{
    public List<EnemyData> enemyData = new List<EnemyData>();
    public Vector3 playerPosition;

    private Vector3 startPosition;

    public void UpdatePostion(Transform transform)
    {
        playerPosition = transform.position;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameObject parent = GameObject.Find("Enemy");
        foreach (Transform child in parent.transform)
        {
            GameObject temp = Instantiate(child.gameObject);
            temp.name = child.name + "Position" + child.position;
            enemyData.Add(new EnemyData(temp, child.position,false));
        }
    }

    public void UpdateList()
    {
        foreach (EnemyData gameObject in enemyData)
        {
            if (!gameObject.enemy.GetComponent<EnemyController>().isAlive)
            {
                gameObject.isDead = false;
            }
        }
    }

    public void UpdatePosition()
    {
        foreach (EnemyData enemyData in enemyData)
        {
            enemyData.position = enemyData.enemy.transform.position;
        }
    }
}

[Serializable]
public class EnemyData
{
    public GameObject enemy;
    public Vector3 position;
    public bool isDead;

    public EnemyData(GameObject gameobject, Vector3 pos, bool dead)
    {
        enemy = gameobject;
        position = pos;
        isDead = dead;
    }

    public void UpdateStatus(bool dead)
    {
        isDead = dead;
    }
}