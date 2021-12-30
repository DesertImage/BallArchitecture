using System;
using System.Collections;
using System.Collections.Generic;
using DesertImage.Extensions;
using UnityEngine;

namespace External
{
    [Serializable]
    public class CustomDictionary<TKey, TValue>
    {
        public TKey[] Keys => keys;
        public TValue[] Values => values;

        public int Count => count;

        [SerializeField] private TKey[] keys;
        [SerializeField] private TValue[] values;
        [SerializeField] private int count;

        public ValueTuple<TKey, TValue> this[int index] => (keys[index], values[index]);

        public TValue this[TKey key]
        {
            get
            {
                var index = keys.IndexOf(key);

                return index >= 0 ? values[index] : default;
            }

            set
            {
                var index = keys.IndexOf(key);

                if (index == -1) return;

                values[index] = value;
            }
        }

        public TKey this[TValue value]
        {
            get
            {
                var index = values.IndexOf(value);

                return index >= 0 ? keys[index] : default;
            }
        }

        private int _fillStep = 3;
        
        public CustomDictionary(int cachedElementsCount = 10, int fillStep = 3)
        {
            keys = new TKey[cachedElementsCount];
            values = new TValue[cachedElementsCount];
            
            _fillStep = fillStep;
        }

        public CustomDictionary(TKey[] keys, TValue[] values, int fillStep = 3)
        {
            this.keys = keys;
            this.values = values;

            _fillStep = fillStep;

            count = keys?.Length ?? 0;
        }

        public void Add(TKey key, TValue value)
        {
            if (count == 0 || keys.Length <= count)
            {
                IncSize(_fillStep);
            }

            keys[count] = key;
            values[count] = value;

            count++;

            // if (count < keys.Length) return;

            // IncSize(_fillStep);
        }

        public void Remove(TKey key)
        {
            Remove(key, keys.IndexOf(key));
        }

        private void Remove(TKey key, int index)
        {
            if (index == -1 || count == 0) return;

            keys[index] = default;
            values[index] = default;

            keys.ShiftLeft(index + 1);
            values.ShiftLeft(index + 1);

            count--;
        }

        private void IncSize(int step)
        {
            step = Mathf.Max(1, step);
            
            var newKeys = keys;
            var newValues = values;

            Array.Resize(ref newKeys, count + step);
            Array.Resize(ref newValues, count + step);

            keys = newKeys;
            values = newValues;
        }

        public void Clear()
        {
            count = 0;

            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = default;
            }

            for (var i = 0; i < values.Length; i++)
            {
                values[i] = default;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (keys.Contains(key))
            {
                value = values[keys.IndexOf(key)];

                return true;
            }

            value = default;

            return false;
        }
    }
}