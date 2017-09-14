# Error Log Emailer

Given the high frequence of exceptions thrown by the variety of applications, a way to notify the CTO about exceptions thrown.

The network operations team need a quick way to send a log file containing exceptions to the email inbox of the CTO.

## Changeset #1

* Network operations team would love to use an API to send files from scripts without using the console application.
* Besides raw text, the library needs to handle and prettify XML files before sending them.