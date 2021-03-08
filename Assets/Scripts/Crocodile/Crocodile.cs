using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
    public Animator Animator { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public CrocodileIdleState idleState;
    public CrocodileOpenState openState;
    public CrocodileOpenIdleState openIdleState;
    public CrocodileCloseState closeState;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        idleState = new CrocodileIdleState(this, "idle");
        openState = new CrocodileOpenState(this, "open");
        openIdleState = new CrocodileOpenIdleState(this, "openIdle");
        closeState = new CrocodileCloseState(this, "close");
        StateMachine.Initialize(idleState);
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    public void PlaySnapSfx()
    {
        AudioManager.instance.PlaySoundEffectAtPoint("CrocSnap", transform.position);
    }
}
