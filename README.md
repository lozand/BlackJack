BlackJack
=========

Game of blackjack with the intent of recording probabilities. Used as an exercise in C# as well as probability.

The Game
========
The game of Black Jack is played between the dealer and the players. The object of the game is to <b>get a higher score than the dealer</b> without exceeding a score of 21. (it should be noted that the object is NOT to beat other players). Score is determined by the sum of the values of the cards played where King, Queen, and Jack are worth 10 pts, an Ace is worth either 11 points or 1 point and all other cards are worth their face value . Upon winning, the player will receive back twice the amount of his or her bet (i.e. if a player bets $50, he is awarded $50 so that he or she now has $100). 
<br>
After each player has made his or her bet, the game begins when the dealer deals 2 cards to each player (from the left) including the dealer himself (though only one card will be shown). If a player receives an Ace and a 10-value card (King, Queen, Jack or 10), then that is called "Black Jack" and that player is awarded 1.5x his or her bet. (i.e. if the player bet $10, he or she would win $15 for a total of $25). 
Otherwise, each player has a few options:
- Hit - This means that the player will receive one additional card. If that card is less than 21, then the player can opt to hit again or stand.
- Double - The player can double his or her bet, but only get ONE more card. This is most advantageous to do with a score of 9, 10, or 11 and some casinos only allow doubles on these cards. Other casinos will allow doubles on any 2 cards, but a player can only ever double as his or her THIRD card. 
- Split - If two cards are the same value, then the player can opt to "split" his or her cards, which means that they effectively are playing two hands with two separate bets. The dealer will deal the first card of the first hand, and the player will play normally with that hand until he or she either stands or busts, and then will move onto the second hand. Some casinos only allow one card per split, and some casinos have rules around splitting aces. 
- Stand - If the player is alright with the cards he or she has, and the sum of those cards are less than 21, then the player can stand and end his or her turn. A player's turn will always end with a stand or a bust.

<br>

Once all players have made their decisions, the dealer will reveal his or her second card, and then hit until he or she reaches 17. Once 17 is reached, the dealer must stand, and any player with a score above the dealer will win. If the dealer busts, then all hands that are not busted will win and bets are awarded. The players then bet again and another round begins.

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

The Future
============
There are a number of features that I would like to implement later on in the 2.0 version. 
- For starters, I'd like to see an actual UI. Wouldn't that be nice?
- Secondly, there should be a simulation mode, and an option to play with AI
- If possible, I would like there to be some sort of multiplayer (though, this may not be available until v3.0)
- Ability to change "house rules"
- Ability to create player settings
- Player "advice"
- Custom players (so you don't have to play with the player names that I decide).
- Full-fledged unit tests!!

