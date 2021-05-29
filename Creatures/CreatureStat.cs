using System.Collections.Generic;

public class CreatureStat
{
    private float m_value;
    private int m_baseValue;
    private List<float> m_statModifiersRaw;
    private List<float> m_statModifiersPercent;
 
    public CreatureStat(int baseValue = 50)
    {
        m_value = (float)baseValue;
        m_baseValue = baseValue;
        m_statModifiersRaw = new List<float>();
        m_statModifiersPercent = new List<float>();
    }

    public void SetBaseValue(int baseValue)
    {
        m_value = (float)baseValue;
        m_baseValue = baseValue;
    }
}
