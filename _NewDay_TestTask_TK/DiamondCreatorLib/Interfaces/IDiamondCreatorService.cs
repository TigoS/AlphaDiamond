using DiamondCreatorLib.Utils;

namespace DiamondCreatorLib.Interfaces;

public interface IDiamondCreatorService
{
    /// <summary>
    ///     Gets the Alphabet (ordered set of symbols) to create a Diamonds from.
    /// </summary>
    /// <remarks>
    ///     By default the English alphabet (uppercase) should be used.
    /// </remarks>
    string Alphabet { get; }

    /// <summary>
    ///     Gets the target letter based on which the Diamond should be created.
    /// </summary>
    char TargetLetter { get; }

    /// <summary>
    ///     Gets or sets the whitespace visualizer symbol.
    /// </summary>
    /// <remarks>
    ///     By default the UNDERSCORE ( _ ) symbol should be used.
    /// </remarks>
    char WhitespaceVisualizer { get; set; }

    /// <summary>
    ///     Defines an Alphabet by given first and last symbols, based on which the Diamonds should be created.
    /// </summary>
    void DefineAlphabet(char firstSymbol, char lastSymbol);

    /// <summary>
    ///     Defines an Alphabet by given string, based on which the Diamonds should be created.
    /// </summary>
    /// <param name="alphabet"></param>
    void DefineAlphabet(string alphabet);

    /// <summary>
    ///     Creates a Diamond by given <paramref name="targetLetter"/>, based on the Alphabet.
    /// </summary>
    /// <param name="targetLetter">
    ///     The letter to create a Diamond by.
    /// </param>
    /// <returns>
    ///      A newly created Diamond by given <paramref name="targetLetter"/>, based on the Alphabet.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="targetLetter"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref = "DiamondCreatorException" >
    ///     <paramref name="targetLetter"/> is invalid (out of Alphabet).
    /// </exception>
    string CreateDiamond(char targetLetter);
}
