﻿using UnityEngine;
using System.Collections;
using Interaction;
using System;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
/**
 * Ein Skript, welches es ermoeglicht, Objekte zu tragen. Es verhindert uebermaessiges clipping, gestattet jedoch dem getragenen Objekt ohne Kollision an Ecken vorbeibewegt zu werden.
 */
public class HoldInteraction : Interactable
{
    private float distance = 2.5f;
    private float smooth = 100;

    private DummyColliderObject DummyObj;
    private BoxCollider DummyCollider;
    Rigidbody dummyRigid;

    private BoxCollider myCollider;
    private Rigidbody myRigid;
    

    /**
     * Initialisiert die Variablen des Objekts und des DummyObjekts.
     */
    void Awake()
    {
        myCollider = gameObject.GetComponent<BoxCollider>();
        myRigid = gameObject.GetComponent<Rigidbody>();
        GameObject Dummy = new GameObject();
        DummyObj = Dummy.AddComponent<DummyColliderObject>();
        DummyObj.distance = distance;
        DummyObj.smooth = smooth;
        DummyObj.safe = gameObject.transform.position;
        DummyObj.saferotate = gameObject.transform.rotation;
        dummyRigid = Dummy.AddComponent<Rigidbody>();
        dummyRigid.useGravity = false;
        dummyRigid.collisionDetectionMode = CollisionDetectionMode.Continuous;
        dummyRigid.isKinematic = true;
        DummyCollider = Dummy.AddComponent<BoxCollider>();
        DummyCollider.center = myCollider.center;
        DummyCollider.size = myCollider.size;
    }
    /**
    * Aktualisiert die Position des getragenen Objekts auf eine mit Lerp interpolierte Position zwischen der aktuellen Lage und einem sicheren Zielpunkt.Erlaubt auch das Fallenlassen des Objekts.
    */
    void FixedUpdate()
    {
        if (DummyObj.carrying)
        {
            if (Input.GetKeyUp(theKeyCode())) drop();

            Vector3 NewLoc = Vector3.Lerp(gameObject.transform.position, DummyObj.safe, Time.deltaTime * 12);
            gameObject.transform.position = NewLoc;
            gameObject.transform.rotation = DummyObj.saferotate;//Quaternion.identity;
          
        }
    }
    public override string interactMessage()
    {
        return "carry";
    }

    public override void OnInteractionKeyPressed()
    {
    //Muss implementiert sein, es soll jedoch eine andere Klick-Logk verwendet werden.
    }
    /**
     * Hebt beim druecken des Knopfes das Objekt auf.
     */
    override public void OnInteractionKeyDown()
    {
        if (!DummyObj.carrying)
        {
            pickup();
        }
    }
    /**
     * Schaltet die Werte zurecht um das Tragen des Objekts zu ermoeglichen. Aktiviert die Interaktion mit dem Dummy-Objekt. Deaktiviert die Interaktionmit dem echten Objekt.
     */
     private void pickup()
    {
        LogWriter.WriteLog("aufgehoben", gameObject);
        myRigid.isKinematic = true;
        myRigid.useGravity = false;
        DummyCollider.enabled = true;
        myCollider.enabled = false;
        dummyRigid.isKinematic = false;
       // DummyObj.safe = gameObject.transform.position;
        DummyObj.carrying = true;
    }
    /**
     * Schaltet die Werte zurecht um das normale Verhalten des Objekts wiederherzustellen. Deaktiviert die Interaktionmit dem Dummy-Objekt. Aktiviert die Interaktion mit dem echten Objekt.
     */
    private void drop()
    {
        LogWriter.WriteLog("fallen gelassen", gameObject);
        dummyRigid.isKinematic = true;
        myRigid.useGravity = true;
        myRigid.isKinematic = false;
        DummyCollider.enabled = false;
        myCollider.enabled = true;
        DummyObj.carrying = false;       
    } 
}