using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEntity : MonoBehaviour {
    internal IThinkBehaviour thinkBehaviour;
    internal Dictionary <BehaviourEnum, IEntityBehaviour> entityBehaviours;
    internal Action think;
    internal Animator animator;

    private Vector3 previousPosition;
    private float CurrentSpeed;
    internal bool Running = false;
    public float WalkSpeed = 2f;
    public float RunSpeed = 4f;

    public float directionChangeInterval;
    public float maxHeadingChange;
    public CharacterController controller;
    public float heading;
    public Vector3 targetRotation;

    internal Transform target;
    public float LoseSightTimer = 5.0f;
    internal float timeTillLoseSight;
    internal NavMeshAgent NavAgent;

    public float WanderRadius = 25;

    public GameObject[] waypoints;
    public int wayPointInd;

    public bool RandomWander;
    internal FieldOfView fieldOfView;

    protected virtual void Start() {
        think = new Think(this);
        entityBehaviours = new Dictionary<BehaviourEnum, IEntityBehaviour>();
        NavAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        NavAgent.speed = WalkSpeed;
    }

    protected virtual void Update() {
        if (fieldOfView.VisibleTargets.Count > 0) {
            target = fieldOfView.VisibleTargets[0];
            timeTillLoseSight = LoseSightTimer;
        } else {
            if (target != null && timeTillLoseSight <= 0.0f) {
                target = null;
            } else if (timeTillLoseSight > 0.0f){
                timeTillLoseSight -= Time.deltaTime;
            }
        }
        think.Process();

        if (animator.GetBool("Moving")) {
            MovementAnimation();
        }
        else {
            StopMovement();
        }
    }

    public void MovementAnimation() {
        NavAgent.speed = Running ? RunSpeed : WalkSpeed;
        Vector3 curMove = transform.position - previousPosition;
        CurrentSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
        float animationMoveSpeedPercent = ((Running) ? CurrentSpeed / RunSpeed : CurrentSpeed / WalkSpeed * .5f);
        animator.SetFloat("MoveSpeed", animationMoveSpeedPercent);
    }

    public void StopMovement() {
        NavAgent.velocity = Vector3.zero;
    }
}