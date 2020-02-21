using System;
using Avocado.Game.Managers.InputManager;
using UnityEngine;

namespace Avocado.Game.Controllers {
    public class Mover : MonoBehaviour
    {
        [SerializeField]
        private float _speedMove = 1f;

        [SerializeField]
        private Transform _moveTransform;

        [SerializeField]
        private float _rotationSpeed = 1f;

        public Action OnMove;
        public Action OnStop;
        
        private Transform _rotateTransform;
        private bool _playerLoaded;
            
        private InputManager _inputManager;
        private Player _player;
        private float _prevRotateAxis;

        private void Awake() {
            _inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _rotateTransform = _player.transform;
            _playerLoaded = true;
            /*_player.OnLoaded += () => {
                _rotateTransform = _player.GetComponentInChildren<MountBody>().transform;
                _playerLoaded = true;
            };*/
        }

        private void Update() 
        {
            if (_inputManager.MoveAxis != Vector2.zero) {
                 _moveTransform.position += new Vector3(_inputManager.MoveAxis.x * Time.deltaTime * _speedMove, transform.position.y, _inputManager.MoveAxis.y * Time.deltaTime * _speedMove);
            }
            /*if (_playerLoaded) 
            {
                if (Math.Abs(_prevRotateAxis - _inputManager.RotationAxisY) > 0.0f) {
                    _prevRotateAxis = _inputManager.RotationAxisY;
                    _rotateTransform.Rotate(Time.deltaTime * _rotationSpeed * new Vector3(0, _inputManager.RotationAxisY, 0));
                }
            }*/
        }

        public Vector2 GetMoverAxis() {
            return _inputManager.MoveAxis;
        }
    }
}