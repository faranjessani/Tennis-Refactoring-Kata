using System.Collections.Generic;

namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private readonly string _player1Name;
        private readonly string _player2Name;
        private Score _p1Point;
        private Score _p2Point;

        public TennisGame2(string player1Name, string player2Name)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        private readonly IDictionary<Score, string> _scores = new Dictionary<Score, string>(4)
            {
                {Score.Love, "Love"},
                {Score.Fifteen, "Fifteen"},
                {Score.Thirty, "Thirty"},
                {Score.Forty, "Forty"}
            };

        internal enum Score
        {
            Love = 0,
            Fifteen = 1,
            Thirty = 2,
            Forty = 3
        }

        public string GetScore()
        {
            var score = "";
            
            if (IsGameTied && _p1Point <= Score.Thirty)
                score = _scores[_p1Point] + "-All";
            else if(IsGameTied && _p1Point > Score.Thirty)
                score = "Deuce";
            else if (_p1Point <= Score.Forty && _p2Point <= Score.Forty)
                score = _scores[_p1Point] + "-" + _scores[_p2Point];
            else if (_p1Point > Score.Forty && _p1Point - _p2Point >= 2)
                score = $"Win for {_player1Name}";
            else if (_p2Point > Score.Forty && _p2Point - _p1Point >= 2)
                score = $"Win for {_player2Name}";
            else if (_p1Point > _p2Point && _p2Point >= Score.Forty)
                score = $"Advantage {_player1Name}";
            else if (_p2Point > _p1Point && _p1Point >= Score.Forty)
                score = $"Advantage {_player2Name}";

            return score;
        }

        private bool IsGameTied => _p1Point == _p2Point;

        public void WonPoint(string player)
        {
            if (player == _player1Name)
                _p1Point++;
            else
                _p2Point++;
        }
    }
}