/*EnemyBossShoot
 * 
 * EnemyBossShoot is meant to control different shooting options for bosses based
 * upon which phase they are in. All phases are controlled via the animation and the parent
 * barrel object.
 * 
 * 
 * Place on Enemy Barrel objects. 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Phase //waves represent waves of enemies, which can consist of large numbers of enemies.
{
	public string name;					//set name of phase
	public float shotDelay = 0.2f;
	public float delayAfterShotLimit = 0.0f;
	public int shotCountLimit = 0;		
}

public class EnemyBossShoot : MonoBehaviour 
{
	public List<Phase> phases;
	private EnemyBossPhaseControl pc;
	public GameObject parentBarrel;
	public GameObject bullet;		//GameObject of bullet being used.
	
	private int shotCount = 0;

	void Start()
	{
		if (parentBarrel != null) 
		{
			pc = parentBarrel.GetComponent<EnemyBossPhaseControl>();
		}
		StartCoroutine (ReadyToShoot()); //Calls ReadyToShoot with a shotDelay
	}//end of Start()
	
	IEnumerator ReadyToShoot()
	{
		yield return null;
		while (parentBarrel) 
		{

			if(pc.phase  < phases.Count -1)
			{
				yield return new WaitForSeconds(phases[pc.phase].shotDelay);
				if(shotCount >= phases[pc.phase].shotCountLimit)
				{
					yield return new WaitForSeconds(phases[pc.phase].delayAfterShotLimit);
					shotCount = 0;
				}
			}
			else if(pc.phase >= phases.Count -1)
			{
				yield return new WaitForSeconds(phases[phases.Count - 1].shotDelay);
				if(shotCount >= phases[pc.phase].shotCountLimit)
				{
					yield return new WaitForSeconds(phases[phases.Count - 1].delayAfterShotLimit);
					shotCount = 0;
				}
			}

			//yield return new WaitForSeconds(shotDelayPhase[pc.phase]);

//			if(pc.phase == 0 | pc.phase > 4)
//			{
//				yield return new WaitForSeconds(shotDelayPhase0);
//			}
//			else if(pc.phase == 1)
//			{
//				yield return new WaitForSeconds(shotDelayPhase1);
//			}
//			else if(pc.phase == 2)
//			{
//				yield return new WaitForSeconds(shotDelayPhase2);
//			}
//			else if(pc.phase == 3)
//			{
//				yield return new WaitForSeconds(shotDelayPhase3);
//			}
//			else if(pc.phase == 4)
//			{
//				yield return new WaitForSeconds(shotDelayPhase4);
//			}


			Instantiate (bullet, transform.position, transform.rotation); //Sets bullet up based upon position/rotation of object shooting (ie barrels)
			shotCount++;


		}

		

	}//end of ResetReadyToShootPhase1

}