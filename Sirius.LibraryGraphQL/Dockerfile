FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sirius.LibraryGraphQL/Sirius.LibraryGraphQL.csproj", "Sirius.LibraryGraphQL/"]
RUN dotnet restore "Sirius.LibraryGraphQL/Sirius.LibraryGraphQL.csproj"
COPY . .
WORKDIR "/src/Sirius.LibraryGraphQL"
RUN dotnet build "Sirius.LibraryGraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sirius.LibraryGraphQL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sirius.LibraryGraphQL.dll"]
