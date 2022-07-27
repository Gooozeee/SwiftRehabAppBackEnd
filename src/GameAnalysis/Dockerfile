FROM python:3.9
WORKDIR /src
COPY ["GameAnalysis/GameAnalysisRabbitMQClient.py", "GameAnalysis/"]
RUN pip install Pika
CMD [ "python", "GameAnalysisRabbitMQClient.py"]

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["SwiftUserManagement.API/SwiftUserManagement.API.csproj", "SwiftUserManagement.API/"]
#RUN dotnet restore "SwiftUserManagement.API/SwiftUserManagement.API.csproj"
#COPY . .
#WORKDIR "/src/SwiftUserManagement.API"
#RUN dotnet build "SwiftUserManagement.API.csproj" -c Release -o /app/build