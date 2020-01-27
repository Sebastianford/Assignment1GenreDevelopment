using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    public KeyCode Up;
    public KeyCode Left;
    public KeyCode Right;
    public float force = 10;
    public float rforce = 10;
    public float move = 10;
    public Rigidbody rb;
   public bool floortouch = false;
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

        if (Input.GetKey(Right))
        {
            rb.AddForce(Vector3.right * rforce);
        }

        if (Input.GetKey(Left))
        {
            rb.AddForce(Vector3.left * rforce);
        }

        if (Input.GetKey(Right))
        {
            xmove += move * Time.deltaTime;
        }

        if (Input.GetKey(Left))
        {
            xmove -= move * Time.deltaTime;
        }



        xmove += this.transform.position.x;

        this.gameObject.transform.position = new Vector3(xmove, this.transform.position.y, this.transform.position.z);




    } 





    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "floor")
        {
            floortouch = true;
        }

        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            this.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "floor")
        {
            floortouch = false;

        }
    }
}
