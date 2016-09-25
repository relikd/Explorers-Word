using UnityEngine;
using System.Collections;

/**
 * Script for breaking the chest in room 2
 */
public class breakableChest : Breakable {

	/** The object which will break the chest (chandelier) */
	[Tooltip("Das Objekt, welches die Truhe zerbricht")]
	[SerializeField] GameObject BreakingObject;
	/** Chest remain parts and any content */
	[Tooltip("Ein Array von GameObjects, die beim Zerbrechen als Inhalt aktiviert werden")]
	[SerializeField] GameObject[] contains;
	private bool rightSpot;

	/**
	 * Disable the chest content upon room loading
	 */
	void Start () {
        if (contains.Length > 0)
        {
            foreach (GameObject current in contains)
            {
                if(current) current.SetActive(false);
            }
        }
	}
	/**
	 * Validate if {@link #BreakingObject} caused the collision and if the chandelier is moving down
	 */
	public void OnCollisionEnter(Collision col) {
        if (col.gameObject == BreakingObject && BreakingObject.GetComponent<Rigidbody>().velocity.y < -0.2) { 
			shatterChest();
		}
	}
	/**
	 * Enables content and call {@link Breakable#shatter()}
	 */
	public void shatterChest() {
		XplrDebug.LogWriter.Write("Truhe zerbrochen", gameObject);
        if (contains.Length > 0)
        {
            foreach (GameObject current in contains)
            {
                if (current)
                {
                    current.transform.position = transform.position;
                    current.SetActive(true);
                }
            }
        }
		shatter();
	}
}
