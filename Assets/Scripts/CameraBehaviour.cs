using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public Vector3 camOffset = new Vector3(0, 5, -5);
    private GameObject m_pDiscoPete;

	// Use this for initialization
	void Start () {
        m_pDiscoPete = GameObject.FindWithTag("DiscoPete");
    }
	
	// Update is called once per frame
	void Update () {
        // Update the position of the camera depending on disco pete

        Vector3 vPositionOfDiscoPete = m_pDiscoPete.transform.position;

        // Take the x of disco pete
        // Take the z of disco pete minus some offset
        // Y remains fixed
        gameObject.transform.position = new Vector3(vPositionOfDiscoPete.x, 0f, vPositionOfDiscoPete.z) + camOffset;
	}
}
