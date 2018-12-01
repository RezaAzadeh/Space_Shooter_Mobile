using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeuver : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public float tilt;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundry boundry;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}
	
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while(true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign (transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

	void FixedUpdate ()
    {
        float newaneuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newaneuver, 0, currentSpeed);
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundry.xMin, boundry.xMax),
            0,
            Mathf.Clamp(rb.position.z, boundry.zMin, boundry.zMax)
        );

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * tilt);
    }
}
