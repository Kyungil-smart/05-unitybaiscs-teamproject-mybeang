using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject Monster;
        public int MonsterLevel;
        public int Monsterweigth;
    }

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<SpawnEntry> _monster;

    public void Spawn(int difficultyLevel, int spawnCount)
    {
        if(spawnCount < 0)
        {
            return;
        }

        if (spawnPoints.Length == 0 || spawnPoints == null)
        {
            Debug.Log("스폰 포인트 지정해 주세요");
            return;
        }

        if (_monster == null || _monster.Count ==0) 
        {
            Debug.Log("몬스터를 목록에 넣어주세요");
            return;
        }

        List<SpawnEntry> SpawnEntry = GetSpawnCandidates(difficultyLevel);

        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            SpawnEntry selectedMonster = Monsterweigth(SpawnEntry);
            if (selectedMonster == null || selectedMonster.Monster == null)
            {
                continue;
            }

            GameObject spawnedMonster = Instantiate(selectedMonster.Monster, spawnPoint.position, spawnPoint.rotation);
        }
    }
    private List<SpawnEntry> GetSpawnCandidates(int difficultyLevel)
    {
        List<SpawnEntry> candidates = new List<SpawnEntry>();
        for (int i = 0; i < _monster.Count; i++)
        {
            SpawnEntry entry = _monster[i];
            if (entry == null)
            {
                continue;
            }
            if (entry.Monster == null)
            {
                continue;
            }
            if (entry.MonsterLevel <= difficultyLevel)
                candidates.Add(entry);
        }
        return candidates;
    }
    private SpawnEntry Monsterweigth(List<SpawnEntry> candidates)
    {
        int totalWeight = 0; //토탈 가중치
        for (int i = 0; i < candidates.Count; i++)
            totalWeight += candidates[i].Monsterweigth; // 가중치

        int RandomPick = Random.Range(0, totalWeight);
        int acc = 0; // 가중치

        for (int i = 0; i < candidates.Count; i++)
        {
            acc += candidates[i].Monsterweigth;
            if (RandomPick < acc)
            {
                return candidates[i];
            }
        }

        return candidates[candidates.Count - 1];
    }
}
