using Identicons;
using System.CommandLine;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var stringOption = new Option<string>(
            name: "--string",
            description: "Input string used to generate an identicon. Required.")
        { IsRequired = true };
        var outputFileOption = new Option<string>(
            name: "--output",
            description: "Output file to write. Can omit extension (and it will be altered if given but incorrect). Required.")
        { IsRequired = true };

        var command = new RootCommand("Identicon Generator");
        command.AddOption(stringOption);
        command.AddOption(outputFileOption);

        command.SetHandler(GenerateIcon, stringOption, outputFileOption);

        return await command.InvokeAsync(args);
    }

    static async Task GenerateIcon(string str, string outputFileName)
    {
        var outputPath = Path.ChangeExtension(outputFileName, "png");

        using (var outputFile = File.OpenWrite(outputPath))
        {
            await RootGenerator.Generate(str, outputFile);
        }
    }
}