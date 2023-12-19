using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Condition
{
    Skill,
    Stun,
    SelfStun,
    Last
}

enum Stat
{
    Health,
    Attack,
    Defense,
    MagicDefense,
    Speed,
    Last = Speed
}

public class Creature : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int m_baseHealth;
    [SerializeField] protected int m_baseAttack;
    [SerializeField] protected int m_baseDefense;
    [SerializeField] protected int m_baseMagicDefense;
    [SerializeField] protected int m_baseSpeed;

    [Header("Ranges")]
    [Tooltip("Range to detect a new target")]
    [SerializeField] protected float m_targetViewRange = 10f;
    [Tooltip("Desired range to keep from the target")]
    [SerializeField] protected float m_targetDistanceMin = 0.5f;
    [Tooltip("Can't attack if range is bigger than this")]
    [SerializeField] protected float m_targetDistanceMax = 2.0f;

    [Header("Collider")]
    public BoxCollider2D m_hitboxCollider;

    protected Animator m_animator;
    protected SpriteRenderer m_spriteRenderer;

    protected Creature m_target;
    protected bool m_walking = false;
    protected Vector2 m_dir = new Vector2(0, 0);

    private int[] m_conditionTimes = new int[(int)Condition.Last];
    private List<CreatureStat> m_stats = new List<CreatureStat>();
 
    protected virtual void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        for(Stat i = 0; i <= Stat.Last; i++)
            m_stats.Add(new CreatureStat());
        SetStats(m_baseHealth, m_baseAttack, m_baseDefense, m_baseMagicDefense, m_baseSpeed);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        m_walking = false;
    }

    // LateUpdate is called once per frame, after Update has finished
    protected virtual void LateUpdate()
    {
        if(m_animator)
            m_animator.SetBool("Walking", m_walking);
    }

    public void AddSkillTime(int duration)
    {
        AddConditionTime(Condition.Skill, duration);
    }

    public void AddStunTime(int duration)
    {
        AddConditionTime(Condition.Stun, duration);
    }

    public void AddSelfStunTime(int duration)
    {
        AddConditionTime(Condition.SelfStun, duration);
    }

    public virtual bool TakeDamage(GameObject attacker, int damage)
    {
        if(!IsAlive())
            return false;

        m_stats[(int)Stat.Health].AddValue(-damage);

        if(!IsAlive())
            OnDeath();
        return true;
    }

    public BoxCollider2D GetHitboxCollider()
    {
        return m_hitboxCollider;
    }

    public Vector2 GetHitboxCenterPosition()
    {
        BoxCollider2D hitboxCollider = GetHitboxCollider();
        return new Vector2(hitboxCollider.bounds.center.x, hitboxCollider.bounds.center.y);
    }

    public int GetHealth()
    {
        return (int)m_stats[(int)Stat.Health].GetValue();
    }

    public int GetMaxHealth()
    {
        return m_stats[(int)Stat.Health].GetBaseValue();
    }

    public int GetAttack()
    {
        return (int)m_stats[(int)Stat.Attack].GetValue();
    }

    public float GetSpeed()
    {
        return m_stats[(int)Stat.Speed].GetValue();
    }

    public float GetSpeedFactor()
    {
        return GetSpeed() * 0.1f;
    }

    public bool IsAlive()
    {
        return GetHealth() > 0;
    }

    public bool IsStunned(bool ignoreSelf = false)
    {
        return HasCondition(Condition.Stun) || (!ignoreSelf && HasCondition(Condition.SelfStun));
    }

    public bool IsUsingSkills()
    {
        return HasCondition(Condition.Skill);
    }

    public bool CanApproachCreature(Creature creature)
    {
        // add offset to avoid approach/distance loop
        float offset = 0.25f;
        return GetDistanceTo(creature) > m_targetDistanceMin + offset;
    }

    public bool CanDistanceCreature(Creature creature)
    {
        return GetDistanceTo(creature) < m_targetDistanceMin;
    }

    public bool CanApproachTarget()
    {
        if(!m_target)
            return false;
        return CanApproachCreature(m_target);
    }

    public bool CanDistanceTarget()
    {
        if(!m_target)
            return false;
        return CanDistanceCreature(m_target);
    }

    public bool CanTargetCreature(Creature creature)
    {
        if(!creature.IsAlive())
            return false;
        return GetDistanceTo(creature) <= m_targetViewRange;
    }

    public bool CanAttackCreature(Creature creature) {
        if(!creature.IsAlive())
            return false;

        // add offset to avoid approach/distance loop
        float offset = 0.25f;
        return GetDistanceTo(creature) <= m_targetDistanceMax + offset;
    }

    public bool CanAttackTarget()
    {
        if(!m_target)
            return false;
        return CanAttackCreature(m_target);
    }

    public bool CanUseSkills()
    {
        return !IsUsingSkills() && !IsStunned();
    }

    public bool CanWalk()
    {
        return !IsStunned();
    }

    public float GetDistanceTo(Creature creature)
    {
        return Math.GetDistanceBetween(GetHitboxCenterPosition(), creature.GetHitboxCenterPosition());
    }

    protected void SetStats(params int[] list)
    {
        for(int i = 0; i < list.Length; i++)
            m_stats[i].SetBaseValue(list[i]);
    }

    protected virtual Creature FindClosestTarget(string tag)
    {
        float closestDistance = 0f;
        Creature closestTarget = null;
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject target in targets) {
            if(!target.GetComponent<Creature>().IsAlive())
                continue;
            float distance = Math.GetDistanceBetween(transform.position, target.transform.position);
            if(distance <= m_targetViewRange && (!closestTarget || distance < closestDistance)) {
                closestDistance = distance;
                closestTarget = target.GetComponent<Creature>();
            }
        }
        return closestTarget;
    }

    protected virtual bool Attack()
    {
        return false;
    }

    protected virtual void OnDeath()
    {
        if(m_animator)
            m_animator.SetBool("Dead", true);
        m_hitboxCollider.enabled = false;
    }

    protected virtual void ChangeDirection(bool flip)
    {
        if(!m_spriteRenderer)
            return;

        m_spriteRenderer.flipX = flip;
        foreach(Collider2D c in GetComponents<Collider2D>()) {
            if((flip && c.offset.x > 0) || (!flip && c.offset.x < 0))
                c.offset = new Vector2(c.offset.x * -1, c.offset.y);
        }
    }

    protected virtual void TurnToTarget()
    {
        if(!m_target)
            return;

        Vector2 targetPos = m_target.GetHitboxCenterPosition();
        Vector2 myPos = GetHitboxCenterPosition();
        Vector2 dir = targetPos - myPos;
        dir.Normalize();
        m_dir = dir;
        ChangeDirection(dir.x < 0);
    }

    private void AddConditionTime(Condition condition, int duration)
    {
        int timeLeft = m_conditionTimes[(int)condition];
        m_conditionTimes[(int)condition] = (int)Mathf.Max(timeLeft, Util.GetTimeMillis() + duration);
    }

    private bool HasCondition(Condition condition)
    {
        return Util.GetTimeMillis() < m_conditionTimes[(int)condition];
    }
}
