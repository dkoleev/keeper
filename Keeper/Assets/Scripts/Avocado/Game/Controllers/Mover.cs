using System;
using Avocado.Game.Entities.Views;
using Avocado.Game.Managers.InputManager;
using Avocado.Game.Utilities;
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
        private PlayerView m_PlayerView;
        private float _prevRotateAxis;

        public Vector2 MoveAxis => _inputManager.MoveAxis;
        public bool Mooving => _mooving;
        private bool _mooving;

        private void Awake() {
            _inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();
            m_PlayerView = GameObject.FindWithTag("Player").GetComponent<PlayerView>();
            _rotateTransform = m_PlayerView.transform;
            _playerLoaded = true;
            /*_player.OnLoaded += () => {
                _rotateTransform = _player.GetComponentInChildren<MountBody>().transform;
                _playerLoaded = true;
            };*/
        }

        private void Update() 
        {
            if (_inputManager.MoveAxis != Vector2.zero) {
                if (!_mooving) {
                    _mooving = true;
                }

                 _moveTransform.position += new Vector3(_inputManager.MoveAxis.x * Time.deltaTime * _speedMove, transform.position.y, _inputManager.MoveAxis.y * Time.deltaTime * _speedMove);
            }else {
                _mooving = false;
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