using UnityEngine;
using System.Collections;

public class BarGraph : MonoBehaviour {

    public Agent.Need need;
    public Color color;

    Agent agent;

	// Use this for initialization
	void Start () {
        Transform t = transform;
        while(agent == null && t!=null)
        {
            agent = t.GetComponent<Agent>();
            t = t.parent;
        }
        GetComponent<MeshRenderer>().material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
        if (agent)
        {
            Vector3 scale = transform.localScale;
            scale.y = agent.m_needs[(int)need];
            transform.localScale = scale;
        }
	}
}
