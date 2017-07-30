using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoOO
{
    public sealed class CollisionListener : MonoBehaviour
    {
        List<CollisionInfo> collisionList;

        public void Init(List<CollisionInfo> collisionList)
        {
            this.collisionList = collisionList;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            collisionList.Add(new CollisionInfo()
            {
                source = gameObject,
                target = collider.gameObject
            });
        }
    }
}