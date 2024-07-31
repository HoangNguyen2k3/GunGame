using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    private Animator animator;
    public Healthbar healthBar;

    private float safeTime;
    public float safeTimeDuration = 0f;
    public bool isDead = false;
    Enemy_AI enemy;
    Enemy_AI_no_gun enemy2;
    public bool camShake = false;
   
    private void Start()
    {
        currentHealth = maxHealth;
        Transform characterTransform = transform.Find("Character");
        animator = characterTransform.GetComponent<Animator>();
        if (healthBar != null)
            healthBar.updatebar(currentHealth, maxHealth);
        enemy=GetComponent<Enemy_AI>();
        enemy2=GetComponent<Enemy_AI_no_gun>();
        //animator= GetComponent<Animator>();
    }

    public void TakeDam(int damage)
    {
        if (safeTime <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (this.gameObject.tag == "Enemy")
                {

                    /*FindObjectOfType<WeaponManager>().RemoveEnemyToFireRange(this.transform);
                    FindObjectOfType<Killed>().UpdateKilled();
                    FindObjectOfType<PlayerExp>().UpdateExperience(UnityEngine.Random.Range(1, 4));*/
                    if (enemy != null)
                    {
                        enemy.moveSpeed = 0;                      
                    }
                    if(enemy2 != null)
                    {
                        enemy2.moveSpeed = 0;
                    }
                    //animator.SetTrigger("killed_enemy");
                    FindObjectOfType<Killed>().UpdateKilled();
                    Destroy(this.gameObject, 0.125f);
                   
                }
                /*if (this.gameObject == null)
                {
                    FindObjectOfType<Killed>().UpdateKilled();
                }*/
                isDead = true;
            }

            // If player then update health bar
            if (healthBar != null)
                healthBar.updatebar(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }

    private void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
        
    }
}