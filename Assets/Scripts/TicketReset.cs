using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketReset : MonoBehaviour
{
    public GameObject TicketStartPos;
    public GameObject Ticket;

    Rigidbody ticketRig;

    float distance;

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Card"))
        //{
        //    collision.gameObject.transform.position = TicketPos.transform.position;
        //}
    }

    private void Update()
    {
        distance = Vector3.Distance(TicketStartPos.transform.position, Ticket.transform.position);
        ticketRig = Ticket.GetComponent<Rigidbody>();
        Debug.Log(distance);
        if (distance >= 15.0f)
        {
            Ticket.GetComponent<Rigidbody>().ResetCenterOfMass();
            ticketRig.velocity = Vector3.zero;
            Ticket.transform.position = TicketStartPos.transform.position;
        }
    }


}
