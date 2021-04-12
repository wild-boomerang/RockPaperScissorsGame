# Simple console Rock paper scissors game on .NET Core 3.1

## Command line options
Arguments represent the moves in the game.
1. Odd number of arguments
1. Arguments must not be repeated
1. Number of arguments >= 3.  

## Releases
### A platform-specific executable
A platform-specific executable don't require any additional software, just launch by passing arguments.
#### Workflow example
1. Download appropriate your OS game version
1. Run the game with this command: `<filepath> rock paper scissors`.

### A cross-platform binary
A cross-platform binary (_.zip_ containing _.dll_ file and 2 additional _.json_ files) can be run on any operating system as long as the .NET runtime is already installed.
#### Requirements
.NET runtime must be installed.
#### Workflow example
1. Download game version with _dotnet_ postfix
1. Unzip file
1. Run the game with this command: `dotnet <path to RockPaperScissorsGame.dll> rock paper scissors`.
