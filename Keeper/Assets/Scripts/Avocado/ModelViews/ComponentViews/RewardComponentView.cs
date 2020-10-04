using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Models.Components.Reward;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.Reward)]
    public class RewardComponentView : BaseComponentView<RewardComponent> {
        public RewardComponentView(RewardComponent componentModel, EntityView entityView) : base(componentModel,
            entityView) {
            
        }
    }
}
