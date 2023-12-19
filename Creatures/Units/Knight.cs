using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Unit
{
    public BoxCollider2D m_attackCollider;

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
    }

    protected override bool ApproachTarget()
    {
        return base.ApproachTarget();
    }

    protected override bool DistanceTarget()
    {
        return base.DistanceTarget();
    }

    protected override bool Attack()
    {
        if(!CanUseSkills() || CanApproachTarget())
            return false;

        m_animator.Play("Attack_sword");
        AddSkillTime(1500);
        AddSelfStunTime(800);
        Invoke("ApplyAttackDamage", 0.22f);
        return true;
    }

    private void ApplyAttackDamage()
    {
        if(!IsAlive() || !m_target)
            return;

        if(m_attackCollider.IsTouching(m_target.GetHitboxCollider()))
            m_target.TakeDamage(gameObject, GetAttack());
    }
}
