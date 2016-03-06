# YGOPRODraft
Simple Draft Client/Server Application for YGOPRO dueling simulator

##General Notes:
While this does work, it was basically only written to have a drafting app for YGOPRO for private use with friends. I decided to put it online so the YGOPRO community can have fun with it, and also maybe improve the code if anyone wants. Expect some issues, bad error handling and other inconveniences, as it basically just is a proof of concept which was designed for private use ;-)

##How does it work
Both server and client applications pull the card information from the cards.cdb file of YGOPRO. The server uses the json-Files in the jsons pack folder to query the cards.cdb file, and sends the card IDs to the client. The client queries the card ids in cards.cdb to display the card infos.

##Usage: 
###Server
Starts "YGOPRODraftServer.exe" in "ServerFiles", picks the path to the Pack folder (called "jsons" in "Serverfiles"), also picks the path to your YGORPO "cards.cdb" file.

###Client
Starts "YGOPRODraftClient.exe" in "ClientFiles", chooses Server IP (Port is fixed at 12345 right now, might need forwarding or hamachi for Internet drafting), and also picks the path to the YGOPRO "cards.cdb" file.

Picking cards as the client is simple. Mouse-Over shows information of the cards. Cliking them adds them to your draft. If you want to add a card to the side deck, just click it from the main deck. You can also scroll through the respective card areas.

###Server-Client Interaction
The server needs to configure the draft settings. As soon as this is done, the server can be started by clicking "Start Server". The client can afterwards connect to the server by entering the IP and clicking "Connect to Server". The Draft tool automatically supports multiple clients if they connect. After you connected, you wait for the other clients to connect. When all clients are connected, they can send their "Ready" message to the server. As soon as all clients are ready, the server starts to distribute the cards to the clients.


##Server configuration
As soon as the pack folder is chosen ("jsons" folder by default), you can add packs by doubleclicking them from the "Choose Packs" list. They will then appear in the "Chosen Packs" list.

###Number of Cards Per Booster
This determines the number of cards pulled from each pack.

###Number of Rares Per Booster
This determines the number of Rares (Rares, Super Rares, Ultra Rares...) per Booster.

###Number of Cards Per Draft Round
How many cards are drafted before new cards are pulled. This is per player, so in default config, every player will draft 5 cards before new cards are pulled.

###Remove Pulled Cards From Pool
If this is ticked on, pulled cards are removed from the Booster pool. That means that every card listed in the pack can only be pulled once! (Be careful to make sure you don't empty the pack this way.)

###Pull only from one Pack each round
If this is picked, every round only pulls from a single Pack in "Chosen Packs". Otherwise, cards are pulled from all Packs simultaneously.

##Pack-List Configuration
The default path for packs is "jsons". If you want to add another pack, you can just add another pack file to this folder. These packs are stored as json files. In "YDKToJSONRelease" you can also find a tool which converts your YGOPRO .ydk deck files into json pack files for usage with the draft tool.


#Credits:
Thanks to Lidgren, Newtonsoft for their amazing networking and JSON libraries, as well as adosreis (https://github.com/adosreis/Yugioh-Pack-Opening-Simulator) for the inspiration to use JSON files and the basic JSON files from lots of booster packs!

#Probable Issues:
Error Handling is basically non-existant for certain cases. The disconnect of clients takes a while to actually get through to the server, which might result in "ghost" connections still active on the server. Most issues are resolved by simply restarting the server/client. 
