using System;

namespace Tennis
{
    public class TennisGame3 : ITennisGame
    {
        private readonly string _player1Name;
        private int _player2Score;
        private readonly string _player2Name;
        private int _player1Score;

        public TennisGame3(string player1Name, string player2Name)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        public string GetScore()
        {
            if (_player1Score < 4 && _player2Score < 4 && _player1Score + _player2Score < 6)
            {
                string[] scoreDescriptions = {"Love", "Fifteen", "Thirty", "Forty"};
                return _player1Score == _player2Score
                    ? scoreDescriptions[_player1Score] + "-All"
                    : scoreDescriptions[_player1Score] + "-" + scoreDescriptions[_player2Score];
            }

            if (_player1Score == _player2Score)
                return "Deuce";

            var playerInLead = _player1Score > _player2Score ? _player1Name : _player2Name;
            var currentLead = Math.Abs(_player1Score - _player2Score);
            return currentLead == 1
                ? "Advantage " + playerInLead
                : "Win for " + playerInLead;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == _player1Name)
                _player1Score++;
            else
                _player2Score++;
        }
    }
}