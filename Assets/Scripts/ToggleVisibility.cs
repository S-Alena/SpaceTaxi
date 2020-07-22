using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject showObj;
    public GameObject noShow1;
    public GameObject noShow2;

    public GameObject highScores;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(Toggle);



        Text[] highScoreTexts = highScores.GetComponentsInChildren<Text>();

        foreach (Text highScore in highScoreTexts)
        {
            float score = PlayerPrefs.GetFloat(highScore.name, 0);
            highScore.text = "Highscore: " + score;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Toggle()
        {
            showObj.SetActive(true);
            noShow1.SetActive(false);
            noShow2.SetActive(false);
        }
}
