using System;
using System.IO;
using System.Threading;
using System.Timers;

namespace pikachuface.UI
{

    class Banner
    {
        public enum Types { PressToContine, AutoContinue }
        public string Content; 
        public Types Type{get; private set;}
        public int WaitTime = 1000;

        public Banner(string content, Types type)
        {
            this.Content = content;
            this.Type = type;
        }

        public Banner(Stream content, Types type)
        {
            using (var sr = new StreamReader(content)) this.Content = sr.ReadToEnd();
            this.Type = type;
        }



        public void Show()
        {
            Console.Clear();
            int maxWidth = renderContents();
            if(maxWidth==0) 
            {
                Console.WriteLine("ERROR404 - no banner");
                maxWidth = 21;
            }
                
            switch (Type)
            {
                case Types.PressToContine:
                    string whatToDo = "Press any button to continue.";
                    int offset = (Console.BufferWidth-whatToDo.Length)/2;
                    Console.WriteLine("\n\n"+new string(' ',offset > 0 ? offset : 0)+whatToDo);
                    Console.ReadKey();
                    break;
                case Types.AutoContinue:
                    Thread.Sleep(WaitTime);
                    break;
            }
        }

        private int renderContents()
        {
            int maxWidth = 0;
            foreach (string line in Content.Split("\n"))
                if(maxWidth<line.Length) maxWidth = line.Length;
            int offset = (Console.BufferWidth-maxWidth)/2;
            if(offset>=0)
            {
                foreach(string line in Content.Split("\n")) 
                    Console.WriteLine(new string(' ',offset)+line);
                return maxWidth;
            }
            return 0;
        }
    }


}