using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

namespace FirstPersonSystem
{
    [AlwaysSynchronizeSystem]
    [UpdateAfter(typeof(PlayerInputApplySystem))]
    public class CharacterMoveSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var dt = Time.DeltaTime;

            Entities.ForEach((
                ref Translation trans, 
                in CharacterMoveData move, 
                in CharacterLookData look) =>
            {
                trans.Value += move.Velocity * move.Speed;
            }).Run();
            
            return default;
        }
    }
}
