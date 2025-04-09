using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject CreatEnemyPoint;
    public float FirstCreatEnemyTime = 0f;
    public float CreatEnemyTime = 3f;
    private void Start()
    {
        InvokeRepeating("Spawn", FirstCreatEnemyTime, CreatEnemyTime);
    }
    private void Spawn()
    {
        Instantiate(Enemy,CreatEnemyPoint.transform.position,CreatEnemyPoint.transform.rotation);
    }
}
