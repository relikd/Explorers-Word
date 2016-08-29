using UnityEngine;
using System.Collections;
/*
 * Ein Skript, mit dem ein Objekt zerbrochen werden kann, sofern die Einzelteile als Prefab uebergeben wurden. 
 */
public class Breakable : MonoBehaviour {
    [SerializeField]
    GameObject remains;
    [SerializeField]
    float disappearAfterInSeconds;
	[SerializeField]
	AudioClip shatterSound;

    /*
     * Entfernt das Objekt und laed das Prefab fuer die Einzelteile an seine Position.
     */
    public void shatter() 
    {
        Instantiate(remains, transform.position, transform.rotation);
		AudioSource.PlayClipAtPoint (shatterSound, this.transform.position);
        Destroy(gameObject);
    }
}
