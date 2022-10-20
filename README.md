[![Build Status](https://travis-ci.org/sdcote/dataframe.svg?branch=master)](https://travis-ci.org/sdcote/dataframe)
[![codecov](https://codecov.io/gh/sdcote/dataframe/branch/master/graph/badge.svg)](https://codecov.io/gh/sdcote/dataframe)

# DataFrame

A data marshaling toolkit.

Data Frame is a compact, efficient, hierarchical, self-describing and utilitarian data format with the ability to marshal other formats.

This toolkit was conceived in 2002 to implement the Data Transfer Object (DTO) design pattern in distributed applications; passing a DataFrame as both argument and return values in remote service calls. Using this implementation of a DTO allowed for more efficient transfer of data between distributed components, reducing latency, improving throughput and decoupling not only the components of the system, but moving business logic out of the data model.

A DataFrame can also be used as a Value Object design pattern implementation, providing access to data without carrying any business logic. Value Objects tend to make service interfaces simpler and can many times reduce the number of calls to remote services through the population of the entire data model thereby avoiding multiple calls to the service. Value Objects can also be used to reduce the number of parameters on service methods.

DataFrame uses a binary wire format which is more efficient to parse and transfer. Marshaling occurs only when the data is accessed so unnecessary conversions are avoided.

## Binary Format

One of the first purposes of the DataFrame was to represent objects on message oriented middleware and to send field telemetry as a small efficient message. These binary messages were sent more efficiently than XML.

A data frame is an abstract data type that represents itself in a fairly self-describing format where each attribute of the instance is named and typed in its native binary format. A data frame is an array of fields, each with a name, type, length, and value. 

The first octet is unsigned integer (0-255) indicating the length of the name of the field. If non-zero, the given number of octets are read and parsed into a UTF-8 string.

Next, another byte representing an unsigned integer (0-255) is read in and used to indicate the type of the field. If it is a numeric or other fixed type, the appropriate number of bytes are read in. If a variable type is indicated then the next U32 integer (4-bytes) is read as the length of the data. U32 is used to support nesting of frames within frames which can quickly exceed U16 values of 65535 bytes in length.

Normally, a data frame starts with a name, a type of "frame" (int 0) and a length indicating the size of the payload. The payload is then read in as an array of named Type-Length-Value (TLV) fields. Nesting is obviously supported, but there is a size limitation for the root frame; it must be smaller than 4.2 gigabytes (4,294,967,295 bytes). This is easy to avoid, by simply using multiple root frames as might be read in from a stream.

# Prerequisites:
  * JDK 1.8 or later installed
  * Ability to run bash scripts
  * Assumes you do not have gradle installed (if you do, you can replace gradlew with gradle)
  
# Building

```
gradlew clean build publish
```

## Versioning

This project has adopted [Semantic Versioning](https://semver.org/) of project artifacts. Given a version number MAJOR.MINOR.PATCH, this team will increment the:
                                                                                          
* **MAJOR** version when we make incompatible API changes,
* **MINOR** version when we add functionality in a backwards-compatible manner, and
* **PATCH** version when we make backwards-compatible bug fixes.

Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format. For example, this project has adopted the Maven SNAPSHOT and RELEASE extensions to assist with release management and artifact publication:

* **-SHAPSHOT** denotes a pre-release version of the artifact which may change over time. 
* **-RELEASE** denotes the final, official release which will never change.

**Snapshot builds** represent the current release candidate. It will change with each new build of the artifact daily, even hourly. Depending on where and when the artifact was retrieved it may or may not satisfy the intended compatibility requirements as denoted by its associated normal version. Snapshot builds should only be used during development as the contents will change frequently. For example:
* **Monday** 1.3.1-SNAPSHOT contains the `Log.dump(String)` method.
* **Tuesday** 1.3.1-SNAPSHOT contains the `Log.dump(Event)` method.
* **Wednesday** 1.3.1-SNAPSHOT has removed the `Log.dump(String)` method.

If you retrieved the 1.3.1-SNAPSHOT on Monday and started using the  `Log.dump(String)` method, your code will break on Wednesday when the latest snapshot is retrieved as it has been removed from the artifact. 

**Release builds** are static, they will not be updated. Once a RELEASE version is created, the project version is incremented and new SNAPSHOT releases will be built.

**Note:** If neither RELEASE nor SNAPSHOT are present in the version, it can be assumed the version refers to the final, static RELEASE version of that artifact.

Adopting the Maven SNAPSHOT and RELEASE designations allows the team to make better use of the Maven artifact repositories and dependency management. 

## Tagging

When a **Release build** is created, the versioning code is updated from MAJOR.MINOR.PATCH-SNAPSHOT to MAJOR.MINOR.PATCH-RELEASE, the source code is committed to the source code management system (GitHub) and the artifacts are built, tested and published to the corporate artifact repository (Artifactory).

After a successful delivery to the artifact repository and subsequent testing in all lower environments, the source code is tagged with the MAJOR.MINOR.PATCH of the release. (Note the "-RELEASE" is omitted.) The tag now allows the team to checkout the exact code used to create the released version at any point in the future.

Tagging is performed with a Git command similar to the following:

    $ git tag -a 1.3.1 -m "Support for AES 256 encryption"  

The commit editor will be opened, allowing the developer to add additional details (annotations) to the tag. When the editor is closed the release is tagged. The developer then pushes the tag to origin to save the tag in the source code management system.

Once the release has been tagged, the developer then increments the version of the project, commits the change, then pushes the change to origin.

All other branches currently opened should pull the new version from origin to utilize the new version number.

A tagging session after a successful release build might look like the following:

    $ git tag -a 1.3.1 -m "Support for AES 256 encryption"  
    $ git push origin --tags
    $ <increment to the next appropriate version>
    $ git add .
    $ git commit -m "Incremented version"
    $ git push origin 

For more details on tagging refer to [Git Tagging](https://git-scm.com/book/en/v2/Git-Basics-Tagging) in the Pro Git book.
 