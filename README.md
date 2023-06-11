# powerplant-coding-challenge
This is a .NET 7 implmentation of Powerplant Coding Challenge written by Riad Lakehal-Ayat.
## Build and run

### Command line
In /src folder run these commands

`dotnet build PowerplantCodingChallenge.sln`

`dotnet run --project PowerplantCodingChallenge/PowerplantCodingChallenge.csproj`
### Dockerfile
In /src/PowerplantCodingChallenge folder run these commands

`docker image build -f Dockerfile .. -t coding-challenge-riad`

`docker run -p 8888:8888 coding-challenge-riad`
### Use API
Post payloads on the endpoint `http://localhost:8888/productionplan/`

## Code coverage
Code coverage reports can be visualised here 
https://htmlpreview.github.io/?https://github.com/riadus/powerplant-coding-challenge/blob/master/src/PowerplantCodingChallenge.Tests/coveragereport/index.html