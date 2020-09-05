using System.IO;

namespace pikachuface.UI
{
    class FileDialog
    {
        public string StartPath {get; private set;}





        public bool tryChangeStartPath(string path)
        {
            if(Directory.Exists(path))
            {
                StartPath = path;
                return true;
            }
            return false;
        }

        //public bool OpenDirectory()
        
        //public bool OpenFile()

        //public bool SaveFile()


    }





}