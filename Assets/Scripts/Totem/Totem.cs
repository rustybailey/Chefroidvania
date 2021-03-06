using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowOrigin;
    [SerializeField] public float startUpTime = 0f;

    public Animator Animator { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public TotemIdleState idleState;
    public TotemWarningState warningState;
    public TotemAttackState attackState;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        idleState = new TotemIdleState(this, "idle");
        warningState = new TotemWarningState(this, "warning");
        attackState = new TotemAttackState(this, "attack");
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

    public void FireArrow()
    {
        Instantiate(arrowPrefab, arrowOrigin.transform.position, transform.rotation);
        AudioManager.instance.PlaySoundEffectAtPoint("TotemShoot", transform.position);
    }
}
