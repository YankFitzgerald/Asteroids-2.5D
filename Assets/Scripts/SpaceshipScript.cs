using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceshipScript : MonoBehaviour
{
    public Rigidbody rd;
    private int moveSpeed = 450;
    private int rotateSpeed = 300;
    private float v;
    private float h;
    private int bulletSpeed = 100000;
    public GameObject bullet;
    public int score;
    public int relativeScore;
    public int threshold = 35;
    public Text scoreText;
    public Text finalScoreText;
    public GameObject explosion;
    public GameObject gameOverPanel;
    public bool shoot = true;
    public GameObject supplyPack;
    public GameObject shockWave;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        relativeScore = 0;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        // Spaceship control
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        rd.AddRelativeForce(Vector3.forward * v * moveSpeed * Time.deltaTime);
        transform.Rotate(0, h * rotateSpeed * Time.deltaTime, 0);

        // Fire
        if (Input.GetButtonDown("Fire1"))
        {
            if (shoot) 
            {
                GameObject shootBullet = Instantiate(bullet, transform.position, transform.rotation);
                shootBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed * Time.deltaTime);
            }
        }

        // Update score
        scoreText.text = "Score: " + score;

        // Generate a supply pack for every 35 points earned
        if (relativeScore >= threshold) {
            relativeScore -= threshold;
            Vector3 position = new Vector3(Random.Range(-14f, 14f), 1, Random.Range(-10f, 10f));
            Instantiate(supplyPack, position, Quaternion.identity);
        }
    }

    // Eat supply pack
    private void OnTriggerEnter(Collider other)
    {
        GameObject supPack = other.gameObject;
        if (supPack.tag == "Supply Pack")
        {
            // Eat supply pack, show particle effect
            Destroy(other.gameObject);
            GameObject newShockWave = Instantiate(shockWave, supPack.GetComponent<Rigidbody>().position, Quaternion.identity);
            BulletTime();
            // Destroy the shock wave effect after 3 sec
            Destroy(newShockWave, 3f);
        }
    }

    // Stop all asteroids for a few seconds
    public void BulletTime()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject ast in asteroids)
        {
            StartCoroutine(freeze(ast));
        }
    }

    IEnumerator freeze(GameObject ast)
    {
        // Record original speed
        Rigidbody rd = ast.GetComponent<Rigidbody>();
        Vector3 velocity = rd.velocity;
        // Freeze all asteroids for 3.5 seconds
        rd.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(3.5f);
        // Unfreeze
        rd.constraints = RigidbodyConstraints.None;
        rd.constraints = RigidbodyConstraints.FreezePositionY;
        rd.velocity = velocity;
    }

    // Spaceship hit asteroid
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            // Boom, then Game Over
            Instantiate(explosion, transform.position, transform.rotation);
            GameOver();
        }
    }

    private void GameOver()
    {
        // Make spaceship invisible, disable shoot
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        shoot = false;
        finalScoreText.text = "Final Score: " + score;
        // Show Game Over panel
        gameOverPanel.SetActive(true);
    }

    // Reload main scene
    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }
    // Back to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
