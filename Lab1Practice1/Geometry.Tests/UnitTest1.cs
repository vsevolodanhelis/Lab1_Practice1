using FluentAssertions;
using Geometry;
using Xunit;

namespace Geometry.Tests;

public class PointTests
{
    [Fact]
    public void Constructor_Default_ShouldCreatePointAtOrigin()
    {
        // Arrange & Act
        var point = new Point();

        // Assert
        point.X.Should().Be(0);
        point.Y.Should().Be(0);
    }

    [Fact]
    public void Constructor_SingleValue_ShouldCreatePointWithSameXAndY()
    {
        // Arrange
        const double value = 5.5;

        // Act
        var point = new Point(value);

        // Assert
        point.X.Should().Be(value);
        point.Y.Should().Be(value);
    }

    [Fact]
    public void Constructor_TwoValues_ShouldCreatePointWithSpecifiedCoordinates()
    {
        // Arrange
        const double x = 3.0;
        const double y = 4.0;

        // Act
        var point = new Point(x, y);

        // Assert
        point.X.Should().Be(x);
        point.Y.Should().Be(y);
    }

    [Fact]
    public void Move_WithPositiveValues_ShouldIncreaseCoordinates()
    {
        // Arrange
        var point = new Point(1.0, 2.0);
        const double deltaX = 3.0;
        const double deltaY = 4.0;

        // Act
        point.Move(deltaX, deltaY);

        // Assert
        point.X.Should().Be(4.0);
        point.Y.Should().Be(6.0);
    }

    [Fact]
    public void Move_WithNegativeValues_ShouldDecreaseCoordinates()
    {
        // Arrange
        var point = new Point(5.0, 6.0);
        const double deltaX = -2.0;
        const double deltaY = -3.0;

        // Act
        point.Move(deltaX, deltaY);

        // Assert
        point.X.Should().Be(3.0);
        point.Y.Should().Be(3.0);
    }

    [Fact]
    public void Move_WithZeroValues_ShouldNotChangeCoordinates()
    {
        // Arrange
        var point = new Point(2.5, 3.5);
        const double deltaX = 0.0;
        const double deltaY = 0.0;

        // Act
        point.Move(deltaX, deltaY);

        // Assert
        point.X.Should().Be(2.5);
        point.Y.Should().Be(3.5);
    }

    [Fact]
    public void Distance_FromOrigin_ShouldReturnZero()
    {
        // Arrange
        var point = new Point();

        // Act
        var distance = point.Distance();

        // Assert
        distance.Should().Be(0);
    }

    [Fact]
    public void Distance_FromPoint3And4_ShouldReturn5()
    {
        // Arrange
        var point = new Point(3.0, 4.0);

        // Act
        var distance = point.Distance();

        // Assert
        distance.Should().Be(5.0);
    }

    [Fact]
    public void Distance_FromNegativeCoordinates_ShouldReturnPositiveDistance()
    {
        // Arrange
        var point = new Point(-3.0, -4.0);

        // Act
        var distance = point.Distance();

        // Assert
        distance.Should().Be(5.0);
    }

    [Fact]
    public void Distance_FromMixedCoordinates_ShouldCalculateCorrectly()
    {
        // Arrange
        var point = new Point(-3.0, 4.0);

        // Act
        var distance = point.Distance();

        // Assert
        distance.Should().Be(5.0);
    }
}
