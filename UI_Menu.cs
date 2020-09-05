using System;
using System.Collections.Generic;

namespace pikachuface.UI
{
    /// <summary>
    /// Class for creating simple menu
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Name of the Menu.
        /// </summary>
        public string Name;
        /// <summary>
        /// Title that will be writen on top of the menu.
        /// </summary>
        public string Title;
        /// <summary>
        /// Description of the menu.
        /// </summary>
        public string Description;
        /// <summary>
        /// Amount of entris in the menu.
        /// </summary>
        public int EntriesCount { get => menuEntries.Count; }

        /// <summary>
        /// All of the menu entries.
        /// </summary>
        private Dictionary<int, object[]> menuEntries;


        /// <summary>
        /// Constructor for class <see cref="pikachuface.UI.Menu" />
        /// </summary>
        public Menu()
        {
            this.menuEntries = new Dictionary<int, object[]>();
        }

        /// <summary>
        /// Constructor for class <see cref="pikachuface.UI.Menu"/>
        /// </summary>
        /// <param name="title">Sets the <see cref="pikachuface.UI.Menu.Title"/> of the menu.</param>
        /// <param name="description">Sets the <see cref="pikachuface.UI.Menu.Description"/> for the menu.</param>
        public Menu(string title, string description = null)
        {
            this.menuEntries = new Dictionary<int, object[]>();
            if (Title != null) this.Title = title;
            if (description != null) this.Description = description;
        }


        /// <summary>
        /// Shows the menu.
        /// </summary>
        /// <param name="repeat">If false the menu will close after finishing one entry.</param>
        public void Show(bool repeat = false)
        {
            bool correctInput = false;
            ConsoleKeyInfo keyPress;
            do
            {
                Console.Clear();
                if (Title != default(string)) Console.WriteLine($"--{Title.ToUpper()}--");
                if (Description != default(string)) Console.WriteLine(Description + "\n" + new String('-', Title.Length + 4));
                foreach (var entry in menuEntries)
                    Console.WriteLine(idToChar(entry.Key) + ") " + (string)entry.Value[0]);
                Console.WriteLine("\nEsc) Exit menu");
                keyPress = Console.ReadKey(true);
                if (menuEntries.TryGetValue(charToID(keyPress.KeyChar), out object[] output))
                {
                    Action temp = (Action)output[1];
                    temp.Invoke();
                    correctInput = true;
                }
            } while ((repeat ||!correctInput)&& keyPress.Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Adds entry to the menu.
        /// </summary>
        /// <param name="text">Text of the entry.</param>
        /// <param name="method">Method which will be called on opening the entry.</param>
        public void AddEntry(string text, Action method)
        {
            int ID = 1;
            while (!menuEntries.TryAdd(ID, new object[] { text, method }))
            {
                ID++;
                if (ID > 64) throw new Exception($"Too many entries, above {64} in {this.Name} menu!");
            }
        }

        /// <summary>
        /// Adds entry to the menu
        /// </summary>
        /// <param name="text">Text of the entry.</param>
        /// <param name="method">Method which will be called on opening the entry.</param>
        /// <param name="key">Under which char will the wntry will be.</param>
        public void AddEntry(string text, Action method, char key)
        {
            if (!menuEntries.TryAdd(charToID(key), new object[] { text, method }))
                throw new Exception($"Entry in {this.Name} menu under char {key} already exists!");
        }

        /// <summary>
        /// Converts char into ID which will be stored in <see cref="pikachuface.UI.Menu.menuEntries">.
        /// </summary>
        /// <returns>The ID of the char.</returns>
        private int charToID(char c)
        {
            return (int)c % 32 + (Char.IsUpper(c) ? 32 : 0);
        }

        /// <summary>
        /// Converts ID back to the char value.
        /// </summary>
        /// <returns>Original char value.</returns>
        private char idToChar(int id)
        {
            return Convert.ToChar(id + (id <= 32 ? 96 : 64));
        }
    }
}