using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.DeploymentTool.Helpers {
    internal static class MessageHelper {
        internal static void DisplayMessage(MessageType type, string message) {
            switch (type) {
                case MessageType.Message:
                    DisplayInfoMessage(message);
                    break;

                case MessageType.Warning:
                    DisplayWarningMessage(message);
                    break;

                case MessageType.Error:
                    DisplayErrorMessage(message);
                    break;

                default:
                    break;
            }
        }

        internal static void DisplayMessage(MessageType type, Exception ex) {
            DisplayMessage(type, ex.Message);
        }

        private static void DisplayInfoMessage(string message) {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        private static void DisplayWarningMessage(string message) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Warning! ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        private static void DisplayErrorMessage(string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error! ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}
