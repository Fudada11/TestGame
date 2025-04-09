using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyHealth : MonoBehaviour
{
    public AudioClip DeathClip;
    
    private int StartingHealth = 100;
    private AudioSource enemyAudioSource;
    private ParticleSystem enemyParticles;
    private Animator enemyAnimator;
    private CapsuleCollider enemyCapsuleCollider;
    public bool IsDead = false;
    public bool IsSinking = false;

    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyParticles = GetComponentInChildren<ParticleSystem>();
        enemyAnimator = GetComponent<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsSinking)
        {
            transform.Translate(-transform.up * 2.5f * Time.deltaTime);
        }

    }
    public void TakeDamage(int amount,Vector3 hitPoint)
    {
        if (IsDead)
            return;
        //声音的播放
        enemyAudioSource.Play();
        //粒子特效的播放
        enemyParticles.gameObject.transform.position = hitPoint;
        enemyParticles.Play();
        //
        StartingHealth -= amount;
        if (StartingHealth <= 0)
            Death();
    }

    private void Death()
    {
        IsDead = true;

        MyPlayerScores.Scores += 10;
        //播放敌人死亡动画
        enemyAnimator.SetTrigger("Death");
        enemyCapsuleCollider.enabled = false;

        //禁用NavMeshAgent
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        //播放敌人死亡音效
        enemyAudioSource.clip = DeathClip;
        enemyAudioSource.Play();
    }
    public void StartSinking()
    {
        IsSinking = true;
        Destroy(gameObject, 2f);
    }
}
