using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public partial struct EnemySpawnerSystem : ISystem
{
    private float _lastSpawnTime;
    private float spawnRadius;
    private const float TAU = Mathf.PI * 2;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Config config = SystemAPI.GetSingleton<Config>();

        _lastSpawnTime += Time.deltaTime;
        
        if (_lastSpawnTime >= config.EnemySpawnFrequency)
        {
            for (int i = 0; i < config.EnemySpawnAmount; i++)
            {
                Entity asteroid = state.EntityManager.Instantiate(config.EnemyPrefab);
                
                spawnRadius = Random.Range(config.EnemySpawnRadius, config.EnemySpawnRadius + 5f);
                float angle = Random.Range(0f, TAU);
                float xPos = spawnRadius * Mathf.Cos(angle);
                float yPos = spawnRadius * Mathf.Sin(angle);

                SystemAPI.GetComponentRW<LocalTransform>(asteroid).ValueRW.Position = new float3(xPos, yPos, 0);
            }
            _lastSpawnTime = 0f;
        }
    }
}