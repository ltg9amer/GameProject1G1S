using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePooler : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] private int poolSize;
    private List<GameObject> objectList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            objectList.Add(Instantiate(prefabList[GameManager.Instance.StageNumber - 1]));
            objectList[i].SetActive(false);
        }
    }

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj;

        if (objectList.Count > 0)
        {
            obj = objectList[0];
            objectList.RemoveAt(0);
        }
        else
        {
            obj = Instantiate(prefabList[GameManager.Instance.StageNumber - 1]);
        }

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        if (obj.gameObject.layer == 8)
        {
            GameManager.Instance.CurrentSpawnEnemy++;
        }
        else
        {
            GameManager.Instance.CurrentSpawnBoss++;
        }

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectList.Add(obj);
        
        if (obj.gameObject.layer == 8)
        {
            GameManager.Instance.CurrentDeadEnemy++;
        }
        else
        {
            GameManager.Instance.CurrentDeadBoss++;
        }
    }
}
