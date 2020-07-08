﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button StartGame;
    public string level;

    void Start()
    {
        Button btn = StartGame.GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
        
        }
     
    public void LoadLevel()
    {
        SceneManager.LoadScene(level,LoadSceneMode.Single);
    }

  

}

