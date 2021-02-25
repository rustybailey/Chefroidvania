using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPan : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] float throwSpeed = 5.0f;
    [SerializeField] float throwDistance = 10.0f;
    #endregion

    #region Component Variables
    public Animator Animator { get; private set; }
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public FryingPanState idleState;
    public FryingPanThrowState throwState;
    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        idleState = new FryingPanState(this, "idle");
        throwState = new FryingPanThrowState(this, "throw");
        StateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThrowFromLocation(Vector3 position)
    {
        transform.position = position;
    }
}
