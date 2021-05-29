[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_nanoFramework.Logging&metric=alert_status)](https://sonarcloud.io/dashboard?id=nanoframework_nanoFramework.Logging) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_nanoFramework.Logging&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=nanoframework_nanoFramework.Logging) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/nanoFramework.Logging.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Logging/) [![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md) [![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)

![nanoFramework logo](https://github.com/nanoframework/Home/blob/main/resources/logo/nanoFramework-repo-logo.png)

-----

### Welcome to the **nanoFramework** nanoFramework.Logging Library repository!

## Build status

| Component | Build Status | NuGet Package |
|:-|---|---|
| nanoFramework.Logging | [![Build Status](https://dev.azure.com/nanoframework/nanoframework.Logging/_apis/build/status/nanoframework.nanoFramework.Logging?branchName=main)](https://dev.azure.com/nanoframework/nanoframework.Logging/_build/latest?definitionId=71&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.Logging.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Logging/) |
| nanoFramework.Logging (preview) | [![Build Status](https://dev.azure.com/nanoframework/nanoframework.Logging/_apis/build/status/nanoframework.nanoFramework.Logging?branchName=develop)](https://dev.azure.com/nanoframework/nanoframework.Logging/_build/latest?definitionId=71&branchName=develop) | [![NuGet](https://img.shields.io/nuget/vpre/nanoFramework.Logging.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Logging/) |
| nanoFramework.Logging.Serial | [![Build Status](https://dev.azure.com/nanoframework/nanoframework.Logging/_apis/build/status/nanoframework.nanoFramework.Logging?branchName=main)](https://dev.azure.com/nanoframework/nanoframework.Logging/_build/latest?definitionId=71&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.Logging.Serial.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Logging.Serial/) |
| nanoFramework.Logging.Serial (preview) | [![Build Status](https://dev.azure.com/nanoframework/nanoframework.Logging/_apis/build/status/nanoframework.nanoFramework.Logging?branchName=develop)](https://dev.azure.com/nanoframework/nanoframework.Logging/_build/latest?definitionId=71&branchName=develop) | [![NuGet](https://img.shields.io/nuget/vpre/nanoFramework.Logging.Serial.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Logging.Serial/) |
| nanoFramework.Logging.Stream | [![Build Status](https://dev.azure.com/nanoframework/nanoframework.Logging/_apis/build/status/nanoframework.nanoFramework.Logging?branchName=main)](https://dev.azure.com/nanoframework/nanoframework.Logging/_build/latest?definitionId=71&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.Logging.Stream.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Logging.Stream/) |
| nanoFramework.Logging.Stream (preview) | [![Build Status](https://dev.azure.com/nanoframework/nanoframework.Logging/_apis/build/status/nanoframework.nanoFramework.Logging?branchName=develop)](https://dev.azure.com/nanoframework/nanoframework.Logging/_build/latest?definitionId=71&branchName=develop) | [![NuGet](https://img.shields.io/nuget/vpre/nanoFramework.Logging.Stream.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Logging.Stream/) |


## Feedback and documentation

For documentation, providing feedback, issues and finding out how to contribute please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The **nanoFramework** Class Libraries are licensed under the [MIT license](LICENSE.md).

## Usage

In your class, make sure you have a global ILogger declared and in your constructor that you call `_logger = this.GetCurrentClassLogger();`

```csharp
using Microsoft.Extensions.Logging;
using nanoFramework.Logging;
using System;

namespace UnitTestDebugLogging
{
    internal class MyTestComponent
    {
        private ILogger _logger;

        public MyTestComponent()
        {
            _logger = this.GetCurrentClassLogger();
        }

        public void DoSomeLogging()
        {
            _logger.LogInformation("An informative message");
            _logger.LogError("An error situation");
            _logger.LogWarning(new Exception("Something is not supported"), "With exception context");
        }
    }
}
```

In your main code, you'll need to create a logger:

```csharp
LogDispatcher.LoggerFactory = new DebugLoggerFactory();
// Then you can create your object and the logging will happen
MyTestComponent test = new MyTestComponent();
test.DoSomeLogging();
```

You can have 3 different types of logger: Debug, Serial and Stream.

### Debug logger

As presented previously, you can use the Factory pattern:

```csharp
LogDispatcher.LoggerFactory = new DebugLoggerFactory();
// Then you can create your object and the logging will happen
MyTestComponent test = new MyTestComponent();
test.DoSomeLogging();
```

You can as well directly create a DebugLogger:

```csharp
DebugLogger _logger;
_logger = new DebugLogger("test");
_logger.MinLogLevel = LogLevel.Trace; 
_logger.LogTrace("This is a trace");
```

### Serial logger

You can use the Factory pattern:

```csharp
LogDispatcher.LoggerFactory = new SerialLoggerFactory("COM6");
// Then you can create your object and the logging will happen
MyTestComponent test = new MyTestComponent();
test.DoSomeLogging();
```

Note that you can adjust the baud speed and all other elements.

Or directly using a SerialLogger:

```csharp
SerialPort _serial;
_serial = new SerialPort("COM6", 115200);
SerialLogger _logger = new SerialLogger(ref _serial);
_logger.MinLogLevel = LogLevel.Trace; 
_logger.LogTrace("This is a trace");
```

**Important**: make sure to refer to the documentation of your board to understand how to properly setup the serial port. The tests include an example with an ESP32.

### Stream logger

Similar as for the others, you can either use a FileStream or a Stream in the LoggerFactory:

```csharp
MemoryStream memoryStream = new MemoryStream();
LogDispatcher.LoggerFactory = new StreamLoggerFactory(memoryStream);
MyTestComponent test = new MyTestComponent();
test.DoSomeLogging();
```

And you can as well use it directly:

```csharp
var _stream = new FileStream("D:\\mylog.txt", FileMode.Open, FileAccess.ReadWrite);
StreamLogger _logger = new StreamLogger(_stream);
_logger.MinLogLevel = LogLevel.Trace; 
_logger.LogTrace("This is a trace");
```

**Important**: please refer to the documentation for USB and SD Card reader to make sure they are properly setup before trying to setup the logger.

### Create your own logger

You can create your own logger using the ILogger and ILoggerFactory interfaces. The DebugLogger is the simplest one.

### The Log extensions

Different Log extensions are at your disposal to help you log the way you like. You can simply log a string or having parameters as well as exception and EventId:

```csharp
_logger.LogTrace("TRACE {0} {1}", new object[] { "param 1", 42 });
_logger.LogDebug("DEBUG {0} {1}", new object[] { "param 1", 42 });
_logger.LogInformation("INFORMATION and nothing else");
_logger.LogWarning("WARNING {0} {1}", new object[] { "param 1", 42 });
_logger.LogError(new Exception("Big problem"), "ERROR {0} {1}", new object[] { "param 1", 42 });
_logger.LogCritical(42, new Exception("Insane problem"), "CRITICAL {0} {1}", new object[] { "param 1", 42 });
```

Note that all log level extensions have a minimum of string logging upo to EventId, string, parameters and exception. You are responsible to format properly the string when there are parameters.

### Log level

You can adjust the log level in all the predefined logger. For example:

```csharp
DebugLogger _logger;
_logger = new DebugLogger("test");
_logger.MinLogLevel = LogLevel.Trace;
_logger.LogTrace("This will be displayed");
_logger.LogCritical("Critical message will be displayed");
_logger.MinLogLevel = LogLevel.Critical;
_logger.LogTrace("This won't be displayed, only critical will be");
_logger.LogCritical("Critical message will be displayed");
```

### Create your own formatting

You can use a custom formatter which will give you the name of the logger, the log level, the event ID, the message itself and a potential exception. The function definition should follow the following pattern:

```csharp
public interface IMessageFormatter
{     
    string MessageFormatter(string className, LogLevel logLevel, EventId eventId, string state, Exception exception);
}
```

**Important**: this function will be called directly, without instantiating the class it is part of. So make sure either this function is a static, either it's part of the class using the logger. The static option always works. The interface is given for convenience and to give the format.

To setup the formatting, just use the following line. The type of the class containing the function and the exact name of the function are required.

```csharp
LoggerExtensions.MessageFormatter = typeof(MyFormatter).GetType().GetMethod("MessageFormatterStatic");

public class MyFormatter
{        
    public string MessageFormatterStatic(string className, LogLevel logLevel, EventId eventId, string state, Exception exception)
    {
        string logstr = string.Empty;
        switch (logLevel)
        {
            case LogLevel.Trace:
                logstr = "TRACE: ";
                break;
            case LogLevel.Debug:
                logstr = "I love debug: ";
                break;
            case LogLevel.Warning:
                logstr = "WARNING: ";
                break;
            case LogLevel.Error:
                logstr = "ERROR: ";
                break;
            case LogLevel.Critical:
                logstr = "CRITICAL:";
                break;
            case LogLevel.None:
            case LogLevel.Information:
            default:
                break;
        }

        string eventstr = eventId.Id != 0 ? $" Event ID: {eventId}, " : string.Empty;
        string msg = $"[{className}] {eventstr}{logstr} {state}";
        if (exception != null)
        {
            msg += $" {exception}";
        }

        return msg;
    }
}
```

You are free to use anything you'd like and format as you like the message.

Note: It is **not** necessary to add a \r\n at the end, this is done by each logger.

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

### .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
