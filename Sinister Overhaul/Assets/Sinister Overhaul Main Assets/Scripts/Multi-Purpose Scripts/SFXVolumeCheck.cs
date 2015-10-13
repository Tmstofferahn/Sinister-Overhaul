using UnityEngine;
using System.Collections;

public class SFXVolumeCheck : MonoBehaviour {

    public AudioSource source;
    public float volumeMultiplier = 1.0f;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();

        source.volume = GameControl.control.masterSFXVolume * GameControl.control.masterVolume * volumeMultiplier;
	
	}
    void OnGUI()
    {
        source.volume = GameControl.control.masterSFXVolume * GameControl.control.masterVolume * volumeMultiplier;
    }
	
}
