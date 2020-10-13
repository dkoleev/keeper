using System.Collections.Generic;
using Avocado.Core.SaveEngine;
using Avocado.Data;
using Avocado.Models.Components;
using Avocado.Models.Entities;
using Avocado.Progress;
using Sigtrap.Relays;
using UnityEngine;

namespace Avocado.Models.Worlds {
    public class World {
        public Player.Player Player { get; }
        public Relay<Entity> OnEntityCreate = new Relay<Entity>();
        public Entity Hero { get; private set; }
        public GameData GameData { get; }
        public GameProgress GameProgress { get; }
        public Vector3 Size { get; private set; }
        public List<Entity> Entities => _entities;
        public List<Entity> ChildEntities => _childEntities;
        
        private readonly List<Entity> _entities = new List<Entity>();
        private readonly List<Entity> _childEntities = new List<Entity>();
        private IWorldGenerator _generator;
        
        public World(GameData gameData, GameProgress gameProgress) {
            GameData = gameData;
            GameProgress = gameProgress;
            Player = new Player.Player(gameProgress.Player, this);
            _generator = new WorldGeneratorLogDecorator(new WorldGenerator());
        }

        public void Create(Vector3 size, Vector3 playerSpawnPosition) {
            Hero = CreateEntity("Player", position:playerSpawnPosition);
            Size = size;
            _generator.Generate(this);
        }

        public Entity CreateEntity(string entityId, Entity parent = null, Vector3? position = null) {
            return CreateEntity<Entity>(entityId, parent, position);
        }

        public Entity CreateEntity<T>(string entityId, T parent = null, Vector3? position = null)
            where T : Entity, new() {
            var entityData = GameData.Entities.Entities[entityId];
            var entity = new T();
            entity.Initialize(entityId, entityData, this, parent);
            entity.PostInitialize();
            if (position != null) {
                entity.SetPosition(position.Value);
            }
            
            if (parent is null) {
                _entities.Add(entity);
            } else {
                _childEntities.Add(entity);
            }
            
            OnEntityCreate.Dispatch(entity);

            return entity;
        }

        public List<Entity> GetEntitiesWithComponent<T>() where T : IComponent {
            var result = new List<Entity>();
            foreach (var entity in _entities) {
                if (entity.GetComponentByType<T>() is null) {
                    continue;
                }
                
                result.Add(entity);
            }

            return result;
        }
    }
}