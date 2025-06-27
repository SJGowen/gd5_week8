using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public ChallengeManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
