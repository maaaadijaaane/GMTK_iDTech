using System;
using System.Collections.Generic;
using UnityEngine;

namespace TempustScript
{
    [Serializable]
    public class FlagList
    {
        [SerializeField] private string label;
        [SerializeField] private List<string> flags;
        [SerializeField] private List<bool> values;

        public string Label { get { return label; } }

        public FlagList(string label, Dictionary<string, bool> data)
        {
            this.label = label;
            flags = new List<string>();
            values = new List<bool>();

            foreach (string flag in data.Keys)
            {
                flags.Add(flag);
                values.Add(data[flag]);
            }
        }

        public Dictionary<string, bool> GetDictionary()
        {
            Dictionary<string, bool> dict = new Dictionary<string, bool>();
            for (int i = 0; i < flags.Count; i++)
            {
                dict.Add(flags[i], values[i]);
            }
            return dict;
        }

        public class FlagListGroup
        {
            [SerializeField] private FlagList[] objects;

            public FlagListGroup(FlagList[] lists)
            {
                objects = lists;
            }
            
            public FlagList[] GetList()
            {
                return objects;
            }
        }
    }
}
