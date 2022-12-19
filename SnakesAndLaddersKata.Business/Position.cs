namespace SnakesAndLadders.Business
{
    public class Position
    {
        public int Index { get;}

        public int SquareNumber => Index + 1;

        public Position(int index)
        {
            Index = index;
        }
    }
}