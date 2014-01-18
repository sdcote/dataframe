DataFrame
=========

A data marshaling toolkit.

Data Frame is a compact, efficient, hierarchical, self-describing and utilitarian data format with the ability to marshal other formats.

This toolkit was conceived in 2002 to implement the Data Transfer Object (DTO) design pattern in distributed applications; passing a DataFrame as both argument and return values in remote service calls. Using this implementation of a DTO allowed for more efficient transfer of data between distributed components, reducing latency, improving throughput and decoupling not only the components of the system, but moving business logic out of the data model.

DataFrame uses a binary wire format which is more efficient to parse and transfer. Marshaling occurs only when the data is accessed so unnecessary conversions are avoided.
