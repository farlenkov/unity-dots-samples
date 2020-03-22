using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace FirstPersonSystem
{
    [AlwaysSynchronizeSystem]
    [UpdateAfter(typeof(PlayerInputReadSystem))]
    public class PlayerInputApplySystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        { 
            var dt = Time.DeltaTime;
            
            Entities.ForEach((
                ref CharacterMoveData move, 
                ref CharacterLookData look, 
                in PlayerInputData input, 
                in PlayerInputSettings settings) =>
            {
                look.LookX = math.clamp(look.LookX + input.MouseX * dt, settings.VerticalLookMinMax.x, settings.VerticalLookMinMax.y) ;
                look.LookY += input.MouseY * dt;
                look.LookForward = new float3(look.LookY, look.LookX, 0f);
                
                var up = math.up();
                var horizontal = input.Horizontal;
                var vertical = input.Vertical;
                var forward = math.forward(quaternion.identity);
                var right = math.cross(up, forward);
                var haveInput = (math.abs(horizontal) > float.Epsilon) || (math.abs(vertical) > float.Epsilon);
                
                if (haveInput)
                {
                    var localSpaceMovement = forward * vertical + right * horizontal;
                    var worldSpaceMovement = math.rotate(quaternion.AxisAngle(up, look.LookY), localSpaceMovement);
                    move.Velocity = math.normalize(worldSpaceMovement) * dt;
                }
                else
                {
                    move.Velocity = float3.zero;
                }
                
            }).Run();
        
            return default;
        }
    }
}
