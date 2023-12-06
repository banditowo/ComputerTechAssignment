using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ConfigAuthoring : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject BulletPrefab;

    [Header("Player Params")] 
    public int PlayerHealth = 100;
    //public float PlayerSpeed = 1;
    public float PlayerRotationSpeed = 2;
    public float PlayerHitInvincibilitySeconds = 1.0f;
    
    [Header("Player Bullets")]
    public float BulletSpeed = 10;
    public float BulletSpawnForwardOffset = 0.2f;
    public float FireCooldown = 0.02f;
    public bool DestroyBulletOnImpact = true;

    [Header("Enemy Spawning")]
    public int EnemySpawnAmount = 5;
    public float EnemySpawnFrequency = 0.5f;
    public float EnemySpawnRadius = 7.0f;

    [Header("Enemy Params")]
    public float EnemySpeed = 0.5f;

    class Baker : Baker<ConfigAuthoring>
    {
        public override void Bake(ConfigAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(entity, new Config
            {
                PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic),
                EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
                BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
                //PlayerSpeed = authoring.PlayerSpeed,
                PlayerRotationSpeed = authoring.PlayerRotationSpeed,
                EnemySpawnAmount = authoring.EnemySpawnAmount,
                EnemySpawnFrequency = authoring.EnemySpawnFrequency,
                EnemySpawnRadius = authoring.EnemySpawnRadius,
                EnemySpeed = authoring.EnemySpeed,
                BulletSpeed = authoring.BulletSpeed,
                DestroyBulletOnImpact = authoring.DestroyBulletOnImpact,
                BulletSpawnForwardOffset = authoring.BulletSpawnForwardOffset,
                FireCooldown = authoring.FireCooldown,
                PlayerHitInvincibilitySeconds = authoring.PlayerHitInvincibilitySeconds,
                PlayerHealth = authoring.PlayerHealth
            });
        }
    }
}

public struct Config : IComponentData
{
    public Entity PlayerPrefab;
    public Entity EnemyPrefab;
    public Entity BulletPrefab;
    public float3 BulletSpawnForwardOffset;
    //public float PlayerSpeed;
    public float PlayerRotationSpeed;
    public float EnemySpeed;
    public float EnemySpawnFrequency;
    public float EnemySpawnRadius;
    public float BulletSpeed;
    public float FireCooldown;
    public float PlayerHitInvincibilitySeconds;
    public int PlayerHealth;
    public int EnemySpawnAmount;
    public bool DestroyBulletOnImpact;
}