using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyAttack : MonoBehaviour
{
    
    
    private GameObject player;
    private bool playerInRange = false;
    private MyPlayerHealth myPlayerHealth;
    private float timer = 0;
    private Animator enemyAnim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myPlayerHealth = player.GetComponent<MyPlayerHealth>();
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!myPlayerHealth.IsPlayerDead && playerInRange && timer>0.5f)
        {
            //����������˺ܽ����Ͷ��������˺�
            Attack();
        }
        if(myPlayerHealth.IsPlayerDead)
        {
            //���Ŷ�Ӧ����
            enemyAnim.SetTrigger("PlayerDead");
        }
    }

    private void Attack()
    {
        timer = 0;
        myPlayerHealth.TakeDamage(10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject==player)
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject==player)
        {
            playerInRange = false;
        }
    }
}
