namespace PatientWebApp.Auth
{
    public class EncryptionService
    {
        public EncryptionService()
        {
        }
        public string DecryptString(string encryptedJmbg)
        {
            char[] arr;
            string result = "";
            arr = encryptedJmbg.ToCharArray(0, 13);

            foreach (char c in arr)
            {
                if (c.ToString() == "A")
                {
                    result += "0";
                }
                else if (c.ToString() == "p")
                {
                    result += "1";
                }
                else if (c.ToString() == "t")
                {
                    result += "2";
                }
                else if (c.ToString() == "o")
                {
                    result += "3";
                }
                else if (c.ToString() == "g")
                {
                    result += "4";
                }
                else if (c.ToString() == "e")
                {
                    result += "5";
                }
                else if (c.ToString() == "x")
                {
                    result += "6";
                }
                else if (c.ToString() == "w")
                {
                    result += "7";
                }
                else if (c.ToString() == "y")
                {
                    result += "8";
                }
                else if (c.ToString() == "K")
                {
                    result += "9";
                }
            }
            return result;
        }

        public string EncryptString(string jmbg)
        {
            char[] arr;
            string result = "";
            arr = jmbg.ToCharArray(0, 13);

            foreach (char c in arr)
            {
                if (c.ToString() == "0")
                {
                    result += "A";
                }
                else if (c.ToString() == "1")
                {
                    result += "p";
                }
                else if (c.ToString() == "2")
                {
                    result += "t";
                }
                else if (c.ToString() == "3")
                {
                    result += "o";
                }
                else if (c.ToString() == "4")
                {
                    result += "g";
                }
                else if (c.ToString() == "5")
                {
                    result += "e";
                }
                else if (c.ToString() == "6")
                {
                    result += "x";
                }
                else if (c.ToString() == "7")
                {
                    result += "w";
                }
                else if (c.ToString() == "8")
                {
                    result += "y";
                }
                else if (c.ToString() == "9")
                {
                    result += "K";
                }
            }
            return result;
        }
    }
}
