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
        Object.Destroy(icicle.gameObject.GetComponentInChildren<DamageDealer>());
        icicle.gameObject.layer = LayerMask.NameToLayer("Platform");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
