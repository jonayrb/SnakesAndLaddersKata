using NUnit.Framework;
using SnakesAndLadders.Business;

namespace SnakesAndLadders.Tests.Business
{
    public class SixSidedDiceShould
    {
        [Test]
        [Repeat(10)]
        public void when_rolls_a_die_then_the_result_should_be_between_1_6_inclusive()
        {
            var sixSidedDice = new SixSidedDice();
            var rollValue = sixSidedDice.GetValue();

            Assert.LessOrEqual(rollValue, 6);
            Assert.GreaterOrEqual(rollValue, 1);
        }
    }
}