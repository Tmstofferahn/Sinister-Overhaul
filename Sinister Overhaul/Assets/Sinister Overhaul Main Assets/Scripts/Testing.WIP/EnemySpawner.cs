///*EnemySpawner
// * 
// * This is the first iteration of the enemy spawn command.
// * 
// * EnemySpawner will allow the enemy to spawn just out of camera view.
// * 
// * It will work based upon a looping structure by calling the spawn
// * command within the spawn command.
// * 
// * The screen will be split into 3 sections: left, middle and right.
// * Each section will have a subsection: left, middle, and right.
// * Then one more subsection of the same
// * 
// * If the section says Center at all, it is a major point.
// * LeftCenter is at point -9, which is the center of the left third of the screen
// * middleCenter is at point 0, the center of the screen
// * RightCenter is at point 9, which is the center of the right thrid of the screen
// * 
// * Spawning naming structure broken down as follows:
// * Spawn, position on screen, position within that position.
// * 
// * 
// * X needs to be between -14 and 14
// * Y should be 15 or above.
// * Z should be -2.
// * 
// * Place on Enemy Spawner
// */


//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public enum EnemyType //setup easy selection for enemies
//{
//	None = 0, Small = 1, Medium = 2, Large = 3, Boss = 10

//};
//public enum PositionSelect //setup easy selection for position selection
//{
//	Preset = 0, CustomPosition = 1, CustomXCoord = 2
//};

//public enum TransformPrefab //setup easy transform selection
//{
//	leftLeftLeft 		= -13,
//	leftLeftMiddle 		= -12,
//	leftLeftRight 		= -11,

//	leftMiddleLeft		= -10,
//	leftCenter			= -9,
//	leftMiddleRight		= -8,

//	leftRightLeft		= -7,
//	leftRightMiddle		= -6,
//	LeftRightRight		= -5,

//	middleLeftLeft		= -4,
//	middleLeftMiddle	= -3,
//	middleLeftRight		= -2,
	
//	middleMiddleLeft 	= -1,
//	middleCenter	 	= 0,
//	middleMiddleRight 	= 1,

//	middleRightLeft		= 2,
//	middleRightMiddle	= 3,
//	middleRightRight	= 4,

//	rightLeftLeft		= 5,
//	rightLeftMiddle		= 6,
//	rightLeftRight		= 7,

//	rightMiddleLeft		= 8,
//	rightCenter			= 9,
//	rightMiddleRight	= 10,

//	rightRightLeft 		= 11,
//	rightRightMiddle 	= 12,
//	rightRightRight 	= 13
//};

//[System.Serializable]
//public class WaveAction //each wave has actions, these actions represent enemies within Columns that will be spawned within the screen.
//{
//	//Set name for Wave portion (ie what it spawns, location, etc)
//	[Tooltip("Name the action performed in the wave")]
//	public string name;	

//	//holds gameObject to be spawned for this action of a wave.
//	[Tooltip("Set the enemy to spawn.")]
//	public EnemyType enemyTypeSelect;

//	//Takes selection for the movement pattern the enemy will use.
//	[Tooltip("Set the movement pattern the enemy will follow.")]
//	public Movement movementSelect;

//	//Sets the choice of the spawn position of the enemy
//	[Tooltip("Select the preferred spawn position of the enemy." +
//		"\nPreset: use one of the presets given (such as leftLeft, MiddleMiddle, or RightRight)." +
//		"\nCustomPosition: use the values you set below for the coordinates of the spawn position." +
//		"\nCustomXCoord: use the value you set for the X below for the spawn position. Y and Z are already preset at 15 and -2, respectively.")]
//	public PositionSelect positionSelection;

//	//Sets the position of the sprite spawn, based upon the above prefabs.
//	[Tooltip("Set the transform of the enemy with a prefab. Prefabs for this can be found under Prefabs > Enemy Prefabs > Spawn Postions")]
//	public TransformPrefab transformSelect;

//	//Sets custom X, Y, and Z coordinates for use if chosen.
//	[Tooltip("Insert custom coordinates to where the enemy will spawn. " +
//		"\nStandard positions are as follows." +
//		"\nX: between -14 and 14" +
//		"\nY: 15 or above" +
//		"\nZ: -2")]
//	public Vector3 customPosition;	

//	//Sets custom rotation of enemies. 
//	[Tooltip("Insert custom rotation for enemy. Most enemies should be appropriately rotated. If a rotational change is desired, edit these values.")]
//	public Vector3 customRotation;		

//	//Sets initial spawn delay of the entire action of the wave.
//	[Tooltip("Delay the initial spawn of this wave by time in seconds.")]
//	public float startDelay = 0.0f;		

//	//Sets delay between each enemy spawned within an action.
//	[Tooltip("Delay the time in between each spawn by time in seconds.")]
//	public float spawnDelay = 0.5f;		

//	//Sets the number of enemies spawned per action of a wave.
//	[Tooltip("Change the amount of enemies that this will spawn.")]
//	public int spawnCount;				



//}

//[System.Serializable]
//public class Wave //waves represent waves of enemies, which can consist of large numbers of enemies.
//{
//	public string name;					//set name of wave (ie wave1, wave2, wave3, curved wave, line wave, V formation, etc)
//	public float waveDelay;				//sets value to delay the start of a wave. Waves begin as soon as previous ended.
//	public List<WaveAction> actions;	//list holding all information for portions of waves.
//}


//public class EnemySpawner : MonoBehaviour 
//{
//	//Enemy gameobjects hold references to their gameobject to spawn in later. used for easy selection.
//	public GameObject enemySmall = null; 
//	public GameObject enemyMedium = null;
//	public GameObject enemyLarge = null;
//	public GameObject enemyBoss = null;
//	private GameObject enemy;

//	public List<Wave> waves;		//list of waves
//	private int m_CurrentWave  = 0; //stores index of the waves
//	public int CurrentWave { get {return m_CurrentWave;} } //public index
//	private int m_CurrentAction = 0; //stores index of the action, subindex of the wave.
//	public int CurrentAction { get { return m_CurrentAction; } } //public index
//	Vector3 customPositionFinal;


//	//private float m_DelayFactor = 1.0f;
//	//delay (in seconds) how long the spawn will be delayed


//	void Start()
//	{
//		StartCoroutine (SpawnWaves ());		//start loop to spawn enemies based upon values chosen.

//	}//end of Start()


//	IEnumerator SpawnWaves()
//	{

//		foreach (Wave W in waves) //index of Waves.
//		{
//			yield return new WaitForSeconds(W.waveDelay);

//			m_CurrentWave = waves.IndexOf (W);
//			foreach (WaveAction A in W.actions) //index of actions.
//			{

//				yield return new WaitForSeconds(A.startDelay);
//				//select proper enemyType based upon selection
//				EnemyTypeSelect(A.enemyTypeSelect);

//				//select proper spawn position based upon selection
//				SpawnPositionSelect(A.positionSelection, A.transformSelect, A.customPosition);
				
//				if(enemy) // if enemy was selected, spawn enemy
//					StartCoroutine (SpawnEnemies(enemy, A.spawnCount, customPositionFinal, A.customRotation, A.spawnDelay));

//				m_CurrentAction = W.actions.IndexOf (A); //store current index of actions.
//			}


//			yield return null;

//		}
//		yield return null;


//	}//end of SpawnWaves


//	//SpawnPositionSelect allows the user to select a position select from a drop down.
//	//Preset allows the user to select a preset transformation.
//	//Custom position allows the user to select their custom coords for the spawn of that action.
//	//CustomXCoord allows the user to select a custom X, spawning them at an X of their choosing while Y and Z stay and standard spots.
//	Vector3 SpawnPositionSelect(PositionSelect positionSelection, TransformPrefab transformSelect, Vector3 customPosition)
//	{
//		switch(positionSelection)
//		{
			
//		case PositionSelect.Preset:
//		{//Preset will use a preset and place that value in the final position.
//			customPositionFinal = new Vector3((float)transformSelect, 15, -2);
//			break;
//		}
//		case PositionSelect.CustomPosition:
//		{//CustomPosition stores the position set (x, y, z) in the final position.
//			customPositionFinal = customPosition;
//			break;
//		}
//		case PositionSelect.CustomXCoord:
//		{//CustomXCoord stores the custom X coordinate for the final position.
//			customPositionFinal = new Vector3(customPosition.x, 15, -2);
//			break;
//		}
//		default:
//		{//Default is used in case an error occurs. If none are selected it will zero out the x position.
//			customPositionFinal = new Vector3(0, 15, -2);
//			break;
//		}
			
			
//		}// end of switch
//		return customPositionFinal;
//	}

//	//EnemyTypeSelect allows the user to select enemies without constantly dragging prefabs onto each action.
//	//Depending on selection, it will change the enemy to that enemy type.
//	GameObject EnemyTypeSelect(EnemyType enemySelect)
//	{
//		switch (enemySelect) {
//		case EnemyType.None:
//		{
//			enemy = null;
//			break;
//		}
//		case EnemyType.Small:
//		{
//			enemy = enemySmall;
//			break;
//		}
//		case EnemyType.Medium:
//		{
//			enemy = enemyMedium;
//			break;
//		}
//		case EnemyType.Large:
//		{
//			enemy = enemyLarge;
//			break;
//		}
//		case EnemyType.Boss:
//		{
//			enemy = enemyBoss;
//			break;
//		}
//		default:
//		{
//			enemy = null;
//			break;
//		}

//		} //end of switch

//		return enemy;
//	}

//	//SpawnEnemies is the main call which actually spawns enemies, based upon the inputs given.
//	IEnumerator SpawnEnemies(GameObject enemy, int enemyCount, Vector3 position, Vector3 rotation, float spawnDelay)
//	{

//		for (int i = 0; i < enemyCount; i++) 
//		{
//			Vector3 spawnPosition = position;
//			Quaternion spawnRotation = Quaternion.Euler (rotation);
//			Instantiate (enemy, spawnPosition, spawnRotation);
//			yield return new WaitForSeconds (spawnDelay);
//		}
//	}



//}