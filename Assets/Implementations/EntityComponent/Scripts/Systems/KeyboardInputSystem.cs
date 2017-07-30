using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    /// <summary>
    /// Maps raw keyboard inputs into input entities to be used by other systems during the frame.
    /// </summary>
    public class KeyboardInputSystem : IExecuteSystem, ICleanupSystem
    {
        private readonly InputContext context;
        private readonly IGroup<InputEntity> inputs;

        public KeyboardInputSystem(Contexts contexts)
        {
            context = contexts.input;
            inputs = context.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            foreach(var binding in Config.instance.bindings)
            {
                if((binding.triggerWhenDown && Input.GetKeyDown(binding.keyCode))
                 || !binding.triggerWhenDown && Input.GetKey(binding.keyCode))
                {
                    createInput(binding.input);
                }
            }
        }

        public void Cleanup()
        {
            foreach (var e in inputs.GetEntities())
            {
                e.Destroy();
            }
        }

        private void createInput(InputType input)
        {
            context.CreateEntity()
                .AddInput(input);
        }
    }
}