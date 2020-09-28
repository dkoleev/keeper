using System.Collections.Generic;
using UnityEngine;
using Logger = Avocado.UnityToolbox.Logger;

namespace Avocado.Systems {
    public class AnimationSystem {
        private Animator _animator;
        private string _currentState;
        
        private readonly string _idleState = "Idle";
        private readonly string _walkState = "Walk";
        private readonly string _attackState = "Attack";
        private Dictionary<string, int> _animations = new Dictionary<string, int>();
        
        public AnimationSystem(Animator animator) {
            _animator = animator;
            
            _animations.Add(_idleState, Animator.StringToHash(_idleState));
            _animations.Add(_walkState, Animator.StringToHash(_walkState));
            _animations.Add(_attackState, Animator.StringToHash(_attackState));
        }

        public void SetIdle() {
            ResetAllTriggers();
            _animator.SetTrigger(_idleState);
        }
        
        public void SetWalk() {
            ResetAllTriggers();
            _animator.SetTrigger(_idleState);
        }
        
        public void SetAttack() {
            ResetAllTriggers();
            _animator.SetTrigger(_idleState);
        }

        public void SetState(string state) {
            ResetAllTriggers();
            if (_animations.ContainsKey(state)) {
                _animator.SetTrigger(_animations[state]);
            } else {
                Logger.LogError($"State {state} not exists");
            }
        }

        public void ResetAllTriggers() {
            foreach (var animation in _animations.Values) {
                _animator.ResetTrigger(animation);
            }
        }
    }
}