using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.Interfaces
{
    public interface IDeck
    {
        int GetCardsRemaining();

        void Reset(int numberOfDecks);

        int NumberOfDecks { get; set; }

        ICard DealNextCard();
        
        void ProbabilityOfHighCard();

        double ProbabilityOfCard(string cardNames);

        void ProbabilityOfGettingBlackJack();
        
        void PlayNextCard(bool displayCards);

        void ShowCardsRemaining();

        void DealCards(int cardsToDeal, bool showCards);

        void PlayCard(string card);
    }
}
