using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float move_Speech = 10f;
    private Rigidbody rb;
    public Vector3 moveInput;
    public Animator animator;
    public GameObject ghostEffect;
    public float ghostDelaySecond;
    private Coroutine dashEffectCoroutine;
    //Dash
    public float dashBoost;
    public float dashTime;
    private float _dashTime;
    bool isDashing = false;
    //Roll
    float Rollboost = 5f;
    private float rollTime;
    public float RollTime=0.25f;
    private bool rollone=false;
    //public SpriteRenderer CharacterRD;
    private void Start()
    {
        animator=GetComponent<Animator>();
    }
    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * move_Speech * Time.deltaTime;
        animator.SetFloat("Speech", moveInput.sqrMagnitude);
      //Dash
        if(Input.GetKey(KeyCode.Space) && _dashTime<=0 && isDashing==false)
        {
            move_Speech += dashBoost;
            _dashTime = dashTime;
            isDashing = true;
            StartDashEffect();
        }
        if (_dashTime <= 0 && isDashing == true)
        {
            move_Speech -= dashBoost;
            isDashing = false;
            StopDashEffect();
        }
        else
        {
            _dashTime-=Time.deltaTime;
        }

        //roll
        if(Input.GetKeyDown(KeyCode.R)&&rollTime<=0)
        {
            animator.SetBool("roll",true);
            move_Speech += Rollboost;
            rollTime = RollTime;
            rollone = true;

        }
        if (rollone == true && rollTime <= 0)
        {
            animator.SetBool("roll",false);
            move_Speech -= Rollboost;
            rollone = false;
        }
        else
        {
            rollTime -= Time.deltaTime;
        }
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                //CharacterRD.
                    transform.localScale = new Vector3(0.3f, 0.3f, 0);

            }
            else
            {
                //CharacterRD.
                    transform.localScale = new Vector3(-0.3f,0.3f,0);
            }
        }
    }
    void StopDashEffect()
    {
        if (dashEffectCoroutine != null)
        {
            StopCoroutine(dashEffectCoroutine);
        }
        
    }
    void StartDashEffect()
    {
        if (dashEffectCoroutine != null)
        {
            StopCoroutine(dashEffectCoroutine);
        }
        dashEffectCoroutine = StartCoroutine(DashEffectCoroutine());
    }
    IEnumerator DashEffectCoroutine()
    {
        while (true)
        {
            GameObject ghost =Instantiate(ghostEffect,transform.position,transform.rotation);
            Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
            ghost.GetComponent<SpriteRenderer>().sprite = currentSprite;
            Destroy(ghost,0.5f);
            yield return new WaitForSeconds(ghostDelaySecond);
        }
    }
    public PlayerHealth playerhealth ;
    public void TakeDamage(int damage)
    {
        playerhealth.TakeDam(damage);
    }
}
