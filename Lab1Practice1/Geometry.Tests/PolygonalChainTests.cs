using FluentAssertions;
using Geometry;
using Xunit;

namespace Geometry.Tests;

public class PolygonalChainTests
{
    [Fact]
    public void Constructor_WithStartAndEnd_ShouldCreatePolygonalChainWithCorrectPoints()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(3.0, 4.0);

        // Act
        var chain = new PolygonalChain(start, end);

        // Assert
        chain.Start.Should().Be(start);
        chain.End.Should().Be(end);
    }

    [Fact]
    public void Length_WithoutMidpoints_ShouldReturnDirectDistance()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(3.0, 4.0);
        var chain = new PolygonalChain(start, end);

        // Act
        var length = chain.Length;

        // Assert
        length.Should().Be(5.0);
    }

    [Fact]
    public void AddMidpoint_ShouldIncreaseLength()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(6.0, 0.0);
        var chain = new PolygonalChain(start, end);
        var midpoint = new Point(3.0, 4.0);

        // Act
        chain.AddMidpoint(midpoint);
        var length = chain.Length;

        // Assert
        length.Should().Be(10.0); // 5 + 5 (two segments of length 5 each)
    }

    [Fact]
    public void AddMidpoint_MultipleMidpoints_ShouldCalculateCorrectLength()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(9.0, 0.0);
        var chain = new PolygonalChain(start, end);
        var midpoint1 = new Point(3.0, 0.0);
        var midpoint2 = new Point(6.0, 0.0);

        // Act
        chain.AddMidpoint(midpoint1);
        chain.AddMidpoint(midpoint2);
        var length = chain.Length;

        // Assert
        length.Should().Be(9.0); // 3 + 3 + 3
    }

    [Fact]
    public void Move_ShouldMoveAllPoints()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(3.0, 4.0);
        var chain = new PolygonalChain(start, end);
        var midpoint = new Point(1.0, 2.0);
        chain.AddMidpoint(midpoint);

        // Act
        chain.Move(5.0, 10.0);

        // Assert
        chain.Start.X.Should().Be(5.0);
        chain.Start.Y.Should().Be(10.0);
        chain.End.X.Should().Be(8.0);
        chain.End.Y.Should().Be(14.0);
        midpoint.X.Should().Be(6.0);
        midpoint.Y.Should().Be(12.0);
    }

    [Fact]
    public void Move_WithNegativeValues_ShouldMoveAllPointsCorrectly()
    {
        // Arrange
        var start = new Point(5.0, 10.0);
        var end = new Point(8.0, 14.0);
        var chain = new PolygonalChain(start, end);
        var midpoint = new Point(6.0, 12.0);
        chain.AddMidpoint(midpoint);

        // Act
        chain.Move(-2.0, -3.0);

        // Assert
        chain.Start.X.Should().Be(3.0);
        chain.Start.Y.Should().Be(7.0);
        chain.End.X.Should().Be(6.0);
        chain.End.Y.Should().Be(11.0);
        midpoint.X.Should().Be(4.0);
        midpoint.Y.Should().Be(9.0);
    }

    [Fact]
    public void ToString_WithoutMidpoints_ShouldReturnCorrectFormat()
    {
        // Arrange
        var start = new Point(1.0, 2.0);
        var end = new Point(3.0, 4.0);
        var chain = new PolygonalChain(start, end);

        // Act
        var result = chain.ToString();

        // Assert
        result.Should().Be("(1,2),(3,4)");
    }

    [Fact]
    public void ToString_WithOneMidpoint_ShouldReturnCorrectFormat()
    {
        // Arrange
        var start = new Point(1.0, 2.0);
        var end = new Point(5.0, 6.0);
        var chain = new PolygonalChain(start, end);
        var midpoint = new Point(3.0, 4.0);
        chain.AddMidpoint(midpoint);

        // Act
        var result = chain.ToString();

        // Assert
        result.Should().Be("(1,2),(3,4),(5,6)");
    }

    [Fact]
    public void ToString_WithMultipleMidpoints_ShouldReturnCorrectFormat()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(6.0, 0.0);
        var chain = new PolygonalChain(start, end);
        var midpoint1 = new Point(2.0, 0.0);
        var midpoint2 = new Point(4.0, 0.0);
        chain.AddMidpoint(midpoint1);
        chain.AddMidpoint(midpoint2);

        // Act
        var result = chain.ToString();

        // Assert
        result.Should().Be("(0,0),(2,0),(4,0),(6,0)");
    }

    [Fact]
    public void Length_WithComplexPath_ShouldCalculateCorrectly()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(0.0, 0.0);
        var chain = new PolygonalChain(start, end);
        var midpoint1 = new Point(3.0, 0.0);
        var midpoint2 = new Point(3.0, 4.0);
        chain.AddMidpoint(midpoint1);
        chain.AddMidpoint(midpoint2);

        // Act
        var length = chain.Length;

        // Assert
        length.Should().Be(12.0); // 3 + 4 + 5 (right triangle)
    }
}
