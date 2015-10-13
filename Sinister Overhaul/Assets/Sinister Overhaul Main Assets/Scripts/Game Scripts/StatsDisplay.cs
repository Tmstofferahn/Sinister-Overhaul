using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsDisplay : MonoBehaviour {

    private Image shieldProgress;
    private bool flashing = true;
    private Text HighScoreText;
    private Text CurrentScoreText;
    private Text DifficultyText;
    private Text HealthText;
    private Text LivesText;
    private Text LevelText;
    private Image powerProgress;
    private Text powerText;

    // Use this for initialization
    void Start()
    {
        Setup();
    }


    // Update is called once per frame
    void Update()
    {
        ShieldProgress();
        HighScoreDisplay();
        ScoreDisplay();
        DifficultyDisplay();
        HealthDisplay();
        LivesDisplay();
        LevelDisplay();
        PowerDisplay();

    }

    //----------------------------------------------------------------------------
    //setup
    void Setup()
    {
        if(transform.FindInChildren("Shield Progress"))
        {
            shieldProgress = transform.FindInChildren("Shield Progress").GetComponent<Image>();
            StartCoroutine(FlashColors());
        }
        if (transform.FindInChildren("HighScore"))
        {
            HighScoreText = transform.FindInChildren("HighScore").GetComponent<Text>();
        }
        if (transform.FindInChildren("CurrentScore"))
        {
            CurrentScoreText = transform.FindInChildren("CurrentScore").GetComponent<Text>();
        }
        if (transform.FindInChildren("Difficulty"))
        {
            DifficultyText = transform.FindInChildren("Difficulty").GetComponent<Text>();
        }
        if (transform.FindInChildren("Health"))
        {
            HealthText = transform.FindInChildren("Health").GetComponent<Text>();
        }
        if (transform.FindInChildren("Lives"))
        {
            LivesText = transform.FindInChildren("Lives").GetComponent<Text>();
        }
        if (transform.FindInChildren("Level"))
        {
            LevelText = transform.FindInChildren("Level").GetComponent<Text>();
        }
        if (transform.FindInChildren("Power Progress"))
        {
            powerProgress = transform.FindInChildren("Power Progress").GetComponent<Image>();
        }
        if (transform.FindInChildren("Power Text"))
        {
            powerText = transform.FindInChildren("Power Text").GetComponent<Text>();
        }

    }
    //----------------------------------------------------------------------------
    //Shield Progress

    void ShieldProgress()
    {
        if (GameControl.control.shieldReady == false)
        {
            flashing = false;
            if (shieldProgress.color != Color.white)
            {
                shieldProgress.color = Color.white;
            }

            if ((GameControl.control.shieldTimeRemaining / GameControl.control.shieldTimeAlive) <= 0)
            {
                shieldProgress.fillAmount = GameControl.control.shieldEnergyCurrent / GameControl.control.shieldEnergyFull;
            }
            else if ((GameControl.control.shieldTimeRemaining / GameControl.control.shieldTimeAlive) > 0)
            {
                shieldProgress.fillAmount = GameControl.control.shieldTimeRemaining / GameControl.control.shieldTimeAlive;
            }

        }
        if (GameControl.control.shieldReady == true)
        {
            flashing = true;
        }
    }
    IEnumerator FlashColors()
    {
        while (true)
        {
            if (flashing == true)
            {
                if (shieldProgress.color == Color.white | shieldProgress.color == Color.red)
                {
                    shieldProgress.color = Color.green;
                }
                else if (shieldProgress.color == Color.green)
                {
                    shieldProgress.color = Color.red;
                }


            }

            yield return new WaitForSeconds(0.5f);
        }

    }
    //----------------------------------------------------------------------------
    //HighScore Display
    void HighScoreDisplay()
    {
        if (HighScoreText != null)
        {
            string format = System.String.Format("Highscore:\n" + GameControl.control.highScore);
            HighScoreText.text = format;
        }
    }
    //----------------------------------------------------------------------------
    //Score Display
    void ScoreDisplay()
    {
        if (CurrentScoreText != null)
        {
            string format = System.String.Format("Current Score:\n" + GameControl.control.score);
            CurrentScoreText.text = format;
        }
    }

    //----------------------------------------------------------------------------
    //Health Display
    void HealthDisplay()
    {
        if (HealthText != null)
        {
            string format = System.String.Format("Health:\n" + GameControl.control.currentHealth);
            HealthText.text = format;
        }
    }
    //----------------------------------------------------------------------------
    //Life Display
    void LivesDisplay()
    {
        if (LivesText != null)
        {
            string format = System.String.Format("Lives:\n" + GameControl.control.currentLives);
            LivesText.text = format;
        }
    }
    //----------------------------------------------------------------------------
    //Difficulty Display
    void DifficultyDisplay()
    {
        if(DifficultyText != null)
        {
            string format = System.String.Format("Difficulty:\n x" + GameControl.control.difficultyFactor);
            DifficultyText.text = format;
        }
    }
    //----------------------------------------------------------------------------
    //Level Display
    void LevelDisplay()
    {
        if (LevelText != null)
        {
            string format = System.String.Format("Level:\n" + Application.loadedLevel);
            LevelText.text = format;
        }
    }
    //Level Display
    void PowerDisplay()
    {
        if (powerProgress != null)
        {
            powerProgress.fillAmount = Mathf.Lerp(powerProgress.fillAmount, ((float)GameControl.control.playerUpgradeLevel / (float)GameControl.control.playerUpgradeLevelMax), Time.deltaTime * 2);
        }
        if (powerText != null)
        {
            if(GameControl.control.playerUpgradeLevel < GameControl.control.playerUpgradeLevelMax)
            {
                string format = System.String.Format("Power:\n" + GameControl.control.playerUpgradeLevel);
                powerText.text = format;
            }
            else if(GameControl.control.playerUpgradeLevel >= GameControl.control.playerUpgradeLevelMax)
            {
                string format = System.String.Format("Power:\n MAX!");
                powerText.text = format;
            }
        }


    }
}