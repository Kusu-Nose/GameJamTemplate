using System;
using System.Collections.Generic;
using UnityEngine;

namespace HackathonTemplate.Utilities
{
    /// <summary>
    /// シリアライズ可能なディクショナリ
    /// </summary>
    /// <typeparam name="TKey">キー</typeparam>
    /// <typeparam name="TValue">値</typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        /// <summary>
        /// ペア
        /// </summary>
        [Serializable]
        public class Pair
        {
            public TKey key = default;
            public TValue value = default;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Pair(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }

        /// <summary>
        /// インスペクター表示用のリスト
        /// </summary>
        [SerializeField]
        private List<Pair> _list;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dictionary">ディクショナリ</param>
        public SerializableDictionary(Dictionary<TKey, TValue> dictionary = null) : base(dictionary)
        {
            _list = new List<Pair>(Count);
            foreach (var pair in this)
            {
                _list.Add(new Pair(pair.Key, pair.Value));
            }
        }

        /// <summary>
        /// OnAfterDeserialize
        /// </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            foreach (var pair in _list)
            {
                if (ContainsKey(pair.key))
                {
                    continue;
                }
                Add(pair.key, pair.value);
            }
        }

        /// <summary>
        /// OnBeforeSerialize
        /// </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    }
}