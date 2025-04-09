using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerShooting : MonoBehaviour
{
    private AudioSource gunAudio;

    private float time = 0f;
    public float timeBetweenBullets = 0.15f;

    private Light gunLight;
    public float effectsDisplayTime = 0.2f;

    private LineRenderer gunLine;

    private ParticleSystem gunParticle;

    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootMask;

    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunParticle = GetComponent<ParticleSystem>();
        shootMask = LayerMask.GetMask("shootable");
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        
        //��ȡ�����
        
        if(Input.GetButton("Fire1") && time>=timeBetweenBullets)
        {
            //����
            Shoot();
        }

        if(time >= timeBetweenBullets*effectsDisplayTime)
        {
            gunLight.enabled = false;
            gunLine.enabled = false;
        }
    }
    void Shoot()
    {
        time = 0;
        //Debug.Log(DateTime.Now.ToString("HH:mm:ss:fff"));
        gunAudio.Play();
        //���õ��Դ
        gunLight.enabled = true;
        //���Ƶ�������
        gunLine.SetPosition(0, transform.position);
        gunLine.SetPosition(1,transform.position+transform.forward * 100);
        gunLine.enabled = true;
        //����ǹ�ڻ����������
        gunParticle.Play();
        //��ⵯ�������Ƿ���������
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        if(Physics.Raycast(shootRay,out shootHit,100,shootMask))
        {
            gunLine.SetPosition(1, shootHit.point);
            MyEnemyHealth enemyHealth = shootHit.collider.GetComponent<MyEnemyHealth>();
            enemyHealth.TakeDamage(10, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * 100);
        }
    }
}
