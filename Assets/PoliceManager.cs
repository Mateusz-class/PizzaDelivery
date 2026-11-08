using UnityEngine;

//Modifed Code from:
//https://youtu.be/4mzbDk4Wsmk?si=n3rBR3uLm8OFYvjc
//https://youtu.be/nJiFitClnKo?si=5FjTxAf6RvP85azN
public class PoliceManager : MonoBehaviour
{
    public enum PoliceState
    {
        None, Patrol, Chase, Return
    }
    public PoliceState policeState;
    public Transform Player;
    public Transform[] patrolPoints;
    public Transform StartChaseLocation;
    public int targetPoint;
    public float CurrentMovementSpeed, PatrolMovementSpeed, ChaseMovementSpeed, rotationSpeed, wait;
    private void Start()
    {
        targetPoint = 0;
        policeState = PoliceState.Patrol;
    }

    private void Update()
    {
        if(policeState == PoliceState.Patrol)
        {
            PatrolMovement();
        }
        else if (policeState == PoliceState.Chase)
        {
            ChaseMovement();
        }
        else if (policeState == PoliceState.Return)
        {
            ReturnMovement();
        }
    }
    private void PatrolMovement()
    {
        if (wait > 0)
        {
            wait -= Time.deltaTime;
        }
        else
        {
            if (transform.position == patrolPoints[targetPoint].position)
            {
                increaseTargetInt();
                return;
            }
            Movement(patrolPoints[targetPoint]);
        }
    }
    public void ChaseMovement()
    {
        Movement(Player);
    }
    public void ReturnMovement()
    {
        if (transform.position == StartChaseLocation.position)
        {
            policeState = PoliceState.Patrol;
            StartChaseLocation = null;
            return;
        }
        Movement(StartChaseLocation);
    }
    public void Movement(Transform target){
            transform.position = Vector3.MoveTowards(transform.position, target.position, CurrentMovementSpeed * Time.deltaTime);
            Vector3 direction = target.position - transform.position;
            if (direction == Vector3.zero) return;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    private void increaseTargetInt()
    {
        if (patrolPoints[targetPoint].gameObject.tag == "Wait")
        {
            wait = 3;
        }
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
    public void ChasePlayer()
    {
        StartChaseLocation = transform;
        CurrentMovementSpeed = ChaseMovementSpeed;
        policeState = PoliceState.Chase;
    }
    public void EndChase()
    {
        policeState = PoliceState.Return;
        CurrentMovementSpeed = PatrolMovementSpeed;
    }
}
