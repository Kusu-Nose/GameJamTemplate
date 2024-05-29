using System;
using UnityEngine;

namespace HackathonTemplate.Utilities
{
    /// <summary>
    /// シングルトン化されたモノビヘイビアー
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.LogError(t + " をアタッチしているGameObjectはありません");
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Awake
        /// </summary>
        virtual protected void Awake()
        {
            if (this != Instance)
            {
                Destroy(this);
                Debug.LogError(
                    typeof(T) +
                    " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                    " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
                return;
            }
        }

    }
}