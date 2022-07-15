using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEditor;

namespace TempustScript
{
    public class TSEncryption
    {
        private static string key = "BF-23-67-61-28-B5-C1-E0-9F-40-A4-78-9E-08-94-BF-56-9D-A8-E3-E9-D8-5F-9C-A5-11-CD-5F-E0-02-40-1E-D6-63-31-4D-ED-05-7F-59-ED-1E-AF-CB-8D-78-37-DC";
        private static Aes aesKey;

        private static byte[] HexToBytes(string hex)
        {
            hex = hex.Replace("-", "");
            int chars = hex.Length;
            byte[] bytes = new byte[chars / 2];
            for (int i = 0; i < chars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        private static bool LoadKey()
        {
            byte[] bytes = HexToBytes(key);

            if (bytes.Length != 48)
            {
                Debug.LogError("Error reading key. Generate a new key from the Window/TempustScript editor.");
                return false;
            }

            byte[] keyBytes = new byte[32];
            byte[] iv = new byte[16];

            for (int i = 0; i < 48; i++)
            {
                if (i < 32)
                {
                    keyBytes[i] = bytes[i];
                }
                else
                {
                    iv[i % 32] = bytes[i];
                }
            }

            aesKey = Aes.Create();
            aesKey.Key = keyBytes;
            aesKey.IV = iv;
            aesKey.Padding = PaddingMode.Zeros;
            return true;
        }

        public static TSScript DeserializeScript(TextAsset script)
        {
            if (LoadKey())
                return DataSerializer.DeserializeScript(script, aesKey);
            else 
                return null;
        }

        /// <summary>
        /// Calls DataSerializer.SerializeLocalData() with the current key
        /// </summary>
        /// <param name="path">Save file path</param>
        /// <param name="data">Data to save</param>
        public static void SerializeLocalData(string path, FlagList.FlagListGroup data)
        {
            if (LoadKey())
            {
                DataSerializer.SerializeLocalData(path, data, aesKey);
            }
        }

        /// <summary>
        /// Calls DataSerializer.SerializeGlobalData() with the current key
        /// </summary>
        /// <param name="path">Save file path</param>
        /// <param name="data">Data to save</param>
        public static void SerializeGlobalData(string path, FlagList data)
        {
            if (LoadKey())
            {
                DataSerializer.SerializeGlobalData(path, data, aesKey);
            }
        }

        /// <summary>
        /// Calls DataSerializer.SerializeLocalData() with the current key
        /// </summary>
        /// <param name="path">Save file path</param>
        /// <param name="data">Data to save</param>
        public static FlagList.FlagListGroup DeserializeLocalData(string path)
        {
            if (LoadKey())
            {
                return DataSerializer.DeserializeLocalData(path, aesKey);
            }
            else
                return null;
        }

        /// <summary>
        /// Calls DataSerializer.SerializeGlobalData() with the current key
        /// </summary>
        /// <param name="path">Save file path</param>
        /// <param name="data">Data to save</param>
        public static FlagList DeserializeGlobalData(string path)
        {
            if (LoadKey())
            {
                return DataSerializer.DeserializeGlobalData(path, aesKey);
            }
            else
            {
                return null;
            }
        }

#if UNITY_EDITOR
        public static void Compile(string directory, string saveDir)
        {
            string[] files = Directory.GetFiles(directory, "*.tmpst", SearchOption.AllDirectories);
            Debug.Log("Tempust Script: Compiling " + files.Length + " files...");
            foreach (string file in files)
            {
                TSScript script = Interpreter.MakeScript(file);

                string newFile = saveDir + Path.GetDirectoryName(file).Substring(directory.Length) + "/" + Path.GetFileNameWithoutExtension(file) + ".bytes";
                Debug.Log("Creating  " + newFile);
                SerializeScript(script, newFile);

            }
            AssetDatabase.Refresh();
        }
        private static void SerializeScript(TSScript script, string filePath)
        {
            if (LoadKey())
            {
                DataSerializer.SerializeScript(script, filePath, aesKey);
            }
        }
#endif
    }
}
