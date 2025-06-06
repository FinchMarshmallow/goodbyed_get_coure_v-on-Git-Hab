@echo off
set PROJECT_NAME=MyApp

dotnet new console -n %goodbyed_get_course% --force
copy Program.cs %goodbyed_get_course%\Program.cs
cd %PROJECT_NAME%

dotnet add package OpenQA.Selenium.Chrome
dotnet add package System.Text.Json

dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true --self-contained true -o ../out

echo complite 'out'
pause