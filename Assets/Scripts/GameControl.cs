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
    public float collectionPercentage; //the variable we want to save/load
    public PauseMenu pauseMenu;

    //Arrays for storing gamedata
    public float[] roomPercentage = new float[noOfRooms];
    public int[] noCollected = new int[noOfRooms];
    public int[] roomCollectables = new int[noOfRooms];
    public bool[] isCollected = new bool[noOfCollectables];

    //Ints for storing room data
    public const int noOfCollectables = 16;
    public const int noOfRooms = 6;
    public int roomNumber;

    //floats for storing player location and rotation
    public float PosX, PosY, PosZ;
    public float RotX, RotY, RotZ;
    Scene currentScene;
    
    public void Update()
    {
        //Percentage Calculation
        for (int i = 0; i < noOfRooms; i++)
        {
            roomPercentage[i] = (float)noCollected[i] / (float)roomCollectables[i] * 100;
        }
        collectionPercentage = (roomPercentage[0] + roomPercentage[1] + roomPercentage[2] + roomPercentage[3]) / noOfRooms;
        //UnityEngine.Debug.Log("Player Position: X = " + playerObj.transform.position.x + " --- Y = " + playerObj.transform.position.y + " --- Z = " + playerObj.transform.position.z); //debug no longer needed

        //Sets currentScene to the currently active Scene
        if (currentScene != SceneManager.GetActiveScene())
        {
            if (playerObj == null) playerObj = GameObject.Find("playerCharacter");
            if (playerController == null) playerController = playerObj.GetComponent<CharacterController>();
            if (pauseMenu == null)
            {
                GameObject canvas = GameObject.Find("Canvas");
                pauseMenu = canvas.GetComponent<PauseMenu>();
            }
            currentScene = SceneManager.GetActiveScene();
            Save("/autoSave.dat");
        }

        //Current room check
        if (currentScene.name == "Camp.lvl")
        {
            roomNumber = 0;
        }
        else if (currentScene.name == "K_Temple_Room 1")
        {
            roomNumber = 1;
        }
        else if (currentScene.name == "Templefinal")
        {
            roomNumber = 2;
        }
        else if (currentScene.name == "Dragon")
        {
            roomNumber = 3;
        }
        else if (currentScene.name == "DeanLVL")
        {
            roomNumber = 4;
        }
        else if (currentScene.name == "Room3")
        {
            roomNumber = 5;
        }
        //
        // Could the above be replaced with a roomNumber = (currentScene - 1) as then this allows for future levels to be added, provided the levels with paintings are all in order
        //
        if (currentScene.name != "Room3") RenderSettings.skybox.SetFloat("_Exposure", ((100 - collectionPercentage)/100));
    }

    void Start()
    {
        if (playerObj == null) playerObj = GameObject.Find("playerCharacter");
        if (playerController == null) playerController = playerObj.GetComponent<CharacterController>();
        if (pauseMenu == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            pauseMenu = canvas.GetComponent<PauseMenu>();
        }
        Save("/autoSave.dat");
    }

    //happens before start()
    void Awake()
    {
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

    public void Save(string saveName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveName); //tell where to save

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

        currentScene = SceneManager.GetActiveScene();
        data.savedScene = currentScene.buildIndex;

        bf.Serialize(file, data); //translate the data into binary and save to file
        file.Close();
    }

    public void Load(string saveName)
    {
        if (File.Exists(Application.persistentDataPath + saveName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveName, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file); //casts data pulled into playerdata object
            file.Close();
            noCollected = data.noCollected;
            isCollected = data.isCollected;
            collectionPercentage = data.collectionPercentage;
            PosX = data.PosX;
            PosY = data.PosY;
            PosZ = data.PosZ;
            RotX = data.RotX;
            RotY = data.RotY;
            RotZ = data.RotZ;

            if (data.savedScene != SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(data.savedScene);
            }

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
        else if (File.Exists(Application.persistentDataPath + "/autoSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/autoSave.dat", FileMode.Open);
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

            if (data.savedScene != SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(data.savedScene);
            }

            //disable controller, reposition player then re-enable controller
            playerController.enabled = false;
            playerObj.transform.position = new Vector3(PosX, (PosY + 0.5f), PosZ);
            playerObj.transform.eulerAngles = new Vector3(RotX, RotY, RotZ);
            playerController.enabled = true;

            //setting the game to un pause
            pauseMenu.resumeGame();
        }
        else SceneManager.LoadScene(currentScene.buildIndex);
    }

}

//private class for this only, it will be serialized so it fits into binary format
[Serializable]
public class PlayerData
{
    public float collectionPercentage;
    public int[] noCollected;
    public float[] roomPercentage;
    public bool[] isCollected;
    public const int noOfCollectables = 12;
    public float PosX, PosY, PosZ;
    public float RotX, RotY, RotZ;
    public int savedScene;
    //which paintings have been collected, so they cant be collected again

    public PlayerData() // default constructor
    {
        collectionPercentage = 0;
        PosX = 0f;
        PosY = -1.5f;
        PosZ = 0f;
    }
}