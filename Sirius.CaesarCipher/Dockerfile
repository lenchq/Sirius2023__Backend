FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sirius.CaesarCipher/Sirius.CaesarCipher.csproj", "Sirius.CaesarCipher/"]
RUN dotnet restore "Sirius.CaesarCipher/Sirius.CaesarCipher.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "Sirius.CaesarCipher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sirius.CaesarCipher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sirius.CaesarCipher.dll"]
