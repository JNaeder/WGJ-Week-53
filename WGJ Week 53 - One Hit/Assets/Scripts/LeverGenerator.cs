using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverGenerator : MonoBehaviour {

    public GameObject[] pieces;
    public GameObject[] coins;
    public Transform spawner;
    public Transform levelParent;
    public float spawnRange;
    public float minSize, maxSize;
    public float bufferSize;

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        spawnRange = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - 1;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Piece")
        {
            SpawnNewRow();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Coin") {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Person")
        {
            Destroy(collision.gameObject);
        }


    }



    void SpawnNewRow() {

        int randNewPiece = Random.Range(0, pieces.Length);
        int randNewCoin = Random.Range(0, coins.Length);


        float newScaleX = Random.Range(minSize, maxSize);
        Vector3 newScale = new Vector3(newScaleX, 1, 0);
        pieces[randNewPiece].transform.localScale = newScale;
        SpriteRenderer pieceRend = pieces[randNewPiece].GetComponent<SpriteRenderer>();
        float pieceExtent = pieceRend.bounds.extents.x;

        float newPieceX = Random.Range(-spawnRange + pieceExtent, spawnRange - pieceExtent);
        Vector3 newPiecePos = new Vector3(spawner.position.x + newPieceX,spawner.position.y,0);
        GameObject newPiece = Instantiate(pieces[randNewPiece], newPiecePos, Quaternion.identity);
        newPiece.transform.parent = levelParent;
        


        SpriteRenderer coinRend = coins[randNewCoin].GetComponent<SpriteRenderer>();
        float coinExtent = coinRend.bounds.extents.x;

        float newCoinX = Random.Range(-spawnRange + coinExtent, spawnRange - coinExtent);
        Vector3 newCoinPos = new Vector3(newCoinX, spawner.position.y, 0);
        // check if its the same as piece position
        
            while (newCoinPos.x < newPiecePos.x + pieceExtent + bufferSize && newCoinPos.x > newPiecePos.x - pieceExtent - bufferSize)
            {
                Debug.Log("Wrong Coin Place");
                newCoinX = Random.Range(-spawnRange, spawnRange);
                newCoinPos = new Vector3(newCoinX, spawner.position.y, 0);
            }

        GameObject newCoin = Instantiate(coins[randNewCoin], newCoinPos, Quaternion.identity);
        newCoin.transform.parent = levelParent;

    }
}
