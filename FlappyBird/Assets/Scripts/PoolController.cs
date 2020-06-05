using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : Singleton<PoolController>
{
    [SerializeField] private List<PoolObject> m_ListObjects;
    private Dictionary<string, Queue<GameObject>> m_ObjectPools;

    private void Start()
    {
    }

    public void Initialize(Vector3 position)
    {
        m_ObjectPools = new Dictionary<string, Queue<GameObject>>();

        foreach (PoolObject obj in m_ListObjects)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < obj.size; ++i)
            {
                GameObject gameObject = Instantiate(obj.prefab, position, Quaternion.identity);
                gameObject.SetActive(false);
                queue.Enqueue(gameObject);
            }

            m_ObjectPools.Add(obj.tag, queue);
        }
    }

    public GameObject Get(string tag, Vector3 position, Quaternion quaternion)
    {
        if(!m_ObjectPools.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = m_ObjectPools[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = quaternion;

        m_ObjectPools[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}

[System.Serializable]
public class PoolObject
{
    public string tag;
    public GameObject prefab;
    public int size;
}
