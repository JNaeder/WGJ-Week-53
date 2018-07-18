using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {


    public float speedRangeMin, speedRangeMax;

    float personSpeed;
    bool isAttacked;

    Animator anim;
	SpriteRenderer sP;
    Rigidbody2D rB;

	// Use this for initialization
	void Start () {
        personSpeed = Random.Range(speedRangeMin, speedRangeMax);
		sP = GetComponent<SpriteRenderer>();
        rB = GetComponent<Rigidbody2D>();

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
        rB.isKinematic = true;
        isAttacked = true;
		sP.sortingLayerName = "Person";
    }
}
