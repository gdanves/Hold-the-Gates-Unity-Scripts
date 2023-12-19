using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject m_attacker;
    private string m_targetTag;
    private int m_damage = 0;
    private bool m_enabled;

    void Awake()
    {
        m_enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetAttacker(GameObject attacker)
    {
        m_attacker = attacker;
    }

    public void SetTargetTag(string tag)
    {
        m_targetTag = tag;
    }

    public void SetDamage(int damage)
    {
        m_damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_enabled && other.gameObject.tag == m_targetTag && !other.isTrigger) {
            other.GetComponent<Creature>().TakeDamage(m_attacker, m_damage);
            m_enabled = false;
            Destroy(gameObject, 0.1f);
        }
    }
}
