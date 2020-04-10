using System.IO;
using UnityEngine;


namespace GeekBrainsFPS
{
	public sealed class SaveDataRepository
	{
		#region Fields

		private readonly IData<SerializableGameObject> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        #endregion


        #region ClassLifeCycles

		public SaveDataRepository()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                _data = new PlayerPrefsData();
            }
            else
            {
                _data = new JSONData<SerializableGameObject>();
            }
            _path = Path.Combine(Application.dataPath, _folderName);
        }

		#endregion


		public void Save()
		{
			if (!Directory.Exists(Path.Combine(_path)))
			{
				Directory.CreateDirectory(_path);
			}
			var player = new SerializableGameObject
			{
				Pos = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform.position,
				Name = "Alex Tsurkan",
				IsEnable = true
			};

			_data.Save(player, Path.Combine(_path, _fileName));
		}

		public void Load()
		{
			var file = Path.Combine(_path, _fileName);
			if (!File.Exists(file)) return;
			var newPlayer = _data.Load(file);
            var characterController = ServiceLocatorMonoBehaviour.GetService<CharacterController>();
            characterController.transform.position = newPlayer.Pos;
            characterController.name = newPlayer.Name;
            characterController.gameObject.SetActive(newPlayer.IsEnable);
        }
	}
}