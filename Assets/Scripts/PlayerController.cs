using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundry
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{

    public float speed;
    public float tilt;
    public Boundry boundry;

    public GameObject shot;
    public Transform shotSpwan;
    public float fireRate;

    private float nextFire;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpwan.position, shotSpwan.rotation);
            this.GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        this.gameObject.GetComponent<Rigidbody>().velocity = movement * speed;

        this.gameObject.GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(this.gameObject.GetComponent<Rigidbody>().position.x, boundry.xMin, boundry.xMax),
            0,
            Mathf.Clamp(this.gameObject.GetComponent<Rigidbody>().position.z, boundry.zMin, boundry.zMax)
        );

        this.gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, this.gameObject.GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
