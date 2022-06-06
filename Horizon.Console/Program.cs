using Horizon;
using Horizon.Logging;

var log = new Logger(new ConsoleLogDestination(LogLevel.Debug));

var compiler = new HorizonCompiler(log);

string source = File.ReadAllText("testSource.hz");

compiler.Compile(source);
