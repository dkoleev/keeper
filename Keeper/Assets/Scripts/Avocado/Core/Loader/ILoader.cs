namespace Avocado.Core.Loader {
    public interface ILoader {
        T LoadObject<T>(string path);
    }
}