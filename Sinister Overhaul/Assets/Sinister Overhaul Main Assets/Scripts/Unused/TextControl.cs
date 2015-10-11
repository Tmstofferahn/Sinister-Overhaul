

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public enum Menus
{
	NULL = 0, PlayGame = 1, Options = 2, Quit = 3, Resume = 4, ReturnToMenu = 5
};


public class TextControl : MonoBehaviour
{


	public Menus menuSelect;
	public Renderer rend;
	// Use this for initialization
	void Start ()
	{
		rend = GetComponent < Renderer > ();

	}

	void OnMouseEnter()
	{
		rend.material.color = Color.red;
	}

	void OnMouseExit()
	{
		rend.material.color = Color.white;
	}
	
	void OnMouseUp()
	{
		switch (menuSelect) 
		{
		case Menus.NULL:
		{
			break;
		}
		case Menus.PlayGame:
		{
			GameControl.control.PlayerSetup();
			GameControl.control.loadNextLevel = true;
			break;
		}
		case Menus.Options:
		{
			break;
		}
		case Menus.Quit:
		{
			GameControl.control.Save();
			Application.Quit ();
			break;
		}
		case Menus.Resume:
		{
			GameControl.control.Pause();
			break;
		}
		case Menus.ReturnToMenu:
		{
			GameControl.control.PlayerSetup ();
			Application.LoadLevel (0);
			break;
		}
		default:
		{
			break;
		}
		}
	}
	// Update is called once per frame
	void Update ()
	{
		
	}
}

