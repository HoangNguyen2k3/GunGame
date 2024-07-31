using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameWinnerScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public void SetUp(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }
}
