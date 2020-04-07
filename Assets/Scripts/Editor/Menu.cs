using UnityEditor;


namespace GeekBrainsFPS.Editor
{
    public class MenuItems
    {
        [MenuItem("FPS/Generate Random Bombs")]
        private static void MenuOption()
        {
            EditorWindow.GetWindow(typeof(BombCreator), false, "FPS");
        }
    }
}