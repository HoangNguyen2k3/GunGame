using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPlayerPositionForEnemy : MonoBehaviour
{
    private GameObject player;
    bool facing_left = true;
    private void Start()
    {
        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObject != null)
        {
            player = PlayerObject;
        }
    }
    private void Update()
    {
        if (player != null)
        {
            Vector3 playerposition=player.transform.position;
            Vector3 enemyposition=transform.position;
            if (playerposition.x  < enemyposition.x && facing_left)
            {
                Flip();
            }
            else if (playerposition.x > enemyposition.x && !facing_left)
            {
                Flip();
            }
        }
        
    }
    void Flip()
    {       
        facing_left=!facing_left;
        transform.Rotate(0, 180, 0);
    }

}
