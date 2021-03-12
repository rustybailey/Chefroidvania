using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleLandState : IcicleState
{
    public IcicleLandState(Icicle icicle, string animationBooleanName) : base(icicle, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffectAtPoint("IcicleCrash", icicle.transform.position);
        icicle.Rigidbody.isKinematic = true;
        icicle.Collider.isTrigger = false;
        Object.Destroy(icicle.gameObject.GetComponent<DamageDealer>());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // TODO: Is there even a way to exit this state?
        // TODO: Take away damage dealer script after < 1 second
    }
}
