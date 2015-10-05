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

public class PlayerShootUBH : MonoBehaviour
{

    public GameObject bullet;
    public float shotDelay = 0.2f;
    private bool readyToShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (bullet != null)
        {
            if (readyToShoot == true && GameControl.control.isPaused == false && GameControl.control.loadNextLevel == false)
            {
                UbhObjectPool.Instance.GetGameObject(bullet, transform.position, transform.rotation);
                readyToShoot = false;
                Invoke("ResetReadyToShoot", shotDelay);

            }
        }


    }//end of Update()

    void ResetReadyToShoot()
    {
        readyToShoot = true;
    }//end of ResetReadyToShoot()
}
