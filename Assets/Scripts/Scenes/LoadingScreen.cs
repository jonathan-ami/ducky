using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadingScreen : MonoBehaviour
{
    public AbstractGenerator generator;
    private SpriteRenderer spriteRenderer;
    public float fadeDuration = 2f;
    private Transform loading;
    public GameObject canvasGroup;
    public Transform player;
    private Movement movement;
    void Start()
    {
        canvasGroup.SetActive(false);
        movement = player.GetComponent<Movement>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        loading = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        movement.canMove = false;

        if (!generator.isLoading)
        {
            if (loading.gameObject != null)
            {
                //Destroy(loading.gameObject);
                loading.gameObject.SetActive(false);
            }
            StartCoroutine(FadeToTransparentCoroutine());
        }
    }
    IEnumerator FadeToTransparentCoroutine()
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color; // Get the current color

        while (elapsedTime < fadeDuration)
        {
            // Calculate the current alpha value based on the elapsed time
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // Set the new color with the updated alpha value
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            spriteRenderer.color = newColor;


            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Make sure the sprite is completely transparent at the end
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        canvasGroup.SetActive(true);
        movement.canMove = true;

        Destroy(gameObject);

    }

}
