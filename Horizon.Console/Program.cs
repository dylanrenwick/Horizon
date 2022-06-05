using Horizon;
using Horizon.Logging;

var log = new Logger(new ConsoleLogDestination(LogLevel.Debug));

var compiler = new HorizonCompiler(log);

string source = "123\n456 789.34\n\"Hello, World!\"\n17 + 4.3 * .5";

compiler.Compile(source);
