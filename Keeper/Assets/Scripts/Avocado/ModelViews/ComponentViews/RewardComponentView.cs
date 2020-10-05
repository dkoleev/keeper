using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Reward)]
    public class RewardComponentView : BaseComponentView<RewardComponent> {
        public RewardComponentView(RewardComponent componentModel, EntityView entityView) : base(componentModel,
            entityView) {
            
        }
    }
}
