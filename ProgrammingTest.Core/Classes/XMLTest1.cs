using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using ProgrammingTest.Core.Interfaces;

namespace ProgrammingTest.Core.Classes {
    public class XMLTest1 
        : ITest {

        private string PuzzleFilePath { get; set; }

        public XMLTest1(string puzzleFilePath) {
            PuzzleFilePath = puzzleFilePath;
        }
        
        public IResult RunTest() {
            XMLResult result = new XMLResult();

            if (!Common.IsFileAvailable(PuzzleFilePath)) {
                result.Errors.Add(string.Format("File not found: [{0}]", PuzzleFilePath));
            }

            if (!result.HasErrors) {
                // Creating the LINQ to XML query 
                var values = from v in XElement.Load(PuzzleFilePath).Elements("value")
                             select v.Value;

                // Executing the query & summing the values
                int sum = 0;
                foreach (var value in values) {
                    sum += Convert.ToInt32(value);
                }

                result.SetResult(string.Format("Sum of values: [{0}]", sum));
            }

            return result;
        }
    }
}
