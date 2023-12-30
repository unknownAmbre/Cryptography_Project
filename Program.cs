namespace AES
{
    public class Aes
    {
        private static ulong key = 1383827165324567;
        private static ulong sbox = 2485962013578904;
        private static ulong mixcolumn = 7123456890234561;


        public static void Main()
        {

            string plaintext = "0123456789BCDEF";
            Console.WriteLine("Original Text: " + plaintext.PadLeft(16, '0'));

            // Convert the plaintext to a 16-bit block
            ulong block = HexStringToUlong(plaintext);
            Console.WriteLine("Ulong block : " + block);

            // Generate round keys
            ulong[] roundKeys = GenerateRoundKeys(key);

            // Add initial round key R0
            block = AddRoundKey(block, roundKeys, 0);

            // Implement 10 Round To Encrypt
            for (int i = 1; i < 11; i++)
            {
                block = SubstituteSBox(block);
                block = ShiftRows(block);
                block = SubstituteMixColumn(block);
                block = AddRoundKey(block, roundKeys, i);
            }

            // Display encrypted text
            string encryptedText = UlongToHexString(block);
            Console.WriteLine("Encrypted text : " + encryptedText);

            // Implement 10 Rounds to Decrypt
            for (int i = 10; i > 0; i--)
            {
                block = AddRoundKey(block, roundKeys, i);
                block = SubstituteMixColumn(block);
                block = UnShiftRows(block);
                block = SubstituteSBox(block);
            }

            // Reverse initial round key R0
            block ^= roundKeys[0];

            // Convert the decrypted block back to a string
            string decryptedText = UlongToHexString(block);
            Console.WriteLine("Decrypted Text: " + decryptedText);

            Console.ReadKey();
        }

        private static ulong[] GenerateRoundKeys(ulong key)
        {
            ulong[] roundKeys = new ulong[12];

            // Implement your key schedule logic here
            // For simplicity, using the same key for all rounds
            for (int round = 0; round < 12; round++)
            {
                roundKeys[round] = key;  
            }

            return roundKeys;
        }

        private static ulong AddRoundKey(ulong block, ulong[] roundKeys, int round)
        {
            block ^= roundKeys[round];
            return block;
        }

        public static ulong ShiftRows(ulong block)
        {
            string s = Convert.ToString(block);

            string news = s.Substring(0, 4)
                + s.Substring(5, 3) + s[4]
            + s.Substring(10, 2) + s.Substring(8, 2)
            + s[15] + s.Substring(12, 3);

            return Convert.ToUInt64(news);
        }

        public static ulong UnShiftRows(ulong block)
        {
            string s = Convert.ToString(block);

            string news = s.Substring(0, 4)
                + s[7] + s.Substring(4, 3)
            + s.Substring(10, 2) + s.Substring(8, 2)
             + s.Substring(13, 3) + s[12];

            return Convert.ToUInt64(news);
        }

        private static ulong SubstituteSBox(ulong block)
        {
            // Placeholder for S-box substitution
            // In a real implementation, this would involve complex logic based on the specific S-box
            // Here, we'll use a simple XOR operation for illustration purposes
            return block ^ (ulong)sbox;
        }

        private static ulong SubstituteMixColumn(ulong block)
        {
            // Placeholder for S-box substitution
            // In a real implementation, this would involve complex logic based on the specific S-box
            // Here, we'll use a simple XOR operation for illustration purposes
            return block ^ (ulong)mixcolumn;
        }

        private static ulong HexStringToUlong(string hex)
        {
            return Convert.ToUInt64(hex, 16);
        }

        private static string UlongToHexString(ulong value)
        {
            return Convert.ToString((long)value, 16).ToUpper().PadLeft(16, '0');
        }
    }
}

