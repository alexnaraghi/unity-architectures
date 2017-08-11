using UnityEngine;

namespace Inheritance
{
    public class StartState : OnePressMenuState
    {
        public override Canvas LoadMenu()
        {
            return Utils.InstantiateFromResources<Canvas>("StartState");
        }
    }
}
