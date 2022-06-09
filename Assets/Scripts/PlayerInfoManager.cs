
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    string saveFile;
    private PlayerInfo playerInfo = new PlayerInfo();

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //AppData\LocalLow\DefaultCompany\Tap Jump
        saveFile = Application.persistentDataPath + "/gamedata.json";

    }

    public PlayerInfo readFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            playerInfo = JsonUtility.FromJson<PlayerInfo>(fileContents);
        }
        else
        {
            playerInfo = null;
        }
        return playerInfo;
    }

    public void writeFile(PlayerInfo playInfo)
    {
        PlayerInfo writenData = this.readFile();
        if (writenData != null)
        {
            for (int i = 0; i <= writenData.level.Length; i++)
            {
                if (playerInfo.score[i] < 0)
                {
                    playerInfo.score[i] = writenData.score[i];
                    playerInfo.atempts[i] = writenData.atempts[i];
                }
            }
        }
            string dataJSON = JsonUtility.ToJson(playInfo);

        File.WriteAllText(saveFile, dataJSON);
    }

}

