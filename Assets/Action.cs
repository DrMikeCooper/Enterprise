using UnityEngine;
using System.Collections;

public abstract class Action : MonoBehaviour {
    public abstract float Evaluate(Agent a);
    public abstract void UpdateAction(Agent agent);
    public abstract void Enter(Agent agent);
    public abstract void Exit(Agent agent);

    public float urgency;
}
