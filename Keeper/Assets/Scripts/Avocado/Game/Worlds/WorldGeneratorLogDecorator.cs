using Avocado.Framework.Utilities;

namespace Avocado.Game.Worlds {
    public class WorldGeneratorLogDecorator : IWorldGenerator {
        private IWorldGenerator _generator;

        public WorldGeneratorLogDecorator(IWorldGenerator generator) {
            _generator = generator;
        }

        public void Generate() {
            _generator.Generate();
            Logger.Log("Start generation");
        }
    }
}
