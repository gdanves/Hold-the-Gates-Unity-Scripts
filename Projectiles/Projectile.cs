using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject m_attacker;
    private string m_targetTag;
    private int m_damage = 0;
    private bool m_enabled;
    private bool m_disableOnTrigger = true;

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

    public void FadeOut(float timeFactor)
    {
        StartCoroutine(FadeOutMaterial(timeFactor));
    }

    public void SetDisableOnTrigger(bool disable)
    {
        m_disableOnTrigger = disable;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(m_enabled && other.gameObject.tag == m_targetTag && !other.isTrigger) {
            other.GetComponent<Creature>().TakeDamage(m_attacker, m_damage);
            if(m_disableOnTrigger) {
                m_enabled = false;
                Destroy(gameObject, 0.1f);
            }
        }
    }

    private IEnumerator FadeOutMaterial(float timeFactor)
    {
        Renderer rend = transform.GetComponent<Renderer>();
        Color matColor = rend.material.color;
        float alphaValue = rend.material.color.a;

        while(rend.material.color.a > 0f)
        {
            alphaValue -= Time.deltaTime / timeFactor;
            rend.material.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
            yield return null;
        }

        Destroy(gameObject);
    }
}
