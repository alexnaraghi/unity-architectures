using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiss
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float invulnerabilityPeriod;
        [SerializeField] private new SpriteRenderer renderer;

        private float spawnTime;

        private bool isInvulnerable
        {
            get
            {
                return Time.time < spawnTime + invulnerabilityPeriod;
            }
        }

        private IEnumerator Start()
        {
            spawnTime = Time.time;

            // Do a blink effect until the invulnerability period ends
            const int intervals = 6;
            for (int i = 0; i < intervals - 1; i++)
            {
                float alpha = i % 2 == 0 ? 0.25f : 0.75f;
                SetAlpha(alpha);
                yield return new WaitForSeconds(invulnerabilityPeriod / intervals);
            }
            SetAlpha(1f);
            yield return null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!isInvulnerable)
            {
                GameManager.instance.LoseLife();
                Destroy(gameObject);
            }
        }

        private void SetAlpha(float alpha)
        {
            if(renderer != null)
            {
                var c = renderer.color;
                renderer.color = new Color(c.r, c.g, c.b, alpha);
            }
        }
    }
}