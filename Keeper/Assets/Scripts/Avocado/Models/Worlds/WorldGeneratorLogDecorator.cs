using Avocado.Framework.Utilities;

namespace Avocado.Models.Worlds {
    public class WorldGeneratorLogDecorator : IWorldGenerator {
        private IWorldGenerator _generator;

        public WorldGeneratorLogDecorator(IWorldGenerator generator) {
            _generator = generator;
        }

        public void Generate(World world) {
            _generator.Generate(world);
            Logger.Log("Start generation");
        }
    }
}
