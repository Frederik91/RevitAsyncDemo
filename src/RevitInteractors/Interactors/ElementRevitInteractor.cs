using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Models;
using Contracts.Requests;
using Contracts.RevitInteractors;

namespace RevitInteractors.Interactors
{
    public class ElementRevitInteractor : RevitInteractorBase, IElementRevitInteractor
    {
        public IEnumerable<CW_Element> Get(string documentTitle, string uniqueId)
        {
            var cwElements = new List<CW_Element>();
            if (GetDocument(documentTitle) is Document document)
            {
                var rvtElements = new List<Element>();

                if (string.IsNullOrEmpty(uniqueId))
                {
                    rvtElements = new FilteredElementCollector(document).WhereElementIsNotElementType().ToList();
                    var typeElements = new FilteredElementCollector(document).WhereElementIsElementType();
                    rvtElements.AddRange(typeElements);
                }
                else
                {
                    var element = document.GetElement(uniqueId);
                    if (element != null)
                    {
                        rvtElements.Add(element);
                    }
                }

                foreach (var element in rvtElements)
                {
                    var cwElement = RvtToCwElementConverter.ConvertElementToCW_Element(element);
                    if (cwElement != null)
                    {
                        cwElements.Add(cwElement);
                    }
                }
            }

            return cwElements;
        }

        public void Delete(IEnumerable<DeleteElementRequest> requests)
        {
            var document = GetDocument(string.Empty);

            using (var trans = new Transaction(document, "Delete element"))
            {
                if (trans.Start() == TransactionStatus.Started)
                {
                    foreach (var request in requests)
                    {
                        var element = GetElement(document, request.ElementId);
                        if (element != null)
                        {
                            document.Delete(element.Id);
                        }
                    }
                }
                trans.Commit();
            }
        }
    }
}
