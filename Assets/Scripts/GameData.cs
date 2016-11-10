using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class GameData {

    public static GameData current;
    private int[] highScores;
    private string fileName;

	public GameData () {
        initData();
        //fileName = Application.persistentDataPath + "/saved.scores";
        fileName = "Saves/saved.scores";
    }

    private void initData()
    {
        if (File.Exists(fileName))
        {
            loadData();

        }
        else
        {
            highScores = new int[10];
        }
    }

    private void loadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(fileName, FileMode.Open);
        this.highScores = (int[])bf.Deserialize(file);
        file.Close();
    }

    public void saveData()
    {
        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileName);
        bf.Serialize(file, highScores);
        file.Close();
    }

    public void updateData(int level)
    {
        int legitSpot = -1;
        for(int i = 0; i < 10; i++)
        {
            if(level > highScores[i])
            {
                legitSpot = i;
                break;
            }
        }

        if(legitSpot > -1)
        {
            for(int j = 9; j > legitSpot; j--)
            {
                highScores[j] = highScores[j - 1];
            }
            highScores[legitSpot] = level;
            //saveData();
        }
    }
	
}
