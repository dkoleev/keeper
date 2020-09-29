using System.Collections.Generic;
using Avocado.Game.Data;
using Avocado.Models.Components;
using Avocado.Models.Entities;

namespace Avocado.Models.Worlds {
    public class World {
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

            PostInitializeEntities();
        }

        public void PostInitializeEntities() {
            foreach (var entity in _entities) {
                entity.PostInitialize();
            }

            foreach (var childEntity in _childEntities) {
                childEntity.PostInitialize();
            }
        }

        public Entity CreateEntity<T>(string entityId, T parent = null)
            where T : Entity, new() {
            var entityData = GameData.Entities.Entities[entityId];
            var entity = new T();
            entity.Initialize(entityId, entityData, this, parent);
            
            if (parent is null) {
                _entities.Add(entity);
            } else {
                _childEntities.Add(entity);
            }

            return entity;
        }

        public IReadOnlyList<Entity> GetEntitiesWithComponent<T>() where T : IComponent {
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