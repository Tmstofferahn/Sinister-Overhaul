using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Menu mainMenu;
    public Menu pauseMenu;
	private Menu CurrentMenu;

	private Slider difficulty;
	private Slider masterVolume;
	public MusicManager MM;


	public void Start()
	{
		if (CurrentMenu == null)
		{
            if (Application.loadedLevel == 0)
            {
                CurrentMenu = mainMenu;
            }
            else if (Application.loadedLevel != 0)
            {
                CurrentMenu = pauseMenu;
            }
		}
		ShowMenu (CurrentMenu);

		difficulty = GameObject.Find ("Difficulty Slider").GetComponent<Slider>();
		masterVolume = GameObject.Find ("Master Volume Slider").GetComponent<Slider>();
		difficulty.value = GameControl.control.difficultyFactor;
		masterVolume.value = GameControl.control.masterVolume;

	}


	void Update()
	{

		if (Application.loadedLevel != 0) 
		{
            if(GameControl.control != null)
            {
                if (GameControl.control.isPaused)
                {
                    CurrentMenu.IsOpen = true;
                }
                else if (!GameControl.control.isPaused)
                {
                    CurrentMenu.IsOpen = false;
                    CurrentMenu = pauseMenu;
                }
            }

        }
	}



	//-----------------------------------------------------------------------------
	//Menu calls
	public void ReturnToInitial()
	{
		CurrentMenu.IsOpen = false;
        if (Application.loadedLevel == 0)
        {
            CurrentMenu = mainMenu;
        }
        else if(Application.loadedLevel != 0)
        {
            CurrentMenu = pauseMenu;
        }
		
		CurrentMenu.IsOpen = true;
	}
	public void ShowMenu(Menu menu)
	{
		if (CurrentMenu != null) 
		{
			CurrentMenu.IsOpen = false;
		}
		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}

	public void PlayGame()
	{
		GameControl.control.PlayerSetup();
		GameControl.control.loadNextLevel = true;
	}
	public void Quit()
	{
		GameControl.control.Save();
		Application.Quit ();
	}
	public void Resume()
	{
		GameControl.control.Pause();
	}
	public void ReturnToMenu()
	{
		GameControl.control.Save();
		GameControl.control.PlayerSetup ();
		GameControl.control.Pause();
		GameControl.control.loadMainMenu = true;
		GameControl.control.loadNextLevel = true;
	}

	//-------------------------------------------------
	// Game Settings sliders
	public void SetDifficulty (float sliderDifficulty)
	{
        if (GameControl.control != null)
        {
            GameControl.control.difficultyFactor = sliderDifficulty;
        }
		//save preference for later
		PlayerPrefs.SetFloat ("Difficulty", sliderDifficulty);
		PlayerPrefs.Save ();


	}

	//---------------------------------------------------------------------------
	//Audio calls
	public void SetMasterVolume (float sliderMasterVolume)
	{
        if (GameControl.control != null)
        {
            GameControl.control.masterVolume = sliderMasterVolume;
            MusicSliderUpdate(GameControl.control.masterVolume);
        }
		PlayerPrefs.SetFloat ("MasterVolume", sliderMasterVolume); //save preference
		PlayerPrefs.Save ();

	}

	public void MusicSliderUpdate(float val)
	{
		MM.SetVolume (val);
	}
	public void MusicToggle(bool val)
	{
		masterVolume.interactable = val;
		MM.SetVolume (val ? masterVolume.value : 0f);
	}






}
