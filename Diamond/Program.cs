// See https://aka.ms/new-console-template for more information

using System.Text;
using DiamondCreatorLib;
using DiamondCreatorLib.Interfaces;
using DiamondCreatorLib.Utils;
using Microsoft.Extensions.DependencyInjection;

string EnglishAlphabetUppercase = DiamondCreatorLib.Properties.Resources.ENGLISH_ALPHABET_UPPERCASE;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IDiamondCreatorService, DiamondCreatorService>()
    .BuildServiceProvider();

try
{
    StringBuilder sbError = new StringBuilder(Environment.NewLine);

    if (args.Length.Equals(1))
    {
        if (args[0].Length.Equals(1))
        {
            var diamondCreatorService = serviceProvider.GetService<IDiamondCreatorService>();

            if (diamondCreatorService is null)
            {
                sbError.AppendLine("Failed to create a service.");
                throw new DiamondCreatorException(sbError.ToString());
            }

            diamondCreatorService.DefineAlphabet(EnglishAlphabetUppercase.First(), EnglishAlphabetUppercase.Last());

            sbError.AppendLine($"The current Alphabet is: '{diamondCreatorService.Alphabet}'");

            Console.Write(diamondCreatorService.CreateDiamond(args[0].ToUpperInvariant()[0]));
            Console.ReadKey();
        }
        else
        {
            sbError.AppendLine($"Argument passed: '{args[0]}'.");
            throw new DiamondCreatorException(sbError.ToString());
        }
    }
    else
    {
        sbError.AppendLine($"{args.Length} arguments were passed.");
        throw new DiamondCreatorException(sbError.ToString());
    }
}
catch (DiamondCreatorException dce)
{
    Console.WriteLine(DiamondCreatorLib.Properties.Resources.DIAMOND_CREATION_FAILED, dce.Message);
}
catch (Exception ex)
{
    Console.WriteLine(DiamondCreatorLib.Properties.Resources.DIAMOND_CREATION_FAILED, ex.Message);
}
