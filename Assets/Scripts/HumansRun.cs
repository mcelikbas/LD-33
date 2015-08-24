using UnityEngine;
using System.Collections;

public class HumansRun : MonoBehaviour
{

    public float dirX;
    public float dirY;
    public float speed = 6.0f;

    public float time = 0.0f;
    public float dir_change_delay = 3.0f;

    private float[] boundary = new float[4] { 0.0f, 70.0f, -0.3f, 0.2f };  // -x, x, -y, y

    private bool run = false;
    private Animator anim;


    void Start ()
    {
        anim = GetComponent<Animator>();

    }

    void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!run)
            {
                anim.SetBool("walk", true);
                run = true;
            }
            else
            {
                anim.SetBool("walk", false);
                run = false;
            }

        }
    }


    void FixedUpdate ()
    {
        time += Time.deltaTime;
        if (time > dir_change_delay)
        {
            dirX = Random.Range(-1, 2);
            dirY = Random.Range(-1, 2);
            time = 0.0f;
        }
        Move(dirX, dirY);
    }


    private void Move (float dirX, float dirY)
    {
        if (dirX == 0 && dirY == 0)
        {
            anim.SetBool("walk", false);
        }
        else
        {
            anim.SetBool("walk", true);
            transform.localScale = new Vector3(dirX, 1, 1);

            float posX = transform.position.x;
            float posY = transform.position.y;


            // -x boundary
            if (posX < boundary[0])
                transform.position = new Vector3(boundary[0], transform.position.y, transform.position.z);
            // +x boundary
            else if (posX > boundary[1])
                transform.position = new Vector3(boundary[1], transform.position.y, transform.position.z);

            // -y boundary
            if (posY < boundary[2])
                transform.position = new Vector3(transform.position.x, boundary[2], transform.position.z);
            // +y boundary
            else if (posY > boundary[3])
                transform.position = new Vector3(transform.position.x, boundary[3], transform.position.z);

           transform.Translate(new Vector3(dirX * speed, dirY * speed, 0) * Time.deltaTime);
        }
    }
}
