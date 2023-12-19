using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MonsterInfo
{
    public GameObject monsterPrefab;
    public int spawnId;

    public MonsterInfo(GameObject _monsterPrefab, int _spawnId)
    {
        monsterPrefab = _monsterPrefab;
        spawnId = _spawnId; 
    }
}

[System.Serializable]
public struct SpawnPositions
{
    public List<Transform> spawnPositions;
           
    public SpawnPositions(List<Transform> _spawnPositions)
    {
        spawnPositions = _spawnPositions;
    }
}

public class Monsters : MonoBehaviour
{
    public List<MonsterInfo> m_monsters;
    public List<SpawnPositions> m_spawnPositions;
    public List<int> m_spawnOrder;

    private int m_lastSpawnId = -1;
    private float m_spawnIntervalEndTime = 0;
    private const int m_spawnInterval = 5000;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (m_lastSpawnId >= m_spawnOrder.Count - 1 || m_spawnIntervalEndTime > Mathf.Round(Time.time*1000))
            return;

        m_spawnIntervalEndTime = Mathf.Round(Time.time*1000) + m_spawnInterval;
        //m_lastSpawnId++;
        m_lastSpawnId = 0;
        SpawnMonster(m_spawnOrder[m_lastSpawnId]);
    }

    public void SpawnMonster(int id)
    {
        List<Transform> spawnPositions = m_spawnPositions[m_monsters[id].spawnId].spawnPositions;
        GameObject monster = Instantiate(m_monsters[id].monsterPrefab, spawnPositions[Random.Range(0, spawnPositions.Count)].position, Quaternion.identity, gameObject.transform);
    }
}
