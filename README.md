BlackJack
=========

Game of blackjack with the intent of recording probabilities. Used as an exercise in C# as well as probability.

The Game
========
[Explain the game]

The Solution
============
The base class is the <code>Card</code>. The <code>Card</code> class has two main properties, <code>Name</code> (which denotes what name a card is) and <code>Suit</code> (the suit of the card). 
<br>
The <code>Deck</code> class contains a <code>List\<Card\></code> which represents all cards in the deck. The <code>Deck</code> is initialized with the number of decks to be used. The class also contains functions for the deck to use such as a queue of played cards, a shuffle method, a reset method and a deal card method that returns the top <code>Card</code> in the <code>Deck</code>. 
The <code>Hand</code> class also has a <code>List\<Card\></code> which represents the cards in a hand. There is also a Total method which sums the cards in the hand.
<br>
Each instace of a <code>Player</code> class has a <code>List\<Hand\></code> (since each player can have multiple hands). The betting and the payout ratios are handled at this level as well.
The most complex class is the <code>Table</code> class which actually plays the game. Each "instance" of the class is a round (so that you have to initialize the class again in order to start another round)

Currently, the game is started in a Program class which allows you to play the game. So, that's real.

The solution also contains a few Status classes as well as a Unit Test project.
