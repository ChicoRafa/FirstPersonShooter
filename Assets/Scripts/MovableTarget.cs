using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the movable target logic
/// </summary>
public class MovableTarget : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] path;
    private int currentPoint = 0;
    public float stopDistance = 0.05f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();
    }

    private void MoveTarget()
    {
        //moves target from point A to point B at a certain speed
        transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, speed * Time.deltaTime);
        //this logic updates the points or makes de target go back to the first one
        if (Vector3.Distance(transform.position, path[currentPoint].position) < stopDistance)
        {
            if (currentPoint == path.Length - 1)
            {
                currentPoint = 0;
            }
            else
            {
                currentPoint++;
            }
        }
    }

    /// <summary>
    /// This method draws a line to see the target's route inside the scene inspector
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        for (int i = 0; i < path.Length; i++)
        {
            if (path[i] != null)
            {
                if (i == 0)
                {
                    Gizmos.DrawLine(path[i].position, path[path.Length - 1].position);
                }
                else
                {
                    Gizmos.DrawLine(path[i].position, path[i - 1].position);
                }
            }
        }
    }
}
