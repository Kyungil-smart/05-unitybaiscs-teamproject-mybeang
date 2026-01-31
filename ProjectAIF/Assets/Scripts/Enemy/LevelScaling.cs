using System;
using UnityEngine;

[Serializable]
public struct LevelScaling
{
    [SerializeField] private int _maxScalingLevel;        // 최대 레벨
    [SerializeField] private int _secDifficultyLevel;     // 몇 초마다 레벨 +1

    [SerializeField] private float _spwanInterval;        // 기본 스폰 간격(예: 20)
    [SerializeField] private float _intervalDecreasePerLevel; // 레벨당 감소(예: 2)

    [SerializeField] private int _spawnCount;               // 기본 소환 수
    [SerializeField] private int _countIncreasePerLevel;    // 레벨당 소환 수

    // 현재 레벨 구하기(0 ~ max)
    public int GetCurrentLevel(float playTimeSec)
    {
        int maxLv = Mathf.Max(0, _maxScalingLevel); // 최대레벨 안전 보정

        int lv = Mathf.FloorToInt(playTimeSec / _secDifficultyLevel); // 시간/구간
        return Mathf.Clamp(lv, 0, maxLv); // 범위 제한
    }

    // 현재 스폰 간격 구하기(0초까지 허용)
    public float GetCurrentSpawnInterval(float playTimeSec)
    {
        int lv = GetCurrentLevel(playTimeSec); // 현재 레벨
        float baseInterval = Mathf.Max(0f, _spwanInterval); // 음수 방지
        float dec = Mathf.Max(0f, _intervalDecreasePerLevel); // 음수 방지

        float interval = baseInterval - (lv * dec); // 레벨 오를수록 빨라짐
        return Mathf.Max(0f, interval); // 0초 허용
    }

    public int GetSpawnCount(float playTimeSec)
    {
        int lv = GetCurrentLevel(playTimeSec);
        int baseCount = _spawnCount;
        int Count = baseCount + (lv * _countIncreasePerLevel); 

        return Count;
    }
}
