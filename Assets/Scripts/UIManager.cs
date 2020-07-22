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
    public AudioSource beamSFX;
    public AudioSource teleportSFX;


    void Start()
    {
        Button btn = StartGame.GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
        
        Button btn0 = QuitGame.GetComponent<Button>();
        btn0.onClick.AddListener(Quit);

    }
     
    public void LoadLevel()
    {
        teleportSFX.Play();
        SceneManager.LoadScene(level,LoadSceneMode.Single);
    }

    public void Quit()
    {
        beamSFX.Play();
        Application.Quit();
    }

}
