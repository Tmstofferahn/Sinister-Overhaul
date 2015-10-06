using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldProgress : MonoBehaviour {

    private Image shieldProgress;
    // Use this for initialization
    void Start () {
       shieldProgress = GetComponent<Image>();
        
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameControl.control.shieldReady == false) 
        {
            shieldProgress.color = Color.white;
            if((GameControl.control.shieldTimeRemaining / GameControl.control.shieldWaitTime) <= 0)
            {
                shieldProgress.fillAmount = GameControl.control.shieldEnergyCurrent / GameControl.control.shieldEnergyFull;
            }
            else if((GameControl.control.shieldTimeRemaining / GameControl.control.shieldWaitTime) > 0)
            {
                shieldProgress.fillAmount = GameControl.control.shieldTimeRemaining / GameControl.control.shieldWaitTime;
            }

        }
        if(GameControl.control.shieldReady == true)
        {
            shieldProgress.fillAmount = 1;
            shieldProgress.color = Color.green;
        }
            
	
	}
}
