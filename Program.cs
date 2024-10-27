using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldPhonePad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OldPhonePad("33#"));              // E
            Console.WriteLine(OldPhonePad("227*#"));            // B
            Console.WriteLine(OldPhonePad("4433555 555666#"));  // HELLO
            Console.WriteLine(OldPhonePad("8 88777444666*664#"));// ????

            Console.ReadKey();


        }
        public static String OldPhonePad(string input)
        {
            StringBuilder Result = new StringBuilder();
            Dictionary<char, string> PhoneKeyboard = new Dictionary<char, string>()
            {
            {'2', "ABC"},
            {'3', "DEF"},
            {'4', "GHI"},
            {'5', "JKL"},
            {'6', "MNO"},
            {'7', "PQRS"},
            {'8', "TUV"},
            {'9', "WXYZ"}
            };

            int PressCounter = 0;
            char LastKeyPressed = ' ';

            foreach(char charInput in input)
            {


                if (charInput == '#')
                {
                    // End the process when i find # '#'
                    break;
                }
                else if (charInput== '*')
                {
                    //When i find a * as input i need to process the letter first and then delete it, because 
                    //the algorithm is based on a postprocessing of the letter received as input if i delete
                    //inmediately then i'll be deleting the previous letter already procesed
                    
                    if (PhoneKeyboard.ContainsKey(LastKeyPressed))
                    {
                        // Search the char according the prescounter
                        string letters = PhoneKeyboard[LastKeyPressed];
                        int index = (PressCounter - 1) % letters.Length;
                        Result.Append(letters[index]);
                    }
                    // then i delete the last letter retorned and reestablish the lastkeypressed 
                    if (Result.Length > 0)
                    {
                        Result.Remove(Result.Length-1 , 1);
                        LastKeyPressed = ' ';
                    }
                    
                }
                else if (charInput== ' ')
                {
                    // If we found a pause beetween chars expressed as an empty char then we process the letter
                    if (PhoneKeyboard.ContainsKey(LastKeyPressed))
                    {
                        // Buscar el carácter usando el valor de la pulsación (pressCount)
                        string letters = PhoneKeyboard[LastKeyPressed];
                        int index = (PressCounter - 1) % letters.Length;
                        Result.Append(letters[index]);
                    }
                    // Reseting the LastKeyPressed and Presscounter
                    LastKeyPressed = ' ';
                    PressCounter = 0;
                }
                else
                {
                    // If i receiving a different key from last one it means we need to proccess the last batch
                    if (charInput!= LastKeyPressed && LastKeyPressed != ' ')
                    {
                        if (PhoneKeyboard.ContainsKey(LastKeyPressed))
                        {
                        
                            string letters = PhoneKeyboard[LastKeyPressed];
                            int index = (PressCounter - 1) % letters.Length;
                            Result.Append(letters[index]);
                        }
                        PressCounter = 0; 
                    }

                    LastKeyPressed = charInput;
                    PressCounter++;
                }
            }

            // Processing the last sequence
            if (PhoneKeyboard.ContainsKey(LastKeyPressed))
            {
                string letters = PhoneKeyboard[LastKeyPressed];
                int index = (PressCounter - 1) % letters.Length;
                Result.Append(letters[index]);
            }
            
            return Result.ToString(); 
        }
    }
}
