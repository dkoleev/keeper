using System.Collections.Generic;
using Avocado.Game.Data;
using Avocado.Game.Entities;
using UnityEngine;

namespace Avocado.Game.Systems {
    public class UpdateSystem : BaseSystem {
        public static UpdateSystem Instance { get; private set; }

        public UpdateSystem(GameData data) : base(data) {
            Instance = this;
        }

        public override void Initialize() {
            var go = new GameObject("UpdateComponent");
            var goInstance = Object.Instantiate(go);
            goInstance.AddComponent<UpdateEntity>().SetupSystem(this);
        }
        
        public enum UpdateMode { BucketA, BucketB, Always }
        private readonly HashSet<IBatchUpdate> _slicedUpdateBehavioursBucketA = new HashSet<IBatchUpdate>();
        private readonly HashSet<IBatchUpdate> _slicedUpdateBehavioursBucketB = new HashSet<IBatchUpdate>();
        private bool _isCurrentBucketA;
        
        public void Update()
        {
            var targetUpdateFunctions = _isCurrentBucketA ? _slicedUpdateBehavioursBucketA : _slicedUpdateBehavioursBucketB;
            foreach (var slicedUpdateBehaviour in targetUpdateFunctions)
            {
                slicedUpdateBehaviour.BatchUpdate();
            }
            _isCurrentBucketA = !_isCurrentBucketA;
        }
        public void RegisterSlicedUpdate(IBatchUpdate slicedUpdateBehaviour, UpdateMode updateMode)
        {
            if (updateMode == UpdateMode.Always)
            {
                _slicedUpdateBehavioursBucketA.Add(slicedUpdateBehaviour);
                _slicedUpdateBehavioursBucketB.Add(slicedUpdateBehaviour);
            }
            else
            {
                var targetUpdateFunctions = updateMode == UpdateMode.BucketA ? _slicedUpdateBehavioursBucketA : _slicedUpdateBehavioursBucketB;
                targetUpdateFunctions.Add(slicedUpdateBehaviour);
            }
        }
    
        public void DeregisterSlicedUpdate(IBatchUpdate slicedUpdateBehaviour)
        {
            _slicedUpdateBehavioursBucketA.Remove(slicedUpdateBehaviour);
            _slicedUpdateBehavioursBucketB.Remove(slicedUpdateBehaviour);
        }
    }
}
