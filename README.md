
## 🌟 Clean Folder / Directories
Clean Folder / Directories is the solution to declutter your desktop. This C# script sweeps through your desktop, creating folders for each file extension, and neatly organizing your files.
## 🎮Todo
- ~~Ignore specified files~~ ✅
- ~~Ignore specified folders~~ ✅
- ~~Ignore specified extensions~~ ✅
- ~~Add total files size and file count.~~ ✅
## 🔧 How It Works
1) Change fromPath and toPath to the path you intend to clean(fromPath) and the path to have all files copied to(toPath).
- string fromPath = @"C:\test";
- string toPath = @"C:\New folder";
2) Set ignored files and ignored extensions
3) Script scans your desktop for scattered files.
4) It creates folders named after each file extension.
5) Your files are moved(atm only copied for testing) to their respective extension folders.
## 💡 Usage
Change the path variable to specify the directory you want to clean. Run the script!
## 🧨 Contributions
If you have ideas to enhance the script, open a pull request.