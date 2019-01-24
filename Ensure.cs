public static class Ensure
    {
        public static string IsNotNullOrWhiteSpace(string value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be empty or contain only whitespace characters.", paramName);
            return value;
        }
    }
