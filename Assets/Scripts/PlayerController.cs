using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float dirX;
    public float dirY;
    public float speed = 6.0f;

    private float[] boundary = new float[4] { 0.0f, 68.0f, -0.6f, 1.6f};  // -x, x, -y, y


    private int score = 0;

    private Animator anim;

    public GameObject blood_splatter;
    public GameObject explosion;

    public Text scoreFloating;


    void Start ()
    {
        anim = GetComponent<Animator>();
    }


	void FixedUpdate () 
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        Move(dirX, dirY);
	}


    private void Move (float dirX, float dirY)
    {

        if (dirX == 0 && dirY == 0)
        {
            anim.SetBool("walk", false);
        }

        if (dirX != 0 || dirY != 0)
        {
            
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
            else if(posY > boundary[3])
                transform.position = new Vector3(transform.position.x, boundary[3], transform.position.z);

            
            transform.Translate(new Vector3(dirX * speed, dirY * speed, 0) * Time.deltaTime);
            anim.SetBool("walk", true);
        }
    }


    private void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.CompareTag("Human"))
        {
            KillHuman(col.gameObject);
        }
        if (col.gameObject.CompareTag("Car"))
        {
            KillCar(col.gameObject);
        }
    }


    private void KillHuman (GameObject h)
    {
        GameObject b = Instantiate(blood_splatter, h.transform.position, Quaternion.identity) as GameObject;
        Destroy(b, 0.7f);

        AudioSource audio = h.GetComponent<AudioSource>();
        audio.Play();
        h.GetComponent<BoxCollider2D>().enabled = false;
        h.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(h, 1.5f);

        score++;
        StartCoroutine(PopAddScore(1));
    }

    private void KillCar (GameObject c)
    {
        GameObject e = Instantiate(explosion, c.transform.position, Quaternion.identity) as GameObject;
        Destroy(e, 0.5f);

        c.GetComponent<BoxCollider2D>().enabled = false;
        c.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(c, 1.5f);

        score = score + 10;
        StartCoroutine(PopAddScore(10));
    }

    public int GetScore ()
    {
        return score;
    }


    IEnumerator PopAddScore (int n)
    {
        scoreFloating.enabled = true;
        scoreFloating.text = "+" + n;

        yield return new WaitForSeconds(1);
        scoreFloating.enabled = false;
    }
}
