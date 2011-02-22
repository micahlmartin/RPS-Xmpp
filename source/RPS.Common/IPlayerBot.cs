using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public interface IPlayerBot
    {
        void GameStart(GameStartInfo info);
        MoveBase MakeMove();
        void TurnResult(TurnData data);
        void GameOver(TurnResult result);
    }
}
