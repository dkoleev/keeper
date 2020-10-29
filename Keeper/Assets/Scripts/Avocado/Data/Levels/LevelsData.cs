using System;
using System.Collections.Generic;
using Avocado.Core.DataTypes;

namespace Avocado.Data.Levels {
    [Serializable]
    public class LevelsData {
        public Dictionary<string, Level> Levels;
    }

    [Serializable]
    public class Level {
        public Dictionary<string, Entity> Entities;
    }

    [Serializable]
    public class Entity {
        public Point Position;
    }
}