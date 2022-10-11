# Welcome to the Itemis.SalesTaxes!
## Coding challenge for Itemis. Problem 1: SALES TAXES. Vyacheslav Shengur's solution.

In general, I would like to talk about the solution. When solving the problem, I adhered to the principles of SOLID, so that the code was clean, easy to read, scalable. After reviewing the task, I decomposed it into several main subtasks:
1) The problem of product classification - how and to what category the product can be attributed according to the entered name.
2) How to correlate this or that type of tax with the category of goods.
3) How to aggregate all the received information, transform it into a supported and extensible class for other types of goods and services.

For each problem, I applied the following solution:
1) I solved the problem using the strategy pattern, so that in the future you can easily add a new strategy without affecting the current implementation.
2) This problem was solved using the bridge pattern so that two independent class hierarchies can be connected.
3) I solved this problem using the facade pattern, that is, the invoice acts as a decal for performing calculations with products, displaying the result.

To start app you can use one of two ways:
* Download binaries of version 1.0.
* Download source code and run solution locally.

To use the first method:
1) Follow the [link](https://github.com/GoodManApps/Itemis.SalesTaxes/releases/tag/v1.0.0), this is a release v1.0.0.
2) Download the archive named Itemis.SalesTaxes.zip.
3) After save to any convenient location, go to the root and run the Itemis.SalesTaxes.exe file.
4) After that, instructions for working with the program will appear on the screen.

To use the second method:
1) make a repository fork and open the project in the project root.
2) Before working, make sure that the device has .NET 6 installed. This can be checked by running the command `dotnet --version`.
3) If the command is not found, please install SDK. Instructions can be found at https://learn.microsoft.com/en-us/dotnet/core/install/. Here are comprehensive instructions for all popular operating systems.
4) After installation, repeat step 2. If unsuccessful, return to step 3 and double-check your actions. Otherwise, you can go to step 5.
5) To run the project, run `dotnet run --project .\Itemis.SalesTaxes\`.
6) After that, instructions for working with the program will appear on the screen.
5) To run the tests, run the command `dotnet test .\Itemis.SalesTaxes.UnitTests\`. This will make sure that most of the functions of the solution work correctly.
