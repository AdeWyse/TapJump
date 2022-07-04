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
            for(int i = 0; i<4; i++){
                if (playInfo.score[i] >= writenData.score[i])
                {
                   writenData.score[i] = playInfo.score[i];
                    writenData.atempts[i] = playInfo.atempts[i];
                }
             }
            
        }
            string dataJSON = JsonUtility.ToJson(writenData);

        File.WriteAllText(saveFile, dataJSON);
    }

}

