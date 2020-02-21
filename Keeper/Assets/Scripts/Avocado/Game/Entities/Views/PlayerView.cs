using Avocado.Game.Controllers;
using Avocado.Game.Entities.Models;
using Avocado.Game.Utilities;
using UnityEngine;

namespace Avocado.Game.Entities.Views {
    public class PlayerView : EntityView {
        private Animator _animator;
        private Player _model;
        private Mover _mover;
        
        private static readonly int Idle = Animator.StringToHash("New Trigger");
        private static readonly int Move = Animator.StringToHash("Blend");

        protected override void Start() {
            base.Start();
            
            _model = new Player();
            _mover = GetComponent<Mover>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update() {
            if (_mover.Mooving) {
                _model.SetState(Player.PlayerState.Idle);
                var speed = (Mathf.Abs(_mover.MoveAxis.x) + Mathf.Abs(_mover.MoveAxis.y))/2.0f;
                AvocadoLogger.Log(speed.ToString() + _mover.MoveAxis.ToString());
                _animator.SetFloat(Move, speed);
            } else {
                if (_model.CurrentState != Player.PlayerState.Idle) {
                    _model.SetState(Player.PlayerState.Idle);
                    _animator.SetTrigger(Idle);
                }
            }
        }
    }
}