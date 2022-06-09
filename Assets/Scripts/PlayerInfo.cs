using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerInfo
{
    public int[] level = new int[5];
    public int[] score = new int[5];
    public int[] atempts = new int[5];

    public PlayerInfo(int level, int score, int  atempts)
    {
        int i = 0;
        for(i=0; i<5; i++)
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

    public PlayerInfo()
    {
        this.level = new int[] {1,2,3,4};
        this.score = new int[] { 0, 0, 0, 0};
        this.atempts = new int[] { 1, 1, 1, 1};
    }
}

