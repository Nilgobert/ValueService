namespace ValueServiceLib {

    /// <summary>
    /// Interface to convert Values in/from decimal, string int with/without Power or PostFactor
    /// </summary>
    public interface IValueService {

        List<IPostFactor> PostFactors { get; set; }

        /// <summary>
        /// Converts a number as string with/without Postfaktor to a decimal
        /// </summary>
        /// <param name="value">string of the number to convert. Can contain - , . postfactor inside or at the end of the string</param>
        /// <returns>decimal representation of the string number without potenz and postfactor</returns>
        decimal GetDecimal(string value);

        /// <summary>
        /// Converts the decimal input value to string including postfactor and a specific number of postcomma digits
        /// </summary>
        /// <param name="value">decimal value to be convertet</param>
        /// <param name="precision">number of postcomma digits (rounded)</param>
        /// <param name="PostFactor">preferred Postfactor if any (default = null) -> gets optimal Postfactor</param>
        /// <returns>string representation to use at UIs</returns>
        string GetDisplayValue(decimal value, int precision, string? Postfactor = null);

        /// <summary>
        /// calculate a decimal number out of a number and its postfactor (e.g. 100k = 100000)
        /// </summary>
        /// <param name="number">decimal number to multiply the postfactor to</param>
        /// <param name="PostFactor">string postfactor to calculate the number</param>
        /// <returns>decimal representation of number and postfactor</returns>
        decimal Pow10PostFactor(decimal number, string PostFactor);

        /// <summary>
        /// calculate value * 10^potenz to resolve the postfactor
        /// </summary>
        /// <param name="value">value to compute</param>
        /// <param name="potenz">how many times should the "value" be multiplied by 10</param>
        /// <returns>decimal value without potenz and postfactor</returns>
        decimal Pow10(decimal value, int potenz);

        /// <summary>
        /// determines the potenz (10^x) from the list of postfactors
        /// </summary>
        /// <param name="value">postfactor to be searched</param>
        /// <returns>potenz x (10^x) as signed integer. null if no postfactor is found)</returns>
        int? GetPotenz(string value);

        /// <summary>
        /// determines the postfactor from a given value. The optimized factor is found, when the value is >0 and < 1000
        /// </summary>
        /// <param name="value">decimal value without postfactor</param>
        /// <returns>optimal postfactor for the value (1 character)</returns>
        string GetPostFactor(decimal value);


    }


    /// <summary>
    /// Element of Postfactor with Text (milli, mikro, ...) TextShort (m, µ, ...) and Potenz (3, 6, ...)
    /// This element is implemented and used in a List of IPostFactor in the Service instance"
    /// </summary>
    public interface IPostFactor {
        public string Text { get; set; }
        public string TextShort { get; set; }
        public int Potenz { get; set; }
    }

}
