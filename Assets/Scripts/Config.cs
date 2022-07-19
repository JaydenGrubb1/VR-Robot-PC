using Robot.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Robot
{
	public static class Config
	{
		private const string FILE_NAME = "config.json";

		private static ConfigData config;

		public static void Save()
		{
			if (config == null)
				config = new ConfigData();

			Storage.Save(config, FILE_NAME, SerializationMethod.Json);
		}

		public static void Load()
		{
			if (File.Exists(FILE_NAME))
				config = Storage.Load<ConfigData>(FILE_NAME, SerializationMethod.Json);
			else
				config = new ConfigData();
		}

		[Serializable]
		private class ConfigData
		{
			public Vector3 headOffset;
		}
	}
}