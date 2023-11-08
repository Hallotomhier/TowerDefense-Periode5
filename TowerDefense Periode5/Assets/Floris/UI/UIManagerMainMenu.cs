using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManagerMainMenu : MonoBehaviour
{
    private int nextSceneToLoad;
    private int artScene;
    public GameObject pickMap;
    public GameObject panelMainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        pickMap.SetActive(true);
        panelMainMenu.SetActive(false) ;
    }
    public void BackOutMapSelect()
    {
        pickMap.SetActive(false);
        panelMainMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void GoToMapOne()
    {
        SceneManager.LoadScene("Map 1");
    }
    public void GoToMapTwo()
    {
        SceneManager.LoadScene("Map 2");
    }
}
