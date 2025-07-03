using UnityEngine;

public class RobotCleaner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dust"))
        {
            Destroy(other.gameObject); // Hút bụi
            DustManager.Instance.DustCollected(); // Báo đã hút 1 bụi
        }
    }
}
