echo Restoring packages...
dotnet restore FizzBuzz\FizzBuzz.csproj
dotnet restore FizzBuzz.Tests\FizzBuzz.Tests.csproj

echo Building the code...
dotnet build -c Release --no-restore FizzBuzz\FizzBuzz.csproj
dotnet build -c Debug --no-restore FizzBuzz.Tests\FizzBuzz.Tests.csproj

echo Running tests...
dotnet test -c Debug --no-restore --no-build FizzBuzz.Tests\FizzBuzz.Tests.csproj

echo Running the program...
dotnet run --no-restore --no-build --project FizzBuzz\FizzBuzz.csproj

echo Done!
