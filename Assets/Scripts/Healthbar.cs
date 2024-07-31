using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image fillbar;
    public TextMeshProUGUI valueText;
    public void updatebar(int currentvalue,int maxvalue)
    {
        fillbar.fillAmount = (float)currentvalue/(float)maxvalue;
        valueText.text=currentvalue.ToString()+" / "+maxvalue.ToString();
    }

}
