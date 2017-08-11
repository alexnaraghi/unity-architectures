using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public static class Utils 
    {
        public static T InstantiateFromResources<T>(string name) where T : Component
        {
            T toReturn = null;

            var prefab = Resources.Load(Consts.prefabPath + name) as GameObject;
            if(prefab != null)
            {
                var go = GameObject.Instantiate(prefab);

                if(go != null)
                {
                    toReturn = go.GetComponent<T>();
                }
                else
                {
                    toReturn = null;
                }
            }
            
            return toReturn;
        }
    }
}
