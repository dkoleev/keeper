using Avocado.Game.Data;
using Avocado.Models.Components;
using Avocado.ModelViews.Behaviour;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.Attack)]
    public class AttackComponentView : BaseComponentView {
        private AttackComponent AttackModel;
        private readonly int _attackAnimationKey = Animator.StringToHash("Attack");
        
        public AttackComponentView(AttackComponent componentModel, EntityView entityView) : base(componentModel, entityView) {
            AttackModel = componentModel;
            AttackModel.OnShoot.AddListener(() => {
                EntityView.Animator.SetTrigger(_attackAnimationKey);
            });
        }

        public override void Initialize() {
            base.Initialize();
            
            CreateWeapon();
        }

        private void CreateWeapon() {
            var weaponParent = EntityView.gameObject.GetComponentInChildren<WeaponPlacer>();
            EntityView.WorldView.CreateEntityView<EntityView>(AttackModel.CurrentWeapon, weaponParent.transform, entityView => {
                    entityView.transform.localPosition = Vector3.zero;
                    entityView.transform.localRotation = Quaternion.identity;
                });
        }

        public override void Update() {
            base.Update();

            if (AttackModel.IsAttack) {
                EntityView.RotateTransform.LookAt(EntityView.WorldView.Entities[AttackModel.CurrentTarget].transform);
            }
        }
    }
}
