// Class to hold the customer data
public class Cust
{
    private long arrive_time, int_time;
    private int cust_num, page_req;
    private static int num;

    // Constructors
    public Cust()
    {
        arrive_time = 0;
        int_time = 0;
        page_req = Utility.Page(Globals.pages);
        cust_num = num;
        num += 1;
    }

    // Return the customer number
    public int getnum()
    {
        return cust_num;
    }

    // Return the interarrival time
    public long getiarrive()
    {
        return int_time;
    }

    // Return the arrival time
    public long getarrive()
    {
        return arrive_time;
    }

    // Return the page requested
    public int getpage()
    {
        return page_req;
    }

    // Set the arrival time
    public void setarrive(long time)
    {
        arrive_time = time;
    }

    // Set the interarrival time
    public void setiarrive(long time)
    {
        int_time = time;
    }

}
