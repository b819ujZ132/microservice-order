###
# Build and publish the application to an intermediate image
###
FROM microsoft/dotnet:2.1-sdk AS builder

WORKDIR /app

# Copy only project file so restore will only redownload if project file changes
COPY ./order.csproj app/order.csproj

RUN dotnet restore

# Publish the application
RUN dotnet publish -c Release --framework netcoreapp2.2 -o /app/publish

###
# Create the runnable image from the published application
###
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS release

# Set required environment variables
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT Development

COPY --from=builder /app/publish /app

EXPOSE 8000/tcp

CMD dotnet order.dll