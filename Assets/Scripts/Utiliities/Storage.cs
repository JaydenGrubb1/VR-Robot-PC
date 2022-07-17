using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Robot.Utilities
{
	public enum SerializationMethod
	{
		Binary,
		Json
	};

	public static class Storage
	{
		public static T Load<T>(FileInfo file, SerializationMethod method)
		{
			return Load<T>(file.FullName, method);
		}

		public static T Load<T>(string file, SerializationMethod method)
		{
			T data;

			switch (method)
			{
				case SerializationMethod.Binary:
					FileStream stream = new FileStream(file, FileMode.Open);
					BinaryFormatter formatter = new BinaryFormatter();

					try
					{
						data = (T)formatter.Deserialize(stream);
					}
					catch (SerializationException e)
					{
						Debug.Log("failed to deserialize: " + e.Message);
						throw;
					}
					finally
					{
						stream.Close();
					}

					return data;
				case SerializationMethod.Json:
					string json = File.ReadAllText(file);
					data = JsonUtility.FromJson<T>(json);

					return data;
				default:
					return default(T);
			}
		}

		public static void Save(object data, FileInfo file, SerializationMethod method)
		{
			Save(data, file.FullName, method);
		}

		public static void Save(object data, string file, SerializationMethod method)
		{
			switch (method)
			{
				case SerializationMethod.Binary:
					FileStream stream = new FileStream(file, FileMode.Create);
					BinaryFormatter formatter = new BinaryFormatter();

					try
					{
						formatter.Serialize(stream, data);
					}
					catch (SerializationException e)
					{
						Debug.Log("failed to serialize: " + e.Message);
						throw;
					}
					finally
					{
						stream.Close();
					}

					break;
				case SerializationMethod.Json:
					string json = JsonUtility.ToJson(data, true);

					File.WriteAllText(file, json);
					break;
			}
		}
	}
}