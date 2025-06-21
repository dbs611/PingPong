#!/bin/bash

# Run tests and collect code coverage
dotnet test BreakoutTests --collect:"XPlat Code Coverage"

# Find the latest coverage result file
COVERAGE_FILE=$(find ./BreakoutTests/TestResults -name 'coverage.cobertura.xml' | sort | tail -n 1)

# Generate report
reportgenerator -reports:$COVERAGE_FILE -targetdir:coverage-report

# Open the report in your browser (macOS)
open coverage-report/index.html
