using System.IO;
using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class JSONData<T> : IData<T>
    {
        #region IData

        public void Save(T data, string path = null)
        {
            var str = JsonUtility.ToJson(data);
            File.WriteAllText(path, str);
        }

        public T Load(string path = null)
        {
            var str = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(str);
        }

        #endregion
    }
}