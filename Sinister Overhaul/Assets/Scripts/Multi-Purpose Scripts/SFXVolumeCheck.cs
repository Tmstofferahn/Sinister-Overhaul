using UnityEngine;
using System.Collections;

public class SFXVolumeCheck : MonoBehaviour {

    public AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();

        source.volume = GameControl.control.masterSFXVolume * GameControl.control.masterVolume;
	
	}
	
}
