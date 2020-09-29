using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed = 10;
	public float tilt = 4;
	public float fireRate = 0.25f;

	private float nextFire = 0;

	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	void Update() {

		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}
	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		float xLimit = Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax);   
		float zLimit = Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax);   
		GetComponent<Rigidbody>().position = new Vector3(xLimit, 0.0f, zLimit);

		float zTilt = GetComponent<Rigidbody>().velocity.x * -tilt;
		GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, zTilt);
	}
}
