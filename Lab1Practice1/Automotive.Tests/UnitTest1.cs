using FluentAssertions;
using Automotive;
using Xunit;

namespace Automotive.Tests;

public class CarTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateCarWithCorrectProperties()
    {
        // Arrange
        const string brand = "Toyota";
        const int tankCapacity = 50;
        const int fuelConsumption = 8;

        // Act
        var car = new Car(brand, tankCapacity, fuelConsumption);

        // Assert
        car.Brand.Should().Be(brand);
        car.TankCapacity.Should().Be(tankCapacity);
        car.FuelConsumptionPer100Km.Should().Be(fuelConsumption);
        car.CurrentFuelLevel.Should().Be(0);
        car.Odometer.Should().Be(0);
        car.TripOdometer.Should().Be(0);
    }

    [Fact]
    public void Constructor_WithNullBrand_ShouldThrowArgumentException()
    {
        // Arrange, Act & Assert
        var action = () => new Car(null, 50, 8);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Brand cannot be null or empty. (Parameter 'brand')");
    }

    [Fact]
    public void Constructor_WithEmptyBrand_ShouldThrowArgumentException()
    {
        // Arrange, Act & Assert
        var action = () => new Car("", 50, 8);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Brand cannot be null or empty. (Parameter 'brand')");
    }

    [Fact]
    public void Constructor_WithWhitespaceBrand_ShouldThrowArgumentException()
    {
        // Arrange, Act & Assert
        var action = () => new Car("   ", 50, 8);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Brand cannot be null or empty. (Parameter 'brand')");
    }

    [Fact]
    public void Constructor_WithZeroTankCapacity_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange, Act & Assert
        var action = () => new Car("Toyota", 0, 8);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Tank capacity must be greater than zero. (Parameter 'tankCapacity')");
    }

    [Fact]
    public void Constructor_WithNegativeTankCapacity_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange, Act & Assert
        var action = () => new Car("Toyota", -10, 8);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Tank capacity must be greater than zero. (Parameter 'tankCapacity')");
    }

    [Fact]
    public void Constructor_WithZeroFuelConsumption_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange, Act & Assert
        var action = () => new Car("Toyota", 50, 0);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Fuel consumption must be greater than zero. (Parameter 'fuelConsumptionPer100Km')");
    }

    [Fact]
    public void Constructor_WithNegativeFuelConsumption_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange, Act & Assert
        var action = () => new Car("Toyota", 50, -5);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Fuel consumption must be greater than zero. (Parameter 'fuelConsumptionPer100Km')");
    }

    [Fact]
    public void Refuel_WithValidAmount_ShouldIncreaseFuelLevel()
    {
        // Arrange
        var car = new Car("Toyota", 50, 8);

        // Act
        car.Refuel(20);

        // Assert
        car.CurrentFuelLevel.Should().Be(20);
    }

    [Fact]
    public void Refuel_ExceedingTankCapacity_ShouldCapAtTankCapacity()
    {
        // Arrange
        var car = new Car("Toyota", 50, 8);

        // Act
        car.Refuel(60);

        // Assert
        car.CurrentFuelLevel.Should().Be(50);
    }

    [Fact]
    public void Refuel_MultipleRefuels_ShouldAccumulateCorrectly()
    {
        // Arrange
        var car = new Car("Toyota", 50, 8);

        // Act
        car.Refuel(20);
        car.Refuel(15);

        // Assert
        car.CurrentFuelLevel.Should().Be(35);
    }

    [Fact]
    public void Refuel_WithZeroAmount_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var car = new Car("Toyota", 50, 8);

        // Act & Assert
        var action = () => car.Refuel(0);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Refuel amount must be greater than zero. (Parameter 'amount')");
    }

    [Fact]
    public void Refuel_WithNegativeAmount_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var car = new Car("Toyota", 50, 8);

        // Act & Assert
        var action = () => car.Refuel(-10);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Refuel amount must be greater than zero. (Parameter 'amount')");
    }

    [Fact]
    public void Drive_WithSufficientFuel_ShouldUpdateOdometerAndConsumeFuel()
    {
        // Arrange
        var car = new Car("Toyota", 50, 10); // 10L per 100km
        car.Refuel(50);

        // Act
        car.Drive(100); // Should consume 10L

        // Assert
        car.Odometer.Should().Be(100);
        car.TripOdometer.Should().Be(100);
        car.CurrentFuelLevel.Should().Be(40);
    }

    [Fact]
    public void Drive_WithInsufficientFuel_ShouldDriveMaximumPossibleDistance()
    {
        // Arrange
        var car = new Car("Toyota", 50, 10); // 10L per 100km
        car.Refuel(20); // Can drive 200km

        // Act
        car.Drive(300); // Try to drive 300km

        // Assert
        car.Odometer.Should().Be(200);
        car.TripOdometer.Should().Be(200);
        car.CurrentFuelLevel.Should().Be(0);
    }

    [Fact]
    public void Drive_WithZeroDistance_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var car = new Car("Toyota", 50, 8);

        // Act & Assert
        var action = () => car.Drive(0);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Distance must be greater than zero. (Parameter 'kilometers')");
    }

    [Fact]
    public void Drive_WithNegativeDistance_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var car = new Car("Toyota", 50, 8);

        // Act & Assert
        var action = () => car.Drive(-50);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Distance must be greater than zero. (Parameter 'kilometers')");
    }

    [Fact]
    public void Drive_MultipleDrives_ShouldAccumulateDistanceAndConsumeFuel()
    {
        // Arrange
        var car = new Car("Toyota", 50, 10);
        car.Refuel(50);

        // Act
        car.Drive(50);
        car.Drive(100);

        // Assert
        car.Odometer.Should().Be(150);
        car.TripOdometer.Should().Be(150);
        car.CurrentFuelLevel.Should().Be(35); // 50 - 15 (consumed)
    }

    [Fact]
    public void TripOdometer_ShouldResetAt1000()
    {
        // Arrange
        var car = new Car("Toyota", 100, 5); // Large tank, low consumption
        car.Refuel(100);

        // Act
        car.Drive(1050); // Drive more than 1000km

        // Assert
        car.Odometer.Should().Be(1050);
        car.TripOdometer.Should().Be(50); // Should reset at 1000
    }

    [Fact]
    public void Odometer_ShouldResetAt1000000()
    {
        // Arrange
        var car = new Car("Toyota", 100, 1); // Very low consumption for testing

        // Refuel multiple times and drive to exceed 1,000,000 km
        for (int i = 0; i < 10001; i++) // Drive 10,001 times 100km each = 1,000,100km
        {
            car.Refuel(100);
            car.Drive(100);
        }

        // Assert
        car.Odometer.Should().Be(100); // Should reset at 1,000,000, so 1,000,100 % 1,000,000 = 100
        car.TripOdometer.Should().Be(100); // 100 % 1000 = 100
    }

    [Fact]
    public void ResetTripOdometer_ShouldSetTripOdometerToZero()
    {
        // Arrange
        var car = new Car("Toyota", 50, 10);
        car.Refuel(50);
        car.Drive(150);

        // Act
        car.ResetTripOdometer();

        // Assert
        car.TripOdometer.Should().Be(0);
        car.Odometer.Should().Be(150); // Main odometer should not be affected
    }

    [Fact]
    public void Drive_WithNoFuel_ShouldNotMove()
    {
        // Arrange
        var car = new Car("Toyota", 50, 10);
        // No refueling

        // Act
        car.Drive(100);

        // Assert
        car.Odometer.Should().Be(0);
        car.TripOdometer.Should().Be(0);
        car.CurrentFuelLevel.Should().Be(0);
    }
}
