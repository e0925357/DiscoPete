using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBallRotator : MonoBehaviour {

	public float rotationSpeed = 22.5f;

	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0.0f, rotationSpeed * Time.deltaTime, 0.0f));
	}
}
