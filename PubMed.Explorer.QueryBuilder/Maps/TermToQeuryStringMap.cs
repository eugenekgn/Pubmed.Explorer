using System.Collections.Generic;
using PubMed.Explorer.Entities.Enums;

namespace PubMed.Explorer.QueryBuilder.Maps
{

    public static class TermToQeuryTermMap
    {
        private static readonly Dictionary<PubMedTerms, string> pubMedTermTextMap;

        static TermToQeuryTermMap()
        {
            pubMedTermTextMap = GetPubMedTermCollection();
        }

        public static string GetQueryTerm(PubMedTerms term)
        {
            return pubMedTermTextMap[term];
        }

        private static Dictionary<PubMedTerms, string> GetPubMedTermCollection()
        {
            return new Dictionary<PubMedTerms, string>
            {
                {PubMedTerms.Affiliation, "Affiliation"},
                {PubMedTerms.Author, "Author"},
                {PubMedTerms.AuthorCorporate, "Author - Corporate"},
                {PubMedTerms.AuthorFirst, "Author - First"},
                {PubMedTerms.AuthorFull, "Author - Full"},
                {PubMedTerms.AuthorIdentifier, "Author - Identifier"},
                {PubMedTerms.AuthorLast, "Author - Last"},
                {PubMedTerms.Book, "Book"},
                {PubMedTerms.DateMeSH, "Date - MeSH"},
                {PubMedTerms.DateModification, "Date - Modification"},
                {PubMedTerms.DatePublication, "Date - Publication"},
                {PubMedTerms.DateCreate, "Date - Create"},
                {PubMedTerms.ECRNNumber, "EC/RN Number"},
                {PubMedTerms.Editor, "Editor"},
                {PubMedTerms.Filter, "Filter"},
                {PubMedTerms.GrantNumber, "GrantNumber"},
                {PubMedTerms.ISBN, "ISBN"},
                {PubMedTerms.Investigator, "Investigator"},
                {PubMedTerms.InvestigatorFull, "Investigator - Full"},
                {PubMedTerms.Issue, "Issue"},
                {PubMedTerms.Journal, "Journal"},
                {PubMedTerms.Language, "Language"},
                {PubMedTerms.LocationId, "Location ID"},
                {PubMedTerms.MeSHMajorTopic, "MeSH Major Topic"},
                {PubMedTerms.MeSHSubheading, "MeSH Subheading"},
                {PubMedTerms.MeSHTerms, "MeSH Terms"},
                {PubMedTerms.OtherTerm, "Other Term"},
                {PubMedTerms.Pagination, "Pagination"},
                {PubMedTerms.PharmacologicalAction, "Pharmacological Action"},
                {PubMedTerms.PublicationType, "Publication Type"},
                {PubMedTerms.Publisher, "Publisher"},
                {PubMedTerms.SecondarySourceId, "Secondary Source ID"},
                {PubMedTerms.SubjectPersonalName, "Subject - Personal Name"},
                {PubMedTerms.SupplementaryConcept, "Supplementary Concept"},
                {PubMedTerms.TextWord, "Text Word"},
                {PubMedTerms.Title, "Title"},
                {PubMedTerms.TitleAbstract, "Title/Abstract"},
                {PubMedTerms.TransliteratedTitle, "Transliterated Title"},
                {PubMedTerms.Volume, "Volume"},

            };
        }
    }


}
