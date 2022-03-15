using System;
using System.Collections.Generic;

public static class MainProgram
{
    //**********************************************************************
    // Name: Main
    // Description
    //    This function performs the main control loop of the simulation.
    // It performs the following steps:
    //    1 - call routines to initialize global variables.
    //    2 - schedules an end of simulation.
    //    3 - generates the first arrival.
    //    4 - processes the events on the event list until the end of
    //        simulation event is reached.
    //    5 - frees event node after it has been processed.
    //    6 - prints out the statistics when the simulation is finished.
    //*********************************************************************
    public static void Main()
    {
        bool not_done;
        Cust new_index;
        long arrive_time;
        EVnode eventid;
        // initialization
        Utility.Read_parms();
        Utility.Initialize();
        // schedule an end of simulation
        Cust null_cust = null;
        Globals.ev_list.insert_event(Globals.EOS, Globals.sim_length, null_cust);
        // generate first arrival
        // generate exponential interarrival time of new customer
        arrive_time = Utility.Expon(Globals.iarrive_time);
        // get new customer
        new_index = new Cust();
        // set interarrival time for new customer
        new_index.setiarrive(arrive_time);
        // add the next arrival to the event list for future 
        Utility.Gen_arrival(new_index, arrive_time);
        // main loop to process the event list
        not_done = true;
        while(not_done)
	    {
	        // get next event
	        eventid = Globals.ev_list.remove_event();
	        // update clock
	        Globals.clock = eventid.get_evtime();
	        // process event type
	        switch (eventid.get_evtype())
		        {
		        case Globals.ARRIVAL    : Arrive(eventid);
								              break;
		        case Globals.COMPLETE   : Depart(eventid);
								              break;
                case Globals.EOS        : Utility.Process_statistics();
								            not_done = false;
								            break;
		        default                 : Console.WriteLine("***Error - invalid event type\n");
                                            break;
		        }
	      }
    }


    //*********************************************************************
    // Name: arrive                                                        
    // Description                                                         
    //    This function processes an arrival to the system.  It performs   
    // the followiong steps:                                               
    //    1 - generates the next arrival.                                  
    //    2 - sets the system statistics for current job                                  
    //    3 - puts the customer into the queue.                   
    //    4 - if the server is not busy then calls start_service.          
    //*********************************************************************
    public static void Arrive(EVnode ev_num)
    {
        Cust cur_index, new_index;
        long arrive_time;
        // generate exponential interarrival time of new customer
        arrive_time = Utility.Expon(Globals.iarrive_time);
        // get new customer
        new_index = new Cust();
        // set interarrival time for new customer
        new_index.setiarrive(arrive_time);
        // add the next arrival to the event list for future 
        Utility.Gen_arrival(new_index, arrive_time);
        // set statistics gathering variable
        cur_index = ev_num.get_cust();
        // set arrive time
        cur_index.setarrive(Globals.clock);
        // put the customer in the queue
        Globals.fcfs.add_to_queue(cur_index);
        // increase count of requests for page requested by customer
        int page_req = cur_index.getpage();
        Globals.page_counts[page_req]++;
        // if server is not busy then start service
        if (!Globals.busy)
	        StartService();
        return;
    }



    //*********************************************************************
    // Name: start_service
    // Description
    //    This function performs the following steps:
    //    1 - removes the first customer from the queue.
    //    2 - sets the server to busy.
    //    3 - schedules a departure event.
    //*********************************************************************
    public static void StartService()
    {
        long servicetime;
        List<Cust> index;
        // remove customers from the queue
        index = Globals.fcfs.take_off_queue();
        // set server to busy
        Globals.busy = true;
        // generate a CPU bust time for the next arrival and associate it with customer
        servicetime = Utility.Expon(Globals.service_time);
        // schedule a departure event based on CPU burst time
        Utility.Gen_departure(index, servicetime);
        //accumulate busy time to compute utilization
        Globals.busytime += servicetime;
        return;
    }



    //********************************************************************* 
    // Name: depart
    // Description
    //    This function processes a departure from the server event.  It
    // performs the following steps:
    //    1 - sets the server to idle.
    //    2 - accumulate response time statistics.
    //    3 - remove the customer from the system.
    //    4 - if the queue is not empty, then start service.
    //*********************************************************************
    public static void Depart(EVnode ev_num)
    {
        List<Cust> index;
        long temp, time, inttime;
        // set server to idle
        Globals.busy = false;
        // accumulate response time
        index = ev_num.get_custs();
        time = Globals.clock;
        foreach (Cust cust in index)
        {
            temp = time - cust.getarrive();
            inttime = cust.getiarrive();
            if (Globals.DEBUG)
                Console.WriteLine(" Response time for customer {0} is {1}", cust.getnum(), temp);
            Globals.accum_resp_time += temp;
            Globals.accum_int_time += inttime;
            Globals.num_resp_time++;
        }
        int page_req = index[0].getpage();
        Globals.page_counts[page_req] = 0; // reset page requests
        // if queue is non-empty, start service
        if(!Globals.fcfs.isempty())
	        StartService();
        return;
    }
}



    