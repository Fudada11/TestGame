using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyPlayerHealth : MonoBehaviour
{
    //���Ѫ��
    public int PlayerStartingHealth = 100;
    //����Ƿ�����
    public bool IsPlayerDead = false;
    //���Ѫ��UI
    public Text PlayerHealthUI;
    //��������ڵ���
    public Image DamageImage;

    private AudioSource playerAudio;
    public AudioClip PlayerDeathClip;
    private Animator playerAnim;
    private PlayerMovement playerMovement;
    private MyPlayerShooting myPlayerShooting;
    private bool damaged = false;
    public Color FlashColor = new Color(1f, 0f, 0f, 0.1f);

    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        myPlayerShooting = GetComponentInChildren<MyPlayerShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
            DamageImage.color = FlashColor;
        else
            DamageImage.color = Color.Lerp(DamageImage.color,Color.clear,5f*Time.deltaTime);
        damaged = false;


    }

    public void TakeDamage(int amount)
    {
        if (IsPlayerDead)
            return;

        damaged = true;

        //������������
        playerAudio.Play();
        
        
        PlayerStartingHealth -= amount;
        //�������Ѫ��UI
        PlayerHealthUI.text = PlayerStartingHealth.ToString();
        if (PlayerStartingHealth <= 0)
            Death();
    }

    void Death()
    {
        IsPlayerDead = true;
        //����������Ч
        playerAudio.clip = PlayerDeathClip;
        playerAudio.Play();
        //������������
        playerAnim.SetTrigger("Die");
        //��ֹ�ƶ���ֹ��ǹ
        playerMovement.enabled = false;
        myPlayerShooting.enabled = false;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

}
