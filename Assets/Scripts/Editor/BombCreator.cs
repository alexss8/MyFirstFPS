using UnityEditor;
using UnityEngine;


namespace GeekBrainsFPS.Editor
{
    public class BombCreator : EditorWindow
    {
        public static GameObject ObjectInstantiate;
        public static Transform root;
        public int СountObject = 1;

        private void OnGUI()
        {
            GUILayout.Label("Настройки создания рандомных бомб вокруг рута", EditorStyles.boldLabel);
            ObjectInstantiate = EditorGUILayout.ObjectField("Укажите префаб бомбы",
                    ObjectInstantiate, typeof(GameObject), true) 
                as GameObject;
            root = EditorGUILayout.ObjectField(
                    "Укажите Объект на сцене, вокруг которого будем генерировать бомбы",
                    root, typeof(Transform), true)
                as Transform;
            СountObject = EditorGUILayout.IntSlider("Количество бомб",
                СountObject, 1, 10);
            var isPressed = GUILayout.Button("Создать бомбы");
            if (isPressed)
            {
                if (ObjectInstantiate)
                {
                    for (int i = 0; i < СountObject; i++)
                    {
                        var randomOffset = root.position + new Vector3(
                            Random.Range(2.0f, 5.0f), 0.0f, Random.Range(2.0f, 5.0f));
                        GameObject temp = Instantiate(ObjectInstantiate, randomOffset,
                            Quaternion.identity);
                        temp.transform.parent = root.transform;
                    }
                }
            }
        }
    }
}