using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    private static string fileText;

    private static string path = Application.persistentDataPath + "/lostInArtApp1.txt";

    public static void setFileName (string name)
    {
        path = Application.persistentDataPath + "/" + name + ".txt";
    }

    public static void saveData(float value, string name)
    {
        

        if (checkFile())
        {
            if (!searchAndReplace(name, value))
            {
                create(name, value);
            }
        }
        else
        {
            create(name, value);
        }



        save();
    }

    public static void saveString (string value, string name)
    {
        if (checkFile())
        {
            if (!searchAndReplaceString(name, value))
            {
                createString(name, value);
            }
        }
        else
        {
            createString(name, value);
        }

        
        save();
    }

    public static string loadString (string name)
    {
        if (checkFile())
        {
            return LoadString(name);
        }

        return null;
    }

    public static void removeFile ()
    {
        if (!File.Exists(path))
            return;

        try
        {
            FileInfo myFile = new FileInfo(path);

            myFile.Attributes &= ~FileAttributes.ReadOnly;
            myFile.Attributes &= ~FileAttributes.Hidden;
        }
        catch
        {

        }

        FileIOPermission f = new FileIOPermission(PermissionState.None);
        f.AllLocalFiles = FileIOPermissionAccess.Write;
        try
        {
            f.Demand();
        }
        catch (SecurityException s)
        {
            System.Console.WriteLine(s.Message);
        }

        try
        {


            File.Delete(path);
        }
        catch
        {

        }
    }

    private static string LoadString (string name)
    {
        Regex reg = new Regex(" " + name + ">>");

        if (!reg.IsMatch(fileText))
        {
            return null;
        }



        int index = reg.Match(fileText).Index;

        index += name.Length + 3;



        char[] array = fileText.ToCharArray();

        int startIndex = index;

        string loadedValue = "";

        for (int i = startIndex; i < array.Length; i++)
        {
            if (array[i] != '<')
                loadedValue += array[i];

            if (array[i].Equals('<'))
            {
                i = array.Length;
            }

        }

        return loadedValue;
    }

    private static void createString (string name, string value)
    {
        string newValue = value;

        fileText += " " + name + ">>" + newValue + "<< ";
    }

    private static bool searchAndReplaceString (string name, string value)
    {
        Regex reg = new Regex(" " + name + ">>");


        if (!reg.IsMatch(fileText))
        {
            return false;
        }




        int index = reg.Match(fileText).Index;



        index += name.Length + 3;


        char[] array = fileText.ToCharArray();

        int startIndex = index;
        int endIndex = array.Length;

        for (int i = startIndex; i < array.Length; i++)
        {
            if (array[i].Equals('<'))
            {
                endIndex = i;
                i = array.Length;
            }


        }


        string newValue = value;

        fileText = fileText.Remove(startIndex, endIndex - startIndex);

        fileText = fileText.Insert(startIndex, newValue);

        return true;
    
}

    public static float loadData(string name)
    {
        if (checkFile())
        {
            return load(name);
        }

        return 0;
    }

    public static void clearAll()
    {
        reset();
    }

    private static void reset()
    {
        fileText = "";
        save();
    }

    private static float load(string name)
    {

        Regex reg = new Regex(" " + name + ">>");

        if (!reg.IsMatch(fileText))
        {
            return 0;
        }



        int index = reg.Match(fileText).Index;

        index += name.Length + 3;



        char[] array = fileText.ToCharArray();

        int startIndex = index;

        string loadedValue = "";

        for (int i = startIndex; i < array.Length; i++)
        {
            if (array[i] != '<')
                loadedValue += array[i];

            if (array[i].Equals('<'))
            {
                i = array.Length;
            }

        }

        float newValue;

        if (float.TryParse(loadedValue, out newValue))
        {
            return newValue;
        }
        else
            return 0;
    }

    private static bool checkFile()
    {
        if (File.Exists(path))
        {
            FileInfo myFile = new FileInfo(path);

            try
            {
                myFile.Attributes &= ~FileAttributes.ReadOnly;
                myFile.Attributes &= ~FileAttributes.Hidden;
            }
            catch
            {

            }

            FileIOPermission f = new FileIOPermission(PermissionState.None);
            f.AllLocalFiles = FileIOPermissionAccess.Write;
            try
            {
                f.Demand();
            }
            catch (SecurityException s)
            {
                System.Console.WriteLine(s.Message);
            }


            fileText = File.ReadAllText(path);

            fileText = Decrypt.decrypt(fileText);

            return true;
        }
        else
        {
            fileText = "";

            StreamWriter sw = File.CreateText(path);

            sw.Close();

            return false;
        }

    }

    private static bool searchAndReplace(string name, float value)
    {
        Regex reg = new Regex(" "+ name + ">>");


        if (!reg.IsMatch(fileText))
        {
            return false;
        }




        int index = reg.Match(fileText).Index;



        index += name.Length + 3;


        char[] array = fileText.ToCharArray();

        int startIndex = index;
        int endIndex = array.Length;

        for (int i = startIndex; i < array.Length; i++)
        {
            if (array[i].Equals('<'))
            {
                endIndex = i;
                i = array.Length;
            }


        }


        string newValue = value.ToString();

        fileText = fileText.Remove(startIndex, endIndex - startIndex);

        fileText = fileText.Insert(startIndex, newValue);

        return true;
    }
    private static void create(string name, float value)
    {
        string newValue = value.ToString();

        fileText += " " + name + ">>" + newValue + "<< ";

    }

    private static void save()
    {
        fileText = Encrypt.encrypt(fileText);

        FileInfo myFile = new FileInfo(path);

        

        FileIOPermission f = new FileIOPermission(PermissionState.None);
        
        try
        {
            f.Demand();
        }
        catch (SecurityException s)
        {
            System.Console.WriteLine(s.Message);
        }


        File.WriteAllText(path, fileText);

        try
        {
            myFile.Attributes |= FileAttributes.ReadOnly;
            myFile.Attributes |= FileAttributes.Hidden;
        }
        catch
        {

        }
    }

}
