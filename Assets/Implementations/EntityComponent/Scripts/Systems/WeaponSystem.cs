using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public class WeaponSystem : ReactiveSystem<InputEntity>
    {
        readonly IGroup<GameEntity> players;
        readonly GameContext context;

        public WeaponSystem(Contexts contexts) : base(contexts.input)
        {
            players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.PlayerControlled, GameMatcher.Weapon));
            context = contexts.game;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.AllOf(InputMatcher.Input));
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasInput;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            bool isFiring = false;

            foreach (InputEntity e in entities)
            {
                switch (e.input.type)
                {
                    case InputType.Fire: isFiring = true; break;
                }
            }

            var playerEntities = players.GetEntities();
            foreach (var player in playerEntities)
            {
                var lastShotTime = player.weapon.lastShotTime;
                var cooldownSeconds = Config.instance.weaponCooldownSeconds;

                if (isFiring && lastShotTime + cooldownSeconds < Time.time)
                {
                    player.ReplaceWeapon(Time.time);
                    context.CreateBullet(player.position.x, player.position.y, player.direction.value);
                }
            }
        }
    }
}