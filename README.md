DataFrame
=========

A data marshaling toolkit.

Data Frame is a compact, efficient, hierarchical, self-describing and utilitarian data format with the ability to marshal other formats.

This toolkit was conceived in 2002 to implement the Data Transfer Object (DTO) design pattern in distributed applications; passing a DataFrame as both argument and return values in remote service calls. Using this implementation of a DTO allowed for more efficient transfer of data between distributed components, reducing latency, improving throughput and decoupling not only the components of the system, but moving business logic out of the data model.

DataFrame uses a binary wire format which is more efficient to parse and transfer. Marshaling occurs only when the data is accessed so unnecessary conversions are avoided.


Prerequisites:
--------
  * JDK 1.6 or later installed
  * Ability to run bash scripts
  * Assumes you do not have gradle installed (if you do, you can replace gradlew with gradle)


Initial Build
--------
After checking out the code:
  * sh build.sh (built.bat for Windows)
  This will compile the system, run the automated tests and report success or failure.


Subsequent Builds
--------
  You can continue to use the same build command, or you can use gradlew directly:
  * sh gradlew build (gradlew.bat for Windows)


Working with Eclipse
--------
  If you want to work in eclipse, you can issue the following command:
  * gradlew eclipse

  This creates the /project/ files necessary to work on the code in Eclipse. Note, this does
  not include the files necessary for a workspace. To open this project in Eclipse, perform,
  the following:
  * Start eclipse
  * Select the parent directory of this directory as the workspace directory
  * Import existing projects into eclipse

Working with IntelliJ
--------
  If you want to work in IntelliJ, you can issue the following command:
  * gradlew idea
  * Open this directory as the project directory in IntelliJ

