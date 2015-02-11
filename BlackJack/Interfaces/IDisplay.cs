using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.Interfaces
{
    public interface IDisplay
    {
        void DisplayMessage();

        void AddToMessage(string content);

        void NewMessage(string content);

        void ClearMessage();

        void Error(string message);

        void PlayerResult(string name, string result);

        void Clear();

        void PlayerBust();

        void PlayerHasBust(string name, double bet);

        void PlayerHasWon(string name, double bet);

        void PlayerHasTied(string name);

        void PlayerGotBlackJack(string name, double bet);

        void PlayerCash(string name, double cash);

        void PlayerNotEnoughCash(string name, double bet, double cash);

        void DealerWins();

        void PlayerWins(string name);

        void PlayerCards(string name);

        void PlayerCardsInPlay(string name);

        void DealerCard(string name);

        void PlayerOptions(string name);

        void PlayerBet(string name, string amount);

        void Card(string name, string suit, string suitSymbol);

        void Total(int total);

        void CardsRemaining(int count);

        void AskDecksToCreate();

        void ProbabilityOfCards(double probability);

        void SkipLine();

        void Wait();
    }
}
