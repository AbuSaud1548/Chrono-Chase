using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public GameObject portalEffect;
    public string nextSceneName;
    public bool requireKey = true;

    GameObject player;
    PlayerKeyTracker keyTracker;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonController");
        keyTracker = player.GetComponent<PlayerKeyTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (portalEffect != null)
            portalEffect.SetActive(keyTracker.HasKey());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (keyTracker.ConsumeKey(1))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
