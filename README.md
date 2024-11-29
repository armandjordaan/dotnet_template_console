# ConsoleTemplate README

## Overview

`ConsoleTemplate` is a C# console application template designed to provide structured logging, configuration management, and command-line argument parsing. This project utilizes:

- **NLog** for logging
- **Newtonsoft.Json** for configuration handling
- **CommandLineParser** for parsing command-line options

The application demonstrates essential practices for building a maintainable and robust console application.

---

## Features

- **Verbose Logging:** Toggle verbose logs at runtime using a command-line option.
- **Configurable Settings:** Load and save configuration settings via JSON files.
- **Command-Line Arguments:** Pass options and arguments to customize the program behavior.
- **Logging Levels:** Demonstrates all logging levels, from `Trace` to `Fatal`.

---

## Requirements

- .NET SDK
- NuGet Packages:
  - [NLog](https://www.nuget.org/packages/NLog/)
  - [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
  - [CommandLineParser](https://www.nuget.org/packages/CommandLineParser/)

---

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/<your-repo>/ConsoleTemplate.git
   cd ConsoleTemplate
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Build the application:

   ```bash
   dotnet build
   ```

---

## Usage

Run the application with the following syntax:

```bash
dotnet run -- [options]
```

### Command-Line Options

| Option       | Alias | Description                                                    | Default Value      |
|--------------|-------|----------------------------------------------------------------|--------------------|
| `--verbose`  | `-v`  | Enable verbose output (logs all levels).                      | `false`            |
| `--config`   | `-c`  | Specify the path to the configuration file.                   | `default.config`   |
| `--saveconfig` |       | Save the default configuration to `./default.config.save`.    | `false`            |

### Example Usage

- Enable verbose logging:

  ```bash
  dotnet run -- --verbose
  ```

- Use a custom configuration file:

  ```bash
  dotnet run -- --config custom.config
  ```

- Save the default configuration:

  ```bash
  dotnet run -- --saveconfig
  ```

---

## Configuration

The configuration file is stored in JSON format. You can define application-specific settings in a `default.config` or a custom file. The default configuration path is `./default.config`.

To save the current configuration to a default location, use the `--saveconfig` option.

---

## Logging

The application uses **NLog** for logging. Log levels include:

- `Trace`
- `Debug`
- `Info`
- `Warn`
- `Error`
- `Fatal`

Logs are configured dynamically. Verbose logging can be toggled via the `--verbose` option. 

---

## Code Structure

- **`Options` Class:** Defines the command-line arguments.
- **`Config` Class:** Handles application configuration (extend as needed).
- **Logging Configuration:** Dynamically enables or disables logging levels.
- **`Main` Method:** Entry point for the application.

### Key Methods

- `EnableAllForAllRules()`: Enables all logging levels.
- `LoadConfig()`: Loads the configuration file.
- `Run()`: Main execution flow.
- `HandleError()`: Handles errors during command-line parsing.

---

## Customization

1. **Extend Configuration:**
   Add new fields to the `Config` class and update the default configuration file.

2. **Add Application Logic:**
   Place your application-specific logic in the `Run()` method.

3. **Modify Logging Rules:**
   Update `NLog.config` or modify the `EnableLevelForAllRules()` method for custom logging behavior.

---

## Example Output

When running the application, it will output logs at different levels. For example:

```text
INFO  - Loading config file ./default.config
WARN  - Config file ./default.config does not exist. Using default config.
TRACE - Hello trace World!
DEBUG - Hello debug World!
INFO  - Hello info World!
WARN  - Hello warn World!
ERROR - Hello error World!
FATAL - Hello fatal World!
```

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your enhancements or fixes.
