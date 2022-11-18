# Connect4 API

Connect4 API is a Rest API for determining the winner of any game standing in the game Connect4. It is written in c# and can be run on dotnet.

## Installation dotnet

Install dotnet 6.0. Further information can be found here: 
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Run API

Use the cli to start the API. Use the following command in the folder 
```./connect4_api```

```cs
dotnet run
```

## Usage

send a GET request with the current game standing rolled out as as string.
Example:  ```input = "BXXXXXBAXXXXBXXXXXBXXXXXAAAXXXXXXXXXXXXXXX"```
To the following path:

```cs

# structure of GET request
"https://{host}/Connect4API/{input}"

# possible returns as char
'A' => Player A has won.
'B' => Player B has won.
'X' => No winner. Game can go on.

# example request
"https://localhost:7223/Connect4Api/BXXXXXBAXXXXBXXXXXBXXXXXAAAXXXXXXXXXXXXXXX"

# example request return a char
'B'
```

## Testing http Requests

Testing http Requests with VS Code extension REST Client:

Name: REST Client
Id: humao.rest-client
Description: REST Client for Visual Studio Code
Version: 0.25.1
Publisher: Huachao Mao
VS Marketplace Link: https://marketplace.visualstudio.com/items?itemName=humao.rest-client


File with tests:
```./test/test_requests.http```

## Testing with Xunit

Unit Tests testing the method ```Check_Winner()``` are written in ```./connect4_UnitTests/Connect4ApiControllerTests.cs```

All Testing Inputs are stored in a separate ```.txt``` file int the folder:
```./test/```

The ```.txt``` files follow the naming convention: ```Name_ExpectedStatusCode_ExpectedWinner.txt```


The Tests can be run with the cli command:
```cs
dotnet test
```


## Authors
Tobias Schraner tobias.schraner@gmail.com
