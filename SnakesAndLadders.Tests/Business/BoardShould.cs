using NSubstitute;
using NUnit.Framework;
using SnakesAndLadders.Business;
using SnakesAndLadders.Business.Interfaces;

namespace SnakesAndLadders.Tests.Business {
    public class BoardShould {
        private const string AnyPlayerId = "anyPlayer";
        private Board board;
        private IDiceGenerator diceGenerator;

        [SetUp]
        public void Setup()
        {
            diceGenerator = Substitute.For<IDiceGenerator>();
            this.diceGenerator.GetValue().Returns(1);
            board = new Board(this.diceGenerator);
        }

        [Test]
        public void when_the_token_is_placed_on_the_board_then_the_token_is_on_square_1()
        {
            board.AddNewTokenFor(AnyPlayerId);

            VerifyTokenPositionForPlayerIsInSquare(AnyPlayerId, 1);
        }

        [Test]
        public void when_the_token_is_moved_3_spaces_then_the_token_is_on_square_4()
        {
            board.AddNewTokenFor(AnyPlayerId);

            board.MoveTokenFor(AnyPlayerId, 3);

            VerifyTokenPositionForPlayerIsInSquare(AnyPlayerId, 4);
        }

        [Test]
        public void when_the_token_is_moved_3_spaces_and_then_it_is_moved_4_spaces_then_the_token_is_on_square_8()
        {
            board.AddNewTokenFor(AnyPlayerId);
            board.MoveTokenFor(AnyPlayerId, 3);

            board.MoveTokenFor(AnyPlayerId, 4);

            VerifyTokenPositionForPlayerIsInSquare(AnyPlayerId, 8);
        }

        [Test]
        public void when_the_token_is_moved_and_the_token_is_on_square_100_the_player_has_won_the_game()
        {
            board.AddNewTokenFor(AnyPlayerId);
            board.MoveTokenFor(AnyPlayerId, 96);

            board.MoveTokenFor(AnyPlayerId, 3);

            var playerIsWinner = board.PlayerIsWinner(AnyPlayerId);
            Assert.That(playerIsWinner, Is.EqualTo(true));
        }

        [Test]
        public void when_the_token_is_moved_and_the_token_is_more_than_square_100_the_player_has_not_won_the_game()
        {
            board.AddNewTokenFor(AnyPlayerId);
            board.MoveTokenFor(AnyPlayerId, 96);

            board.MoveTokenFor(AnyPlayerId, 4);

            var playerIsLoser = board.PlayerIsLoser(AnyPlayerId);
            Assert.That(playerIsLoser, Is.EqualTo(true));
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(6)]
        public void when_the_player_rolls_a_die_then_the_token_should_be_moved_sames_spaces(int dieValue)
        {
            board.AddNewTokenFor(AnyPlayerId);
            diceGenerator.GetValue().Returns(dieValue);

            board.PlayerRollsADie(AnyPlayerId);

            VerifyTokenPositionForPlayerIsInSquare(AnyPlayerId, dieValue + 1);
        }

        [Test]
        public void when_some_player_is_winner_the_game_ends()
        {
            const string anyWinnerPlayer = "anyWinnerPlayer";
            board.AddNewTokenFor(anyWinnerPlayer);
            board.MoveTokenFor(anyWinnerPlayer, 99);
            board.AddNewTokenFor("OtherPlayer");

            var gameIsFinished = board.GameIsFinished();

            Assert.That(gameIsFinished, Is.EqualTo(true));
        }

        [Test]
        public void when_all_players_are_losers_the_game_ends()
        {
            const string aLoserPlayer = "aLoserPlayer";
            board.AddNewTokenFor(aLoserPlayer);
            const string otherLoserPlayer = "OtherLoserPlayer";
            board.AddNewTokenFor(otherLoserPlayer);
            board.MoveTokenFor(aLoserPlayer, 101);
            board.MoveTokenFor(otherLoserPlayer, 105);

            var gameIsFinished = board.GameIsFinished();

            Assert.That(gameIsFinished, Is.EqualTo(true));
        }

        [Test]
        public void when_all_players_are_not_winners_and_losers_the_game_is_not_finished()
        {
            const string aLoserPlayer = "aLoserPlayer";
            board.AddNewTokenFor(aLoserPlayer);
            const string otherLoserPlayer = "OtherLoserPlayer";
            board.AddNewTokenFor(otherLoserPlayer);
            board.MoveTokenFor(aLoserPlayer, 44);
            board.MoveTokenFor(otherLoserPlayer, 22);

            var gameIsFinished = board.GameIsFinished();

            Assert.That(gameIsFinished, Is.EqualTo(false));
        }

        private void VerifyTokenPositionForPlayerIsInSquare(string playerId, int expectedSquareNumber)
        {
            var playerPosition = board.GetTokenForPlayer(playerId);
            Assert.That(expectedSquareNumber, Is.EqualTo(playerPosition.Position.SquareNumber));
        }
    }
}