using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingTest.Core.Interfaces {
    public interface IResult {
        List<string> Errors { get; }
        string Value { get; }
    }
}
