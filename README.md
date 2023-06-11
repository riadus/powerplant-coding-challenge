# powerplant-coding-challenge
This is a .NET 7 implmentation of Powerplant Coding Challenge written by Riad Lakehal-Ayat.
## Build and run
In /src folder run these commands
### Command line
`dotnet build PowerplantCodingChallenge.sln`

`dotnet run --project PowerplantCodingChallenge/PowerplantCodingChallenge.csproj`
### Dockerfile
`docker image build -f Dockerfile .. -t coding-challenge-riad`

`docker run -p 8888:8888 coding-challenge-riad`
### Use API
Post payloads on the endpoint `http://localhost:8888/productionplan/`
