using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CustomStringEnumerator.Tests
{
    // Do not change the name of this class
    public class CustomStringEnumerator :  IEnumerable<string>
    {
        private readonly HashSet<char> numberCharacters = new HashSet<char>(){'0','1','2','3','4','5','6','7','8','9'};
        private readonly IEnumerable<string> _originalCollection;
        private readonly EnumeratorConfig _enumeratorConfig;
        /// <summary> Constructor </summary>
        /// <exception cref="System.ArgumentNullException">If a collection is null</exception>
        /// <exception cref="System.ArgumentNullException">If an config is null</exception>
        public CustomStringEnumerator(IEnumerable<string> collection, EnumeratorConfig config)
        {
            _originalCollection = collection;
            _enumeratorConfig = config;
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach(var item in _originalCollection)
            {
                var firstCharacter = item[0];
                var firstCharacterLower = item.ToLower()[0];
                var firstCharacterUpper = item.ToUpper()[0];
                if(
                    (_enumeratorConfig.MinLength < 0 || item.Length >= _enumeratorConfig.MinLength) &&
                    (_enumeratorConfig.MaxLength < 0 || item.Length <= _enumeratorConfig.MaxLength) &&
                    (_enumeratorConfig.StartWithCapitalLetter == false || firstCharacter == firstCharacterUpper && firstCharacter != firstCharacterLower) &&
                    (_enumeratorConfig.StartWithDigit == false || numberCharacters.Contains(firstCharacter) )
                )
                    yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CustomStringEnumeratorTests
    {
        [Theory]
        [InlineData(-42, new[]{"Any string", "abc", "no", "yep"}, new[]{"Any string", "abc", "no", "yep"})]
        [InlineData(3, new[]{"Any string", "abc", "no", "yep"}, new[]{"Any string", "abc", "yep"})]
        [InlineData(10, new[]{"Any string", "abc", "no", "yep"}, new []{"Any string"})]
        [InlineData(15, new[]{"Any string", "abc", "no", "yep"}, new string[]{})]
        [InlineData(2, new[]{"Any string", "abc", "no", "yep"}, new []{"Any string", "abc", "no", "yep"})]
        public void WhenGivenAStringCollectionWithMinimalValue_ReturnsAFilteredStringCollection(int minimalLength, string[] inputCollection, string[] filteredInputCollection)
        {
            var config = new EnumeratorConfig() { MinLength = minimalLength };
            var customStringEnumerator = new CustomStringEnumerator(inputCollection, config);
            Assert.Equal(filteredInputCollection, customStringEnumerator.ToArray());
        }

        [Theory]
        [InlineData(-42, new[]{"Any string", "abc", "no", "yep"}, new[]{"Any string", "abc", "no", "yep"})]
        [InlineData(3, new[]{"Any string", "abc", "no", "yep"}, new[]{"abc", "no", "yep"})]
        [InlineData(10, new[]{"Any string", "abc", "no", "yep"}, new []{"Any string", "abc", "no", "yep"})]
        [InlineData(2, new[]{"Any string", "abc", "no", "yep"}, new []{"no"})]
        [InlineData(1, new[]{"Any string", "abc", "no", "yep"}, new string[]{})]
        public void WhenGivenAStringCollectionWithMaximumValue_ReturnsAFilteredStringCollection(int maxLength, string[] inputCollection, string[] filteredInputCollection)
        {
            var config = new EnumeratorConfig() { MaxLength = maxLength };
            var customStringEnumerator = new CustomStringEnumerator(inputCollection, config);
            Assert.Equal(filteredInputCollection, customStringEnumerator.ToArray());
        }

        [Theory]
        [InlineData(false, new[]{"Any string", "abc", "No", "yep"}, new []{"Any string", "abc", "No", "yep"})]
        [InlineData(true, new[]{"Any string", "abc", "no", "yep"}, new []{"Any string"})]
        [InlineData(true, new[]{"any string", "abc", "no", "yep"}, new string[]{})]
        [InlineData(true, new[]{"any string", "Abc", "NO", "Yep"}, new []{"Abc", "NO", "Yep"})]
        public void WhenGivenAStringCollectionWithStartWithCapitalLetter_ReturnsAFilteredStringCollection(bool startWithCapitalLetter, string[] inputCollection, string[] filteredInputCollection)
        {
            var config = new EnumeratorConfig() { StartWithCapitalLetter = startWithCapitalLetter };
            var customStringEnumerator = new CustomStringEnumerator(inputCollection, config);
            Assert.Equal(filteredInputCollection, customStringEnumerator.ToArray());
        }

        [Theory]
        [InlineData(false, new[]{"Any string", "abc", "No", "yep"}, new []{"Any string", "abc", "No", "yep"})]
        [InlineData(true, new[]{"1Any string", "2abc", "3esdf", "4asdf"}, new []{"1Any string", "2abc", "3esdf", "4asdf"})]
        [InlineData(true, new[]{"any string", "abc", "no", "yep"}, new string[]{})]
        [InlineData(true, new[]{"any string", "2Abc", "3NO", "Yep"}, new []{"2Abc", "3NO"})]
        public void WhenGivenAStringCollectionWithStartWithDigit_ReturnsAFilteredStringCollection(bool startWithDigit, string[] inputCollection, string[] filteredInputCollection)
        {
            var config = new EnumeratorConfig() { StartWithDigit = startWithDigit };
            var customStringEnumerator = new CustomStringEnumerator(inputCollection, config);
            Assert.Equal(filteredInputCollection, customStringEnumerator.ToArray());
        }
    }

}

/*
// A sample showing how CustomStringEnumerator & EnumeratorConfig will be used
var collection = new string[] { ... };
var config = new EnumeratorConfig
{
    MinLength = 3,
    MaxLength = 10,
    StartWithCapitalLetter = true
};

// The custom enumerator will return strings that are longer or equal to 3 charaters
// and shorter or equal 10 characters and start with a capital letter.
var enumerator = new CustomStringEnumerator(collection, config);
foreach (var s in enumerator)
{
    Console.WriteLine(s);
}
*/

// Your implementation of CustomStringEnumerator should take into account all the properties of EnumeratorConfig
public class EnumeratorConfig
{
    // Specifies the minimal length of strings that should be returned by a custom enumerator.
    // If it is set to negative value then this option is ignored.
    public int MinLength { get; set; } = -1;

    // Specifies the maximum length of strings that should be returned by a custom enumerator.
    // If it is set to negative value then this option is ignored.
    public int MaxLength { get; set; } = -1;

    // Specifies that only strings that start with a capital letter should be returned by a custom enumerator.
    // Please note that empty or null strings do not meet this condition.
    public bool StartWithCapitalLetter { get; set; }

    // Specifies that only strings that start with a digit should be returned by a custom enumerator.
    // Please note that empty or null strings do not meet this condition.
    public bool StartWithDigit { get; set; }
}
