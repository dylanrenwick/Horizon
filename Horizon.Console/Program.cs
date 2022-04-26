using Horizon;
using Horizon.Logging;

var log = new Logger(new ConsoleLogDestination(LogLevel.Debug));

var compiler = new HorizonCompiler(log);

string source = @"123
456 789.34

17 + 4.3 * .5";

compiler.Compile(source);
