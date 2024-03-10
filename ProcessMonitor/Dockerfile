# Use a Microsoft container image with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0

# Set the working directory inside the container
WORKDIR /app

# Copy the C# project files into the container
COPY . /app/ProcessMonitor

# Set the working directory to the location of the C# project
WORKDIR /app/ProcessMonitor

# Restore dependencies and build the project
RUN dotnet restore
RUN dotnet build --no-restore -c Release

# # Set the entry point for the container
# ENTRYPOINT ["dotnet", "run", "--no-build", "-c", "Release"]