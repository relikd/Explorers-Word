using UnityEngine;
using System.Collections;

/*
 * Das Skript zum Zerbrechen der Truhe in Raum 2. Es benoetigt die Ueberreste des OBjekts (Remains), Das Objekt, weclhes die Truhe zerbricht (BreakingObject),
 * sowie ein Array von GameObjects, die beim Zerbrechen als Inhalt activiert werden (contains).
 */ 
public class breakableChest : Breakable {

	[SerializeField]
	GameObject BreakingObject;
	[SerializeField]
	GameObject[] contains;
	private bool rightSpot;

	/*
	 * Setzt den Inhalt der Truhe auf inaktiv.
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
	/*
	 * Prüft bei Kollision, ob die Kollision mit dem BreakingObject war unmd ob dieses sich schnell genung nach unten bewegt hat.
	 */
	public void OnCollisionEnter(Collision col) {
        if (col.gameObject == BreakingObject && BreakingObject.GetComponent<Rigidbody>().velocity.y < -0.2) { 
			shatterChest();
		}
	}
	/*
	 * Schaltet den Inhalt auf Aktiv und ruft shatter() auf.
	 */
	public void shatterChest() {
		LogWriter.WriteLog("Truhe zerbrochen", gameObject);
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
