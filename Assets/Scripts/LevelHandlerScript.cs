using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandlerScript : MonoBehaviour
{
    public int numAsteroids;
    public int level = 1;
    public Text levelText;
    public GameObject largeAsteroid;
    public GameObject mediumAsteroid;
    public GameObject smallAsteroid;
    public GameObject spaceship;
    public GameObject level1Panel;
    public GameObject level2Panel;
    public GameObject winPanel;

    // Start is called before the first frame update
    void Start()
    {
        spaceship = GameObject.Find("Spaceship");
        levelText.text = "Level: " + level;
    }

    // Update the number of asteroids
    // If no asteroid left, load next level after 2.5 seconds 
    public void updateNumAsteroids(int num)
    {
        numAsteroids += num;
        if (numAsteroids <= 0)
        {
            if (level == 1)
            {
                // Load level 2
                StartCoroutine(loadLevel2());
            }
            else if (level == 2)
            {
                // Load level 3
                StartCoroutine(loadLevel3());
            }
            else
            {
                //Win
                winPanel.SetActive(true);
            }
        }
    }

    IEnumerator loadLevel2()
    {
        level1Panel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        level1Panel.SetActive(false);
        Level2();
    }
    IEnumerator loadLevel3()
    {
        level2Panel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        level2Panel.SetActive(false);
        Level3();
    }

    // Initialize level 2
    public void Level2() 
    {
        level++;
        levelText.text = "Level: " + level;
        numAsteroids = 5;
        resetSpaceship();
        // Set Asteroids
        Instantiate(largeAsteroid, new Vector3(9.99f, 0f, 8.64f), Quaternion.identity);
        Instantiate(largeAsteroid, new Vector3(-12.93f, 0f, -1.93f), Quaternion.identity);
        Instantiate(mediumAsteroid, new Vector3(-5.51f, 0f, -9.42f), Quaternion.identity);
        Instantiate(smallAsteroid, new Vector3(-4.25f, 0f, 7.1f), Quaternion.identity);
        Instantiate(smallAsteroid, new Vector3(10.89f, 0f, -0.96f), Quaternion.identity);
    }

    // Initialize level 3
    public void Level3()
    {
        level++;
        levelText.text = "Level: " + level;
        numAsteroids = 6;
        resetSpaceship();
        // Set Asteroids
        Instantiate(largeAsteroid, new Vector3(-14.9f, 0f, 9.74f), Quaternion.identity);
        Instantiate(largeAsteroid, new Vector3(14.67f, 0f, 4.7f), Quaternion.identity);
        Instantiate(largeAsteroid, new Vector3(11.28f, 0f, -9.62f), Quaternion.identity);
        Instantiate(mediumAsteroid, new Vector3(-4.9f, 0f, -9.25f), Quaternion.identity);
        Instantiate(smallAsteroid, new Vector3(-13.98f, 0f, -3.2f), Quaternion.identity);
        Instantiate(smallAsteroid, new Vector3(-7.48f, 0f, 3.6f), Quaternion.identity);
    }

    // Reset spaceship at origin point
    public void resetSpaceship()
    {
        spaceship.transform.position = new Vector3(0, 1, 0);
        spaceship.transform.rotation = Quaternion.identity;
        spaceship.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

}
