using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_no_gun : MonoBehaviour
{
    public bool roaming = true;
    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    bool reachDestination = false;
    public float moveSpeed;
    public float nextWPDistance;
    public bool upadteContinuosPath;

    private void Start()
    {
        InvokeRepeating("CaculatePath", 0f, 0.2f);
        reachDestination = true;
    }
    private void Update()
    {
    }
    void CaculatePath()
    {
        Vector2 target = FileTarget();
        if (seeker.IsDone() && (reachDestination || upadteContinuosPath))
        {
            seeker.StartPath(transform.position, target, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (p.error)
        {
            return;
        }
        path = p;
        MoveToTarget();

    }
    void MoveToTarget()
    {
        if (moveCoroutine != null)
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
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
            {
                currentWP++;
            }
            yield return null;

        }
        reachDestination = true;
    }
    Vector2 FileTarget()
    {
        //Vector3 playerpos = FindObjectOfType<Player>().transform.position;
        Renderer playerRenderer = FindObjectOfType<Player>().GetComponent<Renderer>();
        //Vector3 playerCenter = playerRenderer.bounds.center;

        Vector3 playerpos = playerRenderer.bounds.center;
        playerpos.y -= 2f;

        if (roaming == true)
        {
            return (Vector2)playerpos + (Random.Range(10f, 50f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
        }
        else
        {
            return (Vector2)playerpos;
        }
    }
    }
