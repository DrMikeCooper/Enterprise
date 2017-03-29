using UnityEngine;
using System.Collections;

public class GoToTargetAction : Action
{
    public GameObject target;

    public Agent.Need needType;
    bool close = false;

    public override float Evaluate(Agent agent)
    {
        close = (agent.transform.position - target.transform.position).magnitude < 1;
  
        float val = agent.m_needs[(int)needType];

        // extra term to keep them close by and drinking/eating
        //if (close && agent.m_needs[(int)needType] > 0)
        //    val += 1.0f;

        return val;
        //return Input.GetKey(KeyCode.P) ? 1 : 0;
    }

    public override void UpdateAction(Agent agent)
    {
        agent.SetTarget(target.transform.position);

        // eat or drink when close enough
        if (close)
        {
            agent.m_needs[(int)needType] -= 0.1f *Time.deltaTime;
            //if (agent.m_needs[(int)needType] < 0)
            //   agent.m_needs[(int)needType] = -0.5f;
        }
    }

    public override void Enter(Agent agent)
    {
    }

    public override void Exit(Agent agent)
    {
    }
}
