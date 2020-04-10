using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace GeekBrainsFPS
{
    public sealed class BinarySerializationData<T> : IData<T>
    {
        #region Fields

        private static BinaryFormatter _formatter;

        #endregion


        #region ClassLifeCycles

        public BinarySerializationData()
        {
            _formatter = new BinaryFormatter();
        }

        #endregion


        #region IData

        public void Save(T data, string path = null)
        {
            if (data == null && !String.IsNullOrEmpty(path)) return;
            if (!typeof(T).IsSerializable) return;
            using (var fs = new FileStream(path, FileMode.Create))
            {
                _formatter.Serialize(fs, data);
            }
        }

        public T Load(string path)
        {
            T result;
            if (!File.Exists(path)) return default(T);
            using (var fs = new FileStream(path, FileMode.Open))
            {
                result = (T)_formatter.Deserialize(fs);
            }
            return result;
        }

        #endregion
    }
}