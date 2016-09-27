using System;
using Xunit;

namespace MayLily.DataAccess.FluentMigrator.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void NullFormat_ExpectsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => StringExtensions.Fmt(null));

            Assert.Contains("Value cannot be null.", exception.Message);
            Assert.Contains("Parameter name: format", exception.Message);
        }
    }
}
