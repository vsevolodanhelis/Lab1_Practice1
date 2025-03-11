namespace Automotive;

public class Car
{
    private double _fuelLevel;
    private double _odometer;
    private double _tripOdometer;

    public string Brand { get; }
    public int TankCapacity { get; }
    public int FuelConsumptionPer100Km { get; }

    public int CurrentFuelLevel => (int)_fuelLevel;
    public int Odometer => (int)_odometer;
    public int TripOdometer => (int)_tripOdometer;

    public Car(string brand, int tankCapacity, int fuelConsumptionPer100Km)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand cannot be null or empty.", nameof(brand));

        if (tankCapacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(tankCapacity), "Tank capacity must be greater than zero.");

        if (fuelConsumptionPer100Km <= 0)
            throw new ArgumentOutOfRangeException(nameof(fuelConsumptionPer100Km), "Fuel consumption must be greater than zero.");

        Brand = brand;
        TankCapacity = tankCapacity;
        FuelConsumptionPer100Km = fuelConsumptionPer100Km;
    }

    public void Refuel(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Refuel amount must be greater than zero.");

        _fuelLevel = Math.Min(_fuelLevel + amount, TankCapacity);
    }

    public void Drive(int kilometers)
    {
        if (kilometers <= 0)
            throw new ArgumentOutOfRangeException(nameof(kilometers), "Distance must be greater than zero.");

        var possibleDistance = _fuelLevel * 100 / FuelConsumptionPer100Km;
        var distanceDriven = Math.Min(kilometers, possibleDistance);

        _odometer += distanceDriven;
        _tripOdometer += distanceDriven;
        _fuelLevel -= distanceDriven * FuelConsumptionPer100Km / 100;

        _tripOdometer %= 1000;
        _odometer %= 1000000;
    }

    public void ResetTripOdometer() => _tripOdometer = 0;
}
