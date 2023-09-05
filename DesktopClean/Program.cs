string path = @"C:\Users\paul_\Desktop";
string[] files = Directory.GetFiles(path);
List<string> fileExtension = new List<string>();
decimal totalSize = 0;

//takes every file name from desktop files, gets the extension only (ex: ".torrent"), creates folders with file extension name
//and copy file to specified extension folder
foreach (string file in files)
{
    FileInfo fileInfo = new FileInfo(file);
    totalSize = totalSize + fileInfo.Length;

    string str = file.Replace(path + @"\", "");
    AddToList(str, str.Length);

    string dir = @$"C:\New folder\{fileExtension.Last()}_Files";
    
    //checks if directory exists at specified path, if not create new directory
    if (!Directory.Exists(dir))
        Directory.CreateDirectory(dir);

    //checks if file exists at the specified path, if not copy the file
    if(!File.Exists(dir + "\\" + str))
        File.Copy(file, dir +"\\"+str);
}

// method that adds extensions (.torrent) to a list
void AddToList(string input, int nameLength)
{
    string wordToAdd = string.Empty;
    if (nameLength > 3)
    {
        wordToAdd = GetToThePoint(input);
        wordToAdd = wordToAdd.Replace(".", "");
        fileExtension.Add(wordToAdd);
    }
    else
    {
        wordToAdd = GetToThePoint(input);
        wordToAdd = wordToAdd.Replace(".", "");
        fileExtension.Add(wordToAdd);
    }
}

//returns extension -> .torrent
string GetToThePoint(string input)
{
    var index = input.LastIndexOf('.');
    string returnString = string.Empty;

    if (index > 0)
        returnString = input.Substring(index, input.Length - index);
    else
        returnString = input;

    return returnString;
}

decimal sizeMB = totalSize / 1000 / 1000;
decimal sizeGB = totalSize / (1024 * 1024 * 1024);
Console.WriteLine("Total size: "+ sizeMB.ToString("0.00") +" MB" + "\n\tOR\n" + "Total size: "+ sizeGB.ToString("0.00") +" GB\n");
//testing
fileExtension.Sort();

foreach (var file in fileExtension)
    Console.WriteLine(file);
