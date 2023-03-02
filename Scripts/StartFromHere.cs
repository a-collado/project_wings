#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StartFromHere : EditorWindow
{
    private static string path;
    private static Dictionary<string, Vector3> playerPositionHistory;
    private static Vector3 selectedPositionPoint;
    private static string selectedPositionName;
    private static string newPositionName = "Default1";
    private static GenericMenu menu;

    private static GameObject locationPointer;

    [MenuItem("Window/Start From Here")]

    public static void Init()
    {
        Debug.Log("[Start From Here: Init]");
        StartFromHere window = (StartFromHere)EditorWindow.GetWindow(typeof(StartFromHere));
        window.Show();
        readPlayerPositionHistory();

        Ray ray = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (locationPointer == null)
            {
                if (GameObject.Find("LocationPointer") != null)
                    locationPointer = GameObject.Find("LocationPointer");
                else
                    locationPointer = (GameObject)GameObject.Instantiate(Resources.Load("LocationPointer"), hit.point, Quaternion.identity);
            }
            
        }

    }



    void Update()
    {
        
        
        Ray ray = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (locationPointer == null)
            {
                if (GameObject.Find("LocationPointer") != null)
                    locationPointer = GameObject.Find("LocationPointer");
                else
                    locationPointer = (GameObject)GameObject.Instantiate(Resources.Load("LocationPointer"), hit.point, Quaternion.identity);
            }
            locationPointer.SetActive(true);
            locationPointer.transform.position = hit.point;
            locationPointer.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }

    
  
    void OnGUI()
    {
        
        readPlayerPositionHistory();
        
        


        if (playerPositionHistory == null)
        {
            playerPositionHistory = new Dictionary<string, Vector3>();
            playerPositionHistory.Add("Default", new Vector3(0, 0, 0));
        }


        GUILayout.Label("Move Player Here and Start", EditorStyles.boldLabel);

        menu = new GenericMenu();

        updateMenuContent();

        if (EditorGUILayout.DropdownButton(new GUIContent("Select Position..."), FocusType.Keyboard))
            menu.ShowAsContext();

        GUILayout.Space(20);
        newPositionName = EditorGUILayout.TextField("New Position Name", newPositionName);
        if (GUILayout.Button("Add Position"))
            addPosition();

        GUILayout.Label("Current Selected Position: " + selectedPositionName);
        if (GUILayout.Button("Start Here"))
            startHere();


        GUILayout.Space(40);
        if (GUILayout.Button("Reset Positions"))
            resetLocations();
    }

    
    void OnDisable()
    {
        savePlayerPositionHistory();
        GameObject.DestroyImmediate(locationPointer);
        GameObject.DestroyImmediate(GameObject.Find("LocationPointer"));
        Debug.Log("[Start From Here: Disabled]");
        

    }
    static void startHere()
    {
       
        GameObject.FindWithTag("Player").transform.position = selectedPositionPoint;
        savePlayerPositionHistory();
        GameObject.DestroyImmediate(locationPointer);
        //Switch from Editor to Game
        EditorApplication.ExecuteMenuItem("Edit/Play");
        //Debug.Log("Successfully moved player to position " + selectedPositionName);

    }


    static void addPosition()
    {
        //Add position to list
        Ray ray = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            if (newPositionName != "")
            {
                playerPositionHistory.Add(newPositionName, hit.point);
                savePlayerPositionHistory();
            }
            else
            {
                Debug.Log("[Start From Here]: You must insert a name for the position");
            }
        }
        else
        {
            Debug.Log("[Start From Here]:  No hit");
        }

    }

    public void updateMenuContent()
    {
        if (playerPositionHistory.Count != 0)
        {
            foreach (var pos in playerPositionHistory)
            {
                menu.AddItem(new GUIContent(pos.Key), false, selectPosition, pos.Key);
            }
        }
    }

    public void selectPosition(object param)
    {   
        selectedPositionName = (string)param;
        //Select position from list
        selectedPositionPoint = playerPositionHistory[selectedPositionName];
    }

    public void resetLocations()
    {
        playerPositionHistory = new Dictionary<string, Vector3>();
        playerPositionHistory.Add("Default", new Vector3(0, 0, 0));
        savePlayerPositionHistory();
    }


    //PERSISTENCE
    
    public static void savePlayerPositionHistory()
    { 

        Dictionary<string, SerializableVector3> serializablePlayerPositionHistory = new Dictionary<string, SerializableVector3>();
        foreach (var pos in playerPositionHistory)
        {
            serializablePlayerPositionHistory.Add(pos.Key, new SerializableVector3(pos.Value));
        }

        path = Application.dataPath + "/Scenes/"+ SceneManager.GetActiveScene().name +"/PlayerPositionHistory.json";
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, serializablePlayerPositionHistory);
        fs.Close();
    

    }

    public static void readPlayerPositionHistory()
    {
        
        path = Application.dataPath + "/Scenes/"+ SceneManager.GetActiveScene().name +"/PlayerPositionHistory.json";
        //Deserialize
        Dictionary<string, SerializableVector3> serializablePlayerPositionHistory = new Dictionary<string, SerializableVector3>();
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            serializablePlayerPositionHistory = (Dictionary<string, SerializableVector3>)bf.Deserialize(fs);
            fs.Close();

            playerPositionHistory = new Dictionary<string, Vector3>();
            foreach (var pos in serializablePlayerPositionHistory)
            {
                playerPositionHistory.Add(pos.Key, pos.Value.ToVector3());
            }
        } catch {
            Debug.Log("[Start From Here]: No PlayerPositionHistory.json found");
        }
        

    }


}

[System.Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
#endif