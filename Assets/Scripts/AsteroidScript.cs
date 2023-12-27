using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidScript : MonoBehaviour
{
    public float speed;
    public float rotate;
    public Rigidbody rd;
    public int asteroid;   // 0 = Large, 1 = Medium, 2 = Small
    public GameObject asteroid_M;
    public GameObject asteroid_S;
    public GameObject spaceship;
    public SpaceshipScript spaceshipScript;
    public GameObject explosion;
    public GameObject levelHandler;
    public LevelHandlerScript levelHandlerScript;
    public Vector3 velocity;
    public Vector3 torque;


    // Start is called before the first frame update
    void Start()
    {
        // Set velocity, add torque to asteroid
        velocity = new Vector3(Random.Range(-speed, speed), 0, Random.Range(-speed, speed));
        rd.velocity = velocity;
        torque = new Vector3(Random.Range(-rotate, rotate), 0, Random.Range(-rotate, rotate));
        rd.AddTorque(torque * Time.deltaTime);

        // Get access to SpaceshipScript to update score
        spaceship = GameObject.Find("Spaceship");
        spaceshipScript = spaceship.GetComponent<SpaceshipScript>();
        // Get access to level handler to update num of asteroids
        levelHandler = GameObject.Find("LevelHandler");
        levelHandlerScript = levelHandler.GetComponent<LevelHandlerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            // Destroy the bullet
            Destroy(other.gameObject);
            // Destroy asteroid, add explosion effects
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);

            // Check asteroid type
            if (asteroid == 0) 
            {
                // Large asteroid, break into smaller
                Instantiate(asteroid_M, transform.position, transform.rotation);
                Instantiate(asteroid_M, transform.position, transform.rotation);
                // Update score & num of asteroids
                spaceshipScript.score += 1;
                spaceshipScript.relativeScore += 1;
                levelHandlerScript.updateNumAsteroids(1);
            }
            else if (asteroid == 1)
            {
                // Medium asteroid, break into smaller
                Instantiate(asteroid_S, transform.position, transform.rotation);
                Instantiate(asteroid_S, transform.position, transform.rotation);
                // Update score & num of asteroids
                spaceshipScript.score += 2;
                spaceshipScript.relativeScore += 2;
                levelHandlerScript.updateNumAsteroids(1);
            }
            else if (asteroid == 2)
            {
                // Small asteroid, destroy
                // Update score & num of asteroids
                spaceshipScript.score += 3;
                spaceshipScript.relativeScore += 3;
                levelHandlerScript.updateNumAsteroids(-1);
            }

            // Destroy the explosion effect after 3 sec
            Destroy(newExplosion,3f);
        }
    }


}
