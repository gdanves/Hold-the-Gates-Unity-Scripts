using System.Collections.Generic;
using UnityEngine;

public class CreatureStat
{
    private float m_value;
    private int m_baseValue;
    private List<float> m_statModifiersRaw;
    private List<float> m_statModifiersPercent;
 
    public CreatureStat(int baseValue = 50)
    {
        SetBaseValue(baseValue);
        m_statModifiersRaw = new List<float>();
        m_statModifiersPercent = new List<float>();
    }

    public void SetBaseValue(int baseValue)
    {
        m_value = (float)baseValue;
        m_baseValue = baseValue;
    }

    public void AddValue(int value)
    {
        m_value = Mathf.Max(0, m_value + value);
    }

    public float GetValue()
    {
        return m_value;
    }

    public int GetBaseValue()
    {
        return m_baseValue;
    }
}
