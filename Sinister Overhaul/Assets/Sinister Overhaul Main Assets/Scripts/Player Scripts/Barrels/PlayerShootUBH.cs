/* PlayerShoot
 * 
 * This is the first iteration of the Player shoot command.
 * 
 * PlayerShoot will allow the player to shoot based upon a shotDelay.
 * 
 * Calculates current position of barrel and applies that to the initial spawn of the bullet.
 * Creates the spawn of the bullet.
 * Then makes the player wait based upon a timed delay using shotDelay and ResetReadyToShoot.
 * 
 * Place on Parent Player Barrel objects.
 */
using UnityEngine;
using System.Collections;

public enum ShootPattern //setup easy selection for enemies
{
	Standard = 0, SineWave = 1, SineWaveReverse = 2, Homing = 3

};



public class PlayerShootUBH : MonoBehaviour
{

    public AudioSource audio;
    public AudioClip shootingSFX;
    public ShootPattern chooseShot;
    public GameObject[] bulletUpgrade;
    public float shotDelay = 0.2f;
    private bool readyToShoot = true;
    private PlayerController playerController;  //create instance of playerController script
    public GameObject player = null;            //Create object for the player

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
        Shoot();
        


    }//end of Update()

    void PlayShotSound()
    {
        //if(GameControl.control.playerUpgradeLevel == 0 | GameControl.control.playerUpgradeLevel == 1)
        //{
        //    audio.PlayOneShot(shootingSFX);
        //}
        audio.PlayOneShot(shootingSFX);
    }
    void Shoot()
    {
        switch(chooseShot)
        { 
         case ShootPattern.Standard:
            {
                    if (Input.GetButton("Fire1"))
                    {
                        if (bulletUpgrade != null)
                        {
                            if (readyToShoot == true && GameControl.control.isPaused == false && GameControl.control.loadNextLevel == false)
                            {
                                if (bulletUpgrade[GameControl.control.playerUpgradeLevel] != null)
                                {
                                    if (GameControl.control.playerUpgradeLevel < 2)
                                    {
                                        UbhObjectPool.Instance.GetGameObject(bulletUpgrade[GameControl.control.playerUpgradeLevel], transform.position, transform.rotation);
                                    }
                                    if (GameControl.control.playerUpgradeLevel >= 2)
                                    {
                                        UbhObjectPool.Instance.GetGameObject(bulletUpgrade[1], transform.position, transform.rotation);
                                    }
                                    PlayShotSound();
                                    readyToShoot = false;
                                    Invoke("ResetReadyToShoot", shotDelay);
                                }


                            }
                        }
                    }

                    break;
            }
        case ShootPattern.SineWave:
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
                    pos.z += Mathf.Cos(moveTime * frequency) * 0.4f;  //apply sine wave movement in X coordinate for the initial position of the bullet

                    if (Input.GetButton("Fire1"))
                    {

                        if (readyToShoot && GameControl.control.isPaused == false && GameControl.control.loadNextLevel == false)
                        {
                            if (bulletUpgrade[GameControl.control.playerUpgradeLevel] != null && GameControl.control.playerUpgradeLevel >= 2)
                            {
                                UbhObjectPool.Instance.GetGameObject(bulletUpgrade[2], pos, transform.rotation);
                                readyToShoot = false;                                   //player just shot, can no longer shoot.
                                Invoke("ResetReadyToShoot", shotDelay);             //calls ResetReadyToShoot to set readyToShoot to true after shotDelay timer.
                            }

                        }
                    }
                    break;
            }
        case ShootPattern.SineWaveReverse:
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
                    pos.z -= Mathf.Cos(moveTime * frequency) * 0.4f;  //apply sine wave movement in X coordinate for the initial position of the bullet

                    if (Input.GetButton("Fire1"))
                    {

                        if (readyToShoot && GameControl.control.isPaused == false && GameControl.control.loadNextLevel == false)
                        {
                            if (bulletUpgrade[GameControl.control.playerUpgradeLevel] != null && GameControl.control.playerUpgradeLevel >= 2)
                            {
                                UbhObjectPool.Instance.GetGameObject(bulletUpgrade[2], pos, transform.rotation);
                                readyToShoot = false;                                   //player just shot, can no longer shoot.
                                Invoke("ResetReadyToShoot", shotDelay);             //calls ResetReadyToShoot to set readyToShoot to true after shotDelay timer.
                            }

                        }
                    }

                    break;
            }
        case ShootPattern.Homing:
            {
                    if (Input.GetButton("Fire1"))
                    {
                        if (bulletUpgrade != null)
                        {
                            if (readyToShoot == true && GameControl.control.isPaused == false && GameControl.control.loadNextLevel == false)
                            {
                                if (bulletUpgrade[GameControl.control.playerUpgradeLevel] != null)
                                {
                                    if (GameControl.control.playerUpgradeLevel >= 3)
                                    {
                                        UbhObjectPool.Instance.GetGameObject(bulletUpgrade[3], transform.position, transform.rotation);
                                        PlayShotSound();
                                        readyToShoot = false;
                                        Invoke("ResetReadyToShoot", shotDelay);
                                    }
                                   
                                }


                            }
                        }
                    }
                    break;
            }
        default:
            {
                break;
            }

        } //end of switch


    }

    void ResetReadyToShoot()
    {
        readyToShoot = true;
    }//end of ResetReadyToShoot()
}
