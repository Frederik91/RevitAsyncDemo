using API;
using Contracts.Filtering;
using Contracts.MinimalModels;
using Contracts.Models;
using MyRevitAddinCommand.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System;
using Contracts.Enums;
using Contracts.Requests;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Windows.Controls;
using Contracts.Events;

namespace MyRevitAddinCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly IController m_controller;
        private CW_Category selectedCategory;
        private ObservableCollection<CW_ElementMinimal> elementsInSelectedCategory;
        private ObservableCollection<CW_Category> categories;
        private CW_ElementMinimal selectedElementMinimal;
        private CW_Element selectedElement;
        private ObservableCollection<CW_Parameter> parameters;

        public CW_Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
                GetElementsInSelectedCategory();
            }
        }
        public ObservableCollection<CW_Category> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        public CW_ElementMinimal SelectedElementMinimal
        {
            get => selectedElementMinimal;
            set
            {
                selectedElementMinimal = value;
                GetSelectedElement();
                OnPropertyChanged();
            }
        }

        public CW_Element SelectedElement
        {
            get => selectedElement;
            set
            {
                selectedElement = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CW_Parameter> Parameters
        {
            get => parameters;
            set
            {
                parameters = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<CW_ElementMinimal> ElementsInSelectedCategory
        {
            get => elementsInSelectedCategory;
            set { elementsInSelectedCategory = value; OnPropertyChanged(); }
        }

        public RelayCommand UpdateParametersCommand { get; private set; }

        public MainWindow(IController controller)
        {
            m_controller = controller;
            GetCategories();

            UpdateParametersCommand = new RelayCommand(O => { UpdateParameters(); }, O => HasChanges());

            Closing += MainWindow_Closing;
            controller.DocumentChangedEvent.OnDocumentChanged += DocumentChangedEvent_OnDocumentChanged;

            InitializeComponent();
        }

        private void DocumentChangedEvent_OnDocumentChanged(DocumentChangedEventArgs e)
        {
            GetDocumentChanges(e);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            m_controller.DocumentChangedEvent.OnDocumentChanged -= DocumentChangedEvent_OnDocumentChanged;
        }

        private async void GetDocumentChanges(DocumentChangedEventArgs e)
        {
            if (ElementsInSelectedCategory != null)
            {
                foreach (var addedElementId in e.AddedElementIds)
                {
                    var addedElement = await m_controller.ElementController.Get(e.DocumentTitle, addedElementId);
                    if (addedElement.Category.Name == SelectedCategory.Name)
                    {
                        ElementsInSelectedCategory.Add(new CW_ElementMinimal { Name = addedElement.Name, DocumentTitle = addedElement.Document.Title, Id = addedElement.Id, UniqueId = addedElement.UniqueId });
                    }
                }

                foreach (var modifiedElementId in e.ModifiedElementIds)
                {
                    if (ElementsInSelectedCategory.Any(x => x.Id == modifiedElementId))
                    {
                        var oldElement = ElementsInSelectedCategory.First(x => x.Id == modifiedElementId);
                        var newElement = await m_controller.ElementController.Get(e.DocumentTitle, modifiedElementId);
                        var newElementMinimal = new CW_ElementMinimal { Name = newElement.Name, DocumentTitle = newElement.Document.Title, Id = newElement.Id, UniqueId = newElement.UniqueId };
                        ElementsInSelectedCategory.Remove(oldElement);
                        ElementsInSelectedCategory.Add(newElementMinimal);
                        if (SelectedElement?.Id == modifiedElementId)
                        {
                            SelectedElementMinimal = newElementMinimal;
                            GetSelectedElement();
                        }
                    }
                }

                foreach (var deletedElementId in e.DeletedElementIds)
                {
                    if (ElementsInSelectedCategory.Any(x => x.Id == deletedElementId))
                    {
                        var oldElement = ElementsInSelectedCategory.First(x => x.Id == deletedElementId);
                        ElementsInSelectedCategory.Remove(oldElement);
                    }
                }
            }
        }

        private bool HasChanges()
        {
            if (Parameters != null && Parameters.Any(x => x.Value != x.HumanReadableValue))
            {
                return true;
            }
            return false;
        }

        private async void GetSelectedElement()
        {
            if (SelectedElementMinimal != null)
            {
                SelectedElement = await m_controller.ElementController.Get(SelectedElementMinimal.DocumentTitle, SelectedElementMinimal.UniqueId);
                Parameters = new ObservableCollection<CW_Parameter>(SelectedElement.Parameters.Where(x => x.StorageType != CW_StorageType.ElementId).OrderBy(x => x.Definition.Name));
            }
        }

        private async void UpdateParameters()
        {
            var changedParameters = Parameters?.Where(x => x.HumanReadableValue != x.Value);
            var setParameterRequests = new List<SetParameterRequest>();
            foreach (var changedParameter in changedParameters)
            {
                setParameterRequests.Add(new SetParameterRequest
                {
                    ElementId = changedParameter.OwnerId,
                    ParameterId = changedParameter.Id.ToString(),
                    Value = changedParameter.Value
                });
            }

            await m_controller.ParameterController.Set(setParameterRequests);
            GetSelectedElement();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void GetCategories()
        {
            var result = await m_controller.CategoryController.Get(string.Empty, new List<CW_CategoryType> { CW_CategoryType.Model, CW_CategoryType.Internal }, true);
            Categories = new ObservableCollection<CW_Category>(result.OrderBy(x => x.Name));
        }

        private async void GetElementsInSelectedCategory()
        {
            var elements = await m_controller.FilterController.GetMinimal(new List<string> { string.Empty }, new List<Filter>(), new List<string> { SelectedCategory.Name });
            ElementsInSelectedCategory = new ObservableCollection<CW_ElementMinimal>(elements.OrderBy(x => x.Name));
        }
    }
}
