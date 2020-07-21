using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public AudioSource teleportSFX;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onGameOver += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        teleportSFX.Play();
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
