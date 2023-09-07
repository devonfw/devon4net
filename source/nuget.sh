#!/bin/bash

# Directory where the NuGet packages will be stored
nugets_directory="$(pwd)/nugets"

# Create the nugets directory if it doesn't exist
mkdir -p "$nugets_directory"

# Ask the user for the version input
read -p "Enter the new version: " new_version

# Find all csproj files and build/pack their projects
find . -type f -name "*.csproj" | while read -r csproj_file; do
    echo "Processing project: $csproj_file"
    
    # Update the version in the csproj file
    sed -i "s#<Version>.*</Version>#<Version>$new_version</Version>#g" "$csproj_file"
    
    # Get the directory of the csproj file
    project_dir=$(dirname "$csproj_file")
    
    # Build the project
    dotnet build --configuration Release "$project_dir"
    
    # Pack the project
    dotnet pack --configuration Release "$project_dir"
    
    # Move the generated nupkg files to the nugets directory
    mv "$project_dir"/bin/Release/*.nupkg "$nugets_directory"
done