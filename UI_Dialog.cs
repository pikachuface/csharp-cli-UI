using System;

namespace pikachuface.UI
{
    /// <summary>
    /// Class for creating dialog windows
    /// </summary>
    class Dialog
    {
        /// <summary>
        /// Types of inputs for the dialog window.
        /// </summary>
        public enum InputTypes { OK, YES_NO, YES_NO_serious, YES_NO_CANCEL }
        /// <summary>
        /// Types of answers for the dialoh window.
        /// </summary>
        public enum Answers { YES, NO, OK, CANCEL, NONE }
        /// <summary>
        /// Types of messages for the dialog window.
        /// </summary>
        public enum MessageTypes { GOOD, INFO, CAUTION, WARNING }

        /// <summary>
        /// Text of the dialog window.
        /// </summary>
        public string Text;

        /// <summary>
        /// Defines what inputs can user use for the dialog window.
        /// </summary>
        public InputTypes InputType;
        /// <summary>
        /// Defines what kind of message the dialog window is.
        /// </summary>
        public MessageTypes messageType;

        /// <summary>
        /// Answer given by the user.
        /// </summary>
        public Answers Answer { get; private set; }

        /// <summary>
        /// Constructor for the Dialog class
        /// </summary>
        /// <param name="text">Sets the text of the dialog window.</param>
        /// <param name="messageType">Sets what kind of message the dialog winddow will be.</param>
        /// <param name="inputType">Sets which inputs will be avilable for the user.</param>
        public Dialog(string text, MessageTypes messageType = MessageTypes.INFO, InputTypes inputType = InputTypes.OK)
        {
            this.Text = text;
            this.messageType = messageType;
            this.InputType = inputType;
            this.Answer = Answers.NONE;
        }

        /// <summary>
        ///  Shows the dialog window. The answer is also saved in <see crf="pikachuface.UI.Dialog.Answer"/>
        /// </summary>
        /// <returns>User's answer</returns>
        public Answers Show()
        {
            this.Answer = Show(this.Text, this.messageType, this.InputType);
            return this.Answer;
        }

        /// <summary>
        /// Shows the dialog window.
        /// </summary>
        /// <<param name="text">Sets the text of the dialog window.</param>
        /// <param name="messageType">Sets what kind of message the dialog winddow will be.</param>
        /// <param name="inputType">Sets which inputs will be avilable for the user.</param>
        /// <returns>User's answer</returns>
        public static Answers Show(string text, MessageTypes messageType = MessageTypes.INFO, InputTypes inputType = InputTypes.OK)
        {
            while (true)
            {
                Console.Clear();
                ConsoleColor defaultColor = Console.ForegroundColor;
                switch (messageType)
                {
                    case MessageTypes.GOOD:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case MessageTypes.INFO:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case MessageTypes.CAUTION:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case MessageTypes.WARNING:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
                Console.WriteLine(text);

                Console.ForegroundColor = defaultColor;
                switch (inputType)
                {
                    case InputTypes.OK:
                        Console.WriteLine("Press any key to continue.");
                        break;
                    case InputTypes.YES_NO:
                        Console.WriteLine("y) YES\nn) NO");
                        break;
                    case InputTypes.YES_NO_serious:
                        Console.Write("Write \"yes\" or \"no\": ");
                        break;
                    case InputTypes.YES_NO_CANCEL:
                        Console.WriteLine("y) YES\nn) NO\nq) CANCEL");
                        break;
                }

                string response;
                if (inputType == InputTypes.YES_NO_serious)
                {
                    response = Console.ReadLine().ToLower();
                    if (response == "yes") return Answers.YES;
                    if (response == "no") return Answers.NO;
                }
                else
                {
                    response = Console.ReadKey().KeyChar.ToString();
                    if (inputType == InputTypes.YES_NO_CANCEL && response == "q") return Answers.CANCEL;
                    if (inputType == InputTypes.YES_NO_CANCEL || inputType == InputTypes.YES_NO)
                    {
                        if (response == "y") return Answers.YES;
                        if (response == "n") return Answers.NO;
                    }
                    if (inputType == InputTypes.OK) return Answers.OK;
                }
            }
        }
    }
}