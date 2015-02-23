# QlikSensePowerShell

This is a draft c# based project that builds powershell plugin that enables you to run common Qlik Sense automation functions from the powershell command line.

In this release you can run the following commands...

QS-Get-List <serverurl> <object> [filter]   -  to get a list of any object from sense, eg apps, streams tasks
QS-Get-Task <serverurl> [filter]  - to get either  list of tasks and their history, or a single task
QS-Start-Task <serverurl> <taskid or taskname>   -  to start a given task
QS-Check-Task   <serverurl> <taskid or executionid>   - to check the last status of a given task or a specific execution


To get running...

1 - Build the source code to compile the QlikSensePS.dll
2 - Open the powershell command line in windows and execute the following commands

  set-alias installutil $env:windir\Microsoft.NET\Framework64\v4.0.30319\installutil
  installutil C:\pathtowherethedllis\QlikSensePS.dll
  get-PSsnapin -registered
  add-pssnapin QlikSensePSSnapIn

3 - now you can use the commands e.g.

   QS-get-List -serverurl https://localhost -objecttype app -filter name eq salesapp
   QS-get-Task -serverurl https://localhost
   QS-Start-Task -serverurl https://localhost -taskname reloadofsales
   QS-check-Task -serverurl https://localhost -executionid d9313020-9c53-4a25-bfec-f58916c7a03e





