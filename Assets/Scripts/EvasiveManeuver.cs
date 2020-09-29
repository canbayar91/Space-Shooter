using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {

	public float dodge = 5;
	public float smoothing = 7.5f;
	public float tilt = 14;

	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

	public Boundary boundary;
	
	private float targetManeuver;
	private float currentSpeed;

	void Start () {
		currentSpeed = GetComponent<Rigidbody>().velocity.z;
		StartCoroutine(Evade());
	}

	IEnumerator Evade() {
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
		while(true) {
			targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
		} 
	}
	
	void FixedUpdate () {
		float maneuver = Mathf.MoveTowards(GetComponent<Rigidbody>().velocity.x, targetManeuver, Time.deltaTime * smoothing);
		GetComponent<Rigidbody>().velocity = new Vector3(maneuver, 0.0f, currentSpeed);

		float xLimit = Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax);   
		float zLimit = Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax); 
		GetComponent<Rigidbody>().position = new Vector3(xLimit, 0.0f, zLimit);

		float zTilt = GetComponent<Rigidbody>().velocity.x * -tilt;
		GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, zTilt);
	}
}
