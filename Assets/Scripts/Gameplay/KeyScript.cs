using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public AudioClip pickupSound;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // When Key overlaps Player
        {
            PlayerKeyTracker pkt = other.gameObject.GetComponent<PlayerKeyTracker>(); // Gets the PlayerKeyTracker component
            if (pkt != null)
            {
                // Adds Key to player key tracker, playing pickup sound if not null, then finally destroying the key gameobject
                pkt.AddKey();
                if (pickupSound != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                Destroy(transform.gameObject);
            }
        }
    }
}
