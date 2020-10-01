using System.Collections.Generic;
using Avocado.Game.Data;
using Avocado.Models.Components;
using Avocado.Models.Entities;
using Sigtrap.Relays;
using UnityEngine;

namespace Avocado.Models.Worlds {
    public class World {
        public Relay<Entity> OnEntityCreate = new Relay<Entity>();
        public GameData GameData { get; }
        public List<Entity> Entities => _entities;
        public List<Entity> ChildEntities => _childEntities;
        
        private readonly List<Entity> _entities = new List<Entity>();
        private readonly List<Entity> _childEntities = new List<Entity>();
        private IWorldGenerator _generator;
        
        public World(in GameData gameData) {
            GameData = gameData;
            _generator = new WorldGeneratorLogDecorator(new WorldGenerator());
        }

        public void Create() {
            CreateEntity<PlayerEntity>("Player");
            _generator.Generate(this);
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