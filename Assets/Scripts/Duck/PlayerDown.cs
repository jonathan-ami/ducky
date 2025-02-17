using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDown : MonoBehaviour
{
    // Cooldown time in seconds
    public float hitCooldown = 1.0f;
    private Animator animator;

    // Time of the last hit
    private float lastHitTime = 0.0f;
    public Score score;
    private DuckCounter duckCounter;
    void Start()
    {

        duckCounter = GameObject.Find("DuckCounter").GetComponent<DuckCounter>();
        animator = GetComponent<Animator>();
    }

    // OnTriggerEnter2D is called when a 2D collider enters a trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if enough time has passed since the last hit
        if (Time.time - lastHitTime >= hitCooldown)
        {
            // Check if the collider belongs to an object with the tag "Bullet"
            if (other.CompareTag("Bullet"))
            {
                // Bullet hit the player
                PlayerHit();

                // Update the last hit time
                lastHitTime = Time.time;
            }
        }
    }

    void PlayerHit()
    {
        // Get all ducks in the scene
        FollowParent[] allDucks = FindObjectsOfType<FollowParent>();

        int followingCount = 0;

        // Iterate through all ducks and count the ones following the player
        foreach (FollowParent duck in allDucks)
        {
            if (duck.IsFollowingPlayer())
            {
                followingCount++;
                duck.StopFollowing(); // Assuming you also want to stop them from following
            }
        }

        // Check if the player has no ducks following
        if (duckCounter.GetNumDucks() == 0)
        {
            // Destroy the player
            KillPlayer killPlayer = GetComponent<KillPlayer>();
            killPlayer.Die();
        }

        // Call the UpdateLastDuck function in the PickupDuck script
        PickupDuck pickupDuckScript = FindObjectOfType<PickupDuck>();
        if (pickupDuckScript != null)
        {
            pickupDuckScript.ChangeLast();
        }
    }

    public void LoadGameOverScene()
    {


        DuckCounter duckCounter = GameObject.Find("DuckCounter").GetComponent<DuckCounter>();
        score.intValue = duckCounter.GetScore();

        SceneManager.LoadScene("Intro");
    }

}
