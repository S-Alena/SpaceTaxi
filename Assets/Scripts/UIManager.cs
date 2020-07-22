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
    public GameObject highScores;


    void Start()
    {
        Button btn = StartGame.GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
        
        Button btn0 = QuitGame.GetComponent<Button>();
        btn0.onClick.AddListener(Quit);

        Text[] highScoreTexts = highScores.GetComponentsInChildren<Text>();

        foreach (Text highScore in highScoreTexts)
        {
            float score = PlayerPrefs.GetFloat(highScore.name, 0);
            highScore.text = "Highscore: " + score;
        }

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
