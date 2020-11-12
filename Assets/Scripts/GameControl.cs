using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary; //enables us to write out a binary save file
using System.IO; //enabling input output 
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl control; //create instance of gamecontrol class
    private GameObject playerObj = null;
    public double collectionPercentage; //the variable we want to save/load
    public int noCollected;
    int noOfCollectables = 6;
    public float PosX, PosY, PosZ;
    public int sceneNumber = 1;
    bool isBeingLoaded = false;

    public void Update()
    {
        collectionPercentage = (double)noCollected / (double)noOfCollectables * 100;
        UnityEngine.Debug.Log("Player Position: X = " + playerObj.transform.position.x + " --- Y = " + playerObj.transform.position.y + " --- Z = " + playerObj.transform.position.z);
    }

    private void Start()
    {
        if (playerObj == null) playerObj = GameObject.Find("playerCharacter");
        if (GameControl.control.isBeingLoaded)
        {
            playerObj.transform.position = new Vector3(PosX, PosY, PosZ);
            GameControl.control.isBeingLoaded = false;
        }
    }

    //happens before start()
    void Awake()
    {
        if (playerObj == null) playerObj = GameObject.Find("playerCharacter");
        if (control == null) //check if control already exists and create accordingly
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
        GUI.Label(new Rect(10, 10, 300, 30), "Collection Percentage: " + collectionPercentage); //display on gui for testing purposes
        GUI.Label(new Rect(10, 20, 150, 30), "Collection Number: " + noCollected); //display on gui for testing purposes
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); //tell where to save

        PlayerData data = new PlayerData(); //create new instance of playerdata and set the variables based on the game at save
        data.noCollected = noCollected;
        data.collectionPercentage = collectionPercentage;
        data.PosX = playerObj.transform.position.x;
        data.PosY = playerObj.transform.position.y;
        data.PosZ = playerObj.transform.position.z;

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
            noCollected = data.noCollected;
            collectionPercentage = data.collectionPercentage;
            PosX = data.PosX;
            PosY = data.PosY;
            PosZ = data.PosZ;
            isBeingLoaded = true;
            SceneManager.LoadScene(1);
        }
    }

}

//private class for this only, it will be serialized so it fits into binary format
[Serializable]
public class PlayerData
{
    public double collectionPercentage;
    public int noCollected;
    public int noOfCollectables;
    public float PosX, PosY, PosZ;
    public int sceneNumber;
    //player level possibly - public int SceneID;
    //which paintings have been collected, so they cant be collected again

    public PlayerData()
    {
        collectionPercentage = 0;
        noCollected = 0;
        noOfCollectables = 6;
        PosX = 0f;
        PosY = -1.5f;
        PosZ = 0f;
        sceneNumber = 1;
    }
}