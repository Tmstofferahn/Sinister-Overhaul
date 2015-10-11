using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    private Text HighScoreText;
    private Text CurrentScoreText;

	// Use this for initialization
	void Start ()
    {
       if( transform.FindChild("HighScore"))
        {
            HighScoreText = transform.FindChild("HighScore").GetComponent<Text>();
        }
        if (transform.FindChild("CurrentScore"))
        {
            CurrentScoreText = transform.FindChild("CurrentScore").GetComponent<Text>();
        }
    }
    void Awake()
    {
        if (transform.FindChild("HighScore"))
        {
            HighScoreText = transform.FindChild("HighScore").GetComponent<Text>();
        }
        if (transform.FindChild("CurrentScore"))
        {
            CurrentScoreText = transform.FindChild("CurrentScore").GetComponent<Text>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(HighScoreText != null)
        {
            string format1 = System.String.Format("Highscore: \n" + GameControl.control.highScore);
            HighScoreText.text = format1;
        }
        if(CurrentScoreText != null)
        {
            string format2 = System.String.Format("Current Score: \n" + GameControl.control.score);
            CurrentScoreText.text = format2;
        }

    }
}
