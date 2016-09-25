using UnityEngine;
using System.Collections;

/**
 * Allows an object to be broken apart on collision. All broken parts have to be attached as a prefab
 */
public class Breakable : MonoBehaviour {
    [SerializeField] GameObject remains;
	[SerializeField] AudioClip shatterSound;

    /**
     * Remove current GameObject and load attached prefab
     */
    public void shatter()
    {
        GameObject neu = (GameObject) Instantiate(remains, transform.position, transform.rotation);
        if (gameObject.transform.parent) neu.transform.parent = gameObject.transform.parent;
        AudioSource.PlayClipAtPoint (shatterSound, this.transform.position);
        Destroy(gameObject);
    }
}
