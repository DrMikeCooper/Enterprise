using UnityEngine;
using System.Collections;

public class WanderAction : Action {

    Vector3 target;

    public override float Evaluate(Agent a)
    {
        return 0.5f;
    }

    public override void UpdateAction(Agent agent)
    {
        agent.SetTarget(target);
        if ((agent.transform.position - target).magnitude < 1.0f)
            RandomiseTarget();
    }

    public override void Enter(Agent agent)
    {
        RandomiseTarget();
    }

    public override void Exit(Agent agent)
    {
    }

    void RandomiseTarget()
    {
        target.x = Random.Range(-10, 10);
        target.y = 2;
        target.z = Random.Range(-10, 10);
    }
}
