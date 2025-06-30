using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public ChallengeManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Manager.GameOver();
        Destroy(other.gameObject);
    }
}
