using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace SimpleFolderIcon.Editor
{
    public class IconDictionaryCreator : AssetPostprocessor
    {
        private const string AssetsPath = @"SimpleFolderIcon\Icons";
        internal static Dictionary<string, Texture> IconDictionary;

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!ContainsIconAsset(importedAssets) &&
                !ContainsIconAsset(deletedAssets) &&
                !ContainsIconAsset(movedAssets) &&
                !ContainsIconAsset(movedFromAssetPaths))
            {
                return;
            }

            BuildDictionary();
        }

        private static bool ContainsIconAsset(string[] assets)
        {
            foreach (string str in assets)
            {
                if (Path.GetDirectoryName(str) == @"Assets\" + AssetsPath)
                {
                    return true;
                }
            }
            return false;
        }

        internal static void BuildDictionary()
        {
            Dictionary<string, Texture> dictionary = new Dictionary<string, Texture>();

            DirectoryInfo dir = new DirectoryInfo(Application.dataPath + @"\" + AssetsPath);
            FileInfo[] info = dir.GetFiles("*.png");
            foreach (FileInfo f in info)
            {
                Texture texture = (Texture)AssetDatabase.LoadAssetAtPath($"Assets/SimpleFolderIcon/Icons/{f.Name}", typeof(Texture2D));
                dictionary.Add(Path.GetFileNameWithoutExtension(f.Name), texture);
            }

            IconDictionary = dictionary;
        }
    }
}
