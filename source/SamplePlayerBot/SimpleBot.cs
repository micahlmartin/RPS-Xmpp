using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPS.Common;

namespace SamplePlayerBot
{
    public class SimpleBot : IPlayerBot
    {
        private MoveBase[] _availableMoves = new MoveBase[] { new Rock(), new Paper(), new Scissors(), new Bubbles(), new Dynamite() };
        private static Random _rand = new Random((int)DateTime.Now.Ticks);

        private GameStartInfo _gameInfo;
        private int _dynamiteCount = 0;

        public void GameStart(GameStartInfo info)
        {
            //Log some info about the game
            _gameInfo = info;
        }

        public MoveBase MakeMove()
        {
            var canThrowDynamite = _dynamiteCount < _gameInfo.DynamiteCount;
            var canThrowBubbles = _gameInfo.AllowBubbles;

            var getNext = true;
            var next = 0;

            while (getNext)
            {
                next = _rand.Next(0, 5);

                if (next == 3 && !canThrowBubbles)
                    continue;
                if (next == 4 && !canThrowDynamite)
                    continue;

                getNext = false;
            }

            if (next == 4)
                _dynamiteCount++;

            return _availableMoves[next];
        }

        public void TurnResult(TurnData data)
        {
            //Keep track of stats
        }

        public void GameOver(TurnResult result)
        {
            //Clean everything up
        }
    }
}
