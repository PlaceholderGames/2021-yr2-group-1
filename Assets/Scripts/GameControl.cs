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
    public GameObject playerObj = null; //reference to player
    public CharacterController playerController; //reference to player controller
    public double collectionPercentage; //the variable we want to save/load
    public PauseMenu pauseMenu;

    //Arrays for storing gamedata
    public double[] roomPercentage = new double[noOfRooms];
    public int[] noCollected = new int[noOfRooms];
    public int[] roomCollectables = new int[noOfRooms];
    public bool[] isCollected = new bool[noOfCollectables];

    //Ints for storing room data
    public const int noOfCollectables = 7;
    public const int noOfRooms = 3;
    public int firstHalf = noOfRooms / 2;
    public int secondHalf = noOfRooms;
    public int roomNumber;

    //floats for storing player location and rotation
    public float PosX, PosY, PosZ;
    public float RotX, RotY, RotZ;
    private Scene currentScene;
    
    public void Update()
    {
        //Percentage Calculation
        for (int i = 0; i < noOfRooms; i++)
        {
            roomPercentage[i] = (double)noCollected[i] / (double)roomCollectables[i] * 100;
        }
        collectionPercentage = (roomPercentage[0] + roomPercentage[1]) / noOfRooms;
        //UnityEngine.Debug.Log("Player Position: X = " + playerObj.transform.position.x + " --- Y = " + playerObj.transform.position.y + " --- Z = " + playerObj.transform.position.z); //debug no longer needed
        if (playerObj == null) playerObj = GameObject.Find("playerCharacter");
        if (playerController == null) playerController = playerObj.GetComponent<CharacterController>();
        if (pauseMenu == null) pauseMenu = playerObj.GetComponent<PauseMenu>();
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Camp")
        {
            roomNumber = 0;
        }
        else if (currentScene.name == "TempleUpdated")
        {
            roomNumber = 1;
        }
        else if (currentScene.name == "Dragon")
        {
            roomNumber = 2;
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
        GUI.Label(new Rect(10, 20, 150, 30), "Collection Number: " + noCollected[roomNumber]); //display on gui for testing purposes
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); //tell where to save

        PlayerData data = new PlayerData(); //create new instance of playerdata and set the variables based on the game at save
        data.noCollected = noCollected;
        data.collectionPercentage = collectionPercentage;
        data.roomPercentage = roomPercentage;
        data.isCollected = isCollected;
        data.PosX = playerObj.transform.position.x;
        data.PosY = playerObj.transform.position.y;
        data.PosZ = playerObj.transform.position.z;
        data.RotX = playerObj.transform.eulerAngles.x;
        data.RotY = playerObj.transform.eulerAngles.y;
        data.RotZ = playerObj.transform.eulerAngles.z;

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
            RotX = data.RotX;
            RotY = data.RotY;
            RotZ = data.RotZ;

            //disable controller, reposition player then re-enable controller
            playerController.enabled = false;
            playerObj.transform.position = new Vector3(PosX, (PosY + 0.5f), PosZ);
            playerObj.transform.eulerAngles = new Vector3(RotX, RotY, RotZ);
            playerController.enabled = true;

            //setting the game to un pause
            pauseMenu.resumeGame();

            /*
            if (currentScene != SceneManager.GetActiveScene())
            {
                SceneManager.LoadScene(1);//need to change to load appropriate scene
            }
            */
            
        }
    }

}

//private class for this only, it will be serialized so it fits into binary format
[Serializable]
public class PlayerData
{
    public double collectionPercentage;
    public int[] noCollected;
    public double[] roomPercentage;
    public bool[] isCollected;
    public const int noOfCollectables = 7;
    public float PosX, PosY, PosZ;
    public float RotX, RotY, RotZ;
    //player level possibly - public int SceneID;
    //which paintings have been collected, so they cant be collected again

    public PlayerData() // default constructor
    {
        collectionPercentage = 0;
        PosX = 0f;
        PosY = -1.5f;
        PosZ = 0f;
    }
}