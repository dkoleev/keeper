using Avocado.Framework.Patterns.AbstractFactory;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ObjectType("PlayerControls")]
    public readonly struct PlayerControlsComponentData : IComponentData {
        public readonly float Value;

        public PlayerControlsComponentData(float value) {
            Value = value;
        }
    }
}