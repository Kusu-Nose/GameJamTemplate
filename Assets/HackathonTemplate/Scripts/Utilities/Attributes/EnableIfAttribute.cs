using UnityEngine;

namespace HackathonTemplate.Utilities
{
    /// <summary>
    /// bool型の変数がtrueの場合にプロパティを有効化する属性
    /// </summary>
    public class EnableIfAttribute : PropertyAttribute
    {
        /// <summary>
        /// 変数名
        /// </summary>
        public readonly string VariableName;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="variableName">変数名</param>
        public EnableIfAttribute(string variableName)
        {
            this.VariableName = variableName;
        }
    }
}