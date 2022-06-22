using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    private List<GameObject> objectList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            objectList.Add(Instantiate(prefab));
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
            obj = Instantiate(prefab);
        }

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectList.Add(obj);
    }
}
