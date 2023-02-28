

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

        public class PostFactor : IPostFactor {
            public string Text { get; set; }
            public string TextShort { get; set; }
            public int Potenz { get; set; }
        }
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

        public string GetDisplayValue(decimal value, int precision, string? Postfactor = null)
        {
            string postFactor = Postfactor != string.Empty ? Postfactor : GetPostFactor(value);
            double.TryParse(Convert.ToString(GetPotenz(postFactor)), out var result);
            value /= (decimal)Math.Pow(10.00d, result);
            return $"{Math.Round(value, precision) + postFactor}";
        }

        public string GetPostFactor(decimal value)
        {
            var potenz = (int)Math.Floor(Math.Log10((double)value));
            var postfactor = PostFactors.FirstOrDefault(element => element.Potenz + 1 == potenz || element.Potenz + 2 == potenz || element.Potenz == potenz);

            return postfactor != null ? postfactor.TextShort! : string.Empty;
        }
        

        public int? GetPotenz(string value)
        {
            var faktor = PostFactors.FirstOrDefault(x => x.TextShort == value);
            if (faktor == null)
            {
                return null;
            }
            else
            {
                return faktor.Potenz;
            }
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

        public decimal Pow10PostFactor(decimal number, string PostFactor)
        {
            double.TryParse(Convert.ToString(GetPotenz(PostFactor)), out double result);
            return number * (decimal)Math.Pow(10.00d, result);

        }
        
    }
}