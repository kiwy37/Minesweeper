# Minesweeper
## Overview
Minesweeper is a classic logic game played on computers. Although its exact origins are disputed, with mentions of a variant called Mined-Out dating back to 1983, its mainstream recognition came in the 1990s.

The objective of Minesweeper is simple: the player is presented with a rectangular grid game board, and their task is to uncover cells that do not contain mines. If a mine is incorrectly revealed (by clicking on it), the game is lost. To aid the player, they can flag potential mines with a flag icon, and each revealed cell indicates the number of mines in its 3x3 neighborhood. The game ends when all safe cells are uncovered.

This implementation of Minesweeper follows the classic rules described above, offering multiple difficulty levels (easy, medium, hard) akin to Google Minesweeper.

## User Interface (UI)
The UI consists of three main windows: the main window, the game window, and the statistics window. In the main window, the user selects their desired difficulty level and starts the game. Similar to the classic Minesweeper, the player must uncover all safe cells (those without mines) without triggering any bombs. This window displays the elapsed time since the start of the game and the number of flags remaining. The Material Icons package available in WPF is used for displaying game board elements such as numbers, bombs, flags, and empty cells. Data transfer between pages is facilitated by a navigation service, and after a game is completed, it is added to the list of statistics. Upon finishing a game, the user is brought back to the main window, where they can either start a new game or view previous game statistics.

Of the three view models, the ones for the main page and statistics are registered as singletons, while the game view model is transient, necessitating a new instance for each new game.

## Backend - Game Logic
The backend focuses on implementing the game logic, necessary initializations at the start of a new game, managing user interactions with the game board, and handling actions following game completion (either by losing or winning).

The backend is structured around three design patterns: Factory, Strategy, and Observer. Model classes, interfaces, and their implementations are utilized, leveraging polymorphism.

### Factory and Strategy Patterns: 
These are used for creating and initializing a new game based on the selected difficulty level.
### Observer Pattern: 
Used to determine the game state (InProgress, Won, Lost) and notify listeners accordingly. Statistics regarding played games are saved in an XML file.
The majority of the game logic is written within the IGame interface, as it is common across all difficulty levels. This interface includes methods for updating the timer, game board, flag count, and current game state based on user actions (left-click or right-click on a cell). Random mine placement and determining the number of neighbors for each non-mine cell are handled within the Board class.
