using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Entity.Test;
using CustomTools;
using Newtonsoft.Json;
using UnityEngine;

using Baron.Entity.Test;
using Baron.Entity;

namespace Baron.Service
{
	[Serializable]
	public class Player
	{

		public string playerId;
		public string playerLoc;
		public string playerNick;
	}

	public class DataLoader : IDataLoader
	{
		public void LoadData(GameBase gameBase)
		{

			TextAsset txtAsset = (TextAsset)Resources.Load("Raw/tree", typeof(TextAsset));
			string tileFile = Encoding.UTF8.GetString(txtAsset.bytes);
			Entity.Tree tree = JsonConvert.DeserializeObject<Entity.Tree>(tileFile);
			gameBase.Tree=tree;
			 txtAsset = (TextAsset)Resources.Load("Raw/options", typeof(TextAsset));
			tileFile = Encoding.UTF8.GetString(txtAsset.bytes);
			List<Option> options = JsonConvert.DeserializeObject<List<Option>>(tileFile);
			gameBase.OptionRegistry= options;

		}
		public void TestLoadOptionJson()
		{
			TextAsset txtAsset = (TextAsset)Resources.Load("Raw/options", typeof(TextAsset));
			string tileFile = Encoding.UTF8
				//ASCII
				.GetString(txtAsset.bytes);
			List<Option> options = JsonConvert.DeserializeObject<List<Option>>(tileFile);
			foreach (var item in options)
				CustomLogger.Log("new Json option urf8: " + item.Id + " " + item.Text);
		}

		public void TestLoadTreeJson()
		{
			TextAsset txtAsset = (TextAsset)Resources.Load("Raw/tree", typeof(TextAsset));
			string tileFile = Encoding.UTF8
				//ASCII
				.GetString(txtAsset.bytes);
			Entity.Tree tree = JsonConvert.DeserializeObject<Entity.Tree>(tileFile);
			CustomLogger.Log("new Json brunch1 urf8: " + tree.OptionId + " " );
			foreach (var item1 in tree.InventoryBranches)
			{
			//	CustomLogger.Log("new Json option urf8: " + item1.Branches + " " + item.Text);
				foreach (var item2 in item1.Branches)
					CustomLogger.Log("new Json brunch2 urf8: " + item2.OptionId + " " + item2.Action);
			}
		}
		public void TestLoadDataJson()
		{
			TestArraySerialization();

			//https://www.newtonsoft.com/json/help/html/Samples.htm#!

			TextAsset txtAsset = (TextAsset)Resources.Load("Raw/testArray", typeof(TextAsset));
			string tileFile = Encoding.ASCII.GetString(txtAsset.bytes);
			AudioArrayHolder a1 = JsonConvert.DeserializeObject<AudioArrayHolder>(tileFile);
			CustomLogger.Log("new Json [0]: " + a1.audio[0].Id + " " + a1.audio[0].Duration);
			TrackAudioArrayHolder a2 = JsonConvert.DeserializeObject<TrackAudioArrayHolder>(tileFile);
			foreach (var item in a2.audio)
				CustomLogger.Log("new Json [0]: " + item.Id + " " + a2.audio[0].Duration);

		}
		private void TestArraySerialization()
		{
			AudioArrayHolder audios = new AudioArrayHolder()
			{
				audio = new List<AudioJS_Test>(){
					new AudioJS_Test() { Id = "au1", Duration = 1 },
					new AudioJS_Test() { Id = "aa1c", Duration = 2 },
					new AudioJS_Test() { Id = "au1", Duration = 3 },
					new AudioJS_Test() { Id = "aa1c", Duration = 4 }
					}
			};
			string audiosToJason = JsonUtility.ToJson(audios);
			CustomLogger.Log("audio array in Json: " + audiosToJason);
		}
		public void TestArrayJson()
		{
			List<TrackAudio> au = new List<TrackAudio>{
					new TrackAudio() { Id = "au1", Duration = 1 },
					new TrackAudio() { Id = "aa1c", Duration = 2 },
					new TrackAudio() { Id = "au1", Duration = 3 },
					new TrackAudio() { Id = "aa1c", Duration = 4 }
					};
			string json = JsonConvert.SerializeObject(au);
			CustomLogger.Log("new Json array : " + json);
		}
		public void TestJson()
		{
			Player playerInstance = new Player();
			playerInstance.playerId = "8484239823";
			playerInstance.playerLoc = "Powai";
			playerInstance.playerNick = "Random Nick";

			//Convert to Jason
			//string playerToJason = JsonUtility.ToJson(playerInstance);
			//CustomLogger.Log("Json: " + playerToJason);

			OptionJS_Test opt = new OptionJS_Test()
			{
				id = "12",
				text = "bla-blo",
				images = new List<ImageJS_Test>()
				{
					new ImageJS_Test(){Id="a1",Duration = 100},
					new ImageJS_Test(){Id="a1a",Duration = 102},
					new ImageJS_Test(){Id="a1c",Duration = 1001}
				},
				audio = new List<AudioJS_Test>()
				{
					new AudioJS_Test(){Id="au1",Duration = 200},
					new AudioJS_Test(){Id="aa1c",Duration = 2001}
				}
			};

			string json = JsonConvert.SerializeObject(opt);
			CustomLogger.Log("Json: " + json);
			//string optToJason = JsonUtility.ToJson(opt);
			//CustomLogger.Log("Json: " + optToJason);
		}

		public void ReadFromJson()
		{
			//TextAsset txtAsset = (TextAsset)Resources.Load("Raw/test1", typeof(TextAsset));
			//string tileFile = txtAsset.text;
			//string tileFile1 = Encoding.ASCII.GetString(txtAsset.bytes);

			//OptionJS_Test opt = JsonUtility.FromJson<OptionJS_Test>(tileFile1);
			//CustomLogger.Log("Json: " + opt.id);

			////string cyrillicText = "Ж";
			////System.Text.UTF8Encoding encodingUnicode = new System.Text.UTF8Encoding();
			////byte[] cyrillicTextByte = encodingUnicode.GetBytes(cyrillicText);
			////CustomLogger.Log(encodingUnicode.GetString(cyrillicTextByte));
			////CustomLogger.Log(cyrillicText);

			////string tileFile4=encodingUnicode.GetString(txtAsset.bytes);
			////CustomLogger.Log("Json from file: r " + tileFile4);
		}
	}
}
