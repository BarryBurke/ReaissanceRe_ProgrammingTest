using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProgrammingTest.Core.Interfaces;

namespace ProgrammingTest.Core.Classes {
    class XMLResult 
        : IResult {

        List<string> _errors = new List<string>();
        string _result = string.Empty;

        public List<string> Errors {
            get {
                return _errors;
            }
        }

        public bool HasErrors {
            get {
                return _errors.Count > 0;
            }
        }

        public string Value {
            get {
                return _result;
            }
            set {
                if (!_result.Equals(value)) {
                    _result = value;
                }
            }
        }

        public void SetResult(string value) {
            _result = value;
        }
    }
}
