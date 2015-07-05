using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MoveShip();
	}

	void MoveShip () {
		Vector3 shipPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		float movement = speed * Time.deltaTime;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			shipPos.x -= movement;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			shipPos.x += movement;
		}

		this.transform.position = shipPos;
	}
}
