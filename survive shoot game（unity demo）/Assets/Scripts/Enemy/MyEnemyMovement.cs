using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyMovement : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent nav;
    private MyEnemyHealth myEnemyHealth;
    private MyPlayerHealth myPlayerHealth;
    
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        myEnemyHealth = GetComponent<MyEnemyHealth>();
        myPlayerHealth = player.GetComponent<MyPlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        //判断敌人死否死亡，如果死亡，不再进行导航坐标点的设置
        if (!myEnemyHealth.IsDead && !myPlayerHealth.IsPlayerDead)
            nav.SetDestination(player.transform.position);
        else
            nav.enabled = false;
    }
}
