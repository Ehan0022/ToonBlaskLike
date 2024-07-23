using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAnimationBlock : MonoBehaviour
{
    public Transform reachingPoint;
    public float distance;
    public float fallTime;
    void Start()
    {
        distance = Vector3.Distance(transform.position, reachingPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        GoToPoint(fallTime);
    }

    public void GoToPoint(float fallTime)
    {
        transform.position = Vector3.MoveTowards(transform.position, reachingPoint.position, (distance / fallTime)*Time.deltaTime);          
    }

    public void SetAttributes(float fallTime, Transform reachingPoint)
    {
        this.reachingPoint = reachingPoint;
        this.fallTime = fallTime;
    }
}
