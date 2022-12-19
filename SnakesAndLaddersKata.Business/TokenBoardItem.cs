namespace SnakesAndLadders.Business {
    public class TokenBoardItem : BoardItem {
        private readonly Player player;

        public TokenBoardItem(Position position, Player player) : base(position)
        {
            this.player = player;
        }

        public string GetPlayerId()
        {
            return player.Id;
        }
    }
}