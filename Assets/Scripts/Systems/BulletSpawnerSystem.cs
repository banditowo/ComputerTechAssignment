using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
[UpdateBefore(typeof(EnemyMoveSystem))]
public partial struct BulletSpawnerSystem : ISystem
{
    private double LastFire; // used for cooldown checking
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Player>();
        state.RequireForUpdate<Config>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            // not pressing Space
            return;
        }
        
        var config = SystemAPI.GetSingleton<Config>();
        
        if (LastFire + config.FireCooldown > SystemAPI.Time.ElapsedTime)
        {
            // still in cooldown period
            return;
        }
        
        LastFire = SystemAPI.Time.ElapsedTime;

        var playerTransform = SystemAPI.GetComponentRO<LocalTransform>(SystemAPI.GetSingleton<Player>().Entity).ValueRO;
        
        var bullet = state.EntityManager.Instantiate(config.BulletPrefab);
        
        
        state.EntityManager.SetComponentData(bullet, new LocalTransform
        {
            Position = playerTransform.Position + (playerTransform.Forward() * config.BulletSpawnForwardOffset),
            Rotation = playerTransform.Rotation,
            Scale = 0.1f
        });
        
        state.EntityManager.SetComponentData(bullet, new Velocity
        {
            // set Bullet velocity to be Players forward
            Value = math.mul(playerTransform.Rotation, new float3(0,1,0))
        });
    }
}