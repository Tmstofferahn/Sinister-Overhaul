/* EnemyShootSpecial
 * 

 */

using UnityEngine;
using System.Collections;

public class EnemyShootSpecial : MonoBehaviour
{
    public GameObject bullet = null;            //Create object for the bullet

    public float shotDelay = 0.2f;              //delay inbetween bullets in seconds.
    private bool readyToShoot = true;           //value used to allow the delay.

    public float frequency = 20.0f;             //how fast it moves back and forth (look up sine waves)... use negative value for a mirrored effect.
    public float amplitude = 1.0f;              //how far it moves back and forth (look up sine waves)
    public float rotSpeed = 0.0f;
    private Vector3 pos;                        //used to hold new position of the inital bullet location.


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;                                   //Get position of barrel.
        pos.x += Mathf.Sin(Time.time * frequency) * amplitude;  //apply sine wave movement in X coordinate for the initial position of the bullet
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
 

        if (readyToShoot)
        {
            Instantiate(bullet, pos, transform.rotation);           //apply position and rotation to spawn of bullet, then spawn bullet
            readyToShoot = false;                                   //player just shot, can no longer shoot.
            Invoke("ResetReadyToShoot", shotDelay);             //calls ResetReadyToShoot to set readyToShoot to true after shotDelay timer.
        }

    }//end of Update()

    void ResetReadyToShoot()
    {
        readyToShoot = true;
    }//end of ResetReadyToShoot()
}
