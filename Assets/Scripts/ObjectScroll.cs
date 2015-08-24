using UnityEngine;
using System.Collections;

public class ObjectScroll : MonoBehaviour
{

    private float speed;

    private int dir;

    public int Dir
    {
        set { dir = value; }
    }

    private float[] Splash_boundary = new float[2] { -8.5f, 8.5f };
    private float[] Game_boundary = new float[2] { -7.8f, 80.0f };

    private float[] boundary = new float[2];
    void Start ()
    {
        if (gameObject.CompareTag("Cloud"))
        {
            speed = Random.Range(1, 3);
        }
        else if (gameObject.CompareTag("Car"))
        {
            if (dir == 1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            speed = Random.Range(5, 15);
        }
        else if (gameObject.CompareTag("Sun"))
        {
            speed = 0.3f;
            Dir = 1;
        }


        if (Application.loadedLevel == 0)
        {
            boundary = Splash_boundary;
        }
        else if (Application.loadedLevel == 1)
        {
            boundary = Game_boundary;
        }
    }

    void Update ()
    {
        if ((dir == -1 && transform.position.x < boundary[0]) ||
            (dir == 1 && transform.position.x > boundary[1]))
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate ()
    {

        transform.Translate(new Vector3(dir * speed, 0, 0) * Time.deltaTime);
    }


}
