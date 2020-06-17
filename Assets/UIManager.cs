using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button StartGame;
    public Button QuitGame;
    public string level;

    void Start()
    {
        Button btn = StartGame.GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
        
        Button btn2 = QuitGame.GetComponent<Button>();
        btn2.onClick.AddListener(Quit);
    }
     
    public void LoadLevel()
    {
        SceneManager.LoadScene(level,LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
