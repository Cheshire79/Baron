using Baron.Entity;
using Baron.History;
using Baron.Tools;
using CustomTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Baron.Service
{
	public interface IHistoryManager
	{
		void Sync(History.History history);
		History.History FetchHistory();
	}
	public class HistoryManager : IHistoryManager
	{
		private const string HistorKey = "History";
		private readonly object _thisLock = new object();
		private string _dataPath;
		private string _fileName = "history.json";
		private string _fileName1 = "history1.json";
		//	private File internalDir;
		//private File backupDir;
		public HistoryManager()
		{
			_dataPath = Application.persistentDataPath;
		}

		public History.History FetchHistory()
		{
			History.History history;
			try
			{
				history = LoadHistoryFromFile();
				//history = LoadHistoryFromZipFile();
				//loadHistory();
				//history = LoadHistoryFromFileUsingString();
				Validate(history);
			}
			catch (Exception e)
			{
				CustomLogger.Log(" History UnValid " + e);
				history = new History.History();
			}

			ValidatePlayer(history);
			ValidateSaves(history);

			Scenario scenario = history.GetScenario();
			if (scenario != null)
			{
				scenario.Init();
			}

			//history.version = BuildConfig.VERSION_CODE;//todo
			history.Cid = StringUtils.Cid();
			history.OnChange += PersistHistory;
			return history;
		}

		public void Sync(History.History history)
		{
			lock (_thisLock)
			{
				try
				{

					history.ValidateProgress();

					if (history.Cid != null)
					{
						PersistHistory(history);

					}

					//History.History newHistory = FetchHistory();

					//gameBase.setHistory(newHistory);
				}
				catch (Exception e)
				{
					CustomLogger.Log("HistoryManager " + e);
				}
			}
		}
		public void PersistHistory(History.History history)
		{
			if (history.CreatedAt == null) //todo
				history.CreatedAt = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
			//DateUtils.now();
			if (history.Cid == null)
				history.Cid = StringUtils.Cid();
			//if (history.version == 0)
			//	history.version = BuildConfig.VERSION_CODE;
			history.UpdatedAt = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
			ThreadExecutor.Instance.PushTask(() =>
		{
			try
			{
				SaveHistoryToFileUsingStringAndOrder(history);
				//saveHistoryToFile(history);
				//saveHistoryToZipFile(history);
				//saveHistoryToFileUsingString(history);
				//saveHistory(history);					
			}
			catch (Exception e)
			{
				CustomLogger.Log("HistoryManager cannot write to file " + e);
			}
		});
		}

		private void ResetPlayer(History.History history)
		{
			Player player = new Player();
			//Locale locale = new Locale();

			//if (presenter != null)
			//{
			//	ApiService apiService = presenter.getApiService();
			//	LocaleService localeService = presenter.getLocaleService();

			//	locale.code = localeService.get();

			//	player.device = apiService.getClientId();
			//	player.name = apiService.getClientName();

			//}
			//else
			//{
			//	locale.code = LocaleService.UK;
			//	player.device = "test";
			//	player.name = "test";
			//}

			//player.locale = locale;

			//history.player = player;
		}
		private void ValidateSaves(History.History history)
		{
			List<Save> restoredSaves = history.Saves;
			List<Save> validSaves = new List<Save>(History.History.SAVE_LIMIT);

			if (restoredSaves == null || restoredSaves.Count != History.History.SAVE_LIMIT)
			{
				for (int i = 0; i < History.History.SAVE_LIMIT; i++)
				{
					Save save = new Save();
					save.Order = i;

					validSaves.Add(save);
				}
			}
			else
			{
				foreach (Save save in restoredSaves)
				{
					if (save.IsValid())
					{
						validSaves.Add(save);
					}
					else
					{
						Save save1 = new Save();
						save1.Order = restoredSaves.IndexOf(save);

						validSaves.Add(save1);
					}
				}
			}

			history.Saves = validSaves;

			Save activeSave = history.ActiveSave;
			if (activeSave == null || !activeSave.IsValid())
			{
				activeSave = new Save();
			}

			history.ActiveSave = activeSave;
		}

		private void ValidatePlayer(History.History history)//todo
		{

			Player player = history.Player;

			if (player == null)
			{
				ResetPlayer(history);
				player = history.Player;
			}

			//String device = player.Device;
			//String name = player.Name;
			//String localeName = LocaleService.UK;

			//if (presenter != null)
			//{
			//	ApiService apiService = presenter.getApiService();
			//	LocaleService localeService = presenter.getLocaleService();
			//	if (apiService != null)
			//	{
			//		device = apiService.getClientId();
			//		name = apiService.getClientName();
			//	}

			//	if (localeService != null)
			//	{
			//		localeName = localeService.get();
			//	}
			//}

			//if (device == null || name == null)
			//{
			//	resetPlayer(history);
			//}
			//else if (!device.Equals(player.device) && !name.Equals(player.name))
			//{
			//	resetPlayer(history);
			//}

			//player = history.Player();

			//Locale locale = player.locale;
			//if (locale == null)
			//{
			//	locale = new Locale();
			//}

			//locale.code = localeName;

			//player.locale = locale;

			//history.player = player;
		}

		private void Validate(History.History history)
		{

			if (history == null)
			{
				throw new NullReferenceException("Missing history");
			}

			if (history.Day < 0)
			{
				// throw new IllegalArgumentException("Day should not be lower 0");
				throw new ArgumentException("Day should not be lower 0");
			}


			if (history.Ng < 0)
			{
				throw new ArgumentException("NG should not be lower 0");
			}

			if (false)//history.Version > BuildConfig.VERSION_CODE) todo
			{
				throw new ArgumentException("History version mismatch. History is reset");
			}

			foreach (AudioEntry entry in history.Audio)
			{
				if (!entry.IsValid())
				{
					throw new ArgumentException("Invalid entry: " + entry); //todo
				}
			}

			foreach (ImageEntry entry in history.Images)
			{
				if (!entry.IsValid())
				{
					throw new ArgumentException("Invalid entry: " + entry); //todo
				}
			}

			foreach (InteractionEntry entry in history.CompletedInteractions)
			{
				if (!entry.IsValid())
				{
					throw new ArgumentException("Invalid entry: " + entry); //todo
				}
			}

			foreach (Entry entry in history.GetInventory())
			{
				if (!entry.IsValid())
				{
					throw new ArgumentException("Invalid entry: " + entry); //todo
				}
			}

			foreach (Entry entry in history.GetSteps())
			{
				if (!entry.IsValid())
				{
					throw new ArgumentException("Invalid entry: " + entry); //todo
				}
			}
		}


		private MemoryStream CreateToMemoryStream(MemoryStream memStreamIn, string zipEntryName)
		{


			MemoryStream outputMemStream = new MemoryStream();
			ZipOutputStream zipStream = new ZipOutputStream(outputMemStream);

			zipStream.SetLevel(8); //0-9, 9 being the highest level of compression

			ZipEntry newEntry = new ZipEntry(zipEntryName);
			newEntry.DateTime = DateTime.Now;

			zipStream.PutNextEntry(newEntry);

			zipStream.Write(memStreamIn.ToArray(), 0, memStreamIn.ToArray().Length);
			zipStream.CloseEntry();

			zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
			zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.

			outputMemStream.Position = 0;
			return outputMemStream;

		}

		private void saveHistoryToZipFile(History.History history)
		{
			using (MemoryStream stream = new MemoryStream())
			using (StreamWriter writer = new StreamWriter(stream))
			using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
			{
				JsonSerializer ser = new JsonSerializer();
				ser.Serialize(jsonWriter, history);
				CustomLogger.Log("HistoryManager save history string" + history.GetScenario().CurrentTrackBranch.Id);
				CustomLogger.Log("HistoryManager  history.GetScenario().Cid ------------------------------=" + history.GetScenario().Cid);

				jsonWriter.Flush();
				stream.Position = 0;
				using (MemoryStream zipStream = CreateToMemoryStream(stream, "test"))
				{
					zipStream.Position = 0;
					using (var fileStream = File.Create(_dataPath + _fileName))
					{
						zipStream.Seek(0, SeekOrigin.Begin);
						CopyStream(zipStream, fileStream);
					}
				}
			}
		}
		private void saveHistoryToFileUsingString(History.History history)
		{
			string text = JsonConvert.SerializeObject(history);
			CustomLogger.Log("HistoryManager save history string" + text.Length);

			using (MemoryStream zipStream = CreateToMemoryStream(GenerateStreamFromString(text), "test"))
			{
				CustomLogger.Log("HistoryManager 3 " + _dataPath + _fileName);
				using (var fileStream = File.Create(_dataPath + _fileName1))
				{
					zipStream.Seek(0, SeekOrigin.Begin);
					CopyStream(zipStream, fileStream);
				}
			}
		}

		private MemoryStream GenerateStreamFromString(string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
		public void CopyStream(Stream input, Stream output)// to toest
		{
			byte[] buffer = new byte[32768];
			int read;
			while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, read);
			}
		}

		private History.History LoadHistoryFromZipFile()
		{
			try
			{
				using (FileStream fileStream = File.OpenRead(_dataPath + _fileName))
				using (ZipInputStream zipInputStream = new ZipInputStream(fileStream))
				{
					ZipEntry zipEntry = zipInputStream.GetNextEntry();
					History.History history = null;
					using (StreamReader sr = new StreamReader(zipInputStream))
					using (JsonReader reader = new JsonTextReader(sr))
					{
						JsonSerializer serializer = new JsonSerializer();
						history = serializer.Deserialize<History.History>(reader);
					}
					if (history != null)
						return history;
					else
					{
						CustomLogger.Log("HistoryManager cannot Deserialize history");
						return new History.History();
					}
				}
			}
			catch (Exception e)
			{
				CustomLogger.Log("HistoryManager " + e);
				return new History.History();
			}
		}

		private History.History LoadHistoryFromFileUsingString()
		{
			History.History history = null;
			FileStream fileStream;
			try
			{
				fileStream = File.OpenRead(_dataPath + _fileName1);

			}
			catch (Exception e)
			{
				CustomLogger.Log("HistoryManager " + e);
				return new History.History();
			}

			ZipInputStream zipInputStream = new ZipInputStream(fileStream);
			ZipEntry zipEntry = zipInputStream.GetNextEntry();

			StreamReader reader = new StreamReader(zipInputStream);

			string t = reader.ReadToEnd();
			CustomLogger.Log("HistoryManager Load history string" + t.Length);

			//ZipEntry zipEntry = zipInputStream.GetNextEntry();

			//History.History history = null;
			//using (StreamReader sr = new StreamReader(zipInputStream))

			{



				history = JsonConvert.DeserializeObject<History.History>(t);
			}
			if (history != null)
				return history;
			else
			{
				CustomLogger.Log("HistoryManager cannot Deserialize history");
				return new History.History();
			}
		}


		private void saveHistoryToFile(History.History history)
		{
			using (MemoryStream stream = new MemoryStream())
			using (StreamWriter writer = new StreamWriter(stream))
			using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
			{
				JsonSerializer ser = new JsonSerializer();
				ser.Serialize(jsonWriter, history);
				CustomLogger.Log("HistoryManager save history string" + history.GetScenario().CurrentTrackBranch.Id);
				CustomLogger.Log("HistoryManager  history.GetScenario().Cid ------------------------------=" + history.GetScenario().Cid);

				jsonWriter.Flush();
				stream.Position = 0;
				//	using (MemoryStream zipStream = CreateToMemoryStream(stream, "test"))
				{
					//zipStream.Position = 0;
					using (var fileStream = File.Create(_dataPath + _fileName))
					{
						stream.Seek(0, SeekOrigin.Begin);
						CopyStream(stream, fileStream);
					}
				}
			}
		}

		private History.History LoadHistoryFromFile()
		{
			try
			{
				using (FileStream fileStream = File.OpenRead(_dataPath + _fileName))
				//	using (ZipInputStream zipInputStream = new ZipInputStream(fileStream))
				{
					//		ZipEntry zipEntry = zipInputStream.GetNextEntry();
					History.History history = null;
					using (StreamReader sr = new StreamReader(fileStream))
					using (JsonReader reader = new JsonTextReader(sr))
					{
						JsonSerializer serializer = new JsonSerializer();
						history = serializer.Deserialize<History.History>(reader);
					}
					if (history != null)
						return history;
					else
					{
						CustomLogger.Log("HistoryManager cannot Deserialize history");
						return new History.History();
					}
				}
			}
			catch (Exception e)
			{
				CustomLogger.Log("HistoryManager " + e);
				return new History.History();
			}
		}

		private void SaveHistoryToFileUsingStringAndOrder(History.History history)
		{
			string text_temp = JsonConvert.SerializeObject(history);
			string text = NormalizeJsonString(text_temp);
			CustomLogger.Log("HistoryManager save history string" + text.Length);

			using (MemoryStream zipStream = GenerateStreamFromString(text))
			{
				CustomLogger.Log("HistoryManager 3 " + _dataPath + _fileName);
				using (var fileStream = File.Create(_dataPath + _fileName))
				{
					zipStream.Seek(0, SeekOrigin.Begin);
					CopyStream(zipStream, fileStream);
				}
			}
		}



		public static string NormalizeJsonString(string json)
		{
			// Parse json string into JObject.
			var parsedObject = JObject.Parse(json);

			// Sort properties of JObject.
			var normalizedObject = SortPropertiesAlphabetically(parsedObject);

			// Serialize JObject .
			return JsonConvert.SerializeObject(normalizedObject);
		}

		private static JObject SortPropertiesAlphabetically(JObject original)
		{
			var result = new JObject();

			foreach (var property in original.Properties().ToList().OrderBy(p => p.Name))
			{
				var value = property.Value as JObject;

				if (value != null)
				{
					value = SortPropertiesAlphabetically(value);
					result.Add(property.Name, value);
				}
				else
				{
					result.Add(property.Name, property.Value);
				}
			}

			return result;
		}
	}
}
