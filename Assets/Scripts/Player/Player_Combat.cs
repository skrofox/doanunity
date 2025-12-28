using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter Attack Details")]
    [SerializeField] private float CounterRecovery = .1f;
    [SerializeField] private LayerMask whatIsCounterable;

    public bool CounterAttackPerformed()
    {
        bool hasPerformedCounter = false;

        foreach (var target in GetDetectedColliders(whatIsCounterable))
        {
            ICouterable couterable = target.GetComponent<ICouterable>();

            if(couterable == null)
                continue;

            if (couterable.CanBeCounter)
            {
                couterable.HandleCouter();
                hasPerformedCounter = true;
            }
        }

        return hasPerformedCounter;
    }

    public float GetCounterRecoveryDuration() => CounterRecovery;
}
