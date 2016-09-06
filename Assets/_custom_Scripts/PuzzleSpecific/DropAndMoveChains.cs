using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Rigidbody))]

/**
 * Ein Skript fuer Raum 2, mit dem die 2 diagonalen Ketten im Raum 2 bewegt werden, wenn sich der Kronleuchter bewegt. Steuert auserdem die Schwerkraft des Kronleuchters.
 */ 
public class DropAndMoveChains : MonoBehaviour {

    [SerializeField]
    GameObject Chain1;
    [SerializeField]
    GameObject Chain2;
    private Vector3 InitialPos;
    private Vector3 LastPos;
    private Vector3 Direction1;
    private Vector3 Direction2;
    private bool dropping;
    private Rigidbody myRigid;
    void Start ()
    {
        InitialPos = transform.position;
        LastPos = InitialPos;
        Direction1 = Chain1.transform.rotation * Vector3.right;
        Direction2 = Chain2.transform.rotation * Vector3.right;
        myRigid = gameObject.GetComponent<Rigidbody>();
        dropping = myRigid.useGravity;
    }

    /**
     * Aktualisiert die Positionen der 2 diagonalen Ketten. 
     */
	void Update () {
        if(!dropping)
            gameObject.transform.position = Vector3.Lerp(LastPos, InitialPos, Time.deltaTime * 4); 
        float Distance = Vector3.Distance( transform.position,  LastPos);
        if (LastPos.y > transform.position.y)
        {
            Chain1.transform.localPosition = Chain1.transform.localPosition + Direction1 * Distance;
            Chain2.transform.localPosition = Chain2.transform.localPosition + Direction2 * Distance;
        }
        else 
        {
            Chain1.transform.localPosition = Chain1.transform.localPosition - Direction1 * Distance;
            Chain2.transform.localPosition = Chain2.transform.localPosition - Direction2 * Distance;
        }
        LastPos = transform.position;
    }
    /**
     * Laesst den Kronleuchter fallen.
     */
    public void drop() 
    {
        myRigid.useGravity = true;
        dropping = true;
    }
    /**
     * Zieht den Kronleuchter nach oben.
     */
    public void pull() 
    {
        myRigid.useGravity = false; 
        dropping = false; ;
    }
    /**
     * Gibt zurueck, ober de Kronleuchter gerade faellt.
     */
    public bool isDropping() 
    {
        return dropping;
    }
}
