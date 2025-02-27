# MakeGiantTXTs

## Overview
The `MakeGiantTXTs` program is designed to generate large text files for testing purposes. It allows users to specify the size and content of the text files to be created.

## How to Generate a Text File
To generate a text file with the program, you can use the following command in your terminal:

- `MakeGiantTXTs <content|-random> <desired size> [file name]`

Here is an explanation of the command parameters:

- `<content>`: The text that will be repeated to fill the file.
- `-random`: A flag indicating that the content should be random characters.
- `<desired size>`: The desired size of the file in bytes. You can use the following suffixes to specify the size:
  - `B`: Bytes (1024 bytes)
  - `K`: Kilobytes (1024 kilobytes)
  - `M`: Megabytes (1024 megabytes)
  - `G`: Gigabytes (1024 gigabytes)
  - `T`: Terabytes (1024 terabytes)
- `[file name]`: (Optional) The name of the file to be created. If this parameter is not provided, a default name will be used.

### Example Usage

To create a 1 MB file with the content "test" and the name "output.txt", you can run the following commands:

- `MakeGiantTXTs test 1M output.txt`
- `MakeGiantTXTs -random 1M output.txt`
- `MakeGiantTXTs -random 1M`
- `MakeGiantTXTs test 1M`
- `MakeGiantTXTs test 1000000 output.txt`

## How to Compile the Program
To compile the program and create a publishable package, you can use the provided `publish.ps1` PowerShell script. Follow these steps:

1. Open a PowerShell terminal.
2. Navigate to the directory containing the `publish.ps1` script.
3. Execute the script by running:
4. The script will compile the project and create a publishable package in the `/publish/<platform>` directory.

Make sure you have the .NET 6 SDK installed on your machine before running the script.
