Overview:
=======================
  This is the DataFrame project for Java, a (Data Transmission Object (DTO) toolkit that 
  allows developers to marshal data formats between a variety of technologies
  (XML, JSON, et. el.) in a platform agnostic manner.

Prerequisites:
=======================
  * JDK 1.6 or later installed
  * Ability to run bash scripts
  * Assumes you do not have gradle installed (if you do, you can replace gradlew with gradle)

Initial Build
=======================
  After checking out the code:
  * sh build.sh (built.bat for Windows)
  This will compile the system, run the automated tests and report success or failure.

Subsequent Builds
=======================
  You can continue to use the same build command, or you can use gradlew directly:
  * sh gradlew build (gradlew.bat for Windows)

Running the Tests
=======================
  * gradlew test


Working with Eclipse
=======================
  If you want to work in eclipse, you can issue the following command:
  * gradlew eclipse

  This creates the /project/ files necessary to work on the code in Eclipse. Note, this does
  not include the files necessary for a workspace. To open this project in Eclipse, perform,
  the following:
  * Start eclipse
  * Select the parent directory of this directory as the workspace directory
  * Import existing projects into eclipse

Working with IntelliJ
=======================
  If you want to work in IntelliJ, you can issue the following command:
  * gradlew idea
  * Open this directory as the project directory in IntelliJ

