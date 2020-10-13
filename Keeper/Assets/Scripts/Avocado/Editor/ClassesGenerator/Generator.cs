using System.IO;
using UnityEditor;
using UnityEngine;

namespace Avocado.Editor.ClassesGenerator {
    public static class Generator
    {
        [MenuItem("Avocado/GenerateClassesByData")]
        private static void Create()
        {
            if (EditorApplication.isCompiling) {
                Debug.LogWarning("Cant run while compiling");
                return;
            }

            GenerateDataClasses();
            AssetDatabase.Refresh();
        }

        private static void GenerateDataClasses() {
            var name = "Test";
            string copyPath = "Assets/Scripts/Avocado/Data/Components/"+name+".cs";
            Debug.Log("Creating Classfile: " + copyPath);
            if(!File.Exists(copyPath)){
                using (StreamWriter outfile = new StreamWriter(copyPath)) {
                    outfile.WriteLine("using JetBrains.Annotations;");
                    outfile.WriteLine("using System.Collections.Generic;");
                    outfile.WriteLine("using Avocado.Core.Factories.ObjectTypes;");
                    outfile.WriteLine("using Newtonsoft.Json.Linq;");
                    outfile.WriteLine("using Avocado.Framework.Patterns.Factory;");
                    outfile.WriteLine("using Avocado.Data.Components;");
                    outfile.WriteLine("");
                    outfile.WriteLine("namespace Avocado.Data.Components {");
                    outfile.WriteLine("    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]");
                    outfile.WriteLine("    public class " + name + " : BaseComponentData {");
                    outfile.WriteLine("        public  " + name + "(JObject data) : base(data) { }");
                    outfile.WriteLine("    }");
                    outfile.WriteLine("}");
                }
            }
        }

        private static void AddComponent() {
            /*GameObject selected = Selection.activeObject as GameObject;
         if (selected == null || selected.name.Length == 0 )
         {
             Debug.Log("Selected object not Valid");
             return;
         }*/
 
            //  string name = selected.name.Replace(" ","_");
            // name = name.Replace("-","_");
            AssetDatabase.Refresh();
            //selected.AddComponent(Type.GetType(name));
        }
    }
}