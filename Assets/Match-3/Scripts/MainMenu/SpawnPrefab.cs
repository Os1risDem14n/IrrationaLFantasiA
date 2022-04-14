using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    [SerializeField] private List<GameObject> listPrefab = new List<GameObject>();
    [SerializeField] private Transform positionToSpawn;
    [SerializeField] private float timeDelay;
    private float time;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;
            float randX = Random.Range(-1.5f, 1.5f);
            float randY = Random.Range(-1.5f, 1.5f);
            Vector3 pos = new Vector3(positionToSpawn.position.x + randX, positionToSpawn.position.y + randY, positionToSpawn.position.z);
            int randPrefab = Random.Range(0, listPrefab.Count);
            Instantiate(listPrefab[randPrefab], pos, Quaternion.identity,positionToSpawn);
        }
        
    }
}
