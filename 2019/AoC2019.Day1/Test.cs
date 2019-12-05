using Xunit;

namespace AoC2019.Day1
{
    public class Test
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void TestCalculateFuel(int mass, int expected)
        {
            Assert.Equal(Solution.CalculateFuel(mass), expected);
        }

        [Theory]
        [InlineData("2019.Day1.Test.Input.txt", 34241)]
        [InlineData("2019.Day1.Input.txt", 3323874)]
        public void TestCalculateFuelForGivenInput(string path, int expected)
        {
            Assert.Equal(Solution.CalculateFuelForGivenInput(path), expected);
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void TestCalculateFuelIncludingMassOfFuel(int mass, int expected)
        {
            Assert.Equal(Solution.CalculateFuelIncludingMassOfFuel(mass), expected);
        }

        [Theory]
        [InlineData("2019.Day1.Test.Input.txt", 51316)]
        [InlineData("2019.Day1.Input.txt", 4982961)]
        public void TestCalculateFuelIncludingMassOfFuelForGivenInput(string path, int expected)
        {
            Assert.Equal(Solution.CalculateFuelIncludingMassOfFuelForGivenInput(path), expected);
        }

    }
}
