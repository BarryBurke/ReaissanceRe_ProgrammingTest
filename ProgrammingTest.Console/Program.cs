using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ProgrammingTest.Core.Classes;
using ProgrammingTest.Core.Interfaces;

namespace ProgrammingTest.Console {
    class Program {
        private static ITest _test = null;

        private static void Main(string[] args) {
            if (args.Length == 0) {
                ExecutePuzzleTest();
            }
            else if (args.Length == 2) {
                ExecuteEventExposureTest(args);
                
            }
            else {
                DisplayOutput("Invalid arguments supplied");
            }
            System.Console.Read();
        }

        private static void ExecutePuzzleTest() {                       
            _test = new XMLTest1(Path.Combine(GetExecutingDirectory(), "puzzle1.xml"));

            IResult result = _test.RunTest();

            DisplayOutput(result);
        }

        private static void ExecuteEventExposureTest(string[] args) {                        
            _test = new XMLTest2(Path.Combine(GetExecutingDirectory(), args[0]), Path.Combine(GetExecutingDirectory(), args[1]));

            IResult result = _test.RunTest();

            DisplayOutput(result);
        }
        
        private static void DisplayOutput(IResult result) {
            if (result.Errors.Count > 0) {
                foreach (string error in result.Errors) {
                    DisplayOutput(error);
                }
            }
            else {
                DisplayOutput(result.Value);
            }
        }

        private static void DisplayOutput(string result) {
            System.Console.WriteLine(result);
           
        }

        private static string GetExecutingDirectory() {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
