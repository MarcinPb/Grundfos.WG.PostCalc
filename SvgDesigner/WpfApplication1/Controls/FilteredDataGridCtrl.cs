using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApplication1.Controls
{
    class FilteredDataGridCtrl : DataGrid
    {
        /*
        // Dictionary to keep SortDescriptions per ItemSource
        private readonly Dictionary<object, List<SortDescription>> m_SortDescriptions =
            new Dictionary<object, List<SortDescription>>();

        protected override void OnSorting(DataGridSortingEventArgs eventArgs)
        {
            base.OnSorting(eventArgs);
            UpdateSorting();
        }
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            ICollectionView view = CollectionViewSource.GetDefaultView(newValue);
            if (view == null) return;
            view.SortDescriptions.Clear();

            // reset SortDescriptions for new ItemSource
            if (m_SortDescriptions.ContainsKey(newValue))
                foreach (SortDescription sortDescription in m_SortDescriptions[newValue])
                {
                    view.SortDescriptions.Add(sortDescription);

                    // I need to tell the column its SortDirection,
                    // otherwise it doesn't draw the triangle adornment
                    DataGridColumn column = Columns.FirstOrDefault(c => c.SortMemberPath == sortDescription.PropertyName);
                    if (column != null)
                        column.SortDirection = sortDescription.Direction;
                }
        }

        // Store SortDescriptions in dictionary
        private void UpdateSorting()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);
            m_SortDescriptions[ItemsSource] = new List<SortDescription>(view.SortDescriptions);
        }
        */









        /// <summary>
        /// This dictionary will have a list of all applied filters
        /// </summary>
        private Dictionary<string, string> columnFilters;

        /// <summary>
        /// Cache with properties for better performance
        /// </summary>
        private Dictionary<string, PropertyInfo> propertyCache;

        private bool IsFilteringCaseSensitive = true;

        public static DependencyProperty IsSensitiveProperty =
             DependencyProperty.Register("IsSensitive", typeof(int?), typeof(FilteredDataGridCtrl), new PropertyMetadata(null));

        /// <summary>
        /// Case sensitive filtering
        /// </summary>
        public int? IsSensitive
        {
            get { return (int?)(GetValue(IsSensitiveProperty)); }
            set { SetValue(IsSensitiveProperty, value); }
        }


        public FilteredDataGridCtrl()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);


            // Initialize lists
            columnFilters = new Dictionary<string, string>();
            propertyCache = new Dictionary<string, PropertyInfo>();

            AddHandler(Button.ClickEvent, new RoutedEventHandler(OnClick), true);
            AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(OnTextChanged), true);

            // Datacontext changed, so clear the cache
            DataContextChanged += new DependencyPropertyChangedEventHandler(FilteringDataGrid_DataContextChanged);

            Loaded += FilteringDataGrid_Loaded;


            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(this, ThisIsCalledWhenPropertyIsChanged);
            }

        }
        private void ThisIsCalledWhenPropertyIsChanged(object sender, EventArgs e)
        {
            columnFilters.Clear();

            //var header = (DataGridColumnHeader)((DataGrid) this).Columns[1].Header;
            //Button filterButton = UiHelper.FindVisualChild<Button>(this, "btnFilter");
            var columns = ((DataGrid)this).Columns;
            foreach (var column in columns)
            {
                //var header3 = (DataGridColumn.)((DataGridColumn)columns[0]).Header;
                DataGridColumnHeader headerObj = GetColumnHeaderFromColumn(column);

                Popup filterPopup = UiHelper.FindVisualChild<Popup>(headerObj, "popCountry");
                Border filterBorder = (Border)filterPopup.Child;
                TextBox filterTextBox = (TextBox)filterBorder.Child;
                //TextBox filterTextBox = UiHelper.FindVisualChild<TextBox>(filterPopup, "filterText");
                //var tx = LogicalTreeHelper.GetChildren(this);
                //TextBox filterTextBox = UiHelper.FindVisualChild<TextBox>(header, "filterText");
                filterTextBox.Text = string.Empty;               
            }
        }
        private DataGridColumnHeader GetColumnHeaderFromColumn(DataGridColumn column)
        {
            // dataGrid is the name of your DataGrid. In this case Name="dataGrid"
            List<DataGridColumnHeader> columnHeaders = GetVisualChildCollection<DataGridColumnHeader>(this);
            foreach (DataGridColumnHeader columnHeader in columnHeaders)
            {
                if (columnHeader.Column == column)
                {
                    return columnHeader;
                }
            }
            return null;
        }

        public List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

        private void FilteringDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);
            if (view != null)
            {
                var del = view.Filter;
                var cnt = IsSensitive;
            }
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            // Get the button
            Button buttonFilter = e.OriginalSource as Button;
            if (buttonFilter == null || buttonFilter.Name != "btnFilter") return;

            var parent = LogicalTreeHelper.GetParent(buttonFilter) as Grid;
            var parent2 = LogicalTreeHelper.GetParent(parent);
            var parent3 = LogicalTreeHelper.GetParent(parent2) as Grid;
            var parent4 = LogicalTreeHelper.GetParent(parent3) as Grid;
            var popUp = parent4.Children[1] as Popup;
            popUp.IsOpen = true;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the textbox
            TextBox filterTextBox = e.OriginalSource as TextBox;
            if (filterTextBox == null || filterTextBox.Name != "filterText") return;

            //var g = VisualTreeHelper.GetChild(txtBox, 0);
            var parent = LogicalTreeHelper.GetParent(filterTextBox) as Border;
            var parent2 = LogicalTreeHelper.GetParent(parent) as Popup;
            var parent3 = LogicalTreeHelper.GetParent(parent2) as Grid;

            Button filterButton = UiHelper.FindVisualChild<Button>(parent3, "btnFilter");
            filterButton.ToolTip = string.IsNullOrEmpty(filterTextBox.Text) ? "<not set>" : filterTextBox.Text;
            //filterButton.Content = string.IsNullOrEmpty(filterTextBox.Text) ? "Off" : "On";
            Border filterBorder = UiHelper.FindVisualChild<Border>(filterButton, "brdFilter");
            //filterBorder.BorderThickness = new Thickness(string.IsNullOrEmpty(filterTextBox.Text) ? 0 : 1);
            filterBorder.BorderBrush = string.IsNullOrEmpty(filterTextBox.Text) ? Brushes.LightGray : Brushes.Red;

            // Get the header of the textbox
            //DataGridColumnHeader header = TryFindParent<DataGridColumnHeader>(filterTextBox);
            var header = VisualTreeHelper.GetParent(parent3) as DataGridColumnHeader;

            //var par1 = VisualTreeHelper.GetParent(header);

            if (header != null)
            {
                UpdateFilter(filterTextBox, header);
                ApplyFilters();
            }
        }

        /// <summary>
        /// Clear the property cache if the datacontext changes.
        /// This could indicate that an other type of object is bound.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilteringDataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);
            if (view != null)
            {
                var del = view.Filter;
            }
            propertyCache.Clear();
        }

        /// <summary>
        /// Update the internal filter
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="header"></param>
        private void UpdateFilter(TextBox textBox, DataGridColumnHeader header)
        {
            // Try to get the property bound to the column.
            // This should be stored as datacontext.
            //string columnBinding = header.DataContext != null ? header.DataContext.ToString() : "";


            Binding bnd;
            DataGridTextColumn dgtc = header.Column as DataGridTextColumn;
            if (dgtc == null)
            {
                DataGridCheckBoxColumn checkBoxCol = header.Column as DataGridCheckBoxColumn;
                bnd = checkBoxCol.Binding as Binding;
            }
            else
            {
                bnd = dgtc.Binding as Binding;              
            }
            // var dd = dgtc.Binding as Binding;
            var t = bnd.Path;

            string columnBinding = t.Path;

            // Set the filter 
            if (!String.IsNullOrEmpty(columnBinding))
                columnFilters[columnBinding] = textBox.Text;
        }

        /// <summary>
        /// Apply the filters
        /// </summary>
        /// <param name="border"></param>
        private void ApplyFilters()
        {
            // Get the view
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);
            if (view != null)
            {
                // Create a filter
                view.Filter = delegate (object item)
                {
                    // Show the current object
                    bool show = true;

                    // Loop filters
                    foreach (KeyValuePair<string, string> filter in columnFilters)
                    {
                        object property = GetPropertyValue(item, filter.Key);
                        if (property != null)
                        {
                            // Check if the current column contains a filter
                            bool containsFilter = false;
                            if (IsFilteringCaseSensitive)
                                containsFilter = property.ToString().Contains(filter.Value);
                            else
                                containsFilter = property.ToString().ToLower().Contains(filter.Value.ToLower());

                            // Do the necessary things if the filter is not correct
                            if (!containsFilter)
                            {
                                show = false;
                                break;
                            }
                        }
                    }

                    // Return if it's visible or not
                    return show;
                };
            }
        }

        /// <summary>
        /// Get the value of a property
        /// </summary>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private object GetPropertyValue(object item, string property)
        {
            // No value
            object value = null;
            string suffix = string.Empty;
            if (property.Contains("."))
            {
                var prop = property.Split('.')[0];
                suffix = property.Replace(prop + ".", "");
                property = prop;
            }
        

            // Get property  from cache
            PropertyInfo pi = null;
            if (propertyCache.ContainsKey(property))
                pi = propertyCache[property];
            else
            {
                pi = item.GetType().GetProperty(property);
                propertyCache.Add(property, pi);
                this.Tag = propertyCache;
                IsSensitive = propertyCache.Count();
            }

            // If we have a valid property, get the value
            if (pi != null)
                value = pi.GetValue(item, null);

            if (!string.IsNullOrEmpty(suffix))
            {
                value = GetPropertyValue(value, suffix);
            }

            // Done
            return value;
        }








        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null reference is being returned.</returns>
        public static T TryFindParent<T>(DependencyObject child)
          where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parentObject);
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Do note, that for content element,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise null.</returns>
        public static DependencyObject GetParentObject(DependencyObject child)
        {
            if (child == null) return null;
            ContentElement contentElement = child as ContentElement;

            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            // If it's not a ContentElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }
    }
}

