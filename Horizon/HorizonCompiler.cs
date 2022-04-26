using Horizon.Logging;
using Horizon.Tokenizing;

namespace Horizon;

public class HorizonCompiler
{
    private readonly Logger log;

    private readonly Tokenizer tokenizer;

    public HorizonCompiler(Logger logger)
    {
        log = logger;
        tokenizer = new Tokenizer(log.Label("TKN"));
    }

    public void Compile(string sourceCode)
    {
        log.Info("Beginning Tokenization");
        var tokens = tokenizer.Tokenize(sourceCode);
        log.Info($"Loaded {tokens.Count()} tokens");
    }
}