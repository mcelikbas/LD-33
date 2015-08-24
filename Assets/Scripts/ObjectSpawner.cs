using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{

    public int dir = 0;

    public float time = 0.0f;

    public float spawn_delay = 3.0f;

    public GameObject[] prefab;


    void Start ()
    {
        if(gameObject.CompareTag("HumanSpawner"))
        {
            Spawn();
        }
    }

    void Update ()
    {
        time += Time.deltaTime;

        if (time > spawn_delay)
        {
            Spawn();
            time = 0.0f;
        }
    }


    private void Spawn ()
    {
        int rnd = Random.Range(0, prefab.Length);
        GameObject go = Instantiate(prefab[rnd], transform.position, Quaternion.identity) as GameObject;
        if (go.CompareTag("Cloud") || go.CompareTag("Car"))
        {
            go.GetComponent<ObjectScroll>().Dir = dir;
        }
    }
}
