using DiamondCreatorLib.Utils;
using NUnit.Framework;

namespace DiamondCreatorLib.Tests;

[TestFixture()]
public class DiamondCreatorExceptionTests
{
    [Test()]
    public void DiamondCreatorExceptionTest_WhenCreatedWithMessage_ShouldContainFormattedMessage()
    {
        // ACT
        var ex = new DiamondCreatorException("Test error");

        // ASSERT
        Assert.That(ex.Message, Does.Contain("Test error"));
    }

    [Test()]
    public void DiamondCreatorExceptionTest_WhenCreatedWithFormatAndArgs_ShouldContainFormattedMessage()
    {
        // ACT
        var ex = new DiamondCreatorException("Error: {0} - {1}", "arg1", "arg2");

        // ASSERT
        Assert.That(ex.Message, Does.Contain("arg1"));
        Assert.That(ex.Message, Does.Contain("arg2"));
    }

    [Test()]
    public void DiamondCreatorExceptionTest_WhenCreatedWithMessageAndInnerException_ShouldHaveBoth()
    {
        // ARRANGE
        var inner = new InvalidOperationException("inner error");

        // ACT
        var ex = new DiamondCreatorException("Test error", inner);

        // ASSERT
        Assert.That(ex.InnerException, Is.SameAs(inner));
        Assert.That(ex.Message, Does.Contain("Test error"));
    }

    [Test()]
    public void DiamondCreatorExceptionTest_WhenCreatedWithInnerExceptionFormatAndArgs_ShouldHaveBoth()
    {
        // ARRANGE
        var inner = new ArgumentException("inner error");

        // ACT
        var ex = new DiamondCreatorException(inner, "Error: {0}", "detail");

        // ASSERT
        Assert.That(ex.InnerException, Is.SameAs(inner));
        Assert.That(ex.Message, Does.Contain("detail"));
    }

    [Test()]
    public void DiamondCreatorExceptionTest_ShouldBeDerivedFromException()
    {
        // ACT
        var ex = new DiamondCreatorException("test");

        // ASSERT
        Assert.That(ex, Is.InstanceOf<Exception>());
    }

    [Test()]
    public void DiamondCreatorExceptionTest_WhenCreatedWithMessage_ShouldContainResourcePattern()
    {
        // ACT
        var ex = new DiamondCreatorException("custom message");

        // ASSERT
        Assert.That(ex.Message, Does.Contain("A single letter from the Alphabet is required"));
        Assert.That(ex.Message, Does.Contain("custom message"));
    }

    [Test()]
    public void DiamondCreatorExceptionTest_WhenThrownByDefineAlphabetReversedRange_ShouldContainSymbolDetails()
    {
        // ARRANGE
        var service = new DiamondCreatorService();

        // ACT & ASSERT
        var ex = Assert.Throws<DiamondCreatorException>(() => service.DefineAlphabet('Z', 'A'));
        Assert.That(ex!.Message, Does.Contain("Z"));
        Assert.That(ex.Message, Does.Contain("A"));
    }

    [Test()]
    public void DiamondCreatorExceptionTest_WhenThrownByDefineAlphabetNullString_ShouldHaveInnerArgumentNullException()
    {
        // ARRANGE
        var service = new DiamondCreatorService();

        // ACT & ASSERT
        var ex = Assert.Throws<DiamondCreatorException>(() => service.DefineAlphabet(null!));
        Assert.That(ex!.InnerException, Is.InstanceOf<ArgumentNullException>());
    }

    [Test()]
    public void DiamondCreatorExceptionTest_WhenThrownByCreateDiamondInvalidLetter_ShouldContainLetterAndAlphabet()
    {
        // ARRANGE
        var service = new DiamondCreatorService();
        service.DefineAlphabet("ACE");

        // ACT & ASSERT
        var ex = Assert.Throws<DiamondCreatorException>(() => service.CreateDiamond('D'));
        Assert.That(ex!.Message, Does.Contain("D"));
        Assert.That(ex.Message, Does.Contain("ACE"));
    }
}
