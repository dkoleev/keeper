using System;

namespace Avocado.Core.Loader {
    public interface ILoader {
        void LoadObject<T>(string path, Action<T> onLoad);
    }
}