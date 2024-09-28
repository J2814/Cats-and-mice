using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum MovementType
    {
        moveing, // постоянное движение 
        jerk    // рывок
    }

    public MovementType type = MovementType.moveing;
    public MovementPath myPath;
    public float speed = 1f;
    public float maxDistance = .1f; // на какое расстояние должен подъехать объект к точке, чтобы понять что это точка.

    private IEnumerator<Transform> _pointInPath;  // проверка точек

    // Start is called before the first frame update
    void Start()
    {
        if (myPath == null)  // проверка на наличие пути
        {
            Debug.Log("Примени путь");
            return;  
        }

        _pointInPath = myPath.GetNextPathPoint();
        _pointInPath.MoveNext();           // получаем информацию о следующей точки в пути

        if(_pointInPath.Current == null)
        {
            Debug.Log("Нужны точки");
            return;
        }

        transform.position = _pointInPath.Current.position; // объект встает на стартовую точку пути
    }

    // Update is called once per frame
    void Update()
    {
        if(_pointInPath == null || _pointInPath.Current == null)
        {
            return;
        }

        if(type == MovementType.moveing)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointInPath.Current.position, Time.deltaTime * speed);
        }
        else if(type == MovementType.jerk)
        {
            transform.position = Vector3.Lerp(transform.position, _pointInPath.Current.position, Time.deltaTime * speed);
        }

        float distannceSqure = (transform.position - _pointInPath.Current.position).sqrMagnitude;

        if(distannceSqure < maxDistance * maxDistance)  // достаточно близко к точки?
        {
            _pointInPath.MoveNext();
        }
    }
}
