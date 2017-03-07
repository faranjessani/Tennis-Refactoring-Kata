using System;
using System.Collections.Generic;

namespace Tennis
{
    using static TennisGame1.Score;

    internal class TennisGame1 : ITennisGame
    {
        private readonly Player _player1, _player2;

        public TennisGame1(string player1Name, string player2Name)
        {
            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
        }

        public string GetScore()
        {
            if (GameIsWon)
                return $"Win for {PlayerInLead}";

            if (AtLeastThreePointsScored)
                return GameIsTied 
                    ? "Deuce" 
                    : $"Advantage {PlayerInLead}";

            return GameIsTied
                ? $"{Description.For(PlayerInLead.Score)}-All"
                : $"{Description.For(_player1.Score)}-{Description.For(_player2.Score)}";
        }

        public void WonPoint(string playerName)
        {
            if (playerName.Equals(_player1.Name))
                _player1.Score++;
            else
                _player2.Score++;
        }

        private bool GameIsWon => (_player1.Score > Forty || _player2.Score > Forty) && Math.Abs(_player1.Score - _player2.Score) >= 2;

        private bool GameIsTied => _player1.Score == _player2.Score;

        private bool AtLeastThreePointsScored => _player1.Score >= Forty && _player2.Score >= Forty;

        private Player PlayerInLead => _player1.Score > _player2.Score ? _player1 : _player2;

        internal enum Score
        {
            Love = 0,
            Fifteen = 1,
            Thirty = 2,
            Forty = 3
        }

        internal static class Description
        {
            private static readonly IDictionary<Score, string> Scores = new Dictionary<Score, string>(4)
            {
                {Love, "Love"},
                {Fifteen, "Fifteen"},
                {Thirty, "Thirty"},
                {Forty, "Forty"}
            };

            public static string For(Score score)
            {
                return Scores[score];
            }
        }

        internal class Player
        {
            public Player(string name)
            {
                Name = name;
                Score = Love;
            }

            public string Name { get; set; }
            public Score Score { get; set; }
            public override string ToString()
            {
                return $"{Name}";
            }
        }
    }
}