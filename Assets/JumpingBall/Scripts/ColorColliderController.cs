using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// Class that handles Collecting, Moving, Rotating, and Spawning Obstacles.
public class ColorColliderController : MonoBehaviour 
{
    public float verticalMin = 6f;                                              
    public float verticalMax = 10f;                                        
    public int currentlySelectedObstacle;                                    
    private int numOfObstacles;                                                
    public List<GameObject> pinkColliderGameObjects = new List<GameObject>();   
    public List<GameObject> purpleColliderGameObjects = new List<GameObject>(); 
    public List<GameObject> tealColliderGameObjects = new List<GameObject>();   
    public List<GameObject> yellowColliderGameObjects = new List<GameObject>();
    private List<GameObject> obstacleObjects = new List<GameObject>();         
    public List<GameObject> publicObstacleObjectList;                       
    public Vector3 originalObstaclePosition = new Vector3(0f, 0f, 0f);  
    private Vector3 newPoolPos = new Vector3(0f, 0f, 0f);                   
    private GameObject starPool, colorChangePool;                           
    private ObjectPoolScript starPoolScript, colorChangePoolScript;    
    private PlayerController pController;

    void Start () 
	{
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		
        pinkColliderGameObjects.AddRange(GameObject.FindGameObjectsWithTag("PinkCollider"));
        purpleColliderGameObjects.AddRange(GameObject.FindGameObjectsWithTag("PurpleCollider"));
        tealColliderGameObjects.AddRange(GameObject.FindGameObjectsWithTag("TealCollider"));
        yellowColliderGameObjects.AddRange(GameObject.FindGameObjectsWithTag("YellowCollider"));
        obstacleObjects.AddRange(GameObject.FindGameObjectsWithTag("ObstacleObject"));

        numOfObstacles = obstacleObjects.Count;
        publicObstacleObjectList = new List<GameObject>();

        for (int i = 0; i < numOfObstacles; i++)
        {
            GameObject obj = obstacleObjects[i];
            publicObstacleObjectList.Add(obj);
            obj.SetActive(false);
        }

        starPool = GameObject.FindGameObjectWithTag("StarPool");
        colorChangePool = GameObject.FindGameObjectWithTag("ColorChangePool");

        starPoolScript = starPool.GetComponent<ObjectPoolScript>();
        colorChangePoolScript = colorChangePool.GetComponent<ObjectPoolScript>();

        publicObstacleObjectList.Sort(SortByName);

        IncrementObstacleProgression();
        IncrementObstacleProgression();

        ChangeColorColliderState();
    }

    public void ChangeColorColliderState()
    {
        for (int i = 0; i < tealColliderGameObjects.Count; i++)
        {
            var item = tealColliderGameObjects[i];
            item.GetComponent<MeshCollider>().enabled = true;
        }

        for (int i = 0; i < pinkColliderGameObjects.Count; i++)
        {
            var item = pinkColliderGameObjects[i];
            item.GetComponent<MeshCollider>().enabled = true;
        }

        for (int i = 0; i < purpleColliderGameObjects.Count; i++)
        {
            var item = purpleColliderGameObjects[i];
            item.GetComponent<MeshCollider>().enabled = true;
        }

        for (int i = 0; i < yellowColliderGameObjects.Count; i++)
        {
            var item = yellowColliderGameObjects[i];
            item.GetComponent<MeshCollider>().enabled = true;
        }

        if (pController.playerColor == PlayerColor.Teal)
        {
            for (int i = 0; i < tealColliderGameObjects.Count; i++)
            {
                var item = tealColliderGameObjects[i];
                item.GetComponent<MeshCollider>().enabled = false;
            }
        }

        if (pController.playerColor == PlayerColor.Pink)
        {
            for (int i = 0; i < pinkColliderGameObjects.Count; i++)
            {
                var item = pinkColliderGameObjects[i];
                item.GetComponent<MeshCollider>().enabled = false;
            }
        }

        if (pController.playerColor == PlayerColor.Purple)
        {
            for (int i = 0; i < purpleColliderGameObjects.Count; i++)
            {
                var item = purpleColliderGameObjects[i];
                item.GetComponent<MeshCollider>().enabled = false;
            }
        }

        if (pController.playerColor == PlayerColor.Yellow)
        {
            for (int i = 0; i < yellowColliderGameObjects.Count; i++)
            {
                var item = yellowColliderGameObjects[i];
                item.GetComponent<MeshCollider>().enabled = false;
            }
        }
    }

    public void GetObstacle(int obstacleNum, bool rotateObstacle, float starPosDifference, float colorPosDifference)
    {
        float randDis = Random.Range(verticalMin, verticalMax);
        publicObstacleObjectList[obstacleNum].SetActive(false);
        float newDis = verticalMin * 0.5f;
        originalObstaclePosition.z = 0f;
        originalObstaclePosition.y += randDis;
        publicObstacleObjectList[obstacleNum].transform.position = originalObstaclePosition;

        if (rotateObstacle)
        {
            publicObstacleObjectList[obstacleNum].transform.eulerAngles = new Vector3(0f, 0f, 90f);
            publicObstacleObjectList[obstacleNum].SetActive(true);
            newPoolPos = originalObstaclePosition;
            GetStarObject(newPoolPos, starPosDifference);
            GetColorChangerObject(newPoolPos, newDis + colorPosDifference);
        }
        else
        {
            publicObstacleObjectList[obstacleNum].transform.eulerAngles = new Vector3(0f, 0f, 0f);
            publicObstacleObjectList[obstacleNum].SetActive(true);
            newPoolPos = originalObstaclePosition;
            GetStarObject(newPoolPos, starPosDifference);
            GetColorChangerObject(newPoolPos, newDis + colorPosDifference);
        }

    }

    public void GetStarObject(Vector3 newPos, float yOffset)
    {
        GameObject obj = starPoolScript.GetPooledObject();

        if (obj == null) return;

        obj.transform.position = newPos + new Vector3(0, yOffset, 0);
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);
    }

    public void GetColorChangerObject(Vector3 newPos, float Dis)
    {
        GameObject obj = colorChangePoolScript.GetPooledObject();

        if (obj == null) return;

        obj.transform.position = newPos + new Vector3( 0, Dis /** 0.5f*/, 0 );
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);
    }

    public void IncrementObstacleProgression()
    {
        bool shouldWeRotateObstacle;

        if(pController.playerColor == PlayerColor.Teal || pController.playerColor == PlayerColor.Pink)
        {
            shouldWeRotateObstacle = false;
        }
        else
        {
            shouldWeRotateObstacle = true;
        }

        if (currentlySelectedObstacle >= publicObstacleObjectList.Count)
        {
            currentlySelectedObstacle = 0;
        }

        switch (currentlySelectedObstacle)
        {
            case 0:
                GetObstacle (currentlySelectedObstacle , false, 0f , 0f );
                break;
            case 1:
                GetObstacle(currentlySelectedObstacle, false, 0f, 0f);
                break;
            case 2:
                GetObstacle(currentlySelectedObstacle, false, 2f, 0f);
                break;
            case 3:
                GetObstacle(currentlySelectedObstacle, shouldWeRotateObstacle, 0f, 0f);
                break;
            case 4:
                GetObstacle(currentlySelectedObstacle, false, 0f, 0f);
                break;
            case 5:
                GetObstacle(currentlySelectedObstacle, false, 0f, 0f);
                break;
            case 6:
                GetObstacle(currentlySelectedObstacle, false, 1.5f, 0f);
                break;
            case 7:
                GetObstacle(currentlySelectedObstacle, shouldWeRotateObstacle, 0f, 0f);
                break;
            case 8:
                GetObstacle(currentlySelectedObstacle, false, 3f, 0f);
                break;
            case 9:
                GetObstacle(currentlySelectedObstacle, false, 0f, 0f);
                break;
            case 10:
                GetObstacle(currentlySelectedObstacle, false, 0f, 0f);
                break;
            case 11:
                GetObstacle(currentlySelectedObstacle, false, 0f, 2.75f);
                break;
            case 12:
                GetObstacle(currentlySelectedObstacle, false, 0f, 0f);
                break;
            case 13:
                GetObstacle(currentlySelectedObstacle, false, 2f, 0f);
                break;
            case 14:
                GetObstacle(currentlySelectedObstacle, false, 3f, 0f);
                break;
            case 15:
                GetObstacle(currentlySelectedObstacle, false, 2f, 0f);
                break;
            case 16:
                GetObstacle(currentlySelectedObstacle, shouldWeRotateObstacle, 0f, 0f);
                break;
            case 17:
                GetObstacle(currentlySelectedObstacle, false, 0f, 0f);
                break;
            case 18:
                GetObstacle(currentlySelectedObstacle, false, 2f, -0.5f);
                break;
            case 19:
                GetObstacle(currentlySelectedObstacle, false, 1f, 3.25f);
                break;
            case 20:
                GetObstacle(currentlySelectedObstacle, false, 0f, 0f);
                break;
            case 21:
                GetObstacle(currentlySelectedObstacle, false, 1.25f, 0f);
                break;
            default:
                break;
        }

        currentlySelectedObstacle++;
    }

    private static int SortByName(GameObject obstacle1, GameObject obstacle2)
    {
        return obstacle1.name.CompareTo(obstacle2.name);
    }
}
