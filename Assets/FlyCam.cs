using UnityEngine;
using System.Collections;

public class FlyCam : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position 
            + (Vector3.up * ((Input.GetKey(KeyCode.Z) ? 1 : 0) - (Input.GetKey(KeyCode.X) ? 1 : 0))
            + transform.right * ((Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0))
            + transform.forward * ((Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0))) * 10.0f * Time.deltaTime;
    }
}
