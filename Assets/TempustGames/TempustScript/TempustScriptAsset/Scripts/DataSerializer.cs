using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using TempustScript.Blocks;
using TempustScript.Commands;
using UnityEngine;

namespace TempustScript
{
    public class DataSerializer
    {
        /**
         * List of types to be serialized. Include all ScriptElements
         */
        private static List<Type> types = new List<Type>() {
            typeof(ScriptBlock),
            typeof(Region),
            typeof(ScriptElement),
            typeof(TSScript),
            typeof(Command),
            typeof(AskBlock),
            typeof(ConditionalBlock),
            typeof(OptionBlock),
            typeof(TextBlock),
            typeof(CommandEnd),
            typeof(CommandCloseBox),
            typeof(CommandFace),
            typeof(CommandGive),
            typeof(CommandGoto),
            typeof(CommandMovement),
            typeof(CommandSay),
            typeof(CommandSetFlag),
            typeof(CommandSetPos),
            typeof(ObjectCoordinate),
            typeof(CommandEnable),
            typeof(CommandBGM),
            typeof(CommandPlaySound),
            typeof(CommandWait)
        };

        public static void SerializeScript(TSScript script, string fileName, SymmetricAlgorithm key)
        {
            DataContractSerializer serial = new DataContractSerializer(typeof(TSScript), types);
            byte[] result;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    //Write all data to the stream.
                    serial.WriteObject(cs, script);
                    /*
                    cs.Write(scriptSer);*/
                    cs.Clear();
                    cs.Close();
                    result = ms.ToArray();
                }

                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                using (FileStream file = new FileStream(fileName, FileMode.Create, System.IO.FileAccess.Write))
                {
                    file.Write(result, 0, result.Length);
                }
            }
        }
        public static TSScript DeserializeScript(TextAsset textAsset, SymmetricAlgorithm key)
        {
            DataContractSerializer serial = new DataContractSerializer(typeof(TSScript), types);
            TSScript newObject;

            byte[] bytes = textAsset.bytes;

            using (MemoryStream ms = new MemoryStream())
            {
                using (MemoryStream ms2 = new MemoryStream(bytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms2, key.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        cs.CopyTo(ms);
                        cs.Clear();
                        bytes = RemovePadding(ms.ToArray());
                        cs.Close();
                    }
                }
            }

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                newObject = (TSScript)serial.ReadObject(ms);
            }

            return newObject;
        }

        public static void SerializeLocalData(string path, FlagList.FlagListGroup data, SymmetricAlgorithm key)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            SerializeData(path, bytes, key);
        }
        public static void SerializeGlobalData(string path, FlagList flags, SymmetricAlgorithm key)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(flags));
            SerializeData(path, data, key);
        }

        private static void SerializeData(string path, byte[] data, SymmetricAlgorithm key)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] result;
                using (CryptoStream cs = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    //Write all data to the stream.
                    cs.Write(data, 0, data.Length);

                    cs.Clear();
                    cs.Close();
                    result = ms.ToArray();
                }
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    fs.Write(result, 0, result.Length);
                }
            }
        }

        private static byte[] DeserializeData(string path, SymmetricAlgorithm key)
        {
            byte[] bytes = File.ReadAllBytes(path);

            using (MemoryStream ms = new MemoryStream())
            {
                using (MemoryStream ms2 = new MemoryStream(bytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms2, key.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        cs.CopyTo(ms);
                        cs.Clear();
                        bytes = RemovePadding(ms.ToArray());
                        cs.Close();
                    }
                }
            }

            return bytes;
        }

        public static FlagList.FlagListGroup DeserializeLocalData(string path, SymmetricAlgorithm key)
        {
            if (!File.Exists(path)) return null;

            string json = System.Text.Encoding.UTF8.GetString(DeserializeData(path, key));
            return JsonUtility.FromJson<FlagList.FlagListGroup>(json);
        }

        public static FlagList DeserializeGlobalData(string path, SymmetricAlgorithm key)
        {
            if (!File.Exists(path)) return null;

            string json = System.Text.Encoding.UTF8.GetString(DeserializeData(path, key));
            return JsonUtility.FromJson<FlagList>(json);
        }

        private static byte[] RemovePadding(byte[] bytes)
        {
            int index = bytes.Length - 1;
            while (bytes[index] == 0)
            {
                index--;
            }
            byte[] noPadding = new byte[index + 1];
            Array.Copy(bytes, noPadding, index + 1);
            return noPadding;
        }
    }
}