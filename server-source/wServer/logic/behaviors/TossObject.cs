﻿using System;
using wServer.networking.svrPackets;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.behaviors
{
    internal class TossObject : Behavior
    {
        //State storage: cooldown timer

        private readonly ushort child;
        private readonly int coolDownOffset;
        private readonly bool randomToss;
        private readonly double range;
        private double? angle;
        private Cooldown coolDown;

        public TossObject(string child, double range = 5, double? angle = null,
            Cooldown coolDown = new Cooldown(), int coolDownOffset = 0, bool randomToss = false)
        {
            this.child = BehaviorDb.InitGameData.IdToObjectType[child];
            this.range = range;
            this.angle = angle*Math.PI/180;
            this.coolDown = coolDown.Normalize();
            this.coolDownOffset = coolDownOffset;
            this.randomToss = randomToss;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = coolDownOffset;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            var cool = (int) state;

            if (cool <= 0)
            {
                if (host.HasConditionEffect(ConditionEffects.Stunned)) return;

                double tossAngle = randomToss ? new Random().Next(0, 360)*Math.PI/180 : angle.Value;

                var target = new Position
                {
                    X = host.X + (float) (range*Math.Cos(tossAngle)),
                    Y = host.Y + (float) (range*Math.Sin(tossAngle)),
                };
                host.Owner.BroadcastPacket(new ShowEffectPacket
                {
                    EffectType = EffectType.Throw,
                    Color = new ARGB(0xffffbf00),
                    TargetId = host.Id,
                    PosA = target
                }, null);
                host.Owner.Timers.Add(new WorldTimer(1500, (world, t) =>
                {
                    Entity entity = Entity.Resolve(world.Manager, child);
                    entity.Move(target.X, target.Y);
                    (entity as Enemy).Terrain = (host as Enemy).Terrain;
                    world.EnterWorld(entity);
                }));
                cool = coolDown.Next(Random);
            }
            else
                cool -= time.ElaspedMsDelta;

            state = cool;
        }
    }
}