using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace FirstPersonSystem
{
    [AlwaysSynchronizeSystem]
    [UpdateAfter(typeof(CharacterInputReadSystem))]
    public class CharacterInputApplySystem : ComponentSystem
    {
        CharacterInputSettings.SettingsData InputSettings;

        protected override void OnCreate()
        {
            base.OnCreate();
            InputSettings = CharacterInputSettings.Load();
        }

        protected override void OnUpdate()
        {
            var time = Time.ElapsedTime;
            var dt = Time.DeltaTime;
            var up = math.up();
            
            Entities.ForEach((
                ref CharacterMoveData move,
                ref CharacterLookData look,
                ref CharacterInputData input) =>
            {
                // MOUSE
                
                look.LookY += input.MouseX * dt;
                look.LookX -= input.MouseY * dt;
                look.LookX = math.clamp(look.LookX, InputSettings.VerticalLookMinMax.x, InputSettings.VerticalLookMinMax.y);
                
                // KEYS
                
                var horizontal = input.Horizontal;
                var vertical = input.Vertical;
                var haveInput = (math.abs(horizontal) > float.Epsilon) || (math.abs(vertical) > float.Epsilon);

                if (haveInput)
                {
                    var forward = math.forward(quaternion.identity);
                    var right = math.cross(up, forward);

                    var localSpaceMovement = (forward * vertical) + (right * horizontal);
                    var worldSpaceMovement = math.rotate(quaternion.AxisAngle(up, look.LookY), localSpaceMovement);
                    move.Velocity = math.normalize(worldSpaceMovement) * dt;
                }
                else
                {
                    move.Velocity = float3.zero;
                }

                if (input.IsJump == 1)
                    move.Velocity += up * InputSettings.JumpForce;
            });
        }
    }
}
