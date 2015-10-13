using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldProgress : MonoBehaviour {

    private Image shieldProgress;
    private bool flashing = true;
    // Use this for initialization
    void Start () {
       shieldProgress = GetComponent<Image>();
        StartCoroutine(FlashColors());


    }
	
	// Update is called once per frame
	void Update () {
        if (GameControl.control.shieldReady == false) 
        {
            flashing = false;
            if(shieldProgress.color != Color.white)
            {
                shieldProgress.color = Color.white;
            }

            if((GameControl.control.shieldTimeRemaining / GameControl.control.shieldTimeAlive) <= 0)
            {
                shieldProgress.fillAmount = GameControl.control.shieldEnergyCurrent / GameControl.control.shieldEnergyFull;
            }
            else if((GameControl.control.shieldTimeRemaining / GameControl.control.shieldTimeAlive) > 0)
            {
                shieldProgress.fillAmount = GameControl.control.shieldTimeRemaining / GameControl.control.shieldTimeAlive;
            }

        }
        if(GameControl.control.shieldReady == true)
        {
            flashing = true;
        }
            
	
	}

    IEnumerator FlashColors()
    {
        while(true)
        {
            if (flashing == true)
            { 
                if(shieldProgress.color == Color.white | shieldProgress.color == Color.red)
                {
                    shieldProgress.color = Color.green;
                }
                else if (shieldProgress.color == Color.green)
                {
                    shieldProgress.color = Color.red;
                }
               

            }

            yield return new WaitForSeconds(0.5f);
        }

    }
}
