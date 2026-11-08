using UnityEngine;

//Code from:
//https://youtu.be/WsPkNGEdYxs?si=_yOlk9e1M-WgceBn
public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] public Transform target;
    void Update()
    {
        if (target == null) return;
        var targetPosition = target.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
    }
}
