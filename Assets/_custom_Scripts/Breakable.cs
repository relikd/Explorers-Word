using UnityEngine;
using System.Collections;
public class Breakable : MonoBehaviour {
    [SerializeField]
    GameObject remains;

    void Start () {
	
	}
	void Update () {
	}

    public void shatter() 
    {
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
    }
}
