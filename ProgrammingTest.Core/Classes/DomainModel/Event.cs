using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingTest.Core.Classes.DomainModel {
    class Event {
        private List<int?> _regions = new List<int?>();

        public int? EventId { get; set; }
        public List<int?> Regions {
            get {
                return _regions;
            }
        }
    }
}
