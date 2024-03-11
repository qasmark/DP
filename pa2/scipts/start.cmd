SET PATH_TO_APP=%~dp0..\Valuator\
SET PATH_TO_NGINX=%~dp0..\nginx\ 

start /d %PATH_TO_APP% dotnet run --no-build --urls "http://localhost:5001"
start /d %PATH_TO_APP% dotnet run --no-build --urls "http://localhost:5002"

start /d %PATH_TO_NGINX% nginx.exe


