using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//Script responsible for managing read and write access to the save file located at Aplication.persistentDataPath 
public class PlayerInfoManager : MonoBehaviour
{
    string saveFile;
    private PlayerInfo playerInfo = new PlayerInfo();


    private void Awake()
    {
        //Sets the path to create/access the save file
        saveFile = Application.persistentDataPath + "/gamedata.json";

    }

    /*Reads the save file if said file exists and returns a PlayerInfo object with the player data,
     if it doesn't exists creates and returns a PlayerInfo with clean data*/
    public PlayerInfo readFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            playerInfo = JsonUtility.FromJson<PlayerInfo>(fileContents);
        }
        else
        {
            playerInfo = new PlayerInfo();

        }
        return playerInfo;
    }
    /*Creats a save file and writes clean data so that the game can have initialized player data on the first run*/
    public void Create(){
        File.Create(saveFile);
        playerInfo = new PlayerInfo();
        this.writeFile(playerInfo);  
    }
    /*Checks the content on the atual player data and compares with already is writen on the save file. 
    If the new date has a better it will overwrite what is already there*/
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

