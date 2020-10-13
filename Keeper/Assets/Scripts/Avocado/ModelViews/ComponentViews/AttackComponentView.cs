using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Models.Components;
using Avocado.ModelViews.Behaviour;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Attack)]
    public class AttackComponentView : BaseComponentView<AttackComponent> {
      // private readonly int _attackAnimationKey = Animator.StringToHash("Attack");
        private readonly int _weaponTypeKey = Animator.StringToHash("WeaponType_int");
        private readonly int _isShootKey = Animator.StringToHash("Shoot_b");

        public AttackComponentView(AttackComponent componentModel, EntityView entityView) : base(componentModel, entityView) {
            EntityView.Animator.SetInteger(_weaponTypeKey, 1);
            Model.OnShoot.AddListener(() => {
                EntityView.Animator.SetBool(_isShootKey, true);
            });
        }

        public override void Initialize() {
            base.Initialize();
            
            CreateWeapon();
        }

        private void CreateWeapon() {
            var weaponParent = EntityView.gameObject.GetComponentInChildren<WeaponPlacer>();
            EntityView.WorldView.CreateEntityView<EntityView>(Model.CurrentWeapon, weaponParent.transform, entityView => {
                    entityView.transform.localPosition = Vector3.zero;
                    entityView.transform.localRotation = Quaternion.identity;
                });
        }

        public override void Update() {
            base.Update();
            
            EntityView.Animator.SetInteger(_weaponTypeKey, Model.IsMoving ? 0 : 1);

            if (Model.IsAttack) {
                EntityView.RotateTransform.LookAt(EntityView.WorldView.Entities[Model.CurrentTarget].transform);
            } else {
                EntityView.Animator.SetBool(_isShootKey, false);
            }
        }
    }
}
