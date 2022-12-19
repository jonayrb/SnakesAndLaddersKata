using System;
using SnakesAndLadders.Business.Interfaces;

namespace SnakesAndLadders.Business
{
    public class SixSidedDice : IDiceGenerator
    {
        public int GetValue()
        {
            return new Random(DateTime.Now.Millisecond).Next(1, 6);
        }
    }
}