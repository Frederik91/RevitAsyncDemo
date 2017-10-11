using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;

namespace RevitInteractors
{
    public class RevitInteractorBase
    {
        private static UIApplication UIApplication
        {
            get => RevitInteractor.UIApplication;
        }

        public static Document GetDocument(string documentTitle = "")
        {       
            if (!string.IsNullOrEmpty(documentTitle))
            {
                var documents = UIApplication.Application.Documents;
                foreach (Document doc in documents)
                {
                    if (doc.Title.Contains(documentTitle))
                    {
                        return doc;
                    }
                }
            }
            else
            {
                return UIApplication.ActiveUIDocument.Document;
            }
            return null;
        }

        public static Element GetElement(Document document, string id)
        {
            Element element = null;
            if (int.TryParse(id, out int intVal))
            {
                element = document.GetElement(new ElementId(intVal));
            }
            else if(!string.IsNullOrEmpty(id))
            {
                element = document.GetElement(id);
            }
            return element;
        }

        public static UIDocument GetUIDocument()
        {
            {
                var uiDocument = UIApplication.ActiveUIDocument;
                return uiDocument;
            }
        }

        public static UIApplication GetUIApplication()
        {
            return UIApplication;
        }

        public static RevitLinkInstance GetLinkByDocumentTitle(string documentTitle)
        {
            var links = new FilteredElementCollector(UIApplication.ActiveUIDocument.Document).WherePasses(new ElementClassFilter(typeof(RevitLinkInstance))).OfType<RevitLinkInstance>();
            var link = links.FirstOrDefault(x => x.GetLinkDocument() is Document doc && doc.Title.Contains(documentTitle));
            return link;
        }
    }
}
