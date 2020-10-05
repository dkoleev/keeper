using System.Collections.Generic;

namespace Avocado.Models.Components.Rewards {
    public interface IReward {
        IReadOnlyDictionary<string, int> Content { get; }
    }
}