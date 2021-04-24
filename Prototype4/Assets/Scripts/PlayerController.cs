using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRB;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public GameObject powerupIdnicator;

    private float powerupStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        //player movements
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIdnicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        //item power up 
        hasPowerup = true;
        powerupIdnicator.gameObject.SetActive(true);
        if (other.CompareTag("Powerup"))
            Destroy(other.gameObject);
        StartCoroutine(PowerupCountdownRoutine());
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIdnicator.gameObject.SetActive(false);
    }


     //powerup
    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with:" + collision.gameObject.name + "with powerup set to" + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        } 
    }
}
