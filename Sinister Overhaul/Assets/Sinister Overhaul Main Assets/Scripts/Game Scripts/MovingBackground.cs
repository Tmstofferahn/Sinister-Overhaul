using UnityEngine;
using System.Collections;

public class MovingBackground : MonoBehaviour {
	
	public float scrollSpeed;
	private Vector2 savedOffset;
    private float moveTime = 0.0f;
	
	void Start () {
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
	}
	
	void Update () {
        if (GameControl.control.isPaused)
            return;
        moveTime += Time.deltaTime;
		float y = Mathf.Repeat (moveTime * scrollSpeed, 1);
		Vector2 offset = new Vector2 (savedOffset.x, y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
	
	void OnDisable () {
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}