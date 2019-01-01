FROM microsoft/dotnet:2.2-sdk AS builder
WORKDIR /sln

COPY ./*.sln ./

COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

COPY test/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p test/${file%.*}/ && mv $file test/${file%.*}/; done

RUN dotnet restore

COPY ./src ./src
COPY ./test ./test
RUN dotnet build -c Release --no-restore

RUN dotnet test ./test/TodoList.Web.Tests/TodoList.Web.Tests.csproj -c Release --no-build --no-restore

RUN dotnet publish ./src/TodoList.Web/TodoList.Web.csproj -c Release -o ../../dist --no-restore

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=builder /sln/dist .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TodoList.Web.dll