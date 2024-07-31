using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Player playerS;
    public int minDamage=5;
    public int maxDamage=15;
    Health health;

    public GameObject FloatTextPrefab;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    public void TakedDamage(int damage)
    {
        health.TakeDam(damage);
        //Show floatingText
        if(FloatTextPrefab != null)
        {
            var go = Instantiate(FloatTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text =damage.ToString();
/*            Animator animator = GetComponent<Animator>();
            
            if (damage >= 10)
            {
                animator.SetTrigger("critdamage");
            }*/
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerS = collision.GetComponent<Player>();
            InvokeRepeating("DamagePlayer",0,0.1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerS = null;
            CancelInvoke("DamagePlayer");
        }
    }
    void DamagePlayer()
    {
        int damage=UnityEngine.Random.Range(minDamage,maxDamage);
        playerS.TakeDamage(damage);
       /* Debug.Log("Player take damage " + damage);*/
    }
}
