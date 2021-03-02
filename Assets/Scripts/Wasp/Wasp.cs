using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    [SerializeField] float leftPatrolDistance;
    [SerializeField] float rightPatrolDistance;
    [SerializeField] float movementSpeed = 5.0f;

    #region Component Variables
    public Animator Animator { get; private set; }
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public WaspIdleState IdleState { get; private set; }
    #endregion

    #region Movement Variables
    public Vector3[] PatrolLocations { get; private set; }
    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        PatrolLocations = new Vector3[2];
        calculatePatrolLocations();
        IdleState = new WaspIdleState(this, "idle");
        StateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Patrol locations
        Gizmos.DrawRay(transform.position, Vector3.left * leftPatrolDistance);
        Gizmos.DrawRay(transform.position, Vector3.right * rightPatrolDistance);
    }

    private void calculatePatrolLocations()
    {
        PatrolLocations[0] = new Vector3(transform.position.x - leftPatrolDistance, transform.position.y, transform.position.z);
        PatrolLocations[1] = new Vector3(transform.position.x + leftPatrolDistance, transform.position.y, transform.position.z);
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
}
