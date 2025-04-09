using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyPlayerHealth : MonoBehaviour
{
    //玩家血量
    public int PlayerStartingHealth = 100;
    //玩家是否死亡
    public bool IsPlayerDead = false;
    //玩家血量UI
    public Text PlayerHealthUI;
    //玩家受伤遮挡层
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

        //播放受伤声音
        playerAudio.Play();
        
        
        PlayerStartingHealth -= amount;
        //更新玩家血量UI
        PlayerHealthUI.text = PlayerStartingHealth.ToString();
        if (PlayerStartingHealth <= 0)
            Death();
    }

    void Death()
    {
        IsPlayerDead = true;
        //播放死亡音效
        playerAudio.clip = PlayerDeathClip;
        playerAudio.Play();
        //播放死亡动画
        playerAnim.SetTrigger("Die");
        //禁止移动禁止开枪
        playerMovement.enabled = false;
        myPlayerShooting.enabled = false;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

}
