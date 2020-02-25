using Avocado.Game.Controllers;
using Avocado.Game.EntitiesOld.Models;
using UnityEngine;

namespace Avocado.Game.EntitiesOld.Views {
    public class PlayerView : EntityView {
        private Animator _animator;
        private Player _model;
        private Mover _mover;
        
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int SpeedMove = Animator.StringToHash("SpeedMove");

        protected override void Start() {
            base.Start();
            
            _model = new Player();
            _mover = GetComponent<Mover>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update() {
            if (_mover.Mooving) {
                _model.SetState(Player.PlayerState.Move);
                var speed =(Mathf.Abs(_mover.MoveAxis.x) + Mathf.Abs(_mover.MoveAxis.y));
                _animator.SetFloat(Move, speed);
                _animator.SetFloat(SpeedMove, speed);
            } else {
                if (_model.CurrentState != Player.PlayerState.Idle) {
                    _model.SetState(Player.PlayerState.Idle);
                    _animator.SetFloat(Move, 0);
                    _animator.SetFloat(SpeedMove, 0);
                    _animator.SetTrigger(Idle);
                }
            }
        }
    }
}