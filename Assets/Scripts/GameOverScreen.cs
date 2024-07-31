using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public void SetUp(int score)
    {
        gameObject.SetActive(true);
        pointsText.text= score.ToString()+" POINTS";
    }

}
