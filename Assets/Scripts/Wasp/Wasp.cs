using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] float leftPatrolDistance;
    [SerializeField] float rightPatrolDistance;
    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] float playerCheckRadius;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float attackDelay = 5.0f;
    [SerializeField] Stinger stingerPrefab;
    [SerializeField] Transform stingerOrigin;
    #endregion

    #region Component Variables
    public Animator Animator { get; private set; }
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public WaspIdleState IdleState { get; private set; }
    public WaspAttackState AttackState { get; private set; }
    #endregion

    #region Movement Variables
    public Vector3[] patrolLocations;
    private int patrolIndex;
    private int facingDirection;
    #endregion

    #region Game Variables
    public Player Player { get; private set; }
    #endregion

    private GameObject beeBuzzSfxGameObject;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        patrolLocations = new Vector3[2];
        IdleState = new WaspIdleState(this, "idle");
        AttackState = new WaspAttackState(this, "attack");
        StateMachine.Initialize(IdleState);
        Player = FindObjectOfType<Player>();
        facingDirection = 1;
        patrolIndex = 0;
        beeBuzzSfxGameObject = AudioManager.instance.PlayLoopingSoundEffectAtPoint("BeeBuzz", transform.position);

        CalculatePatrolLocations();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        beeBuzzSfxGameObject.transform.position = transform.position;
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        Destroy(beeBuzzSfxGameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);

        Gizmos.color = Color.yellow;
        // Patrol locations
        Gizmos.DrawRay(transform.position, Vector3.left * leftPatrolDistance);
        Gizmos.DrawRay(transform.position, Vector3.right * rightPatrolDistance);
    }

    private void CalculatePatrolLocations()
    {
        patrolLocations[0] = new Vector3(transform.position.x - leftPatrolDistance, transform.position.y, transform.position.z);
        patrolLocations[1] = new Vector3(transform.position.x + rightPatrolDistance, transform.position.y, transform.position.z);
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public float GetPlayerCheckRadius()
    {
        return playerCheckRadius;
    }

    public LayerMask GetPlayerLayer()
    {
        return playerLayer;
    }

    public float GetAttackDelay()
    {
        return attackDelay;
    }

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    public Stinger GetStingerPrefab()
    {
        return stingerPrefab;
    }

    public Transform GetStingerOrigin()
    {
        return stingerOrigin;
    }

    public void FlipIfNeeded(int facingDirection)
    {
        if (facingDirection != this.facingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void PatrolNext()
    {
        patrolIndex++;

        if (patrolIndex >= patrolLocations.Length)
        {
            patrolIndex = 0;
        }
    }

    public Vector3 GetCurrentPatrolLocation()
    {
        return patrolLocations[patrolIndex];
    }

    public void FireStinger()
    {
        Vector3 playerPosition = Player.transform.position;

        // Fire 2 shots that go around the player
        Stinger stinger = Instantiate(GetStingerPrefab(), GetStingerOrigin().position, Quaternion.identity);
        Stinger stinger2 = Instantiate(GetStingerPrefab(), GetStingerOrigin().position, Quaternion.identity);

        stinger.FireAt(new Vector3(playerPosition.x + 2, playerPosition.y, playerPosition.z));
        stinger2.FireAt(new Vector3(playerPosition.x - 2, playerPosition.y, playerPosition.z));

        AudioManager.instance.PlaySoundEffectAtPoint("BeeShoot", GetStingerOrigin().position);
    }
}
