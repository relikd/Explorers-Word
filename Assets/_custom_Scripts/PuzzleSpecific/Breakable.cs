using UnityEngine;
using System.Collections;
/*
 * Ein Skript, mit dem ein Objekt zerbrochen werden kann, sofern die Einzelteile als Prefab uebergeben wurden. 
 */
public class Breakable : MonoBehaviour {
    [SerializeField]
    GameObject remains;
	[SerializeField]
	AudioClip shatterSound;

    /*
     * Entfernt das Objekt und laed das Prefab fuer die Einzelteile an seine Position.
     */
    public void shatter() 
    {   
        GameObject neu = (GameObject) Instantiate(remains, transform.position, transform.rotation);
        if (gameObject.transform.parent) neu.transform.parent = gameObject.transform.parent;
        AudioSource.PlayClipAtPoint (shatterSound, this.transform.position);
        Destroy(gameObject);
    }
}
