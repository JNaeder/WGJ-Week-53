using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {


    public float speedRangeMin, speedRangeMax;

    float personSpeed;
    bool isAttacked;

    Animator anim;

	// Use this for initialization
	void Start () {
        personSpeed = Random.Range(speedRangeMin, speedRangeMax);

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAttacked)
        {
            transform.position += Vector3.up * Time.deltaTime * personSpeed;
        }
        anim.SetBool("isAttacked", isAttacked);


	}



    public void GetAttacked() {
        isAttacked = true;

    }
}
