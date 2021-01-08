using System;
using System.Collections.Generic;

namespace NotificationService
{
    class Template
    {
        private string Text { get; }
        private string LeftSeparator { get; }
        private string RightSeparator { get; }

        public Template(string text, string leftSeparator, string rightSeparator)
        {
            Text = text;
            LeftSeparator = leftSeparator;
            RightSeparator = rightSeparator;
            Validate();
        }

        public string Apply(Dictionary<string, string> dictionary)
        {
            string text = Text;
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                string key = LeftSeparator + pair.Key + RightSeparator;
                text = text.Replace(key, pair.Value, StringComparison.OrdinalIgnoreCase);
            }
            return text;
        }

        private void Validate()
        {
            if (String.IsNullOrEmpty(Text))
                throw new ValidationException();
            if (String.IsNullOrEmpty(LeftSeparator))
                throw new ValidationException();
            if (String.IsNullOrEmpty(RightSeparator))
                throw new ValidationException();
        }
    }
}
