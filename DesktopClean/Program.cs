string path = @"C:\Users\paul_\Desktop";

string[] files = Directory.GetFiles(path);

List<string> fileExtension = new List<string>();

//takes every file name from desktop files, replaces the prefix(c/users/etc to ""), creates folders with file extension name
foreach (string file in files)
{

    string str = file.Replace(path+@"\", "");
    string wordToAdd = string.Empty;
    if(str.Length > 3)
    {
        wordToAdd = $"{str[str.Length - 3]}{str[str.Length - 2]}{str[str.Length - 1]}";
        wordToAdd = wordToAdd.Replace(".", "");
        fileExtension.Add(wordToAdd);
    }
    else
    {
        wordToAdd = str;
        wordToAdd = wordToAdd.Replace(".", "");
        fileExtension.Add(wordToAdd);
    }

    //creates directory to move files, check if file exist if not -> copy file to dir
    string dir = @$"C:\New folder\{fileExtension.Last()}_Files";
    
    if (!Directory.Exists(dir))
    {
        Directory.CreateDirectory(dir);
    }
    if(!File.Exists(dir + "\\" + str))
        File.Copy(file, dir +"\\"+str);
}


fileExtension.Sort();

foreach (var file in fileExtension)
    Console.WriteLine(file);
