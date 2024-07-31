using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public GameWinnerScreen gameWinnerScreen;
    public int gamepoint=0;
    public AudioSource aus;
    public AudioClip winner_sound;
    public AudioClip lose_sound;
    public void GameOver()
    {
        gameOverScreen.SetUp(gamepoint);
        if (aus && lose_sound != null)
        {
            aus.PlayOneShot(lose_sound);
        }
    }
    public void GameWinner()
    {   
        gameWinnerScreen.SetUp(gamepoint);
        if (aus && winner_sound != null)
        {
            aus.PlayOneShot(winner_sound);
        }

    }
    private void Update()
    {
        gamepoint=FindObjectOfType<Killed>().currentKilled;
        if(GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            GameOver();
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            GameWinner();
        }
        

    }
}
