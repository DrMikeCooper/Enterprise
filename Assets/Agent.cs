using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {

    NavMeshAgent nv;
    public enum Need
    {
        Drink,
        Food,
        MaxNeeds
    };
    public float[] m_needs = new float[(int)Need.MaxNeeds];

    // an array of all available actions
    Action[] actions;

    // the actionn we're currently carrying out
    public Action currentAction;

	// Use this for initialization
	void Start () {
        // get all the action-derived classes that are siblings of us
        actions = GetComponents<Action>();

        nv = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        // find the best action each frame (TODO - not every frame?)
        Action best = GetBestAction();

        // if its different from what we were doing, switch the FSM
        if (best != currentAction)
        {
            if (currentAction)
                currentAction.Exit(this);
            currentAction = best;
            if (currentAction)
                currentAction.Enter(this);
        }

        // update the current action
        if (currentAction)
            currentAction.UpdateAction(this);

        // update our needs
        m_needs[0] += 0.02f * Time.deltaTime;
        m_needs[1] += 0.01f * Time.deltaTime;
	}

    // checks all our available actions and evaluates each one, getting the best
    Action GetBestAction()
    {
        Action action = null;
        float bestValue = 0;

        foreach (Action a in actions)
        {
            float value = a.Evaluate(this);

            // stops the agent vacillating between two goals by sticking with its current one.
            if (a == currentAction)
                value += 0.2f;

            a.urgency = value; // for debugging
            if (action == null || value > bestValue)
            {
                action = a;
                bestValue = value;
            }
        }

        return action;
    }

    public void SetTarget(Vector3 pt)
    {
        nv.SetDestination(pt);
    }
}
