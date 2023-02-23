using ValueServiceLib;

namespace ValueServiceTest {
    public class ValueServiceTests {


        [Fact]
        public void FillListOnCreation() {
            var vs = new ValueService();
            Assert.Equal(10, vs.PostFactors.Count);
        }

        [Theory]
        [InlineData(100, 100, "")]
        [InlineData(3400, 3.4, "k")]
        [InlineData(0.034, 34, "m")]
        [InlineData(0.0000034, 3.4, "µ")]
        [InlineData(345625100, 345.6251, "M")]
        public void CalcDecimalWithPostFactor(decimal expected, decimal number, string pf) {
            var vs = new ValueService();
            decimal result = vs.Pow10PostFactor(number, pf);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(0, "")]
        [InlineData(3, "k")]
        [InlineData(-12, "p")]
        [InlineData(12, "T")]
        [InlineData(0, "x")]
        public void GetPotenzNumberFromPostFactor(int expected, string pf) {
            var vs = new ValueService();
            int? result = vs.GetPotenz(pf);
            if (result == null) {
                Assert.Null(result);
            } else {
                Assert.Equal(expected, result);
            }
        }


        [Theory]
        [InlineData(3400, "3.4k")]
        [InlineData(3400, "3k4")]
        [InlineData(3400, "3400")]
        [InlineData(0.250, "250m")]
        [InlineData(0.0000039, "3.9µ")]
        [InlineData(0.0000039, "3µ9")]
        public void GetDecimalFromStringWithPostFactor(decimal expected, string value) {
            var vs = new ValueService();
            decimal result = vs.GetDecimal(value);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData("120k", 120000, 0, null)]
        [InlineData("120", 120, 0, null)]
        [InlineData("3,1415k", 3141.5, 4, null)]
        [InlineData("4,7k", 4700, 1, null)]
        [InlineData("3,9µ", 0.0000039, 1, null)]
        [InlineData("300.000k", 300000000, 0, "k")]
        public void GetDisplayValueFromDecimal(string expected, decimal value, int precision, string? pfPreferred) {
            var vs = new ValueService();
            string result = vs.GetDisplayValue(value, precision, pfPreferred);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void InvalidPostfactorInGetDisplayValue() {
            var vs = new ValueService();
            Assert.Throws<ArithmeticException>(() => vs.GetDisplayValue(314, 3, "x"));
        }


        [Fact]
        public void InvalidPostfactorInGetDezimal() {
            var vs = new ValueService();
            Assert.Throws<ArgumentNullException>(() => vs.GetDecimal("100x"));
        }


        [Fact]
        public void TooManyPostfactorsInGetDezimal() {
            var vs = new ValueService();
            Assert.Throws<InvalidCastException>(() => vs.GetDecimal("100mµ"));
        }

        [Fact]
        public void NotANumberInGetDezimal() {
            var vs = new ValueService();
            Assert.Throws<InvalidCastException>(() => vs.GetDecimal("3a0µ"));
        }

    }
}