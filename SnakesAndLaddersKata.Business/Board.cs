using SnakesAndLadders.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SnakesAndLadders.Business {
    public class Board {
        private const int WinnerSquareNumber = 100;
        private readonly List<TokenBoardItem> tokens;
        private readonly IDiceGenerator diceGenerator;
        private string lastOperation { get; set; }

        public Board(IDiceGenerator diceGenerator)
        {
            lastOperation = "Initiating board";
            this.diceGenerator = diceGenerator;
            tokens = new List<TokenBoardItem>();
        }

        public void AddNewTokenFor(string playerId)
        {
            const int initialSquarePositionIndex = 0;
            var initialPosition = new Position(initialSquarePositionIndex);
            var newPlayer = new Player(playerId);
            tokens.Add(new TokenBoardItem(initialPosition, newPlayer));
        }

        public void MoveTokenFor(string playerId, int spaces)
        {
            var tokenPlayer = GetTokenForPlayer(playerId);
            var currentTokenPlayerPosition = tokenPlayer.Position;
            var newPositionIndexForTokenPlayer = currentTokenPlayerPosition.Index + spaces;
            tokenPlayer.UpdatePosition(newPositionIndexForTokenPlayer);
            lastOperation = $"Moved token for {playerId} {spaces} spaces (from {currentTokenPlayerPosition.SquareNumber} to {newPositionIndexForTokenPlayer + 1})";
        }

        public void PlayerRollsADie(string playerId)
        {
            var spacesToMove = diceGenerator.GetValue();
            MoveTokenFor(playerId, spacesToMove);
        }

        public BoardItem GetTokenForPlayer(string playerId)
        {
            return tokens.First(token => token.GetPlayerId() == playerId);
        }

        public bool PlayerIsWinner(string playerId)
        {
            var tokenPlayer = GetTokenForPlayer(playerId);
            return tokenPlayer.Position.SquareNumber == WinnerSquareNumber;
        }

        public bool PlayerIsLoser(string playerId)
        {
            var tokenPlayer = GetTokenForPlayer(playerId);
            return tokenPlayer.Position.SquareNumber > WinnerSquareNumber;
        }

        public bool GameIsFinished()
        {
            var gameHaveAWinner = tokens.FirstOrDefault(token => PlayerIsWinner(token.GetPlayerId()));
            if (gameHaveAWinner != null)
            {
                lastOperation = $"{gameHaveAWinner.GetPlayerId()} is the Winner!!";
                return true;
            }
            var allPlayersAreLosers = tokens.All(token => PlayerIsLoser(token.GetPlayerId()));
            if (allPlayersAreLosers)
            {
                lastOperation = $"All players are loser!! :(";
                return true;
            }
            return false;
        }

        public string GetLastOperationInformation()
        {
            return lastOperation;
        }
    }
}