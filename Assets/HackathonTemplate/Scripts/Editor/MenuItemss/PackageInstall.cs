using UnityEditor;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// ���j���[����p�b�P�[�W��ǉ�����N���X
    /// </summary>
    [InitializeOnLoad]
    public class PackageInstall : MenuConfig
    {
        /// <summary>
        /// ���j���[�A�C�e���̃p�X
        /// </summary>
        private const string MENU_ITEM_PATH = "PackageInstaller/";
        /// <summary>
        /// ���j���[�A�C�e���̗D��x
        /// </summary>
        private const int MENU_ITEM_PRIORITY = 2;
        /// <summary>
        /// �A�C�e���̖��O
        /// </summary>
        private const string ITEM_NAME = "Install";

        /// <summary>
        /// �C���X�g�[�����j���[
        /// </summary>
        [MenuItem(TOP_MENU_PATH + MENU_ITEM_PATH, priority = MENU_ITEM_PRIORITY)]
        private static void MenuItem() { }

        /// <summary>
        /// �C���X�g�[��
        /// </summary>
        [MenuItem(TOP_MENU_PATH + MENU_ITEM_PATH + ITEM_NAME)]
        private static void Install()
        {
            PackageInstaller.InstallPackages();
        }
    }
}
