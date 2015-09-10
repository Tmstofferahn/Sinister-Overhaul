/*DestroyParticleSystemOnFinish
 * 
 * DestroyParticleSystemOnFinish destroys a particle system after it has finished playing
 * through the animation cycle.
 * 
 * Use on any particle effect that is not looping.
 */
using UnityEngine;
using System.Collections;

public class DestroyParticleSystemOnFinish : MonoBehaviour 
{

	private ParticleSystem ps; //set a placeholder for the particle system values

	// Use this for initialization
	void Start () 
	{
		ps = GetComponentInChildren<ParticleSystem> (); //get particle system values
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ps) //if there is a particle system created
		{
			if(!ps.IsAlive())	//if the particle system is not playing the animation
			{
				Destroy (gameObject);	//destroy particle system
			}
		}
	
	}
}
