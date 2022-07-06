using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//A Script to deal with the player data, it is serializable so that it can be converted to JSON and writen/read from a save file
public class PlayerInfo
{
    public int[] level = new int[5];
    public int[] score = new int[5];
    public int[] atempts = new int[5];
    /*Initializes a PlayerInfo with external information,
     it will check which level is being handled and add only that information leaving the other levels with clean data*/
    public PlayerInfo(int level, int score, int  atempts)
    {
        int i = 0;
        for(i=0; i<4; i++)
        {
            if (level == i)
            {
                this.level[i] = level;
                this.score[i] = score;
                this.atempts[i] = atempts;
            }
            else
            {
                this.level[i] = level;
                this.score[i] = -1;
                this.atempts[i] = -1;
            }
        }
        
    }
    //It creats a PlayerInfo with clean data, with only enough information so that the game can run properly
    public PlayerInfo()
    {
        this.level = new int[] {1,2,3,4};
        this.score = new int[] { 0, 0, 0, 0};
        this.atempts = new int[] { 1, 1, 1, 1};
    }
}

