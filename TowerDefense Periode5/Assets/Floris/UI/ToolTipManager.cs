using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public GameObject toolTipUI;
    public TMP_Text toolTipText;
   
    public void ShowToolTip(string explanation, string cost)
    {
        toolTipText.text = "Explanation :"  + explanation + "Cost" + cost;
        toolTipUI.SetActive(true);
    }
    public void HideToolTip()
    {
        toolTipUI.SetActive(false);
    }
}
