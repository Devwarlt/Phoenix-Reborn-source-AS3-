﻿using System;
using log4net;
using wServer.realm.entities;
using wServer.realm.setpieces;

namespace wServer.realm.worlds
{
    internal class GameWorld : World
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (GameWorld));

        private readonly int mapId;
        private readonly bool oryxPresent;

        public GameWorld(int mapId, string name, bool oryxPresent)
        {
            Identifier = "Realm";
            Name = name;
            Background = 0;
            Difficulty = 0;
            SetMusic("world/Overworld");
            this.oryxPresent = oryxPresent;
            this.mapId = mapId;
            SetDisposeTime(12 * 60 * 60 * 1000); //12 hours.
            //SetDisposeTime(60 * 1000); //1 minute for test realm generation
        }

        public Oryx Overseer { get; private set; }

        public static GameWorld AutoName(int mapId, bool oryxPresent)
        {
            string name = RealmManager.realmNames[new Random().Next(RealmManager.realmNames.Count)];
            RealmManager.realmNames.Remove(name);
            return new GameWorld(mapId, name, oryxPresent);
        }

        protected override void Init()
        {
            log.InfoFormat("Initializing Game World {0}({1}) from map {2}...", Id, Name, mapId);
            base.FromWorldMap(
                typeof (RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.gameWorld" + mapId + ".wmap"));
            SetPieces.ApplySetPieces(this);
            if (oryxPresent)
            {
                Overseer = new Oryx(this);
                Overseer.Init();
            }
            else
                Overseer = null;
            log.Info("Game World initalized.");
        }

        public override void Tick(RealmTime time)
        {
            base.Tick(time);
            if (Overseer != null)
                Overseer.Tick(time);
        }

        public void EnemyKilled(Enemy enemy, Player killer)
        {
            if (Overseer != null)
                Overseer.OnEnemyKilled(enemy, killer);
        }

        public override int EnterWorld(Entity entity)
        {
            int ret = base.EnterWorld(entity);
            if (entity is Player)
                Overseer.OnPlayerEntered(entity as Player);
            return ret;
        }
    }
}