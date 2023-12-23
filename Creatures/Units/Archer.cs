using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
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
        m_animator.Play("Attack_bow_front");
        AddSkillTime(500);
        AddSelfStunTime(500);
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

        TurnToTarget();

        // fire the arrow
        m_animator.Play("Attack_bow_end");
        FireBasicAttack();
    }

    private void FireBasicAttack()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(m_dir.y, m_dir.x) * Mathf.Rad2Deg);
        GameObject arrow = (GameObject) Instantiate(m_arrowPrefab, m_arrowSpawnFront.position, rotation);
        arrow.transform.localScale = transform.localScale;
        arrow.GetComponent<Rigidbody2D>().velocity = m_dir * 20f;
        arrow.GetComponent<Projectile>().SetAttacker(gameObject);
        arrow.GetComponent<Projectile>().SetTargetTag("Monster");
        arrow.GetComponent<Projectile>().SetDamage(GetAttack());
        Destroy(arrow, 2);
    }
}
