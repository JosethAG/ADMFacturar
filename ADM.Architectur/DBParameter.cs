﻿namespace ADM.Architectur
{
    public class DBParameter
    {
        public DBParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
