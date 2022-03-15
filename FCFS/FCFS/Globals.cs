using System;
using System.Collections.Generic;

// Class to hold the global variables
public static class Globals
    {
        // Global Variables
        // simulation events
        public const int ARRIVAL = 0;		        	// arrival to queue
        public const int COMPLETE = 1;	            	// completion of service
        public const int EOS = 2;			        	// end of simulation
        // programming constants
        public const bool DEBUG = false;		        // set to true to turn debugging output on
        // Structures
        public static Queue fcfs;
        public static EVlist ev_list;
        // statistics gathering variables
        public static double accum_resp_time;	        // accumulate customer response time
        public static double num_resp_time;             // total number of customers in system
        public static double accum_int_time;            // accumulate customer interarrival time
        // input parameters
        public static int pages;                        // total number of pages (enter 100)
        public static double iarrive_time;	        	// mean exponentially-distributed interarrival time (user-specified)
        public static double service_time;		        // mean service time (enter 1)
        public static long busytime;                    // utilization
        public static long sim_length;		            // length of simulation (user-specified)
        // page request counts
        public static List<int> page_counts = new List<int>();      // stores the current amount of requests for each page
        // system variables
        public static long clock;		            	// simulation clock
        public static bool busy;				    	// flag indicating if server is busy
        public static Random random_num;                // Random number generator
        public static int seed;		      	    	    // seed for random number generator (user-specified)
    }
