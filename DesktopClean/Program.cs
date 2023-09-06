namespace DesktopClean
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "";
            string path = @"C:\Users\paul_\Desktop";
            string[] files = Directory.GetFiles(path);

            bool fileIgnored = false;
            Console.Write("Type the files you want to ignore splited by \", \" \n(Ex: test1.zip, game.exe, photo.jpeg) \n");
            string inputFileIgnore = Console.ReadLine().ToLower();

            string[] filesToIgnore = IgnoredFiles(inputFileIgnore);

            Console.WriteLine(fileIgnored);

            Thread.Sleep(5000);

            List<string> fileExtension = new List<string>();
            decimal totalSize = 0;
            int numberOfElements = files.Length;

            int origRow = Console.CursorLeft;
            int origCol = Console.CursorTop;

            int maxNumberWidth = numberOfElements.ToString().Length;
            string message = "Number of elements remained: ";

            Console.SetCursorPosition(origRow, origCol);
            Console.Write(message);

            origRow += message.Length;

            //takes every file name from desktop files, gets the extension only (ex: ".torrent"), creates folders with file extension name
            //and copy file to specified extension folder
            foreach (string file in files)
            {
                string fileCheckForIgnore = file.Replace(path + @"\", "");
                if (filesToIgnore.Contains(fileCheckForIgnore) == false)
                {
                    //sets cursor and writes number of elements at top left screen
                    WriteAt();

                    FileInfo fileInfo = new FileInfo(file);
                    totalSize = totalSize + fileInfo.Length;

                    string str = file.Replace(path + @"\", "");
                    AddToList(str, str.Length);

                    string dir = @$"C:\New folder\{fileExtension.Last()}_Files";

                    //checks if directory exists at specified path, if not create new directory
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    //checks if file exists at the specified path, if not copy the file
                    if (!File.Exists(dir + "\\" + str))
                        File.Copy(file, dir + "\\" + str);
                }
                else
                    test = test +" "+ file;

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
            Console.WriteLine("Total size: " + sizeMB.ToString("0.00") + " MB" + "\n\tOR\n" + "Total size: " + sizeGB.ToString("0.00") + " GB\n");
            
            //testing
            fileExtension.Sort();

            foreach (var file in fileExtension)
                Console.WriteLine(file);

            void WriteAt()
            {
                Console.SetCursorPosition(origRow, origCol);

                // Format the current value with leading zeros to match the width of the widest number
                string formattedValue = (numberOfElements--).ToString().PadLeft(maxNumberWidth, '0');

                // Write the formatted value
                Console.Write(formattedValue);

                // Sleep for a while to display each number and ease a little bit the cpu
                Thread.Sleep(100);
            }

            string[] IgnoredFiles(string input)
            {
                input = input.Trim();
                string[] filesToIgnore = input.Split(", ");
                if (filesToIgnore.Length > 0)
                    fileIgnored = true;

                return filesToIgnore;
            }
        }
    }
}



