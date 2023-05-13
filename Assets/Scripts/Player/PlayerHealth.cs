using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;

    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
