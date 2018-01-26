# waes-technical-assessment

## Pre-requisites

- .Net Core 2.0
- Docker

## Usage

You should first execute the "docker-mongodb-setup.bat" and use the IP address of your docker machine (usually 192.168.99.100) to change the appSettings.json file inside TechnicalAssessment.Tests and TechnicalAssessment.WebApi folders before running the 'dotnet test TechnicalAssessment.Tests' command.

This command will execute both unit tests and integration tests.

For some strange reason I was able to sucessfully run mongodb docker container then it stopped all of a sudden and couldn't fix the environment in time. So I came up with an alternative connection string of a cloud mongo cluster: mongodb+srv://admin:admin@cluster0-5snvo.mongodb.net/test which is setup by default

## Observations

This project is heavy on comments since it is StyleCop compliant. It is a way to assure it passes with a high score in most of the SONAR code metrics analysis adopted by companies. It can be opted out on demand.