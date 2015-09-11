/*EnemyWaveSpawner
 * 
 * This is the second iteration of the enemy spawn command.
 * 
 * EnemyWaveSpawner will allow the enemy to spawn just out of camera view.
 * 
 * It will work based upon a looping structure by calling the spawn
 * command within the spawn command.
 * 
 * The screen will be split into 3 sections: left, middle and right.
 * Each section will have a subsection: left, middle, and right.
 * Then one more subsection of the same
 * 
 * If the section says Center at all, it is a major point.
 * LeftCenter is at point -9, which is the center of the left third of the screen
 * middleCenter is at point 0, the center of the screen
 * RightCenter is at point 9, which is the center of the right thrid of the screen
 * 
 * Spawning naming structure broken down as follows:
 * Spawn, position on screen, position within that position.
 * 
 * 
 * X needs to be between -14 and 14
 * Y should be 15 or above.
 * Z should be -2.
 * 
 * Place on Enemy Spawner
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum PositionSelect //setup easy selection for position selection
{
    Preset = 0, CustomPosition = 1
};


[System.Serializable]
public class Wave //waves represent waves of enemies, which can consist of large numbers of enemies.
{
    public string name;                 //set name of wave (ie wave1, wave2, wave3, curved wave, line wave, V formation, etc)
    public float waveDelay;             //sets value to delay the start of a wave. Waves begin as soon as previous ended.

    [Tooltip("Wave to spawn")]
    public GameObject wavePrefab;


    //Sets the choice of the spawn position of the enemy
    [Tooltip("Select the preferred spawn position of the enemy." +
        "\nPreset: use one of the presets given (such as leftLeft, MiddleMiddle, or RightRight)." +
        "\nCustomPosition: use the values you set below for the coordinates of the spawn position." +
        "\nCustomXCoord: use the value you set for the X below for the spawn position. Y and Z are already preset at 15 and -2, respectively.")]
    public PositionSelect positionSelection;


    //Sets custom X, Y, and Z coordinates for use if chosen.
    [Tooltip("Insert custom coordinates to where the enemy wave will spawn. " +
        "\nStandard positions are as follows." +
        "\nX: 0" +
        "\nY: 0" +
        "\nZ: -2")]
    public Vector3 customPosition;





}


public class EnemyWaveSpawner : MonoBehaviour
{
    
    public List<Wave> waves;        //list of waves
    private int m_CurrentWave = 0; //stores index of the waves
    public int CurrentWave { get { return m_CurrentWave; } } //public index
    Vector3 customPositionFinal;


    //private float m_DelayFactor = 1.0f;
    //delay (in seconds) how long the spawn will be delayed


    void Start()
    {
        StartCoroutine(SpawnWaves());       //start loop to spawn enemies based upon values chosen.

    }//end of Start()


    IEnumerator SpawnWaves()
    {

        foreach (Wave W in waves) //index of Waves.
        {
            yield return new WaitForSeconds(W.waveDelay);

            m_CurrentWave = waves.IndexOf(W);

            //select proper spawn position based upon selection
            SpawnPositionSelect(W.positionSelection, W.customPosition);

            if (waves.IndexOf(W) >= waves.Count - 1)
            {
                GameObject lastWave = (GameObject)Instantiate(W.wavePrefab, customPositionFinal, Quaternion.identity);

                while (lastWave.transform.childCount > 0)
                {
                    yield return 0;
                }
                yield return new WaitForSeconds(5.0f);
                GameControl.control.loadNextLevel = true;
            }
            else
            {
                Instantiate(W.wavePrefab, customPositionFinal, Quaternion.identity);
            }

            

           
  

        }
        

        yield return null;

    }



    //SpawnPositionSelect allows the user to select a position select from a drop down.
    //Preset allows the user to select a preset transformation.
    //Custom position allows the user to select their custom coords for the spawn of that action.
    //CustomXCoord allows the user to select a custom X, spawning them at an X of their choosing while Y and Z stay and standard spots.
    Vector3 SpawnPositionSelect(PositionSelect positionSelection, Vector3 customPosition)
    {
        switch (positionSelection)
        {

            case PositionSelect.Preset:
                {//Preset will use a preset and place that value in the final position.
                    customPositionFinal = new Vector3((float)0, 0, -2);
                    break;
                }
            case PositionSelect.CustomPosition:
                {//CustomPosition stores the position set (x, y, z) in the final position.
                    customPositionFinal = customPosition;
                    break;
                }

            default:
                {//Default is used in case an error occurs. If none are selected it will zero out the x position.
                    customPositionFinal = new Vector3(0, 0, 0);
                    break;
                }


        }// end of switch
        return customPositionFinal;
    }




}


