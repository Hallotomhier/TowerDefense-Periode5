using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManagerMainMenu : MonoBehaviour
{
    private int nextSceneToLoad;
    private int artScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneToLoad);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadArtScene()
    {
        artScene = SceneManager.GetActiveScene().buildIndex + 2;
        SceneManager.LoadScene(artScene);
    }
}
