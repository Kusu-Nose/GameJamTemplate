using UnityEditor;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// メニューからパッケージを追加するクラス
    /// </summary>
    [InitializeOnLoad]
    public class PackageInstall : MenuConfig
    {
        /// <summary>
        /// メニューアイテムのパス
        /// </summary>
        private const string MENU_ITEM_PATH = "PackageInstaller/";
        /// <summary>
        /// メニューアイテムの優先度
        /// </summary>
        private const int MENU_ITEM_PRIORITY = 2;
        /// <summary>
        /// アイテムの名前
        /// </summary>
        private const string ITEM_NAME = "Install";

        /// <summary>
        /// インストールメニュー
        /// </summary>
        [MenuItem(TOP_MENU_PATH + MENU_ITEM_PATH, priority = MENU_ITEM_PRIORITY)]
        private static void MenuItem() { }

        /// <summary>
        /// インストール
        /// </summary>
        [MenuItem(TOP_MENU_PATH + MENU_ITEM_PATH + ITEM_NAME)]
        private static void Install()
        {
            PackageInstaller.InstallPackages();
        }
    }
}
