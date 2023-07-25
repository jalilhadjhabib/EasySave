# EasySave
## Introduction
EasySave Software allows you to save any folder/file with 2 differents saves (Full Save or Differential Save).

## About the project
Our team has just integrated the software publisher ProSoft.   Under the responsibility of the CIO, we are in charge of managing the "EasySave" project which consists in developing a backup software.  As any software of the ProSoft Suite, the software will be integrated into the pricing policy.


Release dates and versions :

Version 1.0 : 25/11/2020.

Version 2.0 : 07/12/2020.

Version 3.0 : 11/12/2020.

Used Technologies in the development of EasySave :  
This section lists each of the major technologies used to complete this project. For anymore details you can see the acknowledgements section.

-Visual Studio 2019

-Dev Azure

-Framework .CORE

-Visual Paradigm

## Version 1.0
![image](https://github.com/jalilhadjhabib/EasySave/assets/101253359/2a0f40a1-ea88-412c-a047-6680e4717f02)

The specifications of the first version of the software are as follows : 

The software is a Console application using .Net Core. It must allow the creation of up to 5 backup jobs.

A backup job is defined by :

-An appellation

-A source directory

-A target directory

-One type

-Full

-Differential

-English

-The user may request the execution of one of the backup jobs or the sequential execution of the jobs.
The directories (sources and targets) can be on local, external or network drives.
All the elements of the source directory are concerned by the backup.

Daily Log File :

-The software must write in real time in a daily log file the history of the actions of the backup jobs. The minimum expected information is :

-Timestamp  

-Naming the backup job

-Full address of the Source file (UNC format)

-Full address of the destination file (UNC format)

-File Size 

-File transfer time in ms (negative if error)    

-Status File The software must record in real time, in a single file, the progress of the backup jobs.  The minimum expected information is :  

-Timestamp  

-Naming the backup job

-Backup job status (e.g. Active, Not Active...) If the job is active

-The total number of eligible files

-The size of the files to be transferred 

-The progression         

-Number of remaining files  

-Size of remaining files  

-Full address of the Source file being backed up

-Complete address of the destination file

The locations of the two files described above (daily log and status) will have to be studied to work on the clients' servers. As a result, "c:\temp" type locations are to be avoided.


The files (daily log and status) and any configuration files will be in XML or JSON format. In order to allow fast reading via Notepad, it is necessary to put line feeds between the XML (or JSON) elements. A pagination would be a plus.

## Version 1.1
![image](https://github.com/jalilhadjhabib/EasySave/assets/101253359/b8cbb5fe-56c9-4b1d-99c7-2790866338e8)

Basically It has the same functions as the version 1.0 but there is an added option  that let you choose between English or French language.

## Version 2.0
![image](https://github.com/jalilhadjhabib/EasySave/assets/101253359/6f022c51-56f1-4a1f-94a5-08ecd59c322a)


EasySave 1.0 has been distributed to many customers. 

Following a customer survey, the management decided to create a version 2.0 with the following improvements: 


1.Graphical Interface: Leaving the console mode. The application in now a fully adapted GUI app.

2-Unlimited number of saves : In 1.0 user was limited to 5 saves tasks per session , this restriction is no more in 2.0!

3-Encryption via CryptoSoft software : The dev team worked on a software that can encrypt files with extension defined by the user . This feature
is fully implemented in 2.0

4-Log file updated : Now containe the encryption process time (in ms)


Version 2.0 is a GUI enchanced version of 1.0 , with the addition of some new features.

## Version 3.0
![image](https://github.com/jalilhadjhabib/EasySave/assets/101253359/5b0f7d21-7458-458b-8af0-730bb2cec7d6)


1-Parallel backup

The backup work will be done in parallel (abandonment of the sequential mode).

2-Real-time interaction with each job or all of the jobs

For each backup job (or all jobs) , the user is now able to :

-Pause (effective pause after transferring the current file)

-Resume a paused task 

-Cancel the current task

3-Addition of a progress bar of the current backup task.

4-Temporary pause if the app detects a specified process.

Example: If the calculator application is started, all the tasks must be paused.

5-Remote console (graphical interface in WPF)

Design mode: WPF and FrameWork .NetCore

Communication via Sockets.

6-The app is now single instance (can only be executed once)

7-EasySave now offers Two languages (Eng/Fr).

## CryptoSoft
![image](https://github.com/jalilhadjhabib/EasySave/assets/101253359/f9c37155-2dd0-4cd2-ae33-971a9e608e8f)


## How to use it 	
1. Clone the Project in "C:\" 
2. Start the .exe.
3. Choose new save.
4. Enter a name for your save.
5. Entre source director
6. Enter destination directory.
7. Choose a Save type (Full or Differential).

# More info 
For more info about your saves go to :
"\bin\Debug\netcoreapp3.1\Logs" this file contains all the logs of your saves.
