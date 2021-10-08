[![Build Status](https://travis-ci.org/sdcote/dataframe.svg?branch=master)](https://travis-ci.org/sdcote/dataframe)
[![codecov](https://codecov.io/gh/sdcote/dataframe/branch/master/graph/badge.svg)](https://codecov.io/gh/sdcote/dataframe)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/7b5666be8939409faa521ea5fc003e2b)](https://www.codacy.com/app/sdcote/dataframe?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=sdcote/dataframe&amp;utm_campaign=Badge_Grade)

DataFrame
=========

A data marshaling toolkit.

Data Frame is a compact, efficient, hierarchical, self-describing and utilitarian data format with the ability to marshal other formats.

This toolkit was conceived in 2002 to implement the Data Transfer Object (DTO) design pattern in distributed applications; passing a DataFrame as both argument and return values in remote service calls. Using this implementation of a DTO allowed for more efficient transfer of data between distributed components, reducing latency, improving throughput and decoupling not only the components of the system, but moving business logic out of the data model.

A DataFrame can also be used as a Value Object design pattern implementation, providing access to data without carrying any business logic. Value Objects tend to make service interfaces simpler and can many times reduce the number of calls to remote services through the population of the entire data model thereby avoiding multiple calls to the service. Value Objects can also be used to reduce the number of parameters on service methods.

DataFrame uses a binary wire format which is more efficient to parse and transfer. Marshaling occurs only when the data is accessed so unnecessary conversions are avoided.


Prerequisites:
--------
  * JDK 1.8 or later installed
  * Ability to run bash scripts
  * Assumes you do not have gradle installed (if you do, you can replace gradlew with gradle)
