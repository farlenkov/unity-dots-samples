using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace FirstPersonSystem
{
    [AlwaysSynchronizeSystem]
    public class CharacterInputReadSystem : ComponentSystem
    {
        CharacterInputSettings.SettingsData InputSettings;

        protected override void OnCreate()
        {
            base.OnCreate();
            InputSettings = CharacterInputSettings.Load();
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((ref CharacterInputData input) =>
            {
                input.MouseX = Input.GetAxisRaw("Mouse X") * InputSettings.MouseSensitivity;
                input.MouseY = Input.GetAxisRaw("Mouse Y") * InputSettings.MouseSensitivity;
                input.Horizontal = Input.GetAxisRaw("Horizontal");
                input.Vertical = Input.GetAxisRaw("Vertical");
                input.IsJump = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;
            });
        }
    }
}
