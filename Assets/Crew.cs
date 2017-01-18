using UnityEngine;
using System.Collections;

public class Crew : MonoBehaviour {

    static public ArrayList crew = new ArrayList();

    Need damage;
    Need currentNeed;

    NavMeshAgent nv;

    LineRenderer line;
    Vector3[] pts = new Vector3[2];

    // Use this for initialization
    void Start ()
    {
        crew.Add(this);

        nv = GetComponent<NavMeshAgent>();
        damage = GetComponent<Need>();

        GameObject lineChild = new GameObject();
        lineChild.transform.parent = transform;

        line = lineChild.AddComponent<LineRenderer>();
        line.SetColors(Color.red, Color.red);
        line.SetWidth(0.2f, 0.2f);
        line.material = Resources.Load<Material>("Black");
    }

    public int[] skills = new int[3];

    float getScore(Crew crew, Need need)
    {
        float distance = (need.transform.position - crew.transform.position).magnitude;
        float distanceFactor = 1.0f / distance;
        return need.urgency * distanceFactor * crew.skills[need.index];
    }

    // Update is called once per frame
	void Update ()
    {
        float topScore = 0;
        Need topNeed = null;

        // evaluate all needs nearby and find the best one if undamaged
        if (damage.urgency <= 0)
        {
            damage.urgency = 0;
            foreach (Need n in Need.needs)
            {
                if (n != damage)
                {
                    float score = getScore(this, n);
                    if (n.attendee && getScore(n.attendee, n) > score)
                        score = 0; // already being seen to!
                    
                    if (n == currentNeed)
                        score *= 2;

                     if (score > topScore)
                    {
                        topScore = score;
                        topNeed = n;
                    }
                }
            }
        }
        else
        {
            damage.urgency-=0.5f; // slow healing
        }

        if (currentNeed != topNeed)
        {
            // detach from our old need
            if (currentNeed && currentNeed.attendee == this)
                currentNeed.attendee = null;

            currentNeed = topNeed;
            if (currentNeed)
                currentNeed.attendee = this;

            nv.SetDestination(topNeed ? topNeed.transform.position : transform.position);
        }

        if (currentNeed)
        {
            float distance = (currentNeed.transform.position - transform.position).magnitude;
            if (distance < 1.0f)
            {
                nv.SetDestination(transform.position);
                currentNeed.urgency -= skills[currentNeed.index] * 0.1f;
            }

            pts[0] = transform.position + 0.5f * Vector3.up;
            pts[1] = currentNeed.transform.position + 0.5f * Vector3.up;
            line.SetPositions(pts);
            line.enabled = true;

        }
        else
            line.enabled = false; 
	}
}
