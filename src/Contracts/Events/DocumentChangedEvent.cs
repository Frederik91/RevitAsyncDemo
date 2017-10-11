using System.Collections.Generic;

namespace Contracts.Events
{
    public class DocumentChangedEvent
    {
        public delegate void DocumentChangedHandler(DocumentChangedEventArgs e);
        public event DocumentChangedHandler OnDocumentChanged;

        public void DocumentChanged(IEnumerable<string> transactionNames, string documentTitle, IEnumerable<int> addedElementIds, IEnumerable<int> modifiedElementIds, IEnumerable<int> deletedElementIds)
        {
            // Make sure someone is listening to event
            OnDocumentChanged?.Invoke(new DocumentChangedEventArgs(transactionNames, documentTitle, addedElementIds, modifiedElementIds, deletedElementIds));
        }
    }
}
