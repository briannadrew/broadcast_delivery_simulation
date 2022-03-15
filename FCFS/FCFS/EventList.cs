using System;
using System.Collections.Generic;

// Class to hold the elements to be stored in the Event List for the Discrete Event Simulation
public class EVnode
{
    private int ev_type;	    	/* event type */
	private long ev_time;	        /* time for event to occur */
	private Cust cust_index;	    /* which customer is responsible for this event */
    private List<Cust> cust_list;   /* list of customers */
	private EVnode forward;	        /* forward link */
	private EVnode backward;	    /* backward link */                            

    // Constructors
    public EVnode()
	{
	    ev_time = 0;
	    ev_type = -1;
	    cust_index = null;
        cust_list = null;
	    forward = null;
	    backward = null;
	}

    public EVnode(int etype, long etime, Cust ptr)
	{
	    ev_time = etime;
	    ev_type = etype;
	    cust_index = ptr;
        cust_list = null;
	    forward = null;
	    backward = null;
	}
    
    public EVnode(int etype, long etime, List<Cust> custs)
    {
        ev_time = etime;
        ev_type = etype;
        cust_index = null;
        cust_list = custs;
        forward = null;
        backward = null;
    }

    // Properties
    public EVnode Forward
    {
        get
        {
            return forward;
        }
        set
        {
            forward = value;
        }
    }

    public EVnode Backward
    {
        get
        {
            return backward;
        }
        set
        {
            backward = value;
        }
    }
    // return the Event Type from the Event node
    public int get_evtype() 
    {
        return ev_type;
    }

    public long get_evtime() 
    { 
        return ev_time;
    }

    // return the Customer Index from the Event node
    public Cust get_cust() 
    { 
        return cust_index;
    }
    
    // return the customer list from the event node
    public List<Cust> get_custs()
    {
        return cust_list;
    }
}

// Class to manipulate the Event List
public class EVlist
{
    private EVnode top_event;
    private EVnode last_event;

    // Constructor
    public EVlist()
    {
        top_event = null;
        last_event = null;
    }


    // add an element of type Cust to queue
    public void insert_event(int etype, long etime, Cust ptr)
    {
        bool not_found;
        EVnode loc, pos;
        // create a new event node with the appropriate information
        loc = new EVnode(etype, etime, ptr);
        // determine if the list is empty
        if (top_event == null)
        {
            top_event = loc;
            last_event = loc;
            return;
        }
        // see if it belongs on top
        if (top_event.get_evtime() > etime)
        {
            top_event.Backward = loc;
            loc.Forward = top_event;
            top_event = loc;
            return;
        }
        // see if it belongs at the bottom
        if (last_event.get_evtime() <= etime)
        {
            last_event.Forward = loc;
            loc.Backward = last_event;
            last_event = loc;
            return;
        }
        // it belongs somewhere in the middle so find its place
        not_found = true;
        pos = top_event;
        while (pos != null && not_found)
        {
            if (pos.get_evtime() > etime)
                not_found = false;
            else
                pos = pos.Forward;
        }
        // check to see if we found something as we should have
        if (not_found)
        {
            Console.WriteLine("***Error - problems in insert event routine***\n");
            return;
        }
        // add node to appropriate place
        loc.Forward = pos;
        loc.Backward = pos.Backward;
        (pos.Backward).Forward = loc;
        pos.Backward = loc;
        return;
    }

    public void insert_event(int etype, long etime, List<Cust> customers)
    {
        bool not_found;
        EVnode loc, pos;
        // create a new event node with the appropriate information
        loc = new EVnode(etype, etime, customers);
        // determine if the list is empty
        if (top_event == null)
        {
            top_event = loc;
            last_event = loc;
            return;
        }
        // see if it belongs on top
        if (top_event.get_evtime() > etime)
        {
            top_event.Backward = loc;
            loc.Forward = top_event;
            top_event = loc;
            return;
        }
        // see if it belongs at the bottom
        if (last_event.get_evtime() <= etime)
        {
            last_event.Forward = loc;
            loc.Backward = last_event;
            last_event = loc;
            return;
        }
        // it belongs somewhere in the middle so find its place
        not_found = true;
        pos = top_event;
        while (pos != null && not_found)
        {
            if (pos.get_evtime() > etime)
                not_found = false;
            else
                pos = pos.Forward;
        }
        // check to see if we found something as we should have
        if (not_found)
        {
            Console.WriteLine("***Error - problems in insert event routine***\n");
            return;
        }
        // add node to appropriate place
        loc.Forward = pos;
        loc.Backward = pos.Backward;
        (pos.Backward).Forward = loc;
        pos.Backward = loc;
        return;
    }

    //return the top event from the event list
    public EVnode remove_event()
    {
        EVnode ev_ptr;
        // check to see if event list is empty
        if (top_event == null)
        {
            Console.WriteLine("***Error - Event list underflow***\n");
            return (null);
        }
        // remove top element
        ev_ptr = top_event;
        // see if it was the only event - special case to mark empty
        if (top_event == last_event)
        {
            top_event = null;
            last_event = null;
            return (ev_ptr);
        }
        // event list has more than one element so just relink
        top_event = top_event.Forward;
        top_event.Backward = null;
        ev_ptr.Forward = null;
        return (ev_ptr);
    }

    // print out the elements in the queue
    public void print()
    {
        EVnode ptr;
        ptr = top_event;
        Console.WriteLine("\nEvent List contents ... ");
        while (ptr != null)
        {
            Console.Write("\nEvent Type: {0}", ptr.get_evtype());
            Console.WriteLine(" ... Event Time: {0}", ptr.get_evtime());
            ptr = ptr.Forward;
        }
    }
}