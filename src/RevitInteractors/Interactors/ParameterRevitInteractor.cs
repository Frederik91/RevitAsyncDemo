using Autodesk.Revit.DB;
using System;
using System.Linq;
using System.Collections.Generic;
using Contracts.Requests;
using Contracts.RevitInteractors;

namespace RevitInteractors.Interactors
{
    public class ParameterRevitInteractor : RevitInteractorBase, IParameterRevitInteractor
    {
        public void SetParameters(IEnumerable<SetParameterRequest> setParameterRequests)
        {
            var document = GetDocument();

            using (var trans = new Transaction(document, "Set parameters"))
            {
                if (trans.Start() == TransactionStatus.Started)
                {
                    var groupedRequests = setParameterRequests.GroupBy(x => x.ElementId);

                    foreach (var group in groupedRequests)
                    {
                        var element = document.GetElement(group.First().ElementId);
                        var status = WorksharingUtils.GetCheckoutStatus(element.Document, element.Id);
                        if (status != CheckoutStatus.OwnedByOtherUser)
                        {
                            foreach (var setParameterRequest in group)
                            {
                                Parameter parameter = null;
                                if (int.TryParse(setParameterRequest.ParameterId, out int intValue))
                                {
                                    foreach (Parameter param in element.Parameters)
                                    {
                                        if (param.Id.IntegerValue == intValue)
                                        {
                                            parameter = param;
                                        }
                                    }
                                }

                                if (Guid.TryParse(setParameterRequest.ParameterId, out Guid guid))
                                {
                                    foreach (Parameter param in element.Parameters)
                                    {
                                        if (param.GUID == guid)
                                        {
                                            parameter = param;
                                        }
                                    }
                                }

                                if (parameter == null)
                                {
                                    if (Enum.TryParse(setParameterRequest.ParameterId, out BuiltInParameter bip) && Enum.IsDefined(typeof(BuiltInParameter), bip))
                                    {
                                        parameter = element.get_Parameter(bip);
                                    }
                                    else
                                    {
                                        parameter = element.GetParameters(setParameterRequest.ParameterId).FirstOrDefault();
                                    }
                                }

                                if (parameter != null)
                                {
                                    SetParameter(parameter, setParameterRequest.Value);
                                }
                            }
                        }
                        else
                        {
                            // Could not write parameter
                        }

                    }
                }
                trans.Commit();
            }
        }

        private static void SetParameter(Parameter parameter, string value)
        {
            switch (parameter.StorageType)
            {
                case StorageType.Integer:
                    if (int.TryParse(value, out int integerValue) && parameter.AsInteger() != integerValue)
                    {
                        parameter.Set(integerValue);
                    }
                    break;
                case StorageType.Double:
                    if (double.TryParse(value, out double doubleValue) && parameter.AsDouble() != doubleValue)
                    {
                        parameter.Set(doubleValue);
                    }
                    break;
                case StorageType.String:
                    if (parameter.AsString() != value)
                    {
                        parameter.Set(value);
                    }
                    break;
                case StorageType.ElementId:
                    if (int.TryParse(value, out int elementIdIntegerValue) && parameter.AsElementId()?.IntegerValue != elementIdIntegerValue)
                    {
                        parameter.Set(new ElementId(elementIdIntegerValue));
                    }
                    break;
            }
        }
    }
}

