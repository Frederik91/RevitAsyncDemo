using System.Collections.Generic;

namespace Contracts.Events
{
    public class DocumentChangedEventArgs
    {
        public IEnumerable<string> TransactionNames { get; set; }
        public string DocumentTitle { get; set; }
        public IEnumerable<int> AddedElementIds { get; private set; }
        public IEnumerable<int> ModifiedElementIds { get; private set; }
        public IEnumerable<int> DeletedElementIds { get; private set; }

        public DocumentChangedEventArgs(IEnumerable<string> transactionName, string documentTitle, IEnumerable<int> addedElementIds, IEnumerable<int> modifiedElementIds, IEnumerable<int> deletedElementIds)
        {
            TransactionNames = transactionName;
            DocumentTitle = documentTitle;
            AddedElementIds = addedElementIds;
            ModifiedElementIds = modifiedElementIds;
            DeletedElementIds = deletedElementIds;
        }
    }
}
