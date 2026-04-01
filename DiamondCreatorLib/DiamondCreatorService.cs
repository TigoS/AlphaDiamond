using System.Text;
using DiamondCreatorLib.Interfaces;
using DiamondCreatorLib.Utils;

namespace DiamondCreatorLib;

/// <summary>
///     Implementation of the <see cref="IDiamondCreatorService"/> interface.
/// </summary>
public class DiamondCreatorService : IDiamondCreatorService
{
    /// <inheritdoc />
    public string Alphabet { get; private set; } = Properties.Resources.ENGLISH_ALPHABET_UPPERCASE;

    /// <inheritdoc />
    public char TargetLetter { get; private set; }

    /// <inheritdoc />
    public char WhitespaceVisualizer { get; set; } = '_';

    /// <inheritdoc />
    public void DefineAlphabet(char firstSymbol, char lastSymbol)
    {
        if (firstSymbol > lastSymbol)
        {
            throw new DiamondCreatorException($"\tCannot define Alphabet by given parameters: the ASCII/ANSI code of the first symbol '{firstSymbol} ({(int)firstSymbol})' should be less than the last symbol '{lastSymbol} ({(int)lastSymbol})'.");
        }

        StringBuilder sb = new StringBuilder();
        for (int i = firstSymbol; i <= lastSymbol; i++)
        {
            sb.Append((char)i);
        }

        Alphabet = sb.ToString();
    }

    /// <inheritdoc />
    public void DefineAlphabet(string alphabet)
    {
        if (string.IsNullOrWhiteSpace(alphabet))
        {
            throw new DiamondCreatorException("Alphabet should be a valid non-empty string expression.",
                new ArgumentNullException(nameof(alphabet)));
        }
        
        Alphabet = new string(alphabet.ToCharArray().Distinct().Where(w => !char.IsWhiteSpace(w)).OrderBy(o => o).ToArray());
    }

    /// <inheritdoc />
    public string CreateDiamond(char targetLetter)
    {
        TargetLetter = targetLetter;

        if (Alphabet is null)
        {
            throw new DiamondCreatorException("\tAlphabet is not defined.");
        }

        if (TargetLetter >= Alphabet.First() && TargetLetter <= Alphabet.Last())
        {
            int matrixNumber = GetMatrixNumber();
            StringBuilder sb = new StringBuilder();

            int letterIndex = 0;
            for (int row = 1; row <= matrixNumber; row++)
            {
                sb.AppendLine(CreateRow(letterIndex, matrixNumber));

                letterIndex += row <= matrixNumber / 2 ? 1 : -1;
            }

            return sb.ToString();
        }
        else
        {
            throw new DiamondCreatorException($"{Environment.NewLine}\tLetter passed: '{TargetLetter}'{Environment.NewLine}\tAlphabet: '{Alphabet}'");
        }
    }

    /// <summary>
    ///     Calculates and returns the Matrix number - the rows/columns count depending on the position of the Target letter in Alphabet.
    /// </summary>
    /// <returns>
    ///     The Matrix number - the rows/columns count depending on the position of the Target letter in Alphabet.
    /// </returns>
    /// <exception cref = "DiamondCreatorException" >
    ///     The Target letter is invalid (out of Alphabet).
    /// </exception>
    private int GetMatrixNumber()
    {
        return Alphabet.Contains(TargetLetter) ? (Alphabet.IndexOf(TargetLetter) + 1) * 2 - 1 : throw new DiamondCreatorException($"Invalid letter '{TargetLetter}' within the Alphabet '{Alphabet}'");
    }

    /// <summary>
    ///     Returns a single formatted row by the given letter index.
    /// </summary>
    /// <param name="letterIndex">
    ///     Index of the letter in Alphabet to create a formatted row for.
    /// </param>
    /// <param name="matrixNumber">
    ///     The Matrix number - the rows/columns count depending on the position of the Target letter in Alphabet.
    /// </param>
    /// <returns>
    ///     A single formatted row by the given letter index.
    /// </returns>
    private string CreateRow(int letterIndex, int matrixNumber)
    {
        char letter = Alphabet[letterIndex];
        int leadingWhitSpacesCount, inBetweenWhiteSpacesCount;
        string leadingWhitSpaces, inBetweenWhiteSpaces;
        string row;

        if (Alphabet.First().Equals(letter)) // The given letter is the first letter of the Alphabet, so should appear in vertical vertices of the Diamond
        {
            leadingWhitSpacesCount = (matrixNumber - 1) / 2;
            leadingWhitSpaces = new(WhitespaceVisualizer, leadingWhitSpacesCount);

            row = $"{leadingWhitSpaces}{letter}{leadingWhitSpaces}";
        }
        else if (TargetLetter.Equals(letter)) // The given letter is the target letter, so should appear in the horizontal vertices of the Diamond
        {
            inBetweenWhiteSpacesCount = matrixNumber - 2;
            inBetweenWhiteSpaces = new(WhitespaceVisualizer, inBetweenWhiteSpacesCount);

            row = $"{letter}{inBetweenWhiteSpaces}{letter}";
        }
        else // The given letter is not the first nor target letter, so should appear twice in the row, but not in horizontal vertices of the Diamond
        {
            leadingWhitSpacesCount = (matrixNumber - 2 * letterIndex) / 2;
            leadingWhitSpaces = new(WhitespaceVisualizer, leadingWhitSpacesCount);
            inBetweenWhiteSpacesCount = matrixNumber - 2 * (leadingWhitSpacesCount + 1);
            inBetweenWhiteSpaces = new(WhitespaceVisualizer, inBetweenWhiteSpacesCount);

            row = $"{leadingWhitSpaces}{letter}{inBetweenWhiteSpaces}{letter}{leadingWhitSpaces}";
        }

        return string.Join(' ', row.ToCharArray());
    }
}
