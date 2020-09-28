using System.Collections.Generic;
using Avocado.Game.Data;
using Avocado.Models.Components;
using Avocado.Models.Entities;

namespace Avocado.Models.Worlds {
    public class World {
        public GameData GameData { get; }
        public List<Entity> Entities => _entities;
        public Dictionary<Entity, Entity> ChildEntities => _childEntities;
        
        private readonly List<Entity> _entities = new List<Entity>();
        private readonly Dictionary<Entity, Entity> _childEntities = new Dictionary<Entity, Entity>();
        private IWorldGenerator _generator;
        
        public World(in GameData gameData) {
            GameData = gameData;
            _generator = new WorldGeneratorLogDecorator(new WorldGenerator());
        }

        public void Create() {
            CreateEntity<PlayerEntity>("Player");
            _generator.Generate(this);
        }

        public Entity CreateEntity<T>(string entityId)
            where T : Entity, new() {
            var entity = CreateEntityBase<T>(entityId);
            _entities.Add(entity);

            return entity;
        }

        public Entity CreateEntity(string entityId){
            return CreateEntity<Entity>(entityId);
        }
        
        public Entity CreateChildEntity<T>(string entityId, Entity parent)
            where T : Entity, new() {
            var entity = CreateEntityBase<T>(entityId);
            _childEntities.Add(entity, parent);

            return entity;
        }
        
        public Entity CreateChildEntity(string entityId, Entity parent){
            return CreateChildEntity<Entity>(entityId, parent);
        }

        private Entity CreateEntityBase<T>(string entityId)  where T : Entity, new() {
            var entityData = GameData.Entities.Entities[entityId];
            var entity = new T();
            entity.Initialize(entityId, entityData, this);

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