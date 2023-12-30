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

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if(m_lastSpawnId >= m_spawnOrder.Count - 1 || m_spawnIntervalEndTime > Mathf.Round(Time.time*1000))
            return;

        float speedFactor = Time.timeSinceLevelLoad / 4.5f;
        float spawnInterval = Mathf.Max(0, 3000 - 50 * speedFactor);
        float waveFactor = 1 + Time.timeSinceLevelLoad / 12.5f;

        m_spawnIntervalEndTime = Mathf.Round(Time.time*1000) + 500 + spawnInterval;
        //m_lastSpawnId++;
        m_lastSpawnId = 0;
        SpawnMonster(m_spawnOrder[m_lastSpawnId], waveFactor);
    }

    public void SpawnMonster(int id, float waveFactor)
    {
        List<Transform> spawnPositions = m_spawnPositions[m_monsters[id].spawnId].spawnPositions;
        GameObject monster = Instantiate(m_monsters[id].monsterPrefab, spawnPositions[Random.Range(0, spawnPositions.Count)].position, Quaternion.identity, gameObject.transform);
        Creature creature = monster.AsCreature();
        creature.SetBaseHealth((int)(creature.GetMaxHealth() * waveFactor), true);
    }
}
