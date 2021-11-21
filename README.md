# Gentrack - Technical Test

## Installation
* Clone repository
* Open in visual studio

## Usage
Implemented as a library. Call XmlToCsvParser.Parse() or run from unit tests.

## What was done
* Created the XmlToCsvParser library which:
  * Loads xml files and get the data from CsvIntervalData part
  * Validates the structure of the csv data and if it meets the use cases
  * Splits csv data into different blocks for each generated csv file
  * Exports csv files
* Created the automated tests for each part of the library
* Created test xml files for failing test cases

## What wasn't done
* Performance of the validator isn't optimised (it will run through all the rows every time)
* Not validating that the XML is valid

## What would be done with more time
* Return all error messages from validator, rather than just the first one
* Fill out more test cases with invalid data
