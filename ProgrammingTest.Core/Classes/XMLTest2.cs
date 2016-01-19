using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using ProgrammingTest.Core.Classes;
using ProgrammingTest.Core.Classes.DomainModel;
using ProgrammingTest.Core.Collections;
using ProgrammingTest.Core.Interfaces;

namespace ProgrammingTest.Core.Classes {
    public class XMLTest2 
        : Interfaces.ITest {

        private string EventFilePath { get; set; }
        private string ExposureFilePath { get; set; }

        public XMLTest2(string eventFilePath, string exposureFilePath) {
            EventFilePath = eventFilePath;
            ExposureFilePath = exposureFilePath;
        }
             
        public IResult RunTest() {
            XMLResult result = new XMLResult();
            
            if (!Common.IsFileAvailable(EventFilePath)) {
                result.Errors.Add(string.Format("File not found: [{0}]", EventFilePath));
            }
            if (!Common.IsFileAvailable(ExposureFilePath)) {
                result.Errors.Add(string.Format("File not found: [{0}]", ExposureFilePath));
            }

            if (!result.HasErrors) {
                XmlDocument eventXMLDoc = new XmlDocument();
                XmlDocument exposureXMLDoc = new XmlDocument();

                eventXMLDoc.Load(EventFilePath);        
                exposureXMLDoc.Load(ExposureFilePath);                        
               
                XmlNodeList eventNodeList = eventXMLDoc.SelectNodes("/events/event");

                EventCompaniesList eventCompaniesCollection = new EventCompaniesList();

                foreach (XmlNode eventNode in eventNodeList) {                    
                    EventCompanies eventCompanies = new EventCompanies(GetNodeIdValue(eventNode));
                    
                    XmlNodeList eventRegionList = eventNode.SelectNodes("region");
                    foreach (XmlNode regionNode in eventRegionList) {
                        string region = regionNode.InnerText;

                        //get all the matching regions in the Exposure file for each region in the in the Events file
                        XmlNodeList matchingRegions = exposureXMLDoc.SelectNodes(string.Format("descendant::company[region='{0}']", region));
                        foreach (XmlNode matchingRegion in matchingRegions) {
                            //get the company id belonging to the matching region
                            string companyId = matchingRegion.FirstChild.InnerText;
                            if (!eventCompanies.CompanyIds.Contains(companyId)) {
                                eventCompanies.CompanyIds.Add(companyId);
                            }
                        }
                    }

                    if (eventCompanies.CompanyIds.Count > 0) {
                        eventCompaniesCollection.Add(eventCompanies);
                    }
                }

                result.SetResult(FormatTestOutput(eventCompaniesCollection));
            }

            return result;
        }

        private string FormatTestOutput(EventCompaniesList eventCompaniesCollection) {
            StringBuilder result = new StringBuilder();

            foreach (EventCompanies eventCompanies in eventCompaniesCollection.OrderByDescending(ec => ec.EventId)) {
                result.Append(string.Format("{0}", eventCompanies.EventId));
                
                eventCompanies.CompanyIds.Sort();
                foreach (string companyid in eventCompanies.CompanyIds) {
                    result.Append(string.Format(" {0}", companyid));
                }
                result.Append("\n");
            }

            return result.ToString();
        }

        private string GetNodeIdValue(XmlNode xmlNode) {
            return xmlNode.SelectSingleNode("id").InnerText;  
        }




        private EventList LoadEventDocument(IResult result) {
            EventList events = new EventList();

            if (!Common.IsFileAvailable(EventFilePath)) {
                result.Errors.Add(string.Format("File not found: [{0}]", EventFilePath));
            }

            XmlDocument eventXMLDoc = new XmlDocument();            
            eventXMLDoc.Load(EventFilePath);

            XmlNodeList eventNodeList = eventXMLDoc.SelectNodes("/events/event");

            foreach (XmlNode eventNode in eventNodeList) {
                Event evt = new Event();
                evt.EventId = Convert.ToInt32(eventNode.SelectSingleNode("id").InnerText);

                XmlNodeList regionNodeList = eventNode.SelectNodes("region");
                foreach (XmlNode regionNode in regionNodeList) {
                    evt.Regions.Add(Convert.ToInt32(regionNode.InnerText));
                }

                events.Add(evt);
            }

            return events;
        }

        private CompanyList LoadExposureDocument(IResult result) {
            CompanyList companies = new CompanyList();

            if (!Common.IsFileAvailable(ExposureFilePath)) {
                result.Errors.Add(string.Format("File not found: [{0}]", ExposureFilePath));
            }

            XmlDocument exposureXMLDoc = new XmlDocument();
            exposureXMLDoc.Load(ExposureFilePath);

            XmlNodeList companyNodeList = exposureXMLDoc.SelectNodes("/companies/company");

            foreach (XmlNode companyNode in companyNodeList) {
                Company company = new Company();
                company.CompanyId = Convert.ToInt32(companyNode.SelectSingleNode("id").InnerText);

                XmlNodeList regionNodeList = companyNode.SelectNodes("region");
                foreach (XmlNode regionNode in regionNodeList) {
                    company.Regions.Add(Convert.ToInt32(regionNode.InnerText));
                }

                companies.Add(company);
            }

            return companies;
        }
    }
}
