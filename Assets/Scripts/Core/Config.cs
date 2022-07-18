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

		public static int LastUsedProfile
		{
			get { return config.lastUsedProfile; }
			set
			{
				if (value < 0 || value > config.profiles.Count)
					throw new ArgumentOutOfRangeException("LastUsedProfile");

				config.lastUsedProfile = value;
				Save();
			}
		}

		public static int ProfileCount
		{
			get { return config.profiles.Count; }
		}

		public static ConfigProfile GetProfile(int index)
		{
			if (index < 0 || index > config.profiles.Count)
				throw new ArgumentOutOfRangeException("ConfigProfile");

			return config.profiles[index];
		}

		public static ConfigProfile[] GetProfiles()
		{
			return config.profiles.ToArray();
		}

		public static void SetProfile(int index, ConfigProfile profile)
		{
			if (index < 0 || index > config.profiles.Count)
				throw new ArgumentOutOfRangeException("ConfigProfile");

			config.profiles[index] = profile;
			Save();
		}

		public static void AddProfile(ConfigProfile profile)
		{
			config.profiles.Add(profile);
			Save();
		}

		[Serializable]
		private class ConfigData
		{
			public int lastUsedProfile = 0;
			public List<ConfigProfile> profiles = new List<ConfigProfile>();
		}

		[Serializable]
		public struct ConfigProfile
		{
			public string name;
			public Vector3 leftShoulderOffset;
			public Vector3 rightShoulderOffset;
			public float leftArmLength;
			public float rightArmLength;
		}
	}
}