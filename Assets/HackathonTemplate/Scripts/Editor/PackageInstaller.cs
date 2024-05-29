using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// �p�b�P�[�W���C���X�g�[��
    /// </summary>
    public class PackageInstaller
    {
        /// <summary>
        /// ���N�G�X�g
        /// </summary>
        private static AddAndRemoveRequest _request;

        /// <summary>
        /// �p�b�P�[�W���C���X�g�[��
        /// </summary>
        public static void InstallPackages()
        {
            // �_�C�A���O�\��
            bool result = EditorUtility.DisplayDialog(
                "�p�b�P�[�W�̃C���X�g�[��",
                "�ȉ��̃p�b�P�[�W���C���X�g�[�����܂����H\n\n" +
                ConfigLoader.GetConfig().PackageInstallerConfigData._Identifiers.Keys.Aggregate((x, y) => x + "\n" + y),
                "Yes",
                "No");

            if (result)
            {
                Debug.Log("�p�b�P�[�W�̃C���X�g�[����...");

                List<string> identifiers = new(ConfigLoader.GetConfig().PackageInstallerConfigData._Identifiers.Values);
                // �C���X�g�[��
                _request = Client.AddAndRemove(identifiers.ToArray());
                EditorApplication.update += Progress;
            }
        }

        /// <summary>
        /// �i��
        /// </summary>
        static void Progress()
        {
            if (_request.IsCompleted)
            {
                if (_request.Status == StatusCode.Success)
                {
                    Debug.Log("�p�b�P�[�W�̃C���X�g�[������");
                }
                else if (_request.Status >= StatusCode.Failure)
                {
                    Debug.Log(_request.Error.message);
                }
                EditorApplication.update -= Progress;
            }
        }
    }
}