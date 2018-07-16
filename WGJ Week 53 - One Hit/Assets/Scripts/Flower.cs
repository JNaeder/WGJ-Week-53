using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    public float speedRange;
    public float rotSpeedMin, rotSpeedMax;

    float rotSpeed;

    Vector3 newDir;
	// Use this for initialization
	void Start () {
        newDir = new Vector3(Random.Range(-speedRange, speedRange), Random.Range(-speedRange, speedRange), 0);
        rotSpeed = Random.Range(rotSpeedMin, rotSpeedMax);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += newDir * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotSpeed) * Time.deltaTime);


		
	}
}
