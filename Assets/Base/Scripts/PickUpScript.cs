using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        //Setup
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        //Check if player is in range and "F" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
        else if (equipped) MakeConstraints();
    }

    private void MakeConstraints()
    {
         transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                transform.localScale = Vector3.one;
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        MakeConstraints();

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;
        
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        
    }
}