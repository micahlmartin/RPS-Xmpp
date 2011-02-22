Rock, Paper, Scissors - Xmpp
============================

This is an implementation of Rock, Paper, Scissors that communicates over Xmpp. 
It was built for the Charleston Alt.NET user group RPS tournament.   

Rules
-----

The standard Rock, Paper, Scissors rules apply.

- Rock smashes Scissors
- Scissors cut paper
- Paper smothers Rock

There are two additional moves that can be made:

- Dynamite: beats everything but Bubbles
- Bubbles: beats nothing but dynamite

When a game is started there are a few options that can be made:

- MaxMoves: The total number of moves that will be made during a game.
- AllowBubbles: This specifies whether or not Bubbles can be thrown. They are completely useless unless dynamite is also allowed.
- DynamiteCount: It would be unfair if dynamite could be thrown on every turn so this will limit the amount of times it can be used in a game. For example, in a game of 500 moves it could be limited to 50 times.

Playing and Hosting the game
----------------------------

The **RPS.UI** project compiles into **RPS.exe**. This can be used both for hosting a game or playing a game. When you log in you choose the "Connect as server" option to host a game, or leave it unchecked to 
play the game.


Bot Creation
------------

If you're building your bot in .NET then check out the SamplePlayerBot project. There is an example bot in there called **SimpleBot.cs**. The only thing needed to create your own bot is to implement the
**IPlayerBot** interface. Once you have it implemented you can plug it into the client in **RPS.UI/App.xaml.cs**:

    public void SetupAsPlayer()
    {
        Func<IPlayerBot> createNewBotFunc = () => {
            //TODO: Create your bot here
            return new SimpleBot();
        };

		...
    }
	
If you want to built a bot on a platform other than .NET then see the the **Messaging** section below.
	
Messaging
---------

Messages are all Xml sent in the body of an Xmpp message. If you're using the .NET client in this project then all of the messaging is already handled for you. If you're wanting to build a client
on a different platform then you will need to implement the messaging directly. The following is an example sequence of messages.

In order to get started a player must register for the tournament by sending a **Register** message to the server.  
    	
	<Register xmlns="http://charlestonaltnet.org/xml/RPS" xmlns:i="http://www.w3.org/2001/XMLSchema-instance"/>

The server will respond with a <b>RegistrationComplete</b> message.

    <RegistrationComplete xmlns="http://charlestonaltnet.org/xml/RPS" xmlns:i="http://www.w3.org/2001/XMLSchema-instance"/>
	
Once the tournament has started a **TournamentStarted** message containing information for all the scheduled games will be sent.

    <TournamentStarted xmlns="http://charlestonaltnet.org/xml/RPS" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
      <Games>
        <GameStart>
          <AllowBubbles>true</AllowBubbles>
          <DynamiteCount>50</DynamiteCount>
          <GameId>00000000-0000-0000-0000-000000000000</GameId>
          <MaxMoves>500</MaxMoves>
          <Player1>player1@host.com/resource</Player1>
          <Player2>player2@host.com/resource</Player2>
        </GameStart>
        <GameStart>
          <AllowBubbles>true</AllowBubbles>
          <DynamiteCount>50</DynamiteCount>
          <GameId>00000000-0000-0000-0000-000000000000</GameId>
          <MaxMoves>500</MaxMoves>
          <Player1>player1@host.com/resource</Player1>
          <Player2>player3@host.com/resource</Player2>
        </GameStart>
        <GameStart>
          <AllowBubbles>true</AllowBubbles>
          <DynamiteCount>50</DynamiteCount>
          <GameId>00000000-0000-0000-0000-000000000000</GameId>
          <MaxMoves>500</MaxMoves>
          <Player1>player2@host.com/resource</Player1>
          <Player2>player3@host.com/resource</Player2>
        </GameStart>
      </Games>
    </TournamentStarted>

Each game will be started individually. A **GameStart** message will be sent to indicate a particular game has begun.

    <GameStart xmlns="http://charlestonaltnet.org/xml/RPS" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
      <AllowBubbles>true</AllowBubbles>
      <DynamiteCount>50</DynamiteCount>
      <GameId>00000000-0000-0000-0000-000000000000</GameId>
      <MaxMoves>500</MaxMoves>
      <Player1>player1@host.com/resource</Player1>
      <Player2>player2@host.com/resource</Player2>
    </GameStart>
	
Once the game has started a **TurnStart** message will be sent to each player at the start of every turn indicating that the server is awaiting for it to makes its move. The player has 10 seconds 
to respond before their move is forfeited.

    <TurnStart xmlns="http://charlestonaltnet.org/xml/RPS" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
      <GameId>00000000-0000-0000-0000-000000000000</GameId>
    </TurnStart>
	
The player should immediately respond with a **PlayerMove** message. 

    <PlayerMove xmlns="http://charlestonaltnet.org/xml/RPS" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
      <GameId>00000000-0000-0000-0000-000000000000</GameId>
      <Move>rock</Move>
    </PlayerMove>
	
Available moves are:

- rock
- paper
- scissors
- bubbles
- dynamite
	
After both players have sent their move, the server will determine the winner of that turn and send a **TurnResult** message.

    <TurnResult xmlns="http://charlestonaltnet.org/xml/RPS" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
      <GameId>00000000-0000-0000-0000-000000000000</GameId>
      <Player1Move>rock</Player1Move>
      <Player2Move>paper</Player2Move>
      <Result>Player2</Result>
    </TurnResult>
	
When the game is over the server will send a **GameOver** message indicating the winner.

    <GameOver xmlns="http://charlestonaltnet.org/xml/RPS" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
      <GameId>00000000-0000-0000-0000-000000000000</GameId>
      <Winner>Player1</Winner>
    </GameOver>


