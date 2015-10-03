/*PlayerBulletMovement
 * 
 * PlayerBulletMovement is the default movement for player bullets.
 * 
 * This causes the bullets to move in a straight line from the 
 * direction the barrel was facing at time of initial shot.
 * 
 * 
 * Different movement types:
 * Straight - Enemy moves in a straight direction going downwards.
 * SineWave - Enemy moves left and right, while also moving downwards.
 * SineWaveReverse - Enemy moves right and left (opposite of SineWave, causing a crisscross), while also moving downwards.
 * Boss - NOT FINISHED, CONSIDER NO MOVEMENT AND ANIMATE BOSS ENTRANCE SEPARATELY
 * 
 * 
 * 
 * Place on Parent Player Bullet objects.
 */

using UnityEngine;
using System.Collections;

public enum Movement
{
	Straight = 0, SineWave = 1, SineWaveReverse = 2, Boss = 10
};

public class EnemyMovement : MonoBehaviour 
{
	//private EnemySpawner es; //Create a placeholder to get information (list info) from EnemySpawner1

	public Movement movementSelect;	//allows choice for the movement, instead of having each script separate. Any enemy can use this script.
	public float speed = 1.0f; //speed of enemy
	public float frequency = 20.0f; //how fast it moves back and forth (look up sine waves)
	public float amplitude = 0.5f; //how far it moves back and forth (look up sine waves)
	private Vector3 axis;		// holds direction
	private Vector3 pos;		//holds new position.
	private bool bossInPosition = false;	//control for boss movement.
	//private int waveIndex;		//Get index for information on how sprite will spawn.
	//private int actionIndex;	//Get index for information on how sprite will spawn.


	void Start()
	{
	
		//if (GameObject.FindWithTag ("EnemyController") != null) 
		//{
		//	GameObject enemyController = GameObject.FindWithTag ("EnemyController"); //Get object holding script..
		//	es = enemyController.GetComponent<EnemySpawner> ();	//get script information.
		//	waveIndex = es.CurrentWave;		//get wave index for this enemy
		//	actionIndex = es.CurrentAction;	//get action index for this enemy
		//	//using the index for wave/action from above, get information involving the movementSelect from the enemySpawner1.
		//	movementSelect = es.waves [waveIndex].actions [actionIndex].movementSelect;
		//}
		pos = transform.position; 	//store current position
		axis = transform.right;		//get right direction
	}
	// Update is called once per frame
	void Update () 
	{

        switch (movementSelect) 
		{
		//begin switch depending on which movement is selected
		case Movement.Straight:
		{
			transform.position += transform.up * -speed * Time.deltaTime;
			break;
		}
		case Movement.SineWave:
		{
			pos += transform.up * Time.deltaTime * -speed; 
			transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * amplitude;
			break;
		}
		case Movement.SineWaveReverse:
		{
			pos += transform.up * Time.deltaTime * -speed; 
			transform.position = pos + axis * Mathf.Sin (Time.time * -frequency) * amplitude;
			break;
		}
		case Movement.Boss:
		{
			//Consider modification.
			if(bossInPosition == true)
			{
				if(transform.position.y > 8)
				{
					//transform.position += transform.up * speed * Time.deltaTime;
				}

				bossInPosition = false;
			}
				
			break;
		}
		default:
		{
			Destroy (gameObject);
			break;
		}




		}

		
	}//end of Update()
}
