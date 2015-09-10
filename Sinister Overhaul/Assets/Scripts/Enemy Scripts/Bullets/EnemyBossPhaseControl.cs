using UnityEngine;
using System.Collections;

public class EnemyBossPhaseControl : MonoBehaviour {

	Animator animator;
	public int maxPhases = 0;
	private int phaseGet;
	public int phase {get{ return phaseGet; }}


	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		Invoke ("ChangePhases", 10);
	}
	
	void ChangePhases()
	{

		phaseGet = animator.GetInteger ("Phase");
		if (phaseGet < maxPhases - 1) 
		{
			animator.SetInteger("Phase", phaseGet +1);
		}
		if (phaseGet >= maxPhases - 1) 
		{
			animator.SetInteger("Phase", 0);
		}

		phaseGet = animator.GetInteger ("Phase");

	


		if (phaseGet == 0) 
		{

			Invoke ("ChangePhases", 10);
		}
		else if (phaseGet == 1) 
		{
			Invoke ("ChangePhases", 10);
		}
		else if (phaseGet == 2) 
		{
			Invoke ("ChangePhases", 10);
		}
		else if (phaseGet == 3) 
		{
			Invoke ("ChangePhases", 10);
		}
		else if (phaseGet == 4) 
		{

			Invoke ("ChangePhases", 40);
		}
	}
}
