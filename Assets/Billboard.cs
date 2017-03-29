using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fwd = Camera.main.transform.position - transform.position;
        fwd.y = 0;
        fwd.Normalize();
        transform.right = fwd;
	}
}
