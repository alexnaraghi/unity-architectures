using UnityEngine;

namespace Inheritance
{
    public class BigAsteroid : Asteroid
    {
        [SerializeField] private string smallAsteroidName;
        [SerializeField] private int smallAsteroidSpawnCount;

        protected override void OnDead()
        {
            base.OnDead();
            if (smallAsteroidSpawnCount > 0)
            {
                for (int i = 0; i < smallAsteroidSpawnCount; i++)
                {
                    var smallAsteroid = GameplayState.instance.CreateAndRegisterEntity<SmallAsteroid>(smallAsteroidName);
                    if(smallAsteroid != null)
                    {
                        smallAsteroid.Init(transform.localPosition.x, transform.localPosition.y);
                    }
                }
            }
        }
    }
}
