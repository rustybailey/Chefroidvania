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
    public FryingPanHiddenState hiddenState;
    public FryingPanThrowState throwState;
    public FryingPanReturnState returnState;
    public FryingPanHoverState hoverState;
    public bool isHovering;
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
        hiddenState = new FryingPanHiddenState(this, player, "hover");
        throwState = new FryingPanThrowState(this, player, "throw");
        returnState = new FryingPanReturnState(this, player, "throw");
        hoverState = new FryingPanHoverState(this, player, "hover");
        StateMachine.Initialize(hiddenState);
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
