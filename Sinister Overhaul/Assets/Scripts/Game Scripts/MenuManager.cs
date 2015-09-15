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

    Resolution[] resolutions;

    [SerializeField]
    Transform resolutionPanel;

    [SerializeField]
    GameObject resolutionButton;


    public Dropdown resolutionDropdown;

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
        ShowMenu(CurrentMenu);

        difficulty = GameObject.Find("Difficulty Slider").GetComponent<Slider>();
        masterVolume = GameObject.Find("Master Volume Slider").GetComponent<Slider>();
        difficulty.value = GameControl.control.difficultyFactor;
        masterVolume.value = GameControl.control.masterVolume;

        resolutions = Screen.resolutions;
        resolutionDropdown.options.Clear();
        

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
            //resolutionDropdown.options[i].text = ResToString(resolutions[i]);
            //resolutionDropdown.value = i;    
        }

        resolutionDropdown.onValueChanged.AddListener
            ( delegate {
               Screen.SetResolution(resolutions[resolutionDropdown.value].width,
               resolutions[resolutionDropdown.value].height, true);
        });
        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    GameObject button = (GameObject)Instantiate(resolutionButton);
        //    button.GetComponentInChildren<Text>().text = ResToString(resolutions[i]);
        //    int index = i;
        //    button.GetComponent<Button>().onClick.AddListener(
        //        () => { SetResolution(index); }
        //        );
        //    button.transform.SetParent(resolutionPanel);
        //    button.transform.localScale = new Vector3(1, 1, 1);
        //}

    }


    void Update()
    {

        if (Application.loadedLevel != 0)
        {
            if (GameControl.control != null)
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
        else if (Application.loadedLevel != 0)
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
        Application.Quit();
    }
    public void Resume()
    {
        GameControl.control.Pause();
    }
    public void ReturnToMenu()
    {
        GameControl.control.Save();
        GameControl.control.PlayerSetup();
        GameControl.control.Pause();
        GameControl.control.loadMainMenu = true;
        GameControl.control.loadNextLevel = true;
    }

    //-------------------------------------------------
    // Game Settings sliders
    public void SetDifficulty(float sliderDifficulty)
    {
        if (GameControl.control != null)
        {
            GameControl.control.difficultyFactor = sliderDifficulty;
        }
        //save preference for later
        PlayerPrefs.SetFloat("Difficulty", sliderDifficulty);
        PlayerPrefs.Save();


    }
    //---------------------------------------------------------------------------
    //Video calls
    void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, false, 0);
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
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
