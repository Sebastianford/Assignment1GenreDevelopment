using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Test : MonoBehaviour
{
    public KeyCode Up;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode dash;
    public int HP;
    public float force = 10;
    public float rforce = 10;
    public float mforce = -10;
    public float dforce = 10;
    public float jforce = 10;
    public float move = 10;
    public Rigidbody rb;
    public bool floortouch = false;
    public bool jumptouch = false;
    public bool dashing = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xmove = 0;

        if (Input.GetKeyDown(Up) && floortouch == true)
        {
            rb.AddForce(Vector3.up * force);
        }

        if (Input.GetKeyDown(Up) && jumptouch == true)
        {
            jumptouch = false;
            rb.AddForce(Vector3.up * jforce);
        }



        if (Input.GetKey(Right))
        {
            xmove += move * Time.deltaTime;

            if (Input.GetKeyDown(dash) && dashing == true)
            {
                rb.AddForce(Vector3.right * dforce);
                StartCoroutine(Pain());
                dashing = false;
                StartCoroutine(Dash());

            }
        }

        if (Input.GetKey(Left))
        {
            xmove -= move * Time.deltaTime;

            if (Input.GetKeyDown(dash) && dashing == true)
            {
                rb.AddForce(Vector3.left * dforce);
                StartCoroutine(Pain());
                dashing = false;
                StartCoroutine(Dash());

            }
        }

        xmove += this.transform.position.x;

        this.gameObject.transform.position = new Vector3(xmove, this.transform.position.y, this.transform.position.z);




    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "floor")
        {
            floortouch = true;
            jumptouch = false;
        }

        if (other.tag == "Enemy" && this.tag == "Player")
        {
            HP -= 1;

            if (HP <= 0)
            {
                SceneManager.LoadScene("Death");
            }
        }

        if (other.tag == "jump plus")
        {
            Destroy(other.gameObject);
            jumptouch = true;
         
        }

        if (other.tag == "dash plus")
        {
            Destroy(other.gameObject);
            dashing = true;
            
        }

        if (other.tag == "Goal")
        {
            SceneManager.LoadScene("Victory");
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "floor")
        {
            floortouch = false;

        }
    }


    IEnumerator Dash()
    {

        if (dashing == false)
        {

           yield return new  WaitForSeconds(3);
            dashing = true;

        }

    }

    IEnumerator Pain()
    {


            transform.tag = "Attack";
            yield return new WaitForSeconds(1);
            transform.tag = "Player";
    }


} 

