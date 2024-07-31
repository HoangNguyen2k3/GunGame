using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;
    public Healthbar healthBar;
    public UnityEvent OnDeath;
    public float safeTime=1f;
    float safeTimeCoolDown;
    public Animator animator;
   
    private void OnEnable()
    {
        OnDeath.AddListener(Destroy);
    }
    private void OnDisable()
    {
        OnDeath.RemoveListener(Destroy);
    }
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.updatebar(currentHealth, maxHealth);
        animator = GetComponent<Animator>();
    }
    public void TakeDam(int damage)
    {
       
            if (safeTimeCoolDown <= 0)
            {
                currentHealth -= damage;
                if (currentHealth <= 0)
                {
                animator.SetTrigger("Die");
                //OnDeath.Invoke();
                Invoke("Destroy", 0.52f);
                //SceneManager.LoadScene(2);
                //Destroy();
                }
                safeTimeCoolDown = safeTime;
                healthBar.updatebar(currentHealth, maxHealth);
            }
        
    }
    public void Destroy()
    {   
        Destroy(gameObject);      
    }

    private void Update()
    {
        safeTimeCoolDown-= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDam(20);
        }
       
    }

}
