using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    //What to chase?
    public Transform target;
    //How many times each second we will update our path
    public float updateDelay=1;
    //Caching
    private Seeker seeker;
    private Rigidbody2D rb;

    //The calculate path
    public Path path;
    //The AI's speed per second
    public float speed= 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;
    // The max distacne from  the AI to a waypoint for it continue to the next waypoint
    public float nextWayPointDistance = 2f;
    //The waypoint we are  currently moving towards
    private int currentWayPoint = 0;
    private bool searchingForPlayer = false;

    private void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
           if (!searchingForPlayer)
           {
               searchingForPlayer = true;
               StartCoroutine(SearchForPlayer());
           }
           else
           {
               return;
           }
            return;
        }
        // Start a new path to  the target position, return the result to the OnPathComplate method
        seeker.StartPath(transform.position,target.position,OnPathComplate);
        StartCoroutine(UpdatePath());
    }

    IEnumerator SearchForPlayer(){
        GameObject searchResault = GameObject.FindGameObjectWithTag("Player");
        if (searchResault == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            target = searchResault.transform;
            searchingForPlayer =false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    } 
    IEnumerator UpdatePath(){
        if (target == null)
        {
           if (!searchingForPlayer)
           {
               searchingForPlayer = true;
               StartCoroutine(SearchForPlayer());
           }
            yield return false;
        }
        // Start a new path to  the target position, return the result to the OnPathComplate method
        seeker.StartPath(transform.position,target.position,OnPathComplate);
        yield return new WaitForSeconds(1f/updateDelay);    
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplate(Path p){
        Debug.Log("We got a path. Did it have an error??"+p.error);
        if (!p.error)
        {
            path = p;
            currentWayPoint =0;
        }
    }

    private void FixedUpdate() {
        if (target == null)
        {
           if (!searchingForPlayer)
           {
               searchingForPlayer = true;
               StartCoroutine(SearchForPlayer());
           }
            return;
        }
        //TODO : Always look at player?
        if (path == null)
        {
            Debug.LogWarning("İZLENECEK YOL YOK");
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;
        // Directon to the next waypoint 
        Vector3 dir = (path.vectorPath[currentWayPoint]- transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir,fMode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if (dist<nextWayPointDistance)
        {
            currentWayPoint++;
            return;
        }
    }


}
