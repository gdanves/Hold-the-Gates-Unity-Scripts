using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    private int m_targetWaypoint = 0;
    private Transform m_waypoints;
    private GameObject m_target;

    protected override void Awake()
    {
        m_waypoints = GameObject.Find("Waypoints").transform;
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(!m_target)
            m_target = FindNewTarget();
        if(m_target/*/ && m_target.IsAlive() /*/) {
            // reach target/attack
            return;
        }
        FollowWaypoints();
    }

    protected virtual void FollowWaypoints()
    {
        bool walking = true;
        Transform targetWaypoint = m_waypoints.GetChild(m_targetWaypoint);
        float distanceToWaypoint = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetWaypoint.position.x, targetWaypoint.position.y));
        if(distanceToWaypoint <= 2.5f) {
            if(m_targetWaypoint + 1 < m_waypoints.childCount)
                m_targetWaypoint++;
            else {
                walking = false;
                Debug.Log("No waypoint found, monster might be stuck");
            }
        } else {
            m_spriteRenderer.flipX = transform.position.x - targetWaypoint.position.x >= 0;
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, 5.0f * Time.deltaTime);
        }
        m_animator.SetBool("Walking", walking);
    }

    protected virtual GameObject FindNewTarget()
    {
        // TODO: change to target the closest within an range (add range stat?)
        //GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        //if(units.Length != 0)
            //return units[0];
        return null;
    }
}
