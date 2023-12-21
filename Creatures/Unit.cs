using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Creature
{
    protected override void Awake()
    {
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
            m_target = FindClosestTarget("Monster");

        if(m_target && CanAttackTarget())
            Attack();
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
}
