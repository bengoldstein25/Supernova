using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_alt : MonoBehaviour {
    public float movementSpeed = 30;
    public float turningSpeed = 65;
    public float accelFactor = 1;
    public float friction = 0.75f;
    public GameObject com; // center of mass
    Rigidbody rb;
    GameObject minimapMarker;
    bool onGround;
    public int playerNumber = 0;
    private Vector3 grav;
    private bool gravChanged;
    public GameObject followCamera;
	public int item1 = -1;
	public int item2 = -1;
	public GameObject asteroidPrefab;
	public static int itemStolenFrom1 = -1;
	public static int itemStolenFrom2 = -1;

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = com.transform.localPosition;
        rb = GetComponent<Rigidbody>();
        onGround = false;
        rb.drag = friction;
        if(playerNumber == 0) {
            playerNumber = 1;
        }
        gravChanged = false;
    }

    void Update() {
        float vertical = movementSpeed;
        if (playerNumber == 1) {
            vertical = vertical * Input.GetAxis("Vertical-P1");
        } else {
            vertical = vertical * Input.GetAxis("Vertical-P2");
        }
        if (onGround) {
            Vector3 forceToAdd = transform.forward * vertical;
            rb.AddForce(accelFactor * forceToAdd, ForceMode.Acceleration);
        }
        float horizontal = turningSpeed * Time.deltaTime;
        if (playerNumber == 1) {
            horizontal = horizontal * Input.GetAxis("Horizontal-P1");
        } else {
            horizontal = horizontal * Input.GetAxis("Horizontal-P2");
        }
        if (vertical >= 0) {
            transform.Rotate(0, horizontal, 0);
        } else {
            transform.Rotate(0, -1 * horizontal, 0);
        }
        if (gravChanged) {
            rb.AddForce(grav, ForceMode.Acceleration);
        }

		//activates Player 1's powerup
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			if (item1 != -1) {
				UsePlayer1Powerup ();
			}
		}

		//activates Player 2's powerup
		if (Input.GetKeyDown (KeyCode.RightShift)) {
			if (item2 != -1) {
				UsePlayer2Powerup ();
			}
		}
    }

    void OnCollisionEnter(Collision coll) {
		if (coll.collider.tag == "Ground") {
			onGround = true;
			rb.drag = friction;
		} 
    }

    void OnCollisionExit(Collision coll) {
		GameObject collidedWith = coll.gameObject;

        if (coll.collider.tag == "Ground") {
            onGround = false;
            rb.drag = 0f;
		} else if (collidedWith.tag == "Asteroid") {
			Destroy (collidedWith);
		}
    }

    void OnCollisionStay(Collision coll) {
        if (coll.collider.tag == "Ground") {
            onGround = true;
            rb.drag = friction;
        } 
    }

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Powerup" && playerNumber == 1 && item1 == -1) {
			item1 = Random.Range (1, 6);
			itemStolenFrom1= item1;

			Debug.Log ("num for player 1 is : " + item1);
		} else if (coll.gameObject.tag == "Powerup" && playerNumber == 2 && item2 == -1) {
			item2 = Random.Range (1, 6);
			itemStolenFrom2 = item2;
		}
	}

	void UsePlayer1Powerup() {
		if (item1 == 1) {
			//rocket boosters (speed buff for 4 seconds then revert the speed back to normal)
			movementSpeed = 60;
			Invoke ("RevertSpeed", 4f);
			item1 = -1;
			itemStolenFrom1 = -1;
		} else if (item1 == 2) {
			//slows down enemy player for 3 seconds
			GameObject[] player2;
			player2 = GameObject.FindGameObjectsWithTag ("Player2");

			foreach (GameObject player in player2) {
				player.gameObject.SendMessage ("SlowPlayer");
			}
			item1 = -1;
			itemStolenFrom1 = -1;
		} else if (item1 == 3) {
			//drops an asteroid behind the player as a road block item
			GameObject asteroid = Instantiate<GameObject>(asteroidPrefab);
			asteroid.transform.position = transform.position - (transform.forward * 4);
			item1 = -1;
			itemStolenFrom1 = -1;
		} else if (item1 == 4) {
			GameObject[] player2;

			player2 = GameObject.FindGameObjectsWithTag ("Player2");

			foreach (GameObject player in player2) {
				player.GetComponent<Rigidbody> ().useGravity = false;
				StartCoroutine (EnableGravity (player, 5f));
			}
			item1 = -1;
			itemStolenFrom1 = -1;
		} else if (item1 == 5) {
			if (itemStolenFrom2 != -1) {
				GameObject[] player2;
				player2 = GameObject.FindGameObjectsWithTag ("Player2");

				foreach (GameObject player in player2) {
					player.gameObject.SendMessage ("StealItemFromPlayer2");
				}
					
				item1 = itemStolenFrom2;
				itemStolenFrom2 = -1;
			}
		}
	}

	void UsePlayer2Powerup() {
		if (item2 == 1) {
			movementSpeed = 60;
			Invoke ("RevertSpeed", 4f);
			item2 = -1;
			itemStolenFrom2 = -1;
		} else if (item2 == 2) {
			GameObject[] player1;
			player1 = GameObject.FindGameObjectsWithTag ("Player");

			foreach (GameObject player in player1) {
				player.gameObject.SendMessage ("SlowPlayer");
			}
			item2 = -1;
			itemStolenFrom2 = -1;
		} else if (item2 == 3) {
			GameObject asteroid = Instantiate<GameObject>(asteroidPrefab);
			asteroid.transform.position = transform.position - (transform.forward * 4);
			item2 = -1;
			itemStolenFrom2 = -1;
		} else if (item2 == 4) {
			GameObject[] player1;
			player1 = GameObject.FindGameObjectsWithTag ("Player");

			foreach (GameObject player in player1) {
				player.GetComponent<Rigidbody> ().useGravity = false;
				StartCoroutine (EnableGravity (player, 5f));
			}
			item2 = -1;
			itemStolenFrom2 = -1;
		} else if (item2 == 5) {
			if (itemStolenFrom1 != -1) {
				GameObject[] player2;
				player2 = GameObject.FindGameObjectsWithTag ("Player");

				foreach (GameObject player in player2) {
					player.gameObject.SendMessage ("StealItemFromPlayer1");
				}
					
				item2 = itemStolenFrom1;
				Debug.Log ("Powerup used, stolen item num is: " + item2);
				itemStolenFrom1 = -1;
			}
		}
	}

	void RevertSpeed(){
		movementSpeed = 30;
	}

	void SlowPlayer(){
		movementSpeed = 10;
		Invoke ("RevertSpeed", 3f);
	}

	void StealItemFromPlayer2(){
		item2 = -1;
	}

	void StealItemFromPlayer1(){
		item1 = -1;
	}

	IEnumerator EnableGravity(GameObject player, float delayTime){
		yield return new WaitForSeconds (delayTime);
		player.GetComponent<Rigidbody> ().useGravity = true;
	}

	public int returnPlayer1Item(){
		return item1;
	}

	public int returnPlayer2Item(){
		return item2;
	}

    public void ChangeGravity(Vector3 newGrav, Vector3 newDirection) {
        this.rb.useGravity = false;
        this.gravChanged = true;
        this.grav = newGrav;
        this.followCamera.GetComponent<FollowCamera_alt>().changeUp(Vector3.Scale(new Vector3(-1, -1, -1), newGrav).normalized);
        this.transform.rotation = Quaternion.Euler(newDirection);
        this.rb.velocity = Vector3.zero;
    }
}
