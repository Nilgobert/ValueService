

namespace ValueServiceLib {

    /// <summary>
    /// Allows to enter and get numbers in different formats. 
    /// You can enter values without postfactor e.g. 10000 and it will return a string like 10k
    /// You can enter small values like 0.00001234 and it will return 12.34µ
    /// you can enter 4M and it will return 4000000
    /// you can enter 4k7 and it will return 4700
    /// you can access the postfactor table as PostFactors List
    /// you can get a displayvalue as string with postfactors out of a decimal
    /// you can get the potenz of a postfactor
    /// you can get the postfactor of a potenz
    /// </summary>
    public class ValueService : IValueService {

        public List<IPostFactor> PostFactors { get; set; }

        public decimal GetDecimal(string value) {
            throw new NotImplementedException();
        }

        public string GetDisplayValue(decimal value, int precision, string? Postfactor = null) {
            throw new NotImplementedException();
        }

        public string GetPostFactor(decimal value) {
            throw new NotImplementedException();
        }

        public int? GetPotenz(string value) {
            throw new NotImplementedException();
        }

        public decimal Pow10(decimal value, int potenz) {
            throw new NotImplementedException();
        }

        public decimal Pow10PostFactor(decimal number, string PostFactor) {
            throw new NotImplementedException();
        }
    }
}