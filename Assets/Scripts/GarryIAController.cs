using System;
using UnityEngine;
using UnityEngine.AI;

public class GarryIAControlller : MonoBehaviour
{
    [SerializeField] private StateType state = StateType.None;
    [SerializeField] private StateType nextState = StateType.None;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject navepoint;
    [SerializeField] private float catchDistance = 1.5f;
    public enum StateType
    {
      None,
      Patrole,
      Follow,
      Catch
    }
    private void Update()
    {
        if (TestChangeState())
        {
            ChangeState();
        }
        BehaviourAction();
    }

/// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool TestChangeState()
    {
        switch (state)
        {
            case StateType.Catch:
                if(!GetComponent<SightPerception>().isDetected)
                {
                    nextState = StateType.Patrole;
                    return true;
                }
                if (Vector3.Distance(target.transform.position, transform.position) > catchDistance)
                {
                    nextState = StateType.Follow;
                    return true;
                }
                break;

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            case StateType.Patrole:
                if(GetComponent<SightPerception>().isDetected)
                {
                    if (Vector3.Distance(target.transform.position, transform.position) <= catchDistance)
                    {
                        nextState = StateType.Catch;
                        return true;

                    }
                    nextState = StateType.Follow;
                    return true;
                }

                break;

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            case StateType.Follow:

                if(!GetComponent<SightPerception>().isDetected)
                {
                    nextState = StateType.Patrole;
                    return true;
                }

                if (Vector3.Distance(target.transform.position, transform.position) <= catchDistance);
                {
                    nextState = StateType.Catch;
                    return true;
                }
                break;
        }
        return false;
    }

 ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    private void ChangeState()
    {
        EndState();
        state = nextState;
        StartState();
    }

    private void StartState()

    {
        switch (state)
        {
            case StateType.Patrole:
                GetComponent<NavMeshAgent>().speed = 2f;
                break;

            case StateType.Follow:
                GetComponent<NavMeshAgent>().speed = 4f;
                break;
        }
    }
    private void EndState()
    {
        switch (state)
        {
            case StateType.Follow:
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
                break;

            case StateType.Patrole:
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
                break;

        }
    }

    private void BehaviourAction()
    {
       switch (state)
        {
            case StateType.Patrole:
                PatroleBehaviour();
                break;

            case StateType.Follow:
                FollowBehaviour();
                break;

            case StateType.Catch:
                CatchBehaviour();
                break;
        }
    }

    private void PatroleBehaviour()
    {
        GetComponent<NavMeshAgent>().SetDestination(navepoint.transform.position);
        GetComponent<Animator>().SetFloat(name: "Speed",GetComponent<NavMeshAgent>().velocity.magnitude);
    }
     private void FollowBehaviour()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        GetComponent<Animator>().SetFloat(name: "Speed", GetComponent<NavMeshAgent>().velocity.magnitude);
    }
    private void CatchBehaviour()
    {
        GetComponent<Animator>().SetTrigger(name: "Catch");

    }


}
