using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Contracts.Enums;
using Contracts.Models;
using System.IO;

namespace RevitInteractors
{
    public class RvtToCwElementConverter
    {
        public static CW_Category SetCategoryData(CW_Category cwCategory, Category category)
        {
            if (category != null)
            {
                cwCategory.Id = category.Id.IntegerValue;
                cwCategory.Name = category.Name;
                cwCategory.CategoryType = (CW_CategoryType)(int)category.CategoryType;
            }

            return cwCategory;
        }

        public static CW_Element ConvertElementToCW_Element(Element element)
        {
            if (element is null)
            {
                throw new NullReferenceException();
            }

            var cwElement = new CW_Element();

            if (element.LevelId != null)
            {
                cwElement.LevelId = element.LevelId.IntegerValue;
            }
            else
            {
                cwElement.LevelId = -1;
            }

            if (element.AssemblyInstanceId is ElementId assemblyInstanceElementId)
            {
                cwElement.AssemblyInstanceId = assemblyInstanceElementId.IntegerValue;
            }
            else
            {
                cwElement.AssemblyInstanceId = -1;
            }

            cwElement.Id = element.Id.IntegerValue;
            cwElement.UniqueId = element.UniqueId;
            cwElement.Name = element.Name;
            cwElement.Category = SetCategoryData(new CW_Category(), element.Category);
            cwElement.Document = SetDocumentData(new CW_Document(), element.Document);
            cwElement.Parameters = CreateCwParameterListData(element.Parameters);
            cwElement.Pinned = element.Pinned;


            return cwElement;
        }

        public static CW_Parameter SetParameterData(CW_Parameter cwParameter, Parameter parameter)
        {
            if (parameter.Definition != null)
            {
                cwParameter.StorageType = (CW_StorageType)(int)parameter.StorageType;
                cwParameter.Id = parameter.Id.IntegerValue;
                cwParameter.Value = GetParameterValue(parameter)?.ToString();
                cwParameter.HumanReadableValue = GetParameterReadableValueg(parameter)?.ToString();
                cwParameter.HasValue = parameter.HasValue;
                cwParameter.IsShared = parameter.IsShared;
                cwParameter.IsReadOnly = parameter.IsReadOnly;
                cwParameter.OwnerId = parameter.Element.UniqueId;



                if (parameter.IsShared)
                {
                    cwParameter.GUID = parameter.GUID;
                }
                if (parameter.Definition is ExternalDefinition externalDefinition)
                {
                    cwParameter.Definition = SetExternalDefinition(new CW_ExternalDefinition(), externalDefinition);
                }
                else if (parameter.Definition is InternalDefinition internalDefinition)
                {
                    cwParameter.Definition = SetInternalDefinition(new CW_InternalDefinition(), internalDefinition);
                }

                return cwParameter;
            }
            return null;
        }

        public static CW_Definition SetDefinitionData(CW_Definition cwDefinition, Definition definition)
        {
            cwDefinition.Name = definition.Name;
            cwDefinition.ParameterType = (CW_ParameterType)definition.ParameterType;
            return cwDefinition;
        }

        public static CW_ExternalDefinition SetExternalDefinition(CW_ExternalDefinition cwInternalDefinition, ExternalDefinition externalDefinition)
        {
            cwInternalDefinition = (CW_ExternalDefinition)SetDefinitionData(cwInternalDefinition, externalDefinition);
            cwInternalDefinition.GUID = externalDefinition.GUID;
            cwInternalDefinition.Description = externalDefinition.Description;
            return cwInternalDefinition;
        }

        public static CW_InternalDefinition SetInternalDefinition(CW_InternalDefinition cwInternalDefinition, InternalDefinition internalDefinition)
        {
            cwInternalDefinition = (CW_InternalDefinition)SetDefinitionData(cwInternalDefinition, internalDefinition);
            cwInternalDefinition.Id = internalDefinition.Id.IntegerValue;
            cwInternalDefinition.BuiltInParameter = (int)internalDefinition.BuiltInParameter;
            cwInternalDefinition.UnitType = (CW_UnitType)(int)internalDefinition.UnitType;
            switch (internalDefinition.BuiltInParameter)
            {
                case (BuiltInParameter.ELEM_CATEGORY_PARAM_MT):
                    cwInternalDefinition.BuiltInParameter = (int)BuiltInParameter.ELEM_CATEGORY_PARAM;
                    break;
                case (BuiltInParameter.DPART_ORIGINAL_CATEGORY):
                    cwInternalDefinition.BuiltInParameter = (int)BuiltInParameter.DPART_ORIGINAL_CATEGORY_ID;
                    break;
                default:
                    cwInternalDefinition.BuiltInParameter = (int)internalDefinition.BuiltInParameter;
                    break;
            }
            return cwInternalDefinition;
        }

        public static dynamic GetParameterValue(Parameter parameter)
        {
            if (parameter != null)
            {
                switch (parameter.StorageType)
                {
                    case StorageType.Integer:
                        return parameter.AsInteger();
                    case StorageType.Double:
                        return parameter.AsDouble();
                    case StorageType.String:
                        return parameter.AsString();
                    case StorageType.ElementId:
                        return parameter.AsElementId().IntegerValue;
                }
            }

            return string.Empty;
        }

        public static dynamic GetParameterReadableValueg(Parameter parameter)
        {
            if (parameter != null)
            {
                switch (parameter.StorageType)
                {
                    case StorageType.Integer:
                        return parameter.AsInteger();
                    case StorageType.Double:
                        return parameter.AsDouble();
                    case StorageType.String:
                        return parameter.AsString();
                    case StorageType.ElementId:
                        return parameter.AsValueString();
                }
            }
            return string.Empty;
        }

        public static List<CW_Parameter> CreateCwParameterListData(ParameterSet parameters)
        {
            var cwParameters = new List<CW_Parameter>();
            foreach (Parameter parameter in parameters)
            {
                var cwParameter = SetParameterData(new CW_Parameter(), parameter);
                if (cwParameter != null)
                {
                    cwParameters.Add(SetParameterData(new CW_Parameter(), parameter));
                }
            }
            return cwParameters;
        }

        public static CW_Document SetDocumentData(CW_Document cwDocument, Document document)
        {
            cwDocument.IsWorkshared = document.IsWorkshared;
            cwDocument.Path = document.PathName;
            if (cwDocument.IsWorkshared)
            {
                cwDocument.Path = ModelPathUtils.ConvertModelPathToUserVisiblePath(document.GetWorksharingCentralModelPath());
                cwDocument.Title = Path.GetFileNameWithoutExtension(cwDocument.Path);
            }
            else
            {
                cwDocument.Title = document.Title;
            }
            cwDocument.IsLinked = document.IsLinked;

            return cwDocument;
        }
    }
}
