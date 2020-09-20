using System;
using System.IO;
using System.Threading;
using System.Timers;

namespace pikachuface.UI
{

    class Banner
    {
        /// <summary>
        /// Types of banner.
        /// </summary>
        public enum Types { PressToContine, AutoContinue }
        /// <summary>
        /// Content of the banner (e.g. Ascii Art)
        /// </summary>
        public string Content;
        /// <summary>
        /// Type of this banner.
        /// </summary>
        /// <value>If you have to click or wait for the banner to disappear.</value>
        public Types Type{get; private set;}
        /// <summary>
        /// How long will user wait for the banner(Only if you have set type to <code>AutoContine</code>).
        /// </summary>
        /// <value>in milliseconds</value>
        public int WaitTime = 1000;

        /// <summary>
        /// Ctor for Banner class.
        /// </summary>
        /// <param name="content">What will be displayed.</param>
        /// <param name="type">How will user interact with the banner.</param>
        public Banner(string content, Types type)
        {
            this.Content = content;
            this.Type = type;
        }

        /// <summary>
        /// Ctor for Banner class.
        /// </summary>
        /// <param name="content">What will be displayed.</param>
        /// <param name="type">How will user interact with the banner.</param>
        public Banner(Stream content, Types type)
        {
            using (var sr = new StreamReader(content)) this.Content = sr.ReadToEnd();
            this.Type = type;
        }


        /// <summary>
        /// Shows the banner
        /// </summary>    
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

        /// <summary>
        /// Renders the conntent on the screen.
        /// </summary>
        /// <returns>Max width of the banner(<code>0</code> if there is no content)</returns>
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