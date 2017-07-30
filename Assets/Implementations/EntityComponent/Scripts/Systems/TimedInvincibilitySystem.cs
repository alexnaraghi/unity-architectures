using System;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public class TimedInvincibilitySystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> entities;
        readonly Color blinkOnColor = new Color(1f, 1f, 1f, 0.25f);
        readonly Color blinkOffColor = new Color(1f, 1f, 1f, 0.75f);

        public TimedInvincibilitySystem(Contexts contexts)
        {
            entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TimedInvincibility, GameMatcher.Invincible));
        }

        public void Execute()
        {
            var time = Time.time;
            foreach (GameEntity e in entities.GetEntities())
            {
                if (e.timedInvincibility.endTime < Time.time)
                {
                    e.isInvincible = false;

                    if (e.view.sprite != null)
                    {
                        e.view.sprite.color = Color.white;
                    }
                }
                else if (e.view.sprite != null)
                {
                    // Do visual effect for timed invincibility
                    // Do a blink effect until the invulnerability period ends
                    float intervalPeriod = Config.instance.playerInvincibilitySeconds / Config.instance.playerBlinkIntervals;
                    float invulnerabilityTime = Config.instance.playerInvincibilitySeconds - (e.timedInvincibility.endTime - Time.time);
                    if ((int)(invulnerabilityTime / intervalPeriod) % 2 == 0)
                    {
                        e.view.sprite.color = blinkOffColor;
                    }
                    else
                    {
                        e.view.sprite.color = blinkOnColor;
                    }
                }
            }
        }
    }
}