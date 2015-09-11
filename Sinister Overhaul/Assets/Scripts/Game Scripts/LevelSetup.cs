/*LevelSetup
 * 
 * LevelSetup will be the main way of seting up a level on completion of each level.
 * When the level is loaded, the GameController will also be loaded, which will invoke this script.
 * 
 * This script Spawns in the player as well as enemySpawners.
 * 
 * This script can be used to spawn anything else that would need a spawn on each level.
 * 
 * Place on Game Controller
 */

using UnityEngine;
using System.Collections;

//implement this script into the game controller

public class LevelSetup : MonoBehaviour 
{
	public static LevelSetup ls;
	public GameObject player 		= null;
	public GameObject[] enemySpawnerArr = null;
    // public Component[] camera = null;
   // public GameObject[] backgroundArr = null;
    //public GameObject lighting = null;

	public Vector3 playerSpawnPosition;
	public Vector3 enemySpawnerPosition;
	//public Vector3 cameraSpawnPosition;
	//public Vector3 lightingSpawnPosition;
	//public Vector3 backgroundSpawnPosition;

	// Use this for initialization
	void Awake () 
	{
		if (ls == null) 
		{
			DontDestroyOnLoad (gameObject);
			ls = this;
		}
		else if (ls != this) 
		{
			Destroy (gameObject);
		}
		enemySpawnerArr [0] = null;
		SpawnPlayer ();
		SpawnEnemySpawner ();
        SetupMusic();
		//SpawnCamera ();
		//SpawnLighting ();
		//SpawnBackground ();
	}

	void SpawnPlayer()
	{
		if (Application.loadedLevel > 0) 
		{
			Vector3 spawnPosition = (playerSpawnPosition);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (player, spawnPosition, spawnRotation);
		}

	}

	void SpawnEnemySpawner()
	{
		if (enemySpawnerArr [Application.loadedLevel])
		{
			Vector3 spawnPosition = (enemySpawnerPosition);
			Quaternion spawnRotation = Quaternion.Euler (180.0f, 0.0f, 0.0f);
			Instantiate (enemySpawnerArr[Application.loadedLevel], spawnPosition, spawnRotation);
		}
	
	}

    void SetupMusic()
    {
        if(MusicManager.instance != null)
        {
            MusicManager.PlayGameMusic();
        }
       
    }

 //   void SpawnCamera()
	//{
	//	Vector3 spawnPosition = (cameraSpawnPosition);
	//	Quaternion spawnRotation = Quaternion.identity;
	//	Instantiate (camera, spawnPosition, spawnRotation);
	//}

	//void SpawnLighting()
	//{
	//	Vector3 spawnPosition = new Vector3(0, 0, -20);
	//	Quaternion spawnRotation = Quaternion.identity;
	//	Instantiate (lighting, spawnPosition, spawnRotation);
	//}

	//void SpawnBackground()
	//{
	//	Vector3 spawnPosition = (backgroundSpawnPosition);
	//	Quaternion spawnRotation = Quaternion.identity;
	//	Instantiate (backgroundArr[i], spawnPosition, spawnRotation);
	//}
	
}
