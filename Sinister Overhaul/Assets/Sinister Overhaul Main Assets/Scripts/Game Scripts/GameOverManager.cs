using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
	public float restartDelay = 5f;         // Time to wait before restarting the level
	
	
	Animator anim;                          // Reference to the animator component.
	float restartTimer;                     // Timer to count up to restarting the level
	
	
	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}
	

	void Update ()
	{
		// If the player has run out of health...
		if(GameControl.control.currentLives == 0)
		{
			// ... tell the animator the game is over.
			anim.SetBool ("GameOver", true);
			
			// .. increment a timer to count up to restarting.
			restartTimer += Time.deltaTime;
			
			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				anim.SetBool ("GameOver", false);
                // .. then reload the currently loaded level.
                GameControl.control.PlayerSetup();
				Application.LoadLevel(0);
			}
		}
	}
}