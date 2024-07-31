using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy_AI : MonoBehaviour
{ 
    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    bool reachDestination = false;
    public float moveSpeed;
    public float nextWPDistance;
    public bool upadteContinuosPath;
    //Shoot
    public bool isShootable = false;
    public GameObject bullet;
    public float bullet_speech;
    public float timeBtwFire;
    public float fireCooldown;
    public AudioSource aus;
    public AudioClip bullet_boss_sound;
    public AudioClip bullet_enemy_sound;
    private void Start()
    {
        InvokeRepeating("CaculatePath", 0f, 0.2f);
        reachDestination = true;
    }
    private void Update()
    {
        fireCooldown-= Time.deltaTime;
        if( fireCooldown < 0 ) {
            fireCooldown = timeBtwFire;
            //Shoot
            EnemyFireBullet();
            if(aus&&bullet_boss_sound !=null)
            {
                aus.PlayOneShot(bullet_boss_sound);
            }
            else if (aus&&bullet_enemy_sound!=null)
            {
                aus.PlayOneShot(bullet_enemy_sound);
            }

        }
    }
    void EnemyFireBullet()
    {
        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb=bulletTmp.GetComponent<Rigidbody2D>();
        Vector3 playerpos = FindObjectOfType<Player>().transform.position;
        Vector3 direction=playerpos - transform.position;
        rb.AddForce(direction.normalized*bullet_speech,ForceMode2D.Impulse);
    }
    void CaculatePath()
    {
        Vector2 target = FileTarget();
        if (seeker.IsDone()&&(reachDestination||upadteContinuosPath))
        {
            seeker.StartPath(transform.position, target,OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if(p.error)
        {
            return;
        }
        path = p;
        MoveToTarget();

    }
    void MoveToTarget()
    {
        if (moveCoroutine!=null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToTargetCorotine());
    }
    IEnumerator MoveToTargetCorotine()
    {
        int currentWP = 0;
        reachDestination = false;
        while (currentWP < path.vectorPath.Count) 
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP]-(Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;
            float distance=Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if(distance <nextWPDistance)
            {
                currentWP++;
            }
            yield return null;
            
        }
        reachDestination = true;
    }
    Vector2 FileTarget()
    {
        Renderer playerRenderer = FindObjectOfType<Player>().GetComponent<Renderer>();
        Vector3 playerpos = playerRenderer.bounds.center;
        playerpos.y -= 2f;
        return (Vector2)playerpos+(Random.Range(10f,50f)*new Vector2(Random.Range(-1,1),Random.Range(-1,1)).normalized);
        

    }
}
