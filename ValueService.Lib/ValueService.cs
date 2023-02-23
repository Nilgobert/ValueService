

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
        public ValueService()
        {
            PostFactors = new List<IPostFactor>()
            {
                new PostFactor() { Text = "mili", TextShort = "m", Potenz = -3 },
                new PostFactor() { Text = "mikro", TextShort = "µ", Potenz = -6 },
                new PostFactor() { Text = "nano", TextShort = "n", Potenz = -9 },
                new PostFactor() { Text = "piko", TextShort = "p", Potenz = -12 },
                new PostFactor() { Text = "Kilo", TextShort = "K", Potenz = 3 },
                new PostFactor() { Text = "Mega", TextShort = "M", Potenz = 6 },
                new PostFactor() { Text = "Giga", TextShort = "G", Potenz = 9 },
                new PostFactor() { Text = "Tera", TextShort = "T", Potenz = 12 },
                new PostFactor() { Text = "Peta", TextShort = "P", Potenz = 15 },
                new PostFactor() { Text = "Exa", TextShort = "E", Potenz = 18 }
            };
        }

        public List<IPostFactor> PostFactors { get; set; }

        public decimal GetDecimal(string value)
        {
            value = value.Trim();
            value = value.Replace('.', ',');
            if (decimal.TryParse(value, out decimal result))
            {
                return result;
            }
            if (char.IsLetter(value, value.Length -1))
            {
                string pf = value.Substring(value.Length - 1, 1);
                value = value[0..^1];
                if (decimal.TryParse(value, out result))
                {
                    try
                    {
                        result = Pow10PostFactor(result, pf);
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw (ex);
                    }
                }else
                {
                    throw (new InvalidCastException(value + " cannot be casted to decimal"));
                }
            }

            return result;
        }

        public string GetDisplayValue(decimal value, int precision, string? Postfactor = null) {
            throw new NotImplementedException();
        }

        public string GetPostFactor(decimal value) {
            throw new NotImplementedException();
        }

        public int? GetPotenz(string value)
        {
            IPostFactor? result;
            value = value 
        }

        public decimal Pow10(decimal value, int potenz)
        {
            if (potenz == 0) { return 1; }
            if (potenz > 0)
            {
                for (int i = 0; i < potenz; i++)
                {
                    value *= 10;
                }
            }
            else
            {
                for (int i = 0; i < potenz; i++)
                {
                    value /= 10;
                }
            }
            return value;
        }

        public decimal Pow10PostFactor(decimal number, string PostFactor) {
            throw new NotImplementedException();
        }
        public class PostFactor : IPostFactor {
            public string Text { get; set; }
            public string TextShort { get; set; }
            public int Potenz { get; set; }
        }
    }
}