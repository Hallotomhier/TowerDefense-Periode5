using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Recources : MonoBehaviour
{
    public int stone;
    public int wood;

    public TMP_Text stoneText;
    public TMP_Text woodText;

    // Update is called once per frame
    void Update()
    {
        stoneText.text = "Stone :" + stone.ToString();
        woodText.text ="Wood :" + wood.ToString();
    }
}
