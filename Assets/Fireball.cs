﻿using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Reposition();
	}

    void Reposition()
    {
        int index = Random.Range(0,Need.needs.Count-1);
        float dx = Random.Range(0, 50) * 0.1f;
        float dy = Random.Range(0, 50) * 0.1f;

        transform.position = (Need.needs[index] as Need).transform.position + new Vector3(dx, 20, dy);
    }

	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.y -= 0.1f;
        transform.position = pos;

        if (pos.y < 0)
        {
            foreach (Need n in Need.needs)
            {
                if ((n.transform.position - transform.position).magnitude < 3.0f)
                    n.urgency = 100;
            }
            Reposition();
        }        
	}
}
