using UnityEngine;

public class MovementController : MonoBehaviour
{
    public MovementPath myPath;
    public Transform CurrentPoint;
    [SerializeField]
    private bool moveForward;
    private int nextPointIndex;
    public float Speed;
    public float MaxDistance = 0.1f;

    public bool AllowMovement = true;
    private void OnEnable()
    {
        LevelManager.PreEndLevel += StopMoving;
    }

    private void OnDisable()
    {
        LevelManager.PreEndLevel -= StopMoving;
    }

    private void StopMoving()
    {
        AllowMovement = false;
    }
    private void Update()
    {
        UpdateMovement();
    }
    public void UpdateMovement()
    {
        if (!AllowMovement) { return; }

        if(CurrentPoint == null)
        {
            Debug.Log("Current point is null");
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, CurrentPoint.position, Time.deltaTime * Speed);

        float distanceSquared = (transform.position - CurrentPoint.position).sqrMagnitude;

        if (distanceSquared < MaxDistance * MaxDistance)
        {
            NextMove();
        }
    }

    public void NextMove()
    {
        bool transitionFound = false;
        if (moveForward)
        {
            //move forward logic
            if (CurrentPoint != myPath.PathElements[myPath.PathElements.Length - 1]) { CurrentPoint = GetNextPathPoint(); return; }
            foreach (MovementPath.Connection conn in myPath.Connections)
            {
                if (conn.ConnectionType == MovementPath.ConnectionTypeEnum.EndToStart && conn.path != null && conn.path.isActiveAndEnabled)
                {
                    Transition(conn.path, true);
                    transitionFound = true;
                }
                if (conn.ConnectionType == MovementPath.ConnectionTypeEnum.EndToEnd && conn.path != null && conn.path.isActiveAndEnabled)
                {
                    Transition(conn.path, false);
                    transitionFound = true;
                }
            }

            if (!transitionFound)
            {
                foreach (MovementPath.Connection conn in myPath.FallbackConnections)
                {
                    if (conn.ConnectionType == MovementPath.ConnectionTypeEnum.EndToStart && conn.path != null && conn.path.isActiveAndEnabled)
                    {
                        Transition(conn.path, true);
                        transitionFound = true;
                    }
                    if (conn.ConnectionType == MovementPath.ConnectionTypeEnum.EndToEnd && conn.path != null && conn.path.isActiveAndEnabled)
                    {
                        Transition(conn.path, false);
                        transitionFound = true;
                    }
                }
            }
        }
        else
        {
            //move backward logic
            if (CurrentPoint != myPath.PathElements[0]) { CurrentPoint = GetNextPathPoint(); return; }
            foreach (MovementPath.Connection conn in myPath.Connections)
            {
                if (conn.ConnectionType == MovementPath.ConnectionTypeEnum.StartToEnd && conn.path != null && conn.path.isActiveAndEnabled)
                {
                    Transition(conn.path, false);
                    transitionFound = true;
                }
                if (conn.ConnectionType == MovementPath.ConnectionTypeEnum.StartToStart && conn.path != null && conn.path.isActiveAndEnabled)
                {
                    Transition(conn.path, true);
                    transitionFound = true;
                }
            }

            if (!transitionFound)
            {
                foreach (MovementPath.Connection conn in myPath.FallbackConnections)
                {
                    if (conn.ConnectionType == MovementPath.ConnectionTypeEnum.StartToEnd && conn.path != null && conn.path.isActiveAndEnabled)
                    {
                        Transition(conn.path, false);
                        transitionFound = true;
                    }
                    if (conn.ConnectionType == MovementPath.ConnectionTypeEnum.StartToStart && conn.path != null && conn.path.isActiveAndEnabled)
                    {
                        Transition(conn.path, true);
                        transitionFound = true;
                    }
                }
            }
        }
    }

    public void Transition(Transform newPathFirstPoint)
    {
        myPath = newPathFirstPoint.GetComponentInParent<MovementPath>();
        if (myPath == null)
        {
            Debug.Log("Recieved point has no MovementPath");
            return;
        }


        if (myPath.PathElements[0] == newPathFirstPoint)
        {
            moveForward = true;
            nextPointIndex = 0;
        }
        else
        {
            moveForward = false;
            nextPointIndex = myPath.PathElements.Length - 1;

        }

        CurrentPoint = newPathFirstPoint;
    }

    public void Transition(MovementPath mp, bool forward)
    {
        //Debug.Log(mp.gameObject.name);
        myPath = mp;
        if (forward)
        {
            moveForward = true;
            nextPointIndex = 0;
            CurrentPoint = mp.PathElements[0];
        }
        else
        {
            moveForward = false;
            nextPointIndex = mp.PathElements.Length - 1;
            CurrentPoint = mp.PathElements[mp.PathElements.Length - 1];
        }
        
    }

    private Transform GetNextPathPoint()
    {
        if (moveForward == true)
        {
            nextPointIndex++;
        }
        else
        {
            nextPointIndex--;
        }

        return myPath.PathElements[nextPointIndex];
    }


}
