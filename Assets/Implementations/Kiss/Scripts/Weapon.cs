using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiss
{
    /// <summary>
    /// Manages a projectile weapon with a cooldown.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float shotsPerSecond;
        [SerializeField] private KeyCode fireKey = KeyCode.Space;
        [SerializeField] private GameObject projectile;
        [SerializeField] private GameObject weaponOrigin;

        private float lastShotTime;

        private void Update()
        {
            var time = Time.time;
            if (Input.GetKey(fireKey) && (lastShotTime + 1f / shotsPerSecond < time))
            {
                lastShotTime = time;
                Fire();
            }
        }

        private void Fire()
        {
            var origin = weaponOrigin ?? gameObject;

            if (projectile != null)
            {
                Instantiate(projectile, origin.transform.position, transform.localRotation);
            }
        }
    }
}