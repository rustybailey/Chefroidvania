using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPan : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] float throwSpeed = 10.0f;
    [SerializeField] float throwDistance = 3.0f;
    #endregion

    #region Component Variables
    public Animator Animator { get; private set; }
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public FryingPanState idleState;
    public FryingPanThrowState throwState;
    public FryingPanReturnState returnState;
    #endregion

    #region Movement Variables
    public int FacingDirection { get; private set; }
    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        Animator = GetComponent<Animator>();
        FacingDirection = 1;
        idleState = new FryingPanState(this, player, "idle");
        throwState = new FryingPanThrowState(this, player, "throw");
        returnState = new FryingPanReturnState(this, player, "throw");
        StateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public float GetThrowSpeed()
    {
        return throwSpeed;
    }

    public float GetThrowDistance()
    {
        return throwDistance;
    }

    public void FlipIfNeeded(int facingDirection)
    {
        if (facingDirection != this.FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
