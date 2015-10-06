/*PlayerShootSineWave
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
 * Place on Parent Player Barrel objects.
 */

using UnityEngine;
using System.Collections;

public class PlayerShootSineWaveUBH : MonoBehaviour
{
    private PlayerController playerController;  //create instance of playerController script
    public GameObject player = null;            //Create object for the player
    public GameObject bullet = null;            //Create object for the bullet

    public float shotDelay = 0.2f;              //delay inbetween bullets in seconds.
    private bool readyToShoot = true;           //value used to allow the delay.

    public float frequency = 20.0f;             //how fast it moves back and forth (look up sine waves)... use negative value for a mirrored effect.
    public float amplitude = 1.0f;              //how far it moves back and forth (look up sine waves)
    public float amplitudeAiming = 0.5f;        //how far it moves back and forth (look up sine waves) while aiming
    private float amplitudeFinal;				//final variable for amplitude
    private float moveTime = 0.0f;
    private Vector3 pos;                        //used to hold new position of the inital bullet location.
    public Vector3 Pos { get { return pos; } }      //stores a public version of pos for usage elsewhere.

    void Start()
    {

        amplitudeFinal = amplitude; //initialize final as default amplitude to remove null errors.
    }

    // Update is called once per frame
    void Update()
    {

        moveTime += Time.deltaTime;

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


        pos = transform.position;                                   //Get position of barrel.
        pos.x += Mathf.Sin(moveTime * frequency) * amplitudeFinal;  //apply sine wave movement in X coordinate for the initial position of the bullet
        if (Input.GetButton("Fire1"))
        {

            if (readyToShoot && GameControl.control.isPaused == false && GameControl.control.loadNextLevel == false)
            {
                UbhObjectPool.Instance.GetGameObject(bullet, pos, transform.rotation);
                readyToShoot = false;                                   //player just shot, can no longer shoot.
                Invoke("ResetReadyToShoot", shotDelay);             //calls ResetReadyToShoot to set readyToShoot to true after shotDelay timer.
            }
        }


    }//end of Update()

    void ResetReadyToShoot()
    {
        readyToShoot = true;
    }//end of ResetReadyToShoot()
}
