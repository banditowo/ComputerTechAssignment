using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMovement : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
        state.RequireForUpdate<Player>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        if (horizontal != 0)
        {
            RotatePlayer(horizontal, ref state);
        }
    }

    private void RotatePlayer(float horizontalInput, ref SystemState state)
    {
        
        Config config = SystemAPI.GetSingleton<Config>();
        Player player = SystemAPI.GetSingleton<Player>();
        RefRW<LocalTransform> playerTransform = SystemAPI.GetComponentRW<LocalTransform>(player.Entity);
    
        quaternion currentRotation = playerTransform.ValueRO.Rotation;
        float rotationAngle = -config.PlayerRotationSpeed * SystemAPI.Time.DeltaTime * horizontalInput;
        
        quaternion rotationDelta = quaternion.RotateZ(rotationAngle);
        quaternion newRotation = math.mul(currentRotation, rotationDelta);
        
        playerTransform.ValueRW.Rotation = newRotation;
    }
}
