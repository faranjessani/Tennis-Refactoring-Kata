using System;
using System.Collections.Generic;

namespace Tennis
{
    internal class TennisGame1 : ITennisGame
    {
        private readonly string _player1Name;
        private readonly string _player2Name;
        private int _player1Score;
        private int _player2Score;

        public TennisGame1(string player1Name, string player2Name)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        public string GetScore()
        {
            if (GameIsOver)
                return _player1Score > _player2Score ? $"Win for {_player1Name}" : $"Win for {_player2Name}";
            if (GameIsTied)
                return _player1Score >= 3 ? "Deuce" : $"{ScoreDescription.For(_player1Score)}-All";
            if (Player1HasAdvantage)
                return $"Advantage {_player1Name}";
            if (Player2HasAdvantage)
                return $"Advantage {_player2Name}";

            return $"{ScoreDescription.For(_player1Score)}-{ScoreDescription.For(_player2Score)}";
        }

        private bool GameIsOver => (_player1Score >= 4 || _player2Score >= 4) && Math.Abs(_player1Score - _player2Score) >= 2;

        private bool Player2HasAdvantage => _player2Score > _player1Score && _player2Score >= 4;

        private bool Player1HasAdvantage => _player1Score > _player2Score && _player1Score >= 4;

        private bool GameIsTied => _player1Score == _player2Score;

        public void WonPoint(string playerName)
        {
            if (playerName == _player1Name)
                _player1Score++;
            else
                _player2Score++;
        }

        internal static class ScoreDescription
        {
            private static readonly IDictionary<int, string> Scores = new Dictionary<int, string>(4)
            {
                {0, Love},
                {1, Fifteen},
                {2, Thirty},
                {3, Fourty}
            };

            public static string Love => "Love";
            public static string Fifteen => "Fifteen";
            public static string Thirty => "Thirty";
            public static string Fourty => "Forty";

            public static string For(int score)
            {
                return Scores[score];
            }
        }
    }
}