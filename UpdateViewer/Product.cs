using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateViewer
{
    internal class Product
    {
        private string _productName = "";
        private List<Microsoft.UpdateServices.Administration.IUpdate> _updates = new List<Microsoft.UpdateServices.Administration.IUpdate>();

        /// <summary>
        /// Get a new instance of Product with the name 'productName'.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        public Product(string productName)
        {
            ProductName = productName;
        }

        /// <summary>
        /// Get or Set the name of this Product.
        /// </summary>
        internal string ProductName
        {
            get { return _productName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _productName = value;
            }
        }

        /// <summary>
        /// Get the list of all updates for this Product.
        /// </summary>
        internal List<Microsoft.UpdateServices.Administration.IUpdate> Updates
        {
            get {return _updates;}
        }

        /// <summary>
        /// Add an update to this Product.
        /// </summary>
        /// <param name="update">The update to Add to this Product.</param>
        internal void AddUpdate(Microsoft.UpdateServices.Administration.IUpdate update)
        {
            if(update != null)
            _updates.Add(update);
        }

        /// <summary>Get the number of update for this Product.
        /// </summary>
        /// <returns>Number of updates.</returns>
        internal int GetUpdatesCount()
        {
            return _updates.Count;
        }

        /// <summary>
        /// Remove an update from the list.
        /// </summary>
        /// <param name="updateToRemove">Update to remove.</param>
        internal void RemoveUpdate(Microsoft.UpdateServices.Administration.IUpdate updateToRemove)
        {
            if(updateToRemove != null && _updates.Contains(updateToRemove))
            _updates.Remove(updateToRemove);
        }

        /// <summary>
        /// Remove all update from the list.
        /// </summary>
        internal void ClearUpdateList()
        {
            _updates.Clear();
        }
    }
}
