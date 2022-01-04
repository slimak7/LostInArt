using System.Collections;
using System.Collections.Generic;
using System.Text;

public static class Encrypt
{

    public static string encrypt(string text)
    {
        string newText = "";

        char[] array = text.ToCharArray();

        for (int i = array.Length - 1; i >= 0; i--)
        {
            if (array[i] == '>')
                array[i] = ';';
            if (array[i] == '<')
                array[i] = ':';
            if (array[i] == ' ')
                array[i] = '?';

            int number = (int)array[i] + 12;
            array[i] = (char)number;

            if ((int)array[i] > 45)
            {
                number = (int)array[i] + 1;
                array[i] = (char)number;
            }



            newText += array[i];

        }

        return newText;
    }

}
