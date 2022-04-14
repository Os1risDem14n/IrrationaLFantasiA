using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnDestroy : MonoBehaviour
{
    private static UnDestroy instance;
    public Vector3 playerPosition;
    public Vector3 playerStartPosition;
    public static UnDestroy Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UnDestroy>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }

    public void UpdatePosition(Vector3 pos)
    {
        playerPosition = pos;
    }
}
