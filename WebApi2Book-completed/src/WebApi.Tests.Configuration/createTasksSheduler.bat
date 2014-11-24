schtasks /create /sc minute /mo 10 /sd 11/14/2014 /tn "WebApiTestsEvery30Minute" /tr D:\dean\webapi\svn\trunk\WebApi.Tests.Configuration\runAllTests.bat
pause
exit