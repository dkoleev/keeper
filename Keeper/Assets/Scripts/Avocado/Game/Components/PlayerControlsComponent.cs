using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Entities;
using Avocado.Game.Managers.InputManager;
using Avocado.Game.Systems;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ObjectType("PlayerControls")]
    public class PlayerControlsComponent : ComponentBase, IBatchUpdate {
        private Animator _animator;
        private Player _model;
        private MoveComponent _mover;
        
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int SpeedMove = Animator.StringToHash("SpeedMove");
        
        private float _speedMove = 5f;
        private Transform _moveTransform;
        private float _rotationSpeed = 2f;
        private Transform _rotateTransform;
        private bool _playerLoaded;
            
        private InputManager _inputManager;
        private float _prevRotateAxis;

        public Vector2 MoveAxis => _inputManager.MoveAxis;
        public bool Mooving => _mooving;
        private bool _mooving;
        
        public override void Initialize(Entity entity) {
            base.Initialize(entity);

            Register();
            
            _inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();
            _moveTransform = _rotateTransform = Entity.transform;
            _playerLoaded = true;
            
            //_mover = Entity.GetComponent<MoveComponent>();
            _animator = Entity.GetComponentInChildren<Animator>();

            Initialized = true;
        }

        public void Register() {
            UpdateSystem.Instance.RegisterSlicedUpdate(this, UpdateSystem.UpdateMode.Always);
        }

        public void BatchUpdate() {
            if(!Initialized)
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
               // _model.SetState(Player.PlayerState.Move);
                var speed =(Mathf.Abs(_inputManager.MoveAxis.x) + Mathf.Abs(_inputManager.MoveAxis.y));
               // _animator.SetFloat(Move, speed);
                _animator.SetFloat(SpeedMove, speed);
            } else {
                   // _model.SetState(Player.PlayerState.Idle);
                   // _animator.SetFloat(Move, 0);
                    _animator.SetFloat(SpeedMove, 0);
                   // _animator.SetTrigger(Idle);
            }
        }
    }
}