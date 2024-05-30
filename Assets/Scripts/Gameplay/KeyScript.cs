using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public AudioClip pickupSound;
    public GameObject keyModel;

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
                Destroy(gameObject);
            }
        }
    }

    public void FixedUpdate()
    {
        if (keyModel != null)
        {
            keyModel.transform.localPosition = new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad * 2) / 64, 0);
            keyModel.transform.localEulerAngles += new Vector3(0, -1, 0);
        }
    }
}
