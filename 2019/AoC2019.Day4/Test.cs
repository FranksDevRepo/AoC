using Xunit;

namespace AoC2019.Day4
{
/*
It is a six-digit number.
The value is within the range given in your puzzle input.
Two adjacent digits are the same (like 22 in 122345).
Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
Other than the range rule, the following are true:

111111 meets these criteria(double 11, never decreases).
223450 does not meet these criteria(decreasing pair of digits 50).
123789 does not meet these criteria(no double).
How many different passwords within the range given in your puzzle input meet these criteria?

Your puzzle input is 136760-595730.
*/
    public class Test
    {
        [Theory]
        [InlineData("111111", true)]
        [InlineData("223450", false)]
        [InlineData("123789", false)]
        [InlineData("122345", true)]
        [InlineData("111123", true)]
        [InlineData("135679", false)]
        public void TestIsValidPassword(string password, bool expected)
        {
            var actual = PasswordRules.IsValidPassword(password);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestCountValidPasswordsInRange()
        {
            int actual = PasswordRules.CountValidPasswordsInRange(136760, 595730);
            Assert.Equal(1873, actual);
        }

        [Fact]
        public void TestCountValidPasswordsInRangePuzzle2()
        {
            int actual = PasswordRules.CountValidPasswordsInRangePuzzle2(136760, 595730);
            Assert.Equal(1264, actual);
        }

        [Theory]
        [InlineData("112233", true)]
        [InlineData("123444", false)]
        [InlineData("111122", true)]
        [InlineData("111111", false)]
        [InlineData("223450", false)]
        [InlineData("123789", false)]
        [InlineData("122345", true)]
        [InlineData("111123", false)]
        [InlineData("135679", false)]
        [InlineData("114444", true)]
        public void TestIsValidPasswordPuzzle2(string password, bool expected)
        {
            var actual = PasswordRules.IsValidPasswordPuzzle2(password);
            Assert.Equal(expected, actual);
        }

    }
}
