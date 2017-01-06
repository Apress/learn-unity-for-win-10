using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {
	public float jumpStrength = 1000000f;
	Rigidbody theRigidbody;
	float jumpRate = 0.5F;
	float nextJump = 0.0F;
	bool isGrounded = false;
	public float boostRate = 0.4F;
	float nextBoost = 0.0F;
	Vector3 currentVelocity; 
	float boostStrength = 5000f;
	ParticleSystem particles;

	// Use this for initialization
	void Start () {
		theRigidbody = GetComponent<Rigidbody>();
		particles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		currentVelocity = theRigidbody.velocity;
		if (Input.GetButton("Boost") && Time.time > nextBoost) {
			Boost ();
		}

		if (Input.GetButton("Jump") && Time.time > nextJump && isGrounded){
			isGrounded = false; // prevent jumping if in air
			nextJump = Time.time + jumpRate;
			theRigidbody.AddForce(Vector3.up * jumpStrength);
		}

	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	public void Boost () {
		nextBoost = Time.time + boostRate; // set timer
		particles.Play();
		theRigidbody.AddForce(currentVelocity * boostStrength, ForceMode.Impulse);
	}

}
