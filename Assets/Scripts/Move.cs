using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public int MoveSpeed = 5;
//	private KeyCode[] inputKeys;
//	private Vector3[] moveDirection;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame 
	// 上下左右前后
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.W)) {
			this.transform.Translate (Vector3.up * Time.deltaTime * MoveSpeed, Space.World);
		}

		if (Input.GetKey(KeyCode.A)) {
			this.transform.Translate (Vector3.left * Time.deltaTime * MoveSpeed, Space.World);
		}

		if (Input.GetKey(KeyCode.S)) {
			this.transform.Translate (Vector3.down * Time.deltaTime * MoveSpeed, Space.World);
		}

		if (Input.GetKey(KeyCode.D)) {
			this.transform.Translate (Vector3.right * Time.deltaTime * MoveSpeed, Space.World);
		}

		if (Input.GetKey(KeyCode.Q)) {
			this.transform.Translate (Vector3.forward * Time.deltaTime * MoveSpeed, Space.World);
		}

		if (Input.GetKey(KeyCode.E)) {
			this.transform.Translate (Vector3.back * Time.deltaTime * MoveSpeed, Space.World);
		}
	}
}
