using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	public static LoadingScreen ls;
	public GameObject background;
	public GameObject text;
    //AsyncOperation async = null; use this if you want to find loading progress

    // Use this for initialization
    void Start ()
    {
		GameControl.control.loadNextLevel = false;
		GameControl.control.loadMainMenu = false;
		background.SetActive (false);
		text.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (GameControl.control.loadNextLevel) 
		{
			StartCoroutine(DisplayLoadingScreen());
		}
	}
	

	IEnumerator DisplayLoadingScreen()
	{
		background.SetActive (true);
		text.SetActive (true);
        //reloads current level, in case nothing else can load.
        
        yield return new WaitForSeconds (5);
		if (GameControl.control.loadMainMenu == true) 
		{
			Application.LoadLevelAsync(0); //loads main menu
		}
		else if (Application.loadedLevel < Application.levelCount -1) 
		{
			Application.LoadLevelAsync(Application.loadedLevel + 1); //loads next level
		}
		else if(Application.loadedLevel >= Application.levelCount -1)
		{
			Application.LoadLevelAsync(0); //loads main menu
		}

        yield return null;



	}
}
