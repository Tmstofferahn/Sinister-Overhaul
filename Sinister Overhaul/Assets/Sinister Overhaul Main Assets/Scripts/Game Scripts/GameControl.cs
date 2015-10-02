﻿/*GameControl
 * 
 * GameControl will be set to public so any script can access these variables.
 * 
 * GameControl will control the game by keeping track of important variables.
 * These variables include player health and player lives.
 * 
 * GameControl will also be the main way of saving and loading.
 * 
 * GameControl will track score and highScore.
 * 
 *  
 * Place on Game Controller
 */


using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameControl : MonoBehaviour
{

    public static GameControl control;
    public float shieldWaitTime = 60.0f;
    private float shieldTimeRemaining = 0.0f;
    public bool shieldReady = true;
    public int initialHealth = 3;
    public int currentHealth = 0;
    public int initialLives = 3;
    public int currentLives = 0;
    public int score = 0;
    public int highScore = 0;
    private bool loading = false;
    public bool loadNextLevel = false;
    public bool loadMainMenu = false;
    public bool isPaused = false;
    public float difficultyFactor = 1.0f;
    public float masterVolume = 0.5f;
    public float musicVolume = 0.6f;
    public float masterSFXVolume = 0.5f;






    void Awake()
    {
        shieldTimeRemaining = shieldWaitTime;
        //load player preferences
        difficultyFactor = PlayerPrefs.GetFloat("Difficulty");
        masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        masterSFXVolume = PlayerPrefs.GetFloat("MasterSFXVolume");


        loading = false;
        loadMainMenu = false;
        loadNextLevel = false;

        Save();
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayerSetup();
        Load();
    }

    void OnLevelWasLoaded()
    {
        loading = false;
        loadNextLevel = false;
        loadMainMenu = false;
    }

    void Update()
    {

        if (score > highScore)
        {
            highScore = score;
        }
        if (currentHealth <= 0 && currentLives > 0)
        {
            StartCoroutine(RestartLevel());
        }
        if (currentLives == 0)
        {
            //StartCoroutine(ReturnToMenu());
        }


        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            loadNextLevel = true;
        }

    


    }
    public void Pause()
    {

        if (Application.loadedLevel != 0)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
               // Time.timeScale = 0;
            }
            else if (!isPaused)
            {
               // Time.timeScale = 1;
            }
        }

    }
    public void PlayerSetup()
    {
        currentHealth = initialHealth;
        currentLives = initialLives;
        score = 0;
    }

    IEnumerator RestartLevel()
    {
        if (loading == false)
        {
            loading = true;
            yield return new WaitForSeconds(5.0f);
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    IEnumerator ReturnToMenu()
    {
        if (loading == false)
        {
            loading = true;
            yield return new WaitForSeconds(5.0f);
            Application.LoadLevel(0);
        }
    }

    //	IEnumerator ReturnToMenu()
    //	{
    //		if (loading == false) 
    //		{
    //			loading = true;
    //			yield return new WaitForSeconds (5.0f);
    //			PlayerSetup ();
    //			Application.LoadLevel (0);
    //		}
    //	}

    public void ShieldTimer()
    {
        if (isPaused == false)
        {
            if (loadNextLevel == true)
            {
                shieldTimeRemaining = shieldWaitTime;
                shieldReady = true;
            }

            shieldTimeRemaining -= Time.deltaTime;

            if (shieldTimeRemaining <= 0)
            {
                shieldReady = true;
                shieldTimeRemaining = shieldWaitTime;

            }
        }


    }
	void OnGUI()
	{
		difficultyFactor = (float)Math.Round (difficultyFactor * 10.0f) / 10.0f;
        float tempShieldTime = (float)Math.Round(shieldTimeRemaining * 10.0f) / 10.0f;
        if (Application.loadedLevel > 0)
		{
			GUI.Label (new Rect (10, 10, 100, 30), "Health: " + currentHealth);
			GUI.Label (new Rect (10, 40, 100, 30), "Lives: " + currentLives);

			GUI.Label (new Rect (Screen.width - 100, 10, 100, 50), "Score: \n" + score);
			GUI.Label (new Rect (Screen.width / 2 - 50, 10, 100, 50), "Highscore: \n" + highScore);

            if (shieldReady == false)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, 40, 100, 50), "Time on Shield: \n" + tempShieldTime);
            }
            if (shieldReady == true)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, 40, 100, 50), "Time on Shield: READY");
            }





        }
		GUI.Label (new Rect (Screen.width - 100, 40, 100, 50), "Current Difficulty: \n x" + difficultyFactor);
		GUI.Label(new Rect (10, 70, 100, 30), "CurrentLevel: " + Application.loadedLevel);
		GUI.Label(new Rect (10, 100, 100, 30), "LevelCount: " + (Application.levelCount - 1));

	}
	public void Save()
	{



		Load (); //load up information first to see if it better or worse.
		//Since highScore is our only thing, load up what is in the save file. 
		//If the current highScore is lower than that of the one in the save, it will just
		//resave the data from the previous. If it is greater than it, it will place new values.


		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");


		PlayerData data = new PlayerData ();
		data.highScore = highScore;


		bf.Serialize(file, data);
		file.Close();



	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			if(data.highScore > highScore)
				highScore = data.highScore;
		}



	}



}

[Serializable]
class PlayerData
{
	public int highScore;
}
