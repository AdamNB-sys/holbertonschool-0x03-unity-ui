using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// PlayerController
/// Handles everything related to the player object
/// Allows for movement, item pickup, mat interactions, 
/// as well as score and health setting
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    // private Vector3 moveInput;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text gameOverText;
    public Image gameOverBG;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Setting variables for player movement
        //moveInput.x = Input.GetAxisRaw("Horizontal");
        //moveInput.z = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Stops player from moving faster on diagonal
        //moveInput.Normalize();
        Vector3 moveInput = new Vector3(x, 0, z);

        //rb.velocity = moveInput * speed;
        rb.AddForce(moveInput * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // If the object can be picked up
        if (other.tag == "Pickup")
        {
            Destroy(other.gameObject);
            score += 1;
            SetScoreText();
            // Debug.Log($"Score: {score}");
        }

        // If the object is a trap
        if (other.tag == "Trap")
        {
            health -= 1;
            SetHealthText();
            // Debug.Log($"Health: {health}");
        }

        // Win condition
        if (other.tag == "Goal")
        {
            // Debug.Log("You win!");
            gameOverText.text = "You Win!";
            gameOverText.color = Color.black;
            gameOverBG.color = Color.green;
            gameOverBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }

    // Update checks health and resets game
    void Update()
    {
        if (health == 0)
        {
            // Debug.Log("Game Over!");
            health = 5;
            score = 0;
            gameOverText.text = "Game Over!";
            gameOverText.color = Color.white;
            gameOverBG.color = Color.red;
            gameOverBG.gameObject.SetActive(true);
            // SceneManager.LoadScene("maze");
            StartCoroutine(LoadScene(3));
        }
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("menu");
        }
    }

    // Adds pause after Game Over/Win message is displayed and game is reset
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("maze");
    }

    // Sets score in UI
    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    // Sets health in UI
    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }
}
