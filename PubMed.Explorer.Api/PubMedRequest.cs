using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using PubMed.Explorer.Api.Constants;
using PubMed.Explorer.Api.Enums;
using PubMed.Explorer.Api.Models;
using PubMed.Explorer.Utils;
using RestSharp.Portable;

namespace PubMed.Explorer.Api
{
    public class PubMedRequest
    {
        private readonly string databaseName;

        public PubMedRequest(EntrezDatabaseTypes dbType)
        {
            databaseName = dbType.ToString();
        }
        public PubMedPublicationIdsResult GetPublicationsIds(PubMedQueryFilter filter)
        {
            var restClient = new RestClient(ServiceURLs.ESearchBaseURL);

            var restRequest = new RestRequest();
            restRequest.AddParameter("db", databaseName, ParameterType.QueryString);
            restRequest.AddParameter("retmode", "json", ParameterType.QueryString);
            restRequest.AddParameter("retstart", (filter.Skip * filter.Take), ParameterType.QueryString);
            restRequest.AddParameter("term", filter.Query, ParameterType.QueryString);
            restRequest.AddParameter("retmax", filter.Take, ParameterType.QueryString);
            if (filter.RelDate != DateTime.MinValue)
            {
                var pmDate = PubMedDateOperations.DatetimeToPubMedDate(filter.RelDate);
                restRequest.AddParameter("reldate", pmDate, ParameterType.QueryString);
            }

            var waitTime = PubMedThrottler.GetWaitTime();
            Thread.Sleep(waitTime);
            var response = restClient.Execute<PubMedResponse>(restRequest).Result;

            if (response.Data == null)
                throw new Exception("No Response From The Server");

            var result = new PubMedPublicationIdsResult();
            result.PubMedIdCollection = new List<string>();
            response.Data.esearchresult.idlist.ForEach(r => result.PubMedIdCollection.Add(r));

            return result;
        }

        public string GetPublicationSummaries(List<string> publicationIds)
        {
            var restClient = new RestClient(ServiceURLs.ESummaryBaseURL);

            var restRequest = new RestRequest();
            restRequest.AddParameter("db", databaseName, ParameterType.QueryString);
            restRequest.AddParameter("retmode", "json", ParameterType.QueryString);
            restRequest.AddParameter("rettype", "abstract", ParameterType.QueryString);
            restRequest.AddParameter("id", string.Join(", ", publicationIds.ToArray()), ParameterType.QueryString);
            restRequest.AddParameter("version", "2.0", ParameterType.QueryString);

            // Get the response.
            var waitTime = PubMedThrottler.GetWaitTime();
            Thread.Sleep(waitTime);
            var response = restClient.Execute(restRequest).Result;
            var rawBytes = response.RawBytes;

            // Deserialize the XML result.
            var deserializer = new XmlSerializer(typeof(eSummaryResult));
            var result = Encoding.UTF8.GetString(rawBytes, 0, rawBytes.Length);



            return result;
        }
    }


    internal class SummaryInternalClassToPublicConverter
    {
        public Summary Convert(eSummaryResult eSummaryResult)
        {
            // Generate the summary.
            Summary summary = new Summary();
            summary.ID = eSummaryResult.DocSum.Id.ToString();

            // Generate the neccesary components.
            var factory = new ConcreteSummaryItemConverterFactory();

            // Navigate through the xml summary list and convert it to the public summary.
            var items = eSummaryResult.DocSum.Item;
            foreach (var item in items)
            {
                ISummaryValueConverter valueConverter = factory.GetAppropriateConverter(item);
                if (valueConverter != null) valueConverter.AddItemToSummary(item, ref summary);
            }

            // Return the generated summary.
            return summary;
        }
    }


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class eSummaryResult
    {

        private eSummaryResultDocSum docSumField;

        /// <remarks/>
        public eSummaryResultDocSum DocSum
        {
            get
            {
                return this.docSumField;
            }
            set
            {
                this.docSumField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eSummaryResultDocSum
    {

        private uint idField;

        private eSummaryResultDocSumItem[] itemField;

        /// <remarks/>
        public uint Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public eSummaryResultDocSumItem[] Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eSummaryResultDocSumItem
    {

        private eSummaryResultDocSumItemItem[] itemField;

        private string[] textField;

        private string nameField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public eSummaryResultDocSumItemItem[] Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eSummaryResultDocSumItemItem
    {

        private string nameField;

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


    //Summery
    public class Summary
    {
        public Summary()
        {
            AuthorList = new List<Author>();
            History = new List<History>();
            ArticleIDs = new List<ArticleID>();
            LangList = new List<string>();
        }

        public string ID { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public DateTime? PubDate { get; set; }
        public DateTime? EPubDate { get; set; }
        public List<Author> AuthorList { get; set; }
        public List<History> History { get; set; }
        public List<ArticleID> ArticleIDs { get; set; }
        public string LastAuthor { get; set; }
        public List<string> LangList { get; set; }
        public string Volume { get; set; }
        public string Issue { get; set; }
        public string Pages { get; set; }
        public string NlmUniqueID { get; set; }
        public string ISSN { get; set; }
        public string ESSN { get; set; }
        public List<string> PubTypeList { get; set; }
        public string RecordStatus { get; set; }
        public string PubStatus { get; set; }
        public string DOI { get; set; }
        public int HasAbstract { get; set; }
        public int PmcRefCount { get; set; }
        public string FullJournalName { get; set; }
        public string ELocationID { get; set; }
        public string SO { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class History
    {
        public string HistoryEntryType { get; set; }
        public DateTime? Date { get; set; }
    }

    public class ArticleID
    {
        public string IDKey { get; set; }
        public string IDValue { get; set; }
    }


    internal interface ISummaryValueConverter
    {
        void AddItemToSummary(eSummaryResultDocSumItem item, ref Summary summary);
    }

    internal interface ISummaryItemConverterFactory
    {
        ISummaryValueConverter GetAppropriateConverter(eSummaryResultDocSumItem item);
    }

    internal class ConcreteSummaryItemConverterFactory : ISummaryItemConverterFactory
    {
        public ISummaryValueConverter GetAppropriateConverter(eSummaryResultDocSumItem item)
        {
            // Check if it can be converted based on type.
            switch (item.Type.ToLower())
            {
                case "string":
                    return new StringSummaryValueConverter();
                case "date":
                    return new DateSummaryValueConverter();
                case "integer":
                    return new DateSummaryValueConverter.IntegerSummaryValueConverter();
                case "list":
                    return new DateSummaryValueConverter.ListSummaryValueConverter();
            }

            // Check for specific lists.
            if (item.Name == "AuthorList")
            {

            }

            return null;
        }
    }


    internal abstract class BaseSimpleSummaryValueConverter : ISummaryValueConverter
    {
        public void AddItemToSummary(eSummaryResultDocSumItem item, ref Summary summary)
        {
            // Check if there is actually text.
            if (!IsListRoot(item))
            {
                if (item.Text == null)
                {
                    return;
                }
                if (String.IsNullOrEmpty(item.Text[0]))
                {
                    return;
                }
            }


            var properties = summary.GetType().GetRuntimeProperties();
            var propertyInfos = properties as PropertyInfo[] ?? properties.ToArray();
            if (propertyInfos.Any(Predicate(item)))
            {
                var propertyInfo = propertyInfos.Single(Predicate(item));

                if (IsListRoot(item))
                {
                    if (propertyInfo != null) propertyInfo.SetValue(summary, GetObjectViaListValue(item));
                }
                else
                {
                    if (propertyInfo != null) propertyInfo.SetValue(summary, GetObjectValue(item.Text[0]));
                }
            }
        }

        private bool IsListRoot(eSummaryResultDocSumItem item)
        {
            return item.Type.ToLower() == "list";
        }

        protected abstract object GetObjectValue(string text);
        protected abstract object GetObjectViaListValue(eSummaryResultDocSumItem item);

        private static Func<PropertyInfo, bool> Predicate(eSummaryResultDocSumItem item)
        {
            return info => String.Equals(info.Name, item.Name, StringComparison.CurrentCultureIgnoreCase);
        }
    }


    internal class StringSummaryValueConverter : BaseSimpleSummaryValueConverter
    {
        protected override object GetObjectValue(string text)
        {
            return text;
        }

        protected override object GetObjectViaListValue(eSummaryResultDocSumItem item)
        {
            throw new NotImplementedException();
        }
    }

    internal class DateSummaryValueConverter : BaseSimpleSummaryValueConverter
    {
        protected override object GetObjectValue(string text)
        {
            return ConvertTextToDateTime(text);
        }

        protected override object GetObjectViaListValue(eSummaryResultDocSumItem item)
        {
            throw new NotImplementedException();
        }

        private DateTime? ConvertTextToDateTime(string text)
        {
            DateTime time;
            var parsedSuccessfully = DateTime.TryParse(text, out time);
            if (parsedSuccessfully)
            {
                return time;
            }

            return null;
        }

        internal class IntegerSummaryValueConverter : BaseSimpleSummaryValueConverter
        {
            protected override object GetObjectValue(string text)
            {
                return Convert.ToInt32(text);
            }

            protected override object GetObjectViaListValue(eSummaryResultDocSumItem item)
            {
                throw new NotImplementedException();
            }
        }

        internal interface IListTypeDeterminer
        {
            Type DetermineListType(eSummaryResultDocSumItem item);
        }

        internal class BasicListTypeDeterminer : IListTypeDeterminer
        {
            public Type DetermineListType(eSummaryResultDocSumItem item)
            {
                switch (item.Item[0].Type.ToLower())
                {
                    case "string":
                        return typeof(string);
                    case "date":
                        return typeof(DateTime);
                    case "integer":
                        return typeof(int);
                }

                return null;
            }
        }

        internal class ListSummaryValueConverter : BaseSimpleSummaryValueConverter
        {
            protected override object GetObjectValue(string text)
            {
                throw new NotImplementedException();
            }

            protected override object GetObjectViaListValue(eSummaryResultDocSumItem item)
            {
                // Determine if a custom list is needed.
                if (CustomListNeeded(item))
                {
                    return DoCustomList(item);
                }

                var listTypeDeterminer = new BasicListTypeDeterminer();
                Type listType = listTypeDeterminer.DetermineListType(item);

                // Generate a list of that type.
                var genericListType = typeof(List<>);
                var constructedListType = genericListType.MakeGenericType(listType);
                var instance = Activator.CreateInstance(constructedListType) as IList;

                // Add all the items of that type.
                foreach (var subItem in item.Item)
                {
                    switch (listType.Name.ToLower())
                    {
                        case "string":
                            instance.Add(subItem.Value);
                            break;
                        case "date":
                            instance.Add(ConvertTextToDateTime(subItem.Value));
                            break;
                        case "integer":
                            instance.Add(Convert.ToInt32(subItem.Value));
                            break;
                    }
                }

                return instance;
            }

            private DateTime? ConvertTextToDateTime(string text)
            {
                DateTime time;
                var parsedSuccessfully = DateTime.TryParse(text, out time);
                if (parsedSuccessfully)
                {
                    return time;
                }

                return null;
            }

            private object DoCustomList(eSummaryResultDocSumItem item)
            {
                IComplexListGenerator complexListGenerator = null;
                switch (item.Name.ToLower())
                {
                    case "authorlist":
                        complexListGenerator = new AuthorsListGenerator();
                        break;
                    case "articleids":
                        complexListGenerator = new ArticleIDsListGenerator();
                        break;
                    case "history":
                        complexListGenerator = new HistoryListGenerator();
                        break;
                }

                if (complexListGenerator != null) return complexListGenerator.GenerateList(item);
                return null;
            }

            private bool CustomListNeeded(eSummaryResultDocSumItem item)
            {
                switch (item.Name.ToLower())
                {
                    case "authorlist":
                        return true;
                    case "articleids":
                        return true;
                    case "history":
                        return true;
                }


                return false;
            }
        }



        internal interface IComplexListGenerator
        {
            object GenerateList(eSummaryResultDocSumItem baseListItem);
        }

        internal class ArticleIDsListGenerator : IComplexListGenerator
        {
            public object GenerateList(eSummaryResultDocSumItem baseListItem)
            {
                var articleIds = from eSummaryResultDocSumItemItem item in baseListItem.Item
                                 select new ArticleID
                                 {
                                     IDKey = item.Name,
                                     IDValue = item.Value
                                 };
                return articleIds.ToList();
            }
        }

        internal class HistoryListGenerator : IComplexListGenerator
        {
            public object GenerateList(eSummaryResultDocSumItem baseListItem)
            {
                var histories = from eSummaryResultDocSumItemItem item in baseListItem.Item
                                select new History
                                {
                                    HistoryEntryType = item.Name,
                                    Date = ConvertTextToDateTime(item.Value)
                                };
                return histories.ToList();
            }

            private DateTime? ConvertTextToDateTime(string text)
            {
                DateTime time;
                var parsedSuccessfully = DateTime.TryParse(text, out time);
                if (parsedSuccessfully)
                {
                    return time;
                }

                return null;
            }
        }

        internal class AuthorsListGenerator : IComplexListGenerator
        {
            public object GenerateList(eSummaryResultDocSumItem baseListItem)
            {
                var authors = from item in baseListItem.Item
                              select new Author
                              {
                                  Name = item.Value,
                                  Type = item.Name
                              };
                return authors.ToList();
            }
        }
    }
}
