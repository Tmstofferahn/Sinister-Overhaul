using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public static MenuManager guiControl;


    public AudioSource audio;
    public AudioClip clickSound;
    public Canvas canvas;
    public Menu mainMenu;
    public Menu pauseMenu;
    private Menu CurrentMenu;
    
    private float oldTimeScale;

    public GameObject FPS;
    private bool cameraLoaded = false;
    public GameObject HUD;

    //-------------------------------------------
    //Game Settings Variables
    private Slider difficulty;
    private Text difficultyText;

    //-------------------------------------------
    //Audio Settings Variables
    private Slider masterVolume;
    private Slider musicVolume;
    private Slider masterSFXVolume;



    //-------------------------------------------
    //Video Settings Variables
    Resolution[] resolutions;
    [SerializeField] Transform resolutionPanel;
    [SerializeField] GameObject resolutionButton;
    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;

    void OnLevelWasLoaded()
    {
        cameraLoaded = false;
        if (Application.loadedLevel != 0)
        {
            ToggleHUD(true);
        }
        if (Application.loadedLevel == 0)
        {
            ToggleHUD(false);
        }
    }
    void Awake()
    {


        if (guiControl == null)
        {
            guiControl = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        

    }

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
        ShowMenuNoClick(CurrentMenu);

        
        difficulty = GameObject.Find("Difficulty Slider").GetComponent<Slider>();
        difficultyText = difficulty.GetComponentInChildren<Text>();
        masterVolume = GameObject.Find("Master Volume Slider").GetComponent<Slider>();
        musicVolume = GameObject.Find("Music Volume Slider").GetComponent<Slider>();
        masterSFXVolume = GameObject.Find("Master SFX Volume Slider").GetComponent<Slider>();
        difficulty.value = GameControl.control.difficultyFactor;
        masterVolume.value = GameControl.control.masterVolume;
        musicVolume.value = GameControl.control.musicVolume;
        masterSFXVolume.value = GameControl.control.masterSFXVolume;


        resolutions = Screen.resolutions;
        resolutionDropdown.options.Clear();
        fullScreenToggle.isOn = Screen.fullScreen;

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
        }


        resolutionDropdown.onValueChanged.AddListener
            (delegate {
                oldTimeScale = Time.timeScale;
                Screen.SetResolution(resolutions[resolutionDropdown.value].width,
                resolutions[resolutionDropdown.value].height, Screen.fullScreen);
                Time.timeScale = 1.0f;
                StartCoroutine(RefreshScreen());

            });

        resolutionDropdown.value = resolutions.Length - 1;
        

    }



    void Update()
    {
        SetCamera();
        string format = System.String.Format("Difficulty:\n x" + GameControl.control.difficultyFactor.ToString("F1"));
        difficultyText.text = format;

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
        if (Application.loadedLevel == 0)
        {
            if(CurrentMenu == pauseMenu)
            {
                CurrentMenu = mainMenu;
                CurrentMenu.IsOpen = true;
            }
            
        }


    }



    //-----------------------------------------------------------------------------
    //Menu calls
    IEnumerator RefreshScreen()
    {
        
        yield return new WaitForEndOfFrame();
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = oldTimeScale;

    }
    void ToggleHUD(bool isActive)
    {
       
        HUD.SetActive(isActive);
    }
    void SetCamera()
    {
        if(cameraLoaded == false)
        {
            canvas.worldCamera = Camera.main;
            cameraLoaded = true;
        }

    }
    public void ToggleFPS()
    {
        FPS.SetActive(!FPS.activeSelf);
    }

    public void ReturnToInitial()
    {
        if (GameControl.control.loadNextLevel == true)
        {
            return;
        }
        audio.PlayOneShot(clickSound);
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
    public void ShowMenuNoClick(Menu menu)
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.IsOpen = false;
        }
        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
    }
    public void ShowMenu(Menu menu)
    {
        if(GameControl.control.loadNextLevel == true)
        {
            return;
        }
        audio.PlayOneShot(clickSound);
        if (CurrentMenu != null)
        {
            CurrentMenu.IsOpen = false;
        }
        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
    }

    public void PlayGame()
    {
        if (GameControl.control.loadNextLevel == true)
        {
            return;
        }
        audio.PlayOneShot(clickSound);
        GameControl.control.PlayerSetup();
        GameControl.control.loadNextLevel = true;
    }
    public void Quit()
    {
        if (GameControl.control.loadNextLevel == true)
        {
            return;
        }
        audio.PlayOneShot(clickSound);
        GameControl.control.Save();
        Application.Quit();
    }
    public void Resume()
    {
        if (GameControl.control.loadNextLevel == true)
        {
            return;
        }
        audio.PlayOneShot(clickSound);
        GameControl.control.Pause();
    }
    public void ReturnToMenu()
    {
        if (GameControl.control.loadNextLevel == true)
        {
            return;
        }
        audio.PlayOneShot(clickSound);
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

    public void ToggleFullScreen (bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
    //---------------------------------------------------------------------------
    //Audio calls
    public void SetMasterVolume (float sliderMasterVolume)
	{
        if (GameControl.control != null)
        {
            GameControl.control.masterVolume = sliderMasterVolume;
            MusicSliderUpdate(GameControl.control.masterVolume * GameControl.control.musicVolume);
        }
		PlayerPrefs.SetFloat ("MasterVolume", sliderMasterVolume); //save preference
		PlayerPrefs.Save ();

	}
    public void SetMusicVolume(float sliderMusicVolume)
    {
        if (GameControl.control != null)
        {
            GameControl.control.musicVolume = sliderMusicVolume;
            MusicSliderUpdate(GameControl.control.masterVolume * GameControl.control.musicVolume);

        }
        PlayerPrefs.SetFloat("MusicVolume", sliderMusicVolume); //save preference
        PlayerPrefs.Save();

    }
    public void SetMasterSFXVolume(float sliderMasterSFXVolume)
    {
        if (GameControl.control != null)
        {
            GameControl.control.masterSFXVolume = sliderMasterSFXVolume;

        }
        PlayerPrefs.SetFloat("MasterSFXVolume", sliderMasterSFXVolume); //save preference
        PlayerPrefs.Save();

    }

    public void MusicSliderUpdate(float val)
	{
		MusicManager.musicControl.SetVolume (val);
	}
	public void MusicToggle(bool val)
	{
		masterVolume.interactable = val;
        MusicManager.musicControl.SetVolume (val ? masterVolume.value : 0f);
	}






}
