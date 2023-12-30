using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Unit
{
    [Header("Arrow config")]
    [SerializeField]
    private GameObject m_arrowPrefab;
    [SerializeField]
    private Transform m_arrowSpawnFront;

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
        if(!CanUseSkills())
            return false;

        TurnToTarget();
        m_animator.Play("Attack_start");
        AddSkillTime(500);
        AddSelfStunTime(500);
        AddTargetLockTime(500);
        Invoke("UseBasicAttack", 0.4f);
        return true;
    }

    protected override void TurnToTarget()
    {
        if(!m_target)
            return;

        Vector2 targetPos = m_target.GetHitboxCenterPosition();
        Vector2 myPos = new Vector2(m_arrowSpawnFront.position.x, m_arrowSpawnFront.position.y);
        Vector2 dir = targetPos - myPos;
        dir.Normalize();
        m_dir = dir;
        ChangeDirection(dir.x < 0);
    }

    private void UseBasicAttack()
    {
        if(!m_target || !IsAlive() || IsStunned(true)) {
            m_animator.Play("Idle");
            return;
        }

        AddSkillTime(500);
        AddSelfStunTime(500);
        TurnToTarget();
        FireBasicAttack();
        Invoke("EndBasicAttackAnimation", 0.2f);
    }

    private void EndBasicAttackAnimation()
    {
        m_animator.Play("Attack_end");
    }

    private void FireBasicAttack()
    {
        Vector3 pos = m_arrowSpawnFront.position;
        if(IsFlipped())
            pos.x -= m_arrowSpawnFront.transform.localPosition.x * transform.localScale.x * 2;

        Vector2 targetPos = m_target.GetHitboxCenterPosition();
        Vector2 dir = targetPos - new Vector2(pos.x, pos.y);
        dir.Normalize();

        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        GameObject beam = (GameObject) Instantiate(m_arrowPrefab, pos, rotation);
        beam.transform.localScale = transform.localScale;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        beam.GetComponent<Projectile>().SetAttacker(gameObject);
        beam.GetComponent<Projectile>().SetTargetTag("Monster");
        beam.GetComponent<Projectile>().SetDamage(GetAttack());
        beam.GetComponent<Projectile>().FadeOut(0.5f);
        beam.GetComponent<Projectile>().SetDisableOnTrigger(false);
    }
}
