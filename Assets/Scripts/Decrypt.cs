using System.Collections;
using System.Collections.Generic;


public static class Decrypt
{

    public static string decrypt(string text)
    {
        string newText = "";


        char[] array = text.ToCharArray();

        for (int i = array.Length - 1; i >= 0; i--)
        {

            int number = (int)array[i] - 12;
            array[i] = (char)number;

            if ((int)array[i] > 46)
            {
                number = (int)array[i] - 1;
                array[i] = (char)number;
            }

            if (array[i] == ';')
                array[i] = '>';
            if (array[i] == ':')
                array[i] = '<';
            if (array[i] == '?')
                array[i] = ' ';




            newText += array[i];
        }

        return newText;

    }
}

