using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabGroup : MonoBehaviour
{
    public List<TabButtons> tabButtons;

    public Sprite tabIdle_sprite;
    public Sprite tabHover_sprite;
    public Sprite tabActive_sprite;

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;

    public Color textIdle;
    public Color textHover;
    public Color textActive;

    public TabButtons selectedTab;

    public List<GameObject> objectsToSwap;

    public void Start()
    {
        selectedTab = tabButtons[0];

        selectedTab.background.color = tabActive;
        selectedTab.text.color = textActive;
    }

    //Void that makes buttons go on the buttons-list.
    public void Subscribe(TabButtons button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButtons>();
        }

        tabButtons.Add(button);
    }

    //Changes sprite that is hovered over to tabHover.
    public void OnTabEnter(TabButtons button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab) //only change the sprite if that specific sprite isnt the selected sprite.
        {
            button.background.color = tabHover;
            button.text.color = textHover;
        }

    }
    public void OnTabExit(TabButtons button)
    {
        ResetTabs();
    }
    //Changes sprite that is selected to tabActive.
    public void OnTabSelected(TabButtons button)
    {
        //Deselects the selected tab if there was already a tab selected.
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        //Makes sure the selected tab does not reset to the tabIdle sprite.
        selectedTab = button;

        selectedTab.Select();

        ResetTabs();

        button.background.color = tabActive;
        button.text.color = textActive;

        //Make sure the tab that is selected will also show the page that corresponds with that tab.
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    //Set every tab to idle (tabIdle) so this basically changes all the sprites to tabIdle.
    public void ResetTabs()
    {
        foreach (TabButtons button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) //makes sure that it doesnt reset our selected tab back to tabIdle.
            {
                continue;
            }
            button.background.color = tabIdle;
            button.text.color = textIdle;
        }
    }
}