using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    using static TennisGame1.Score;

    internal class TennisGame1 : ITennisGame
    {
        private readonly Player _player1, _player2;
        private readonly Player[] _sortedPlayers;

        public TennisGame1(string player1Name, string player2Name)
        {
            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
            _sortedPlayers = new[] {_player1, _player2};
        }

        public string GetScore()
        {
            if (GameIsWon)
            {
                return $"Win for {_sortedPlayers[0]}";
            }

            if (AtLeastThreePointsScored)
            {
                return GameIsTied ? "Deuce" : $"Advantage {_sortedPlayers[0]}";
            }

            return GameIsTied
                ? $"{Description.For(_sortedPlayers[0].Score)}-All"
                : $"{Description.For(_player1.Score)}-{Description.For(_player2.Score)}";
        }

        public void WonPoint(string playerName)
        {
            if (playerName.Equals(_sortedPlayers[0].Name, StringComparison.InvariantCultureIgnoreCase))
                _sortedPlayers[0].Score++;
            else
            {
                _sortedPlayers[1].Score++;
                if (_sortedPlayers[1].Score <= _sortedPlayers[0].Score) return;

                // Swap order to maintain descending order in list
                var temp = _sortedPlayers[0];
                _sortedPlayers[0] = _sortedPlayers[1];
                _sortedPlayers[1] = temp;
            }
        }

        private bool GameIsWon => _sortedPlayers.Any(p => p.Score > Forty) && Math.Abs(_sortedPlayers[0].Score - _sortedPlayers[1].Score) >= 2;

        private bool GameIsTied => _sortedPlayers[0].Score == _sortedPlayers[1].Score;

        private bool AtLeastThreePointsScored => _sortedPlayers.All(p => p.Score >= Forty);

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