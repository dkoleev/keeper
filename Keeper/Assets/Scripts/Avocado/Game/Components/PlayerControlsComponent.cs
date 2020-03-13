using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using Avocado.Game.Managers.InputManager;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.PlayerControls)]
    public struct PlayerControlsComponent : IComponent {
        public Entity Entity { get; private set; }

        private Animator _animator;
        private Entity _model;
        private MoveComponent _mover;
        private static int SpeedMove = Animator.StringToHash("SpeedMove");
        private float _speedMove;
        private Transform _moveTransform;
        private float _rotationSpeed;
        private Transform _rotateTransform;
        private bool _playerLoaded;
        private InputManager _inputManager;
        private float _prevRotateAxis;
        private bool _mooving;
        private bool _initialized;
        private Transform _rotateTarget;
        private PlayerControlsComponentData _data;

        public void Initialize(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (PlayerControlsComponentData)data;
            
            _speedMove = _data.Value;
            _inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();
            _moveTransform = _rotateTransform = Entity.transform;
            _animator = Entity.GetComponentInChildren<Animator>();
            _rotateTarget = _animator.transform;

            _initialized = true;
        }
        
        public void Update() {
            if(!_initialized)
                return;
            
            if (_inputManager.MoveAxis != Vector2.zero) {
                if (!_mooving) {
                    _mooving = true;
                }

                _moveTransform.position += new Vector3(_inputManager.MoveAxis.x * Time.deltaTime * _speedMove, 0, _inputManager.MoveAxis.y * Time.deltaTime * _speedMove);
            }else {
                _mooving = false;
            }
            
            if (_mooving) {
                var speed =(Mathf.Abs(_inputManager.MoveAxis.x) + Mathf.Abs(_inputManager.MoveAxis.y));
                _animator.SetFloat(SpeedMove, speed);
            } else {
                _animator.SetFloat(SpeedMove, 0);
            }

            Rotate();
        }

        private void Rotate() {
            var h1 = _inputManager.MoveAxis.x;
            var v1 = _inputManager.MoveAxis.y;
            
            if (h1 >= 0f || v1 >= 0f)
            {
                _rotateTarget.localEulerAngles = new Vector3(0f, Mathf.Atan2(h1, v1) * 180 / Mathf.PI,0f);
            }
        }
    }
}