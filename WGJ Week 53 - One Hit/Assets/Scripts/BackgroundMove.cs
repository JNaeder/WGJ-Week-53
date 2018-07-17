using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

	public float speed;

	Renderer rend;
	Vector2 matOffset;

	LevelMovement lM;

	// Use this for initialization
	void Start () {
		lM = FindObjectOfType<LevelMovement>();
		rend = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {

		if (lM.hasStarted)
		{
			matOffset = Vector2.up * Time.deltaTime * 0.1f * (lM.speed / speed);
			rend.material.mainTextureOffset += matOffset;
		}
	}
}
