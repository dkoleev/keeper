using Avocado.Core.DataTypes;
using UnityEngine;

namespace Avocado.Editor.LevelCreator {
    public class PreviewObject {
        public bool enable;
        public GameObject prefab;
        public string SceneObjectKey;
     //   public SceneObject SceneObjectData;
     //   public SceneExpansionData ExpansionData;
        public bool isPrefab = false;
        public float offset = 0;
        public Texture2D preview;

        public Point GetSize() {
            return new Point(1, 1);
        }
    }
}
