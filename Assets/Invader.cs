using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {

    Need need;
    Crew target;
    NavMeshAgent nv;

	// Use this for initialization
	void Start ()
    {
        need = GetComponent<Need>();
        need.urgency = 100;
        nv = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (need.attendee != null && (need.attendee.transform.position - transform.position).magnitude < 2.0f)
        {
            // we're close to someone fighting us. They'll inflict damage and stop us.
        }
        else
        {
            // find the closest non-security and follow
            Crew target = null;
            float closestDist = 1000000;
            foreach (Crew c in Crew.crew)
            {
                float dist = (c.transform.position - transform.position).magnitude / c.skills[2];
                if (dist < closestDist)
                {
                    closestDist = dist;
                    target = c;
                }
            }
            if (target)
                nv.SetDestination(target.transform.position);
        }

        // respawn on death
        if (need.urgency <= 0)
        {
            need.urgency = 100;
            int index = Random.Range(0, 3);
            float angle = index * 1.57f;
            transform.position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        }
	}
}
