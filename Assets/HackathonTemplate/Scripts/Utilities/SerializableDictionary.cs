using System;
using System.Collections.Generic;
using UnityEngine;

namespace HackathonTemplate.Utilities
{
    /// <summary>
    /// �V���A���C�Y�\�ȃf�B�N�V���i��
    /// </summary>
    /// <typeparam name="TKey">�L�[</typeparam>
    /// <typeparam name="TValue">�l</typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        /// <summary>
        /// �y�A
        /// </summary>
        [Serializable]
        public class Pair
        {
            public TKey key = default;
            public TValue value = default;

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            public Pair(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }

        /// <summary>
        /// �C���X�y�N�^�[�\���p�̃��X�g
        /// </summary>
        [SerializeField]
        private List<Pair> _list;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dictionary">�f�B�N�V���i��</param>
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