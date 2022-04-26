using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMapData : Singleton<EnemyMapData>
{
    public struct Enemy
    {
        private EnemyController.EnemyID enemyID;
        private bool enemyAlive;
        private Vector2 enemyPosition;

        public Enemy(EnemyController.EnemyID _enemyID, bool _enemyAlive, Vector2 _enemyPosition)
        {
            enemyID = _enemyID;
            enemyAlive = _enemyAlive;
            enemyPosition = _enemyPosition;
        }
    }
    
    private List<Enemy> EnemyData = new List<Enemy>();

    public void SaveDataLocal()
    {
        
    }

    public void SaveDataFirebase()
    {
    }

    public void LoadData()
    {
    }

    public void ResetData()
    {
        EnemyData.Clear();
        foreach (Transform child in transform)
        {
            var enemy = child.gameObject.GetComponent<EnemyController>();
            var enemyData = new Enemy(enemy.enemyID,enemy.isAlive,enemy.transform.position);
            EnemyData.Add(enemyData);
        }
    }
}