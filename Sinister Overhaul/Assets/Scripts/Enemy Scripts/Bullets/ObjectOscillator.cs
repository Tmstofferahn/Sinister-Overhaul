using UnityEngine;
using System.Collections;

public class ObjectOscillator : MonoBehaviour 
{
	
	public float speedMult = 1.0f; 		//how fast the oscillation is
	public float rangeMult = 1.0f;		//how far the oscillation is
	// Use this for initialization
	public GameObject bullet;
	public float shootInterval = 1.0f;
	float basex = 0.0f;
	float shootTimeAc = 0.0f;
	// Update is called once per frame
	void Start() 
	{
		basex = transform.position.x; 		//oscillation follows the x-axis
	}
	void Update () 
	{
		Vector3 position = transform.position;
		float interval = Mathf.Sin(Time.time * (speedMult / rangeMult)) * rangeMult;
		bool shoot = false;
		if(Time.deltaTime + shootTimeAc > shootInterval)
		{
			shootTimeAc = 0.0f;
			shoot = true;
		}
		
		else
			shootTimeAc += Time.deltaTime;
		
		position.x = basex + interval;

		
		transform.position = position;
		if(shoot)
			Instantiate(bullet, transform.position, transform.rotation);
	}
}