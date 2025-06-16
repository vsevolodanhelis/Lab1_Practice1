using FluentAssertions;
using Geometry;
using Xunit;

namespace Geometry.Tests;

public class SegmentTests
{
    [Fact]
    public void Constructor_WithTwoPoints_ShouldCreateSegmentWithCorrectStartAndEnd()
    {
        // Arrange
        var start = new Point(1.0, 2.0);
        var end = new Point(4.0, 6.0);

        // Act
        var segment = new Segment(start, end);

        // Assert
        segment.Start.Should().Be(start);
        segment.End.Should().Be(end);
    }

    [Fact]
    public void Length_WithHorizontalSegment_ShouldCalculateCorrectLength()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(3.0, 0.0);
        var segment = new Segment(start, end);

        // Act
        var length = segment.Length;

        // Assert
        length.Should().Be(3.0);
    }

    [Fact]
    public void Length_WithVerticalSegment_ShouldCalculateCorrectLength()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(0.0, 4.0);
        var segment = new Segment(start, end);

        // Act
        var length = segment.Length;

        // Assert
        length.Should().Be(4.0);
    }

    [Fact]
    public void Length_WithDiagonalSegment_ShouldCalculateCorrectLength()
    {
        // Arrange
        var start = new Point(0.0, 0.0);
        var end = new Point(3.0, 4.0);
        var segment = new Segment(start, end);

        // Act
        var length = segment.Length;

        // Assert
        length.Should().Be(5.0);
    }

    [Fact]
    public void Length_WithSameStartAndEnd_ShouldReturnZero()
    {
        // Arrange
        var point = new Point(2.0, 3.0);
        var segment = new Segment(point, point);

        // Act
        var length = segment.Length;

        // Assert
        length.Should().Be(0.0);
    }

    [Fact]
    public void Length_WithNegativeCoordinates_ShouldCalculateCorrectLength()
    {
        // Arrange
        var start = new Point(-1.0, -1.0);
        var end = new Point(2.0, 3.0);
        var segment = new Segment(start, end);

        // Act
        var length = segment.Length;

        // Assert
        length.Should().Be(5.0);
    }

    [Fact]
    public void Length_WithDecimalCoordinates_ShouldCalculateCorrectLength()
    {
        // Arrange
        var start = new Point(1.5, 2.5);
        var end = new Point(4.5, 6.5);
        var segment = new Segment(start, end);

        // Act
        var length = segment.Length;

        // Assert
        length.Should().Be(5.0);
    }

    [Fact]
    public void Start_Property_ShouldReturnCorrectStartPoint()
    {
        // Arrange
        var start = new Point(10.0, 20.0);
        var end = new Point(30.0, 40.0);
        var segment = new Segment(start, end);

        // Act & Assert
        segment.Start.X.Should().Be(10.0);
        segment.Start.Y.Should().Be(20.0);
    }

    [Fact]
    public void End_Property_ShouldReturnCorrectEndPoint()
    {
        // Arrange
        var start = new Point(10.0, 20.0);
        var end = new Point(30.0, 40.0);
        var segment = new Segment(start, end);

        // Act & Assert
        segment.End.X.Should().Be(30.0);
        segment.End.Y.Should().Be(40.0);
    }
}
