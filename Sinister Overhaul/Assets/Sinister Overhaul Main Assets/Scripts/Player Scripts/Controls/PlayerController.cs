/*PlayerController
 * 
 * Controls the player movement by using player input.
 * 
 * Player is confined to the boundary spaces below.
 * 
 * Place only on Parent Player object.
 */

using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax; 
}//end of Class Boundary

public class PlayerController : MonoBehaviour
{
    public GameObject shield;

    public float maxSpeed = 5;          //Max speed in any direction the ship can move.
    public float maxSpeedAiming = 2.5f; //Max speed in any direction the ship can move while aiming.
    private float speed;                //variable to hold speed based upon aiming or not.
    public Boundary boundary;           //create a boundary object

    private Vector2 tempVelocity;
    private bool isSleeping = false;
    Animator animator;                  //Get animator access
    public bool aiming { get { return setAiming; } }    //public for other scripts to use	
    private bool setAiming;             //bool to hold if aiming for using in above public bool aiming

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>(); //setup aiming based upon animator for Player
        speed = maxSpeed;                   //Set speed to maxspeed to prevent errors of null values
        setAiming = false;                  //set bool to false to pevent errors of null values.

    }//end of Start()

    // Update is called once per frame
    void Update()
    { 


        //if the player is pressing right mouse or left alt then they are aiming.
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Aiming", true);   //change animation parameter Aiming to true
            setAiming = true;                   //change public to false
            speed = maxSpeedAiming;             //set new speed
        }
        else if (Input.GetButtonUp("Fire1")) //on release, set aiming to false
        {
            animator.SetBool("Aiming", false); //change animation parameter Aiming to false.
            setAiming = false;                  //change public to false
            speed = maxSpeed;                   //set new speed
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (GameControl.control.shieldReady == true)
            {
                GameControl.control.shieldReady = false;
                shield.SetActive(true);
            }
        }
        if (GameControl.control.shieldReady == false)
        {
            GameControl.control.ShieldTimer();
        }

        float moveHorizontal = Input.GetAxis("Horizontal");     //get horizontal input
        float moveVertical = Input.GetAxis("Vertical");     //get vertical input

        //Player movement (x, y)
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);


        //limit player movement to inside boundary
        GetComponent<Rigidbody2D>().position = new Vector3
        (

            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), //x position (horizontal)
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),//y position (vertical)
            0.0f//z position (depth)


        ); //end of boundary

    }//end of Update







}//end of class PlayerController
