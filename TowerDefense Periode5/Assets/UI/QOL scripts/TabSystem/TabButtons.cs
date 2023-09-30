using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//needed for the UnityEvents.
using UnityEngine.Events;

//A check to see if an image is part of the button (cuz the script will be on the button)
[RequireComponent(typeof(Image))]
//IPointer will make sure that unity will call upon this code whenever any of the IPointer interactions happen.
public class TabButtons : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image background;
    public Color c;
    public TMP_Text text;

    //These UntiyEvents are basically the same thing as the thing in buttons that makes you do whatever u want in there.
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    // Start is called before the first frame update
    void Awake()
    {
        //Grab component of imagine from the background then make it subscribe to the button list.
        background = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
        tabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }


    public void Select()
    {
        //Start / Invoke the EventSystem called onTabSelected
        if (onTabSelected != null)
        {
            onTabDeselected.Invoke();
        }
    }

    public void Deselect()
    {
        //Start / Invoke the EventSystem called onTabDeselected
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}