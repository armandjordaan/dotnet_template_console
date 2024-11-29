#!/bin/bash

# publish a self contained single file executable for win-x64
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true

# publish a self contained single file executable for linux-x64
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true

# publish a self contained single file executable for osx-x64
dotnet publish -c Release -r osx-x64 --self-contained true /p:PublishSingleFile=true

rsync -av --progress ./bin/Release/net8.0/win-x64/publish/ ~/rdp/Tenacity/dbtools


# # copy the publish script to the publish directory
# cp publish.sh bin/Release/net8.0/win-x64/publish/
# cp publish.sh bin/Release/net8.0/linux-x64/publish/
# cp publish.sh bin/Release/net8.0/osx-x64/publish/

# # make the publish script executable
# chmod +x bin/Release/net8.0/win-x64/publish/publish.sh
# chmod +x bin/Release/net8.0/linux-x64/publish/publish.sh
# chmod +x bin/Release/net8.0/osx-x64/publish/publish.sh

# # run the publish script
# cd bin/Release/net8.0/win-x64/publish/ && ./publish.sh
# cd bin/Release/net8.0/linux-x64/publish/ && ./publish.sh
# cd bin/Release/net8.0/osx-x64/publish/ && ./publish.sh

# # remove the publish script
# rm bin/Release/net8.0/win-x64/publish/publish.sh
# rm bin/Release/net8.0/linux-x64/publish/publish.sh
# rm bin/Release/net8.0/osx-x64/publish/publish.sh

# # zip the publish directory
# zip -r bin/Release/net8.0/win-x64/publish.zip bin/Release/net8.0/win-x64/publish/
# zip -r bin/Release/net8.0/linux-x64/publish.zip bin/Release/net8.0/linux-x64/publish/
# zip -r bin/Release/net8.0/osx-x64/publish.zip bin/Release/net8.0/osx-x64/publish/

# # remove the publish directory
# rm -r bin/Release/net8.0/win-x64/publish/
# rm -r bin/Release/net8.0/linux-x64/publish/
# rm -r bin/Release/net8.0/osx-x64/publish/

# # move the zip files