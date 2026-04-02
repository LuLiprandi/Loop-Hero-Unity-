using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GarryIAControlller : MonoBehaviour
{
    public enum StateType { Patrol, Follow, Scream, Catch }

    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private SightPerception _sightPerception;

    [Header("Speeds")]
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _followSpeed = 5f;

    [Header("Distances")]
    [SerializeField] private float _catchDistance  = 1.2f;
    [SerializeField] private float _screamDistance = 5f;

    [Header("Timings")]
    [SerializeField] private float _screamDuration       = 2f;
    [SerializeField] private float _catchAnimDuration    = 1.5f;
    [SerializeField] private float _waypointWaitDuration = 1f;


    private static readonly int SpeedHash  = Animator.StringToHash("Speed");
    private static readonly int CatchHash  = Animator.StringToHash("Catch");
    private static readonly int ScreamHash = Animator.StringToHash("Scream");

    private NavMeshAgent    _agent;
    private Animator        _animator;
    private MiniGameManager _miniGameManager;

    private StateType _currentState = StateType.Patrol;
    private int       _waypointIndex = 0;
    private bool      _locked = false;     
    private bool      _waitingAtWaypoint = false;

    private void Awake()
    {
        _agent           = GetComponent<NavMeshAgent>();
        _animator        = GetComponent<Animator>();
        _miniGameManager = FindFirstObjectByType<MiniGameManager>();
    }

    private void Start()
    {
        EnterState(StateType.Patrol);
    }

    private void Update()
    {
        if (_locked) return;

        EvaluateTransitions();
        ExecuteCurrentState();
    }


    private void EvaluateTransitions()
    {
        bool   detected      = _sightPerception.isDetected;
        float  distToPlayer  = Vector3.Distance(transform.position, _player.position);

        switch (_currentState)
        {
            case StateType.Patrol:
                if (!detected) break;
                if (distToPlayer <= _catchDistance)  { EnterState(StateType.Catch);  return; }
                if (distToPlayer <= _screamDistance) { EnterState(StateType.Scream); return; }
                EnterState(StateType.Follow);
                break;

            case StateType.Follow:
                if (!detected)                       { EnterState(StateType.Patrol); return; }
                if (distToPlayer <= _catchDistance)  { EnterState(StateType.Catch);  return; }
                break;
        }
    }

    private void ExecuteCurrentState()
    {
        switch (_currentState)
        {
            case StateType.Patrol: PatrolBehaviour(); break;
            case StateType.Follow: FollowBehaviour(); break;
        }

        _animator.SetFloat(SpeedHash, _agent.velocity.magnitude);
    }

    private void EnterState(StateType newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case StateType.Patrol:
                _agent.speed      = _patrolSpeed;
                _agent.isStopped  = false;
                break;

            case StateType.Follow:
                _agent.speed      = _followSpeed;
                _agent.isStopped  = false;
                break;

            case StateType.Scream:
                StartCoroutine(ScreamRoutine());
                break;

            case StateType.Catch:
                StartCoroutine(CatchRoutine());
                break;
        }
    }


    private void PatrolBehaviour()
    {
        if (_waitingAtWaypoint || _waypoints.Length == 0) return;

        _agent.SetDestination(_waypoints[_waypointIndex].position);

        if (!_agent.pathPending && _agent.remainingDistance < 0.3f)
            StartCoroutine(WaypointWait());
    }

    private void FollowBehaviour()
    {
        _agent.SetDestination(_player.position);
    }



    private IEnumerator WaypointWait()
    {
        _waitingAtWaypoint = true;
        _agent.isStopped   = true;

        yield return new WaitForSeconds(_waypointWaitDuration);

        _waypointIndex     = (_waypointIndex + 1) % _waypoints.Length;
        _agent.isStopped   = false;
        _waitingAtWaypoint = false;
    }

    private IEnumerator ScreamRoutine()
    {
        _locked          = true;
        _agent.isStopped = true;
        _animator.SetFloat(SpeedHash, 0f);
        _animator.SetTrigger(ScreamHash);

        yield return new WaitForSeconds(_screamDuration);

        _locked = false;
        EnterState(StateType.Follow);
    }

    private IEnumerator CatchRoutine()
    {
        _locked          = true;
        _agent.isStopped = true;
        _animator.SetFloat(SpeedHash, 0f);
        _animator.SetTrigger(CatchHash);

        if (_player.TryGetComponent(out AvaController avaController))
            avaController.SetMovement(false);

        yield return new WaitForSeconds(_catchAnimDuration);

        _miniGameManager?.OnPlayerCaught();
    }
}
