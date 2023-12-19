using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    private Transform m_waypoints;
    private Vector3 m_gatePosition;

    protected override void Awake()
    {
        m_waypoints = GameObject.Find("Waypoints").transform;
        m_gatePosition = GameObject.Find("Gate").transform.position;
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

        if(!IsAlive())
            return;

        if(!m_target || !CanTargetCreature(m_target))
            m_target = FindClosestTarget("Unit");

        if(m_target) {
            if(!CanAttackTarget())
                ApproachTarget();
            else if(!Attack()) {
                if(!DistanceTarget())
                    ApproachTarget();
            }
        } else if(CanWalk()) {
            FollowWaypoints();
        }
    }

    protected virtual bool ApproachTarget()
    {
        if(!CanWalk() || !CanApproachTarget())
            return false;

        Vector2 offset = GetHitboxCenterPosition() - new Vector2(transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, m_target.GetHitboxCenterPosition() - offset, GetSpeedFactor() * Time.deltaTime);
        m_walking = true;
        TurnToTarget();
        return true;
    }

    protected virtual bool DistanceTarget()
    {
        if(!CanWalk() || !CanDistanceTarget())
            return false;

        Vector2 offset = GetHitboxCenterPosition() - new Vector2(transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, m_target.GetHitboxCenterPosition() - offset, -GetSpeedFactor() * Time.deltaTime);
        m_walking = true;
        TurnToTarget();
        return true;
    }

    protected virtual void FollowWaypoints()
    {
        Transform targetWaypoint = transform;
        float closestDistance = Mathf.Infinity;
        bool foundWaypoint = false;
        foreach(Transform child in m_waypoints) {
            float waypointDistanceToMonster = Math.GetDistanceBetween(transform.position, child.position);
            float waypointDistanceToGate = Math.GetDistanceBetween(m_gatePosition, child.position);
            float monsterDistanceToGate = Math.GetDistanceBetween(m_gatePosition, transform.position);
            if(waypointDistanceToMonster > 2.5f) { 
                if(monsterDistanceToGate < waypointDistanceToGate) {
                    // add extra distance to avoid being stuck between waypoints
                    waypointDistanceToMonster += 10f;
                }

                if(waypointDistanceToMonster < closestDistance) {
                    closestDistance = waypointDistanceToMonster;
                    targetWaypoint = child;
                    foundWaypoint = true;
                }
            }
        }

        if(!foundWaypoint) {
            m_walking = false;
            return;
        }

        ChangeDirection(transform.position.x - targetWaypoint.position.x >= 0);
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, GetSpeedFactor() * Time.deltaTime);
        m_walking = true;
    }
}
