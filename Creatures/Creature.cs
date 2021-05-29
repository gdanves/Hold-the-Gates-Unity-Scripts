using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    protected Animator m_animator;
    protected SpriteRenderer m_spriteRenderer;
    private List<CreatureStat> m_stats = new List<CreatureStat>();
 
    protected virtual void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_stats.Add(new CreatureStat()); // ATK
        m_stats.Add(new CreatureStat()); // HP
        m_stats.Add(new CreatureStat()); // DEF
        m_stats.Add(new CreatureStat()); // DEFM
        m_stats.Add(new CreatureStat()); // SPD
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected void SetStats(List<int> stats)
    {
        int i = 0;
        foreach(int stat in stats) {
            m_stats[i].SetBaseValue(stat);
            i++;
        }
    }
}
