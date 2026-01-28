using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawnManager : SingleTon<MonsterSpawnManager>
{
    [SerializeField] private LevelScaling _scaling;  // struct라 null 체크 X
    [SerializeField] private float _minSpawnInterval = 0.05f;

    private List<MonsterSpawner> _spawners = new List<MonsterSpawner>();
    private Coroutine _spawnRoutine;
    private float _spawnTimeSec;    // 스폰 스케일링 계산용

    private void Awake()
    {
        // 싱글톤 초기화(중복 제거 + DontDestroyOnLoad)
        SingleTonInit();

        // 씬이 바뀔 때마다 스포너 재수집 + 스폰 상태 갱신
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        RefreshSpawners();
        ApplySpawningState();
    }

    private void OnDestroy()
    {
        // 이벤트 해제(중복 구독 방지)
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene prev, Scene next)
    {
        RefreshSpawners();
        ApplySpawningState();
    }

    private void RefreshSpawners()
    {
        _spawners = new List<MonsterSpawner>(FindObjectsOfType<MonsterSpawner>());
    }

    private void ApplySpawningState()
    {
        if (_spawners.Count > 0) StartSpawning();
        else StopSpawning();
    }

    public void StartSpawning()
    {
        if (_spawnRoutine != null) return;
        _spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (_spawnRoutine == null) return;
        StopCoroutine(_spawnRoutine);
        _spawnRoutine = null;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (_spawners == null || _spawners.Count == 0)
            {
                StopSpawning();
                yield break;
            }

            int difficultyLevel = _scaling.GetCurrentLevel(_spawnTimeSec);
            int spawnCount = _scaling.GetSpawnCount(_spawnTimeSec);
            float interval = Mathf.Max(_minSpawnInterval, _scaling.GetCurrentSpawnInterval(_spawnTimeSec));

            if (spawnCount > 0)
            {
                for (int i = 0; i < _spawners.Count; i++)
                {
                    MonsterSpawner spawner = _spawners[i];
                    if (spawner == null) continue;

                    // 매니저는 스폰만함
                    spawner.Spawn(difficultyLevel, spawnCount);
                }
            }

            _spawnTimeSec += interval;
            yield return new WaitForSeconds(interval);
        }
    }
}
