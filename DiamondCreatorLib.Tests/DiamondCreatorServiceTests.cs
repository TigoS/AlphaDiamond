using DiamondCreatorLib.Utils;
using NUnit.Framework;

namespace DiamondCreatorLib.Tests;

[TestFixture()]
public class DiamondCreatorServiceTests
{
    private DiamondCreatorService _sut = null!;

    [OneTimeSetUp()]
    public void Init()
    {
        // ARRANGE
        _sut = new DiamondCreatorService();
    }

    [SetUp]
    public void SetUp()
    {
        // ARRANGE
        _sut.DefineAlphabet('A', 'Z');
        _sut.WhitespaceVisualizer = '_';
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenUppercaseTheSameValuesA_ThenAlphabetShouldBeUppercaseSingleSymbolA()
    {
        // ACT
        _sut.DefineAlphabet('A', 'A');

        // ASSERT
        Assert.AreEqual("A", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenUppercaseRightOrderAZ_ThenAlphabetShouldBeUppercaseAZ()
    {
        // ACT
        _sut.DefineAlphabet('A', 'Z');

        // ASSERT
        Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenUppercaseFromTheMiddleBY_ThenAlphabetShouldBeUppercaseBY()
    {
        // ACT
        _sut.DefineAlphabet('B', 'Y');

        // ASSERT
        Assert.AreEqual("BCDEFGHIJKLMNOPQRSTUVWXY", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenUppercaseReverseOrderBA_ThenAlphabetShouldThrowException()
    {
        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.DefineAlphabet('B', 'A'));
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenLowercaseFromTheMiddleCG_ThenAlphabetShouldBeLowercaseCG()
    {
        // ACT
        _sut.DefineAlphabet('c', 'g');

        // ASSERT
        Assert.AreEqual("cdefg", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenLowercaseTheSameValuesDD_ThenAlphabetShouldBeLowercaseSingleSymbolD()
    {
        // ACT
        _sut.DefineAlphabet('d', 'd');

        // ASSERT
        Assert.AreEqual("d", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenLowercaseReverseOrderFC_ThenAlphabetShouldThrowException()
    {
        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.DefineAlphabet('f', 'c'));
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenLowercaseAUppercaseA_ThenAlphabetShouldThrowException()
    {
        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.DefineAlphabet('a', 'A'));
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenUppercaseALowercaseA_ThenAlphabetShouldReturnFromUppercaseAToLowercaseA()
    {
        // ACT
        _sut.DefineAlphabet('A', 'a');

        // ASSERT
        Assert.AreEqual(@"ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`a", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenNumbersRightOrder1_9_ThenAlphabetShouldReturnFrom1To9()
    {
        // ACT
        _sut.DefineAlphabet('1', '9');

        // ASSERT
        Assert.AreEqual("123456789", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenNumbersTheSameValues3_3_ThenAlphabetShouldReturnSingleSymbol3()
    {
        // ACT
        _sut.DefineAlphabet('3', '3');

        // ASSERT
        Assert.AreEqual("3", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByFirstAndLastLettersTest_GivenNumbersReverseOrder7_4_ThenAlphabetShouldThrowException()
    {
        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.DefineAlphabet('7', '4'));
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenUppercaseRightOrderAZ_ThenAlphabetShouldBeUppercaseAZ()
    {
        // ACT
        _sut.DefineAlphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        // ASSERT
        Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenUppercaseReverseOrderZA_ThenAlphabetShouldBeUppercaseAZ()
    {
        // ACT
        _sut.DefineAlphabet("ZYXWVUTSRQPONMLKJIHGFEDCBA");

        // ASSERT
        Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenUppercaseSingleCharacterA_ThenAlphabetShouldBeUppercaseA()
    {
        // ACT
        _sut.DefineAlphabet("A");

        // ASSERT
        Assert.AreEqual("A", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenLowercaseRightOrderDX_ThenAlphabetShouldBeLowercaseDX()
    {
        // ACT
        _sut.DefineAlphabet("defghijklmnopqrstuvwx");

        // ASSERT
        Assert.AreEqual("defghijklmnopqrstuvwx", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenRandomNonEmptyDistinctString_ThenAlphabetShouldBeValidAndOrdered()
    {
        // ACT
        _sut.DefineAlphabet(@"qWeRt12YuiOP[}\");

        // ASSERT
        Assert.AreEqual(@"12OPRWY[\eiqtu}", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenRandomNonEmptyStringWithDuplicates_ThenAlphabetShouldBeValidAndContainOnlyDistinctOrderedSymbols()
    {
        // ACT
        _sut.DefineAlphabet(@"qqWWWeRRRRRt112Yuu2uuiOP[O}OP\[qP");

        // ASSERT
        Assert.AreEqual(@"12OPRWY[\eiqtu}", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenRandomNonEmptyStringWithWhitespaces_ThenAlphabetShouldBeValidOrderedAndIgnoreWhitespaceSymbols()
    {
        // ACT
        _sut.DefineAlphabet("qWeRt12Y uiOP[}\\Q\t");

        // ASSERT
        Assert.AreEqual(@"12OPQRWY[\eiqtu}", _sut.Alphabet);
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenEmptyString_ThenAlphabetShouldThrowException()
    {
        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.DefineAlphabet(string.Empty));
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenNull_ThenAlphabetShouldThrowException()
    {
        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.DefineAlphabet(null!));
    }

    [Test()]
    public void DefineAlphabetByStringTest_GivenOnlyWhitespaceString_ThenAlphabetShouldThrowException()
    {
        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.DefineAlphabet($" \t \r\n {Environment.NewLine}"));
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterA_ShouldReturnTheCorrectDiamond()
    {
        // ACT
        var diamond = _sut.CreateDiamond('A');

        // ASSERT
        Assert.AreEqual("A" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterC_ShouldReturnTheCorrectDiamond()
    {
        // ACT
        var diamond = _sut.CreateDiamond('C');

        // ASSERT
        Assert.AreEqual("_ _ A _ _" + Environment.NewLine +
                        "_ B _ B _" + Environment.NewLine +
                        "C _ _ _ C" + Environment.NewLine +
                        "_ B _ B _" + Environment.NewLine +
                        "_ _ A _ _" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenCustomAlphabetLowercaseAZ_WhenCreatingForLetterB_ShouldReturnTheCorrectDiamond()
    {
        // ACT
        _sut.DefineAlphabet('a', 'z');
        var diamond = _sut.CreateDiamond('b');

        // ASSERT
        Assert.AreEqual("_ a _" + Environment.NewLine +
                        "b _ b" + Environment.NewLine +
                        "_ a _" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenCustomOrderedNumericAlphabet_WhenCreatingForNumber5_ShouldReturnTheCorrectDiamond()
    {
        // ACT
        _sut.DefineAlphabet('1', '9');
        var diamond = _sut.CreateDiamond('4');

        // ASSERT
        Assert.AreEqual("_ _ _ 1 _ _ _" + Environment.NewLine +
                        "_ _ 2 _ 2 _ _" + Environment.NewLine +
                        "_ 3 _ _ _ 3 _" + Environment.NewLine +
                        "4 _ _ _ _ _ 4" + Environment.NewLine +
                        "_ 3 _ _ _ 3 _" + Environment.NewLine +
                        "_ _ 2 _ 2 _ _" + Environment.NewLine +
                        "_ _ _ 1 _ _ _" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenCustomAlphabetReverseUppercaseZA_WhenCreatingForLetterB_ShouldReturnTheCorrectDiamond()
    {
        // ACT
        _sut.DefineAlphabet("ZYXWVUTSRQPONMLKJIHGFEDCBA");
        var diamond = _sut.CreateDiamond('B');
        
        // ASSERT
        Assert.AreEqual("_ A _" + Environment.NewLine +
                        "B _ B" + Environment.NewLine +
                        "_ A _" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenChangingTheWhitespaceVisualizerToAsterixAndCreatingForLetterB_ShouldReturnTheCorrectDiamond()
    {
        // ACT
        _sut.WhitespaceVisualizer = '*';
        var diamond = _sut.CreateDiamond('B');
        
        // ASSERT
        Assert.AreEqual("* A *" + Environment.NewLine +
                        "B * B" + Environment.NewLine +
                        "* A *" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenProvidingInvalidLetterLowercaseB_ShouldThrowException()
    {
        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.CreateDiamond('b'));
    }

    [Test()]
    public void CreateDiamondTest_GivenCustomAlphabetUppercaseA_WhenProvidingInvalidLetterUppercaseB_ShouldThrowException()
    {
        // ACT
        _sut.DefineAlphabet("A");

        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.CreateDiamond('B'));
    }

    #region Default State Tests

    [Test()]
    public void DefaultStateTest_NewInstance_AlphabetShouldBeUppercaseEnglishAZ()
    {
        // ARRANGE
        var service = new DiamondCreatorService();

        // ASSERT
        Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", service.Alphabet);
    }

    [Test()]
    public void DefaultStateTest_NewInstance_WhitespaceVisualizerShouldBeUnderscore()
    {
        // ARRANGE
        var service = new DiamondCreatorService();

        // ASSERT
        Assert.AreEqual('_', service.WhitespaceVisualizer);
    }

    [Test()]
    public void DefaultStateTest_NewInstance_TargetLetterShouldBeDefaultChar()
    {
        // ARRANGE
        var service = new DiamondCreatorService();

        // ASSERT
        Assert.AreEqual('\0', service.TargetLetter);
    }

    #endregion

    #region Additional CreateDiamond Tests

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterD_ShouldReturnTheCorrectDiamond()
    {
        // ACT
        var diamond = _sut.CreateDiamond('D');

        // ASSERT
        Assert.AreEqual("_ _ _ A _ _ _" + Environment.NewLine +
                        "_ _ B _ B _ _" + Environment.NewLine +
                        "_ C _ _ _ C _" + Environment.NewLine +
                        "D _ _ _ _ _ D" + Environment.NewLine +
                        "_ C _ _ _ C _" + Environment.NewLine +
                        "_ _ B _ B _ _" + Environment.NewLine +
                        "_ _ _ A _ _ _" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterD_ShouldReturnDiamondWith7Rows()
    {
        // ACT
        var diamond = _sut.CreateDiamond('D');
        var rows = diamond.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        // ASSERT
        Assert.AreEqual(7, rows.Length);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterZ_ShouldReturnDiamondWith51Rows()
    {
        // ACT
        var diamond = _sut.CreateDiamond('Z');
        var rows = diamond.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        // ASSERT
        Assert.AreEqual(51, rows.Length);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterD_FirstAndLastRowsShouldBeIdentical()
    {
        // ACT
        var diamond = _sut.CreateDiamond('D');
        var rows = diamond.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        // ASSERT
        Assert.AreEqual(rows[0], rows[rows.Length - 1]);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterE_DiamondShouldBeVerticallySymmetric()
    {
        // ACT
        var diamond = _sut.CreateDiamond('E');
        var rows = diamond.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        // ASSERT
        for (int i = 0; i < rows.Length; i++)
        {
            Assert.AreEqual(rows[i], rows[rows.Length - 1 - i]);
        }
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterD_WidestRowShouldContainTargetLetterAtBothEnds()
    {
        // ACT
        var diamond = _sut.CreateDiamond('D');
        var rows = diamond.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        int middleIndex = rows.Length / 2;

        // ASSERT
        Assert.That(rows[middleIndex], Does.StartWith("D"));
        Assert.That(rows[middleIndex], Does.EndWith("D"));
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabetUppercaseAZ_WhenCreatingForLetterD_AllRowsShouldHaveSameWidth()
    {
        // ACT
        var diamond = _sut.CreateDiamond('D');
        var rows = diamond.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        // ASSERT
        int expectedWidth = rows[0].Length;
        foreach (var row in rows)
        {
            Assert.AreEqual(expectedWidth, row.Length);
        }
    }

    [Test()]
    public void CreateDiamondTest_GivenSingleLetterAlphabetX_WhenCreatingForLetterX_ShouldReturnSingleLetterDiamond()
    {
        // ACT
        _sut.DefineAlphabet('X', 'X');
        var diamond = _sut.CreateDiamond('X');

        // ASSERT
        Assert.AreEqual("X" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenGapAlphabetACE_WhenCreatingForLetterD_ShouldThrowException()
    {
        // ACT
        _sut.DefineAlphabet("ACE");

        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.CreateDiamond('D'));
    }

    [Test()]
    public void CreateDiamondTest_GivenCustomAlphabetDF_WhenCreatingForLetterBeforeRange_ShouldThrowException()
    {
        // ACT
        _sut.DefineAlphabet('D', 'F');

        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.CreateDiamond('A'));
    }

    [Test()]
    public void CreateDiamondTest_GivenCustomAlphabetDF_WhenCreatingForLetterAfterRange_ShouldThrowException()
    {
        // ACT
        _sut.DefineAlphabet('D', 'F');

        // ACT & ASSERT
        Assert.Throws<DiamondCreatorException>(() => _sut.CreateDiamond('Z'));
    }

    [Test()]
    public void CreateDiamondTest_GivenNumericAlphabet1To9_WhenCreatingForNumber1_ShouldReturnSingleDigitDiamond()
    {
        // ACT
        _sut.DefineAlphabet('1', '9');
        var diamond = _sut.CreateDiamond('1');

        // ASSERT
        Assert.AreEqual("1" + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabet_WhenCreatingMultipleDiamonds_TargetLetterShouldReflectLastCall()
    {
        // ACT
        _sut.CreateDiamond('A');
        _sut.CreateDiamond('C');

        // ASSERT
        Assert.AreEqual('C', _sut.TargetLetter);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabet_WhenCreatingForLetterE_TargetLetterPropertyShouldBeE()
    {
        // ACT
        _sut.CreateDiamond('E');

        // ASSERT
        Assert.AreEqual('E', _sut.TargetLetter);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabet_WhenChangingVisualizerToSpaceAndCreatingForLetterB_ShouldReturnCorrectDiamond()
    {
        // ACT
        _sut.WhitespaceVisualizer = ' ';
        var diamond = _sut.CreateDiamond('B');

        // ASSERT
        Assert.AreEqual("  A  " + Environment.NewLine +
                        "B   B" + Environment.NewLine +
                        "  A  " + Environment.NewLine, diamond);
    }

    [Test()]
    public void CreateDiamondTest_GivenDefaultAlphabet_WhenChangingVisualizerToDotAndCreatingForLetterC_ShouldReturnCorrectDiamond()
    {
        // ACT
        _sut.WhitespaceVisualizer = '.';
        var diamond = _sut.CreateDiamond('C');

        // ASSERT
        Assert.AreEqual(". . A . ." + Environment.NewLine +
                        ". B . B ." + Environment.NewLine +
                        "C . . . C" + Environment.NewLine +
                        ". B . B ." + Environment.NewLine +
                        ". . A . ." + Environment.NewLine, diamond);
    }

    #endregion

    [OneTimeTearDown()]
    public void CleanUp()
    {
        _sut = null!;
    }
}
