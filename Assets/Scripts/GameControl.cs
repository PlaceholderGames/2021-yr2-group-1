using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary; //enables us to write out a binary save file
using System.IO; //enabling input output 

public class GameControl : MonoBehaviour
{
    public static GameControl control; //create instance of gamecontrol class

    public float collectionPercentage; //the variable we want to save/load

    //replaced start with awake as it happens first
    void Awake()
    {
        if(control == null) //check if control already exists and create accordingly
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this) //if a gameobject already exists but isn't this then replace it with the current one to keep values
        {
            Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 30), "Collection Percentage: " + collectionPercentage); //display on gui for testing purposes
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); //tell where to save

        PlayerData data = new PlayerData(); //create new instance of playerdata and set the variables based on the game at save
        data.collectionPercentage = collectionPercentage;

        bf.Serialize(file, data); //translate the data into binary and save to file
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file); //casts data pulled into playerdata object
            file.Close();

            collectionPercentage = data.collectionPercentage;
        }
    }

}

//private class for this only, it will be serialized so it fits into binary format
[Serializable]
class PlayerData
{
    public float collectionPercentage;

    public PlayerData()
    {
        collectionPercentage = 0;
    }
}