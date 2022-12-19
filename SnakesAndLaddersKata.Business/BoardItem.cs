namespace SnakesAndLadders.Business
{
    public class BoardItem
    {
        public Position Position;

        public BoardItem(Position position)
        {
            Position = position;
        }

        public void UpdatePosition(int newPositionIndex)
        {
            Position = new Position(newPositionIndex);
        }
    }
}