using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    public float speedRange;
    public float rotSpeedMin, rotSpeedMax;

	public GameObject flowerParticle;

	LevelMovement lM;

    float rotSpeed;

    Vector3 newDir;
	// Use this for initialization
	void Start () {
        newDir = new Vector3(Random.Range(-speedRange, speedRange), Random.Range(-speedRange, speedRange), 0);
        rotSpeed = Random.Range(rotSpeedMin, rotSpeedMax);
		lM = FindObjectOfType<LevelMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += newDir * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotSpeed) * Time.deltaTime);


		
	}


	public void PlayParticle(){
		GameObject newParticle = Instantiate(flowerParticle, transform.position, Quaternion.identity);
		ParticleSystem.MainModule pS = newParticle.GetComponent<ParticleSystem>().main;
		pS.customSimulationSpace = lM.transform;
		Destroy(gameObject);
		Destroy(newParticle, 2);


	}
}
