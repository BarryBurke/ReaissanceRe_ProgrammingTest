using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingTest.Core.Classes {
    class EventCompanies {
        private List<string> _companyIds = new List<string>();

        public string EventId { get; set; }

        public List<string> CompanyIds {
            get {
                return _companyIds;
            }
        }

        public EventCompanies(string eventId) {
            EventId = eventId;
        }

    }
}
