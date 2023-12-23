using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    [Header("Monster")]
    [SerializeField] int m_goldReward = 0;

    private Transform m_waypoints;
    int m_nextWaypoint = 1;

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

        if(!IsAlive())
            return;

        if(!m_target)
            m_target = GameObject.Find("Gate").GetComponent<Creature>();

        if(CanAttackTarget() && !CanApproachTarget())
            Attack();
        else if(CanWalk() && !FollowWaypoints() && m_target)
            ApproachTarget();
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

    protected virtual bool FollowWaypoints()
    {
        Vector3 targetPosition = transform.position;
        bool foundWaypoint = false;
        int waypointIndex = 0;
        foreach(Transform child in m_waypoints) {
            waypointIndex++;
            Vector3 childPosition = child.position + new Vector3(0.25f, 0.45f, 0);

            float waypointDistance = MathExt.GetDistanceBetween(transform.position, childPosition);
            if(waypointDistance <= 0.25f)
                m_nextWaypoint = waypointIndex + 1;

            if(m_nextWaypoint == waypointIndex) {
                foundWaypoint = true;
                targetPosition = childPosition;
                break;
            }
        }

        if(!foundWaypoint) {
            m_walking = false;
            return false;
        }

        ChangeDirection(transform.position.x - targetPosition.x >= 0);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, GetSpeedFactor() * Time.deltaTime);
        m_walking = true;
        return true;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        if(m_goldReward > 0)
            m_gameManager.AddGold(m_goldReward);
        m_gameManager.AddScore(1);
        Invoke("FadeAndDestroy", 1f);
    }
}
