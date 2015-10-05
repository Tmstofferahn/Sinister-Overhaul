/*PlayerShootSineWave
 * 
 * DO NOT USE
 * 
 * PlayerShootSineWave is used to make the bullets look like they are moving
 * in a wave like motion.
 * 
 * This is applied to the barrel shooting the bullets and will cause the initial position
 * of the bullets to move back and forth along a wave. 
 * 
 * Works like the standard PlayerShoot except with added sine wave movement.
 *
 * 
 * 
 * DO NOT USE
 * 
 * Place on Parent Player Barrel objects.
 */

using UnityEngine;
using System.Collections;

public class PlayerBarrelSineWave : MonoBehaviour 
{
    private PlayerController playerController;	//create instance of playerController script
    public GameObject player;
    public GameObject barrelSprite;
    public GameObject shotControl;
    public float spriteDistanceFromBullets = -0.3f;

	public float frequency = 20.0f; 	//how fast it moves back and forth (look up sine waves)... use negative value for a mirrored effect.
	public float amplitude = 1.0f; 		//how far it moves back and forth (look up sine waves)
    public float amplitudeAiming = 0.5f;
    private float amplitudeFinal;
	private Vector3 pos;                //used to hold new position of the inital bullet location.

    void Start()
    {

        amplitudeFinal = amplitude; //initialize final as default amplitude to remove null errors.
    }
    // Update is called once per frame
    void Update () 
	{
        if (player != null) //if player is not declared, then do nothing. if declared....
        {
            playerController = player.GetComponent<PlayerController>(); //get information of playerController script
            if (playerController.aiming == true)        //if the player is aiming.
            {
                amplitudeFinal = amplitudeAiming;       //change amplitudeFinal to amplitudeAiming
            }
            else if (playerController.aiming == false) //if the player isnt aiming,
            {
                amplitudeFinal = amplitude;             //change amplitudeFinal to amplitude.
            }
        } //end of player aiming check.


        pos = transform.position;								//Get position of barrel.
		pos.x += Mathf.Sin (Time.time * frequency)*amplitudeFinal;   //apply sine wave movement in X coordinate for the initial position of the bullet
		shotControl.transform.position = pos;
        pos.y += spriteDistanceFromBullets;
        barrelSprite.transform.position = pos;

    }//end of Update()

}
