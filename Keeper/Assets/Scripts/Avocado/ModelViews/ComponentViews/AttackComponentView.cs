using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Models.Components;
using Avocado.ModelViews.Behaviour;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Attack)]
    public class AttackComponentView : BaseComponentView<AttackComponent> {
        private readonly int _attackAnimationKey = Animator.StringToHash("Attack");
        public AttackComponentView(AttackComponent componentModel, EntityView entityView) : base(componentModel, entityView) {
            Model.OnShoot.AddListener(() => {
                EntityView.Animator.SetTrigger(_attackAnimationKey);
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

            if (Model.IsAttack) {
                EntityView.RotateTransform.LookAt(EntityView.WorldView.Entities[Model.CurrentTarget].transform);
            }
        }
    }
}
