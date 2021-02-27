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
    public FryingPanHiddenState HiddenState { get; private set; }
    public FryingPanThrowState ThrowState { get; private set; }
    public FryingPanReturnState ReturnState { get; private set; }
    public FryingPanHoverState HoverState { get; private set; }
    public bool IsHovering { get; private set; }
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
        HiddenState = new FryingPanHiddenState(this, player, "hover");
        ThrowState = new FryingPanThrowState(this, player, "throw");
        ReturnState = new FryingPanReturnState(this, player, "throw");
        HoverState = new FryingPanHoverState(this, player, "hover");
        StateMachine.Initialize(HiddenState);
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

    public void StartHovering()
    {
        IsHovering = true;
    }

    public void StopHovering()
    {
        IsHovering = false;
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
