class Program
{
    static void Main(string[] args)
    {
        var vehicle1 = new Vehicle("Wi001", false);
        var vehicle2 = new Vehicle("Wi001", true);
        var tracker1 = new VehicleTracker(3, "winnipeg 1004");

        Console.WriteLine(tracker1.SlotsAvailable);


        tracker1.AddVehicle(vehicle1);
        Console.WriteLine(tracker1.SlotsAvailable);
        Console.WriteLine("add1");
        tracker1.AddVehicle(vehicle2);
        Console.WriteLine(tracker1.SlotsAvailable);
        Console.WriteLine("add2");


        //tracker1.AddVehicle(vehicle1);

        Console.WriteLine(tracker1.PassholderPercentage());



        Console.WriteLine("add2");







    }
}

public class Vehicle
{
    public string Licence { get; set; }
    public bool Pass { get; set; }
    public Vehicle(string licence, bool pass)
    {
        this.Licence = licence;
        this.Pass = pass;
    }
}

public class VehicleTracker
{
    //PROPERTIES
    public string Address { get; set; }
    public int Capacity { get; set; }
    public int SlotsAvailable { get; set; }
    public Dictionary<int, Vehicle> VehicleList { get; set; }

    public VehicleTracker(int capacity, string address)
    {
        this.Capacity = capacity;
        this.Address = address;
        this.SlotsAvailable = capacity;
        this.VehicleList = new Dictionary<int, Vehicle>();

        this.GenerateSlots();
    }

    // STATIC PROPERTIES
    public static string BadSearchMessage = "Error: Search did not yield any result.";
    public static string BadSlotNumberMessage = "Error: No slot with number ";
    public static string SlotsFullMessage = "Error: no slots available.";

    // METHODS
    public void GenerateSlots()
    {
        if (this.Capacity >= 1)
        {
            for (int i = 1; i <= this.Capacity; i++)
            {
                this.VehicleList.Add(i, null);
            }
        }
        else
        {
            throw new ArgumentException("Error: Capacity should be more than 0");
        }


    }

    public void AddVehicle(Vehicle vehicle)
    {

        foreach (KeyValuePair<int, Vehicle> slot in this.VehicleList)
        {
            if (slot.Value == null)
            {
                this.VehicleList[slot.Key] = vehicle;
                this.SlotsAvailable--;
                return;
            }
        }
        throw new IndexOutOfRangeException(SlotsFullMessage);
    }

    public void RemoveVehicle(string licence)
    {
        try
        {
            int slot = this.VehicleList.First(v => v.Value.Licence == licence).Key;
            this.SlotsAvailable++;
            this.VehicleList[slot] = null;
        }
        catch
        {
            throw new ArgumentException(BadSearchMessage);
        }
    }

    public bool RemoveVehicle(int slotNumber)
    {
        if (slotNumber > this.Capacity || slotNumber <= 0)
        {
            return false;
        }
        if (this.VehicleList[slotNumber] == null)
        {
            return false;
        }
        this.VehicleList[slotNumber] = null;
        this.SlotsAvailable++;
        return true;
    }

    public List<Vehicle> ParkedPassholders()
    {
        List<Vehicle> passHolders = new List<Vehicle>();

        foreach (Vehicle vehicle in this.VehicleList.Values)
        {
            if (vehicle == null)
                continue;
            if (vehicle.Pass)
            {
                passHolders.Add(vehicle);
            }
        }
        return passHolders;
    }

    public int PassholderPercentage()
    {
        int passHolders = ParkedPassholders().Count();
        int percentage = (passHolders * 100 / (this.Capacity - this.SlotsAvailable));
        return percentage;
    }
}

