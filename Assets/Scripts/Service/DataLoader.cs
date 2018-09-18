using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity.Test;
using CustomTools;
using UnityEditor;
using UnityEngine;

namespace Baron.Service
{
    [Serializable]
    public class Player
    {

        public string playerId;
        public string playerLoc;
        public string playerNick;
    }

    public class DataLoader: IDataLoader
    {
        public void TestJson()
        {
            Player playerInstance = new Player();
            playerInstance.playerId = "8484239823";
            playerInstance.playerLoc = "Powai";
            playerInstance.playerNick = "Random Nick";

            //Convert to Jason
            string playerToJason = JsonUtility.ToJson(playerInstance);
            CustomLogger.Log("Json: " + playerToJason);

            OptionJS_Test opt = new OptionJS_Test()
            {
                id="12",
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

            string optToJason = JsonUtility.ToJson(opt);
            CustomLogger.Log("Json: " + optToJason);
        }

        public void ReadFromJson()
        {
            TextAsset txtAsset = (TextAsset)Resources.Load("Raw/test1", typeof(TextAsset));
            string tileFile = txtAsset.text;
            string tileFile1 = Encoding.ASCII.GetString(txtAsset.bytes);
         
            OptionJS_Test opt = JsonUtility.FromJson<OptionJS_Test>(tileFile1);
            CustomLogger.Log("Json: " + opt.id);

            //string cyrillicText = "Ж";
            //System.Text.UTF8Encoding encodingUnicode = new System.Text.UTF8Encoding();
            //byte[] cyrillicTextByte = encodingUnicode.GetBytes(cyrillicText);
            //CustomLogger.Log(encodingUnicode.GetString(cyrillicTextByte));
            //CustomLogger.Log(cyrillicText);

            //string tileFile4=encodingUnicode.GetString(txtAsset.bytes);
            //CustomLogger.Log("Json from file: r " + tileFile4);
        }
    }
}
