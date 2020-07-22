using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update


    private Button nextLevelButton;
    public string nextLevel = "";

    void Start()
    {
        GameEvents.current.onWin += NextButton;

        Button[] menuButtons = this.GetComponentsInChildren<Button>();

        foreach (Button btn in menuButtons)
        {
            if (btn.name == "Restart")
            {
                btn.onClick.AddListener(Restart);
            }
            else if (btn.name == "Menu")
            {
                btn.onClick.AddListener(MainMenu);
            }
            else if (btn.name == "Next Level")
            {
                nextLevelButton = btn;
                nextLevelButton.onClick.AddListener(NextLevel);
                nextLevelButton.gameObject.SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Restart()
    {
        GameEvents.current.RestartGame();
        PassengerCount.redPassengerCount = 0;
        PassengerCount.pinkPassengerCount = 0;
        PassengerCount.yellowPassengerCount = 0;
        PassengerCount.bluePassengerCount = 0;
        PassengerCount.transported = 0;
    }

    public void MainMenu()
    {
        PassengerCount.redPassengerCount = 0;
        PassengerCount.pinkPassengerCount = 0;
        PassengerCount.yellowPassengerCount = 0;
        PassengerCount.bluePassengerCount = 0;
        PassengerCount.transported = 0;
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        PassengerCount.redPassengerCount = 0;
        PassengerCount.pinkPassengerCount = 0;
        PassengerCount.yellowPassengerCount = 0;
        PassengerCount.bluePassengerCount = 0;
        PassengerCount.transported = 0;
        if (nextLevel.Length > 0)
        {
            SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
        }
    }

    public void NextButton()
    {
        if(nextLevel.Length > 0)
        {
            nextLevelButton.gameObject.SetActive(true);
        }
    }
}
