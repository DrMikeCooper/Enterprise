using UnityEngine;
using System.Collections;

public class Need : MonoBehaviour {

    public static ArrayList needs = new ArrayList();
    public static ArrayList consoles = new ArrayList();
    static GameObject meterPrefab = null;

    public Crew attendee;
    public float urgency;
    public int index; // what type this is - 0 - engineering, 1 = medical, 2 = security

    Transform meter;
    LineRenderer line;
    Vector3[] pts = new Vector3[2];

    // Use this for initialization 
    void Start () {
        needs.Add(this);
        if (GetComponent<Crew>() == null)
            consoles.Add(this);
        urgency = 0;
        if (meterPrefab == null)
            meterPrefab = Resources.Load<GameObject>("meter");
        meter = Instantiate<GameObject>(meterPrefab).transform;
        meter.parent = transform;
        meter.localPosition = new Vector3(0, 1, 0);

        line = gameObject.AddComponent<LineRenderer>();
        line.SetColors(Color.red, Color.red);
        line.SetWidth(0.2f, 0.2f);
        line.material = Resources.Load<Material>("Red");
         
	}
	
	// Update is called once per frame
	void Update () {

        if (urgency < 5)
            urgency -= 0;

        // update urgency
        /*if (urgency == 0 && Random.Range(1, 1000)==1)
        {
            urgency = 100;
        }*/

        meter.transform.localScale = new Vector3(0.2f, urgency * 0.01f, 0.2f);

        if (attendee)
        {
            pts[0] = transform.position + Vector3.up;
            pts[1] = attendee.transform.position + Vector3.up;
            line.SetPositions(pts);
            line.enabled = true;
        }
        else
            line.enabled = false;
	}
}
