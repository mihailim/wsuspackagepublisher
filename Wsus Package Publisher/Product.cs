using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal class Product
    {
        private string _productName = "";
        private List<IUpdate> _updates = new List<IUpdate>();

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
        internal List<IUpdate> Updates
        {
            get { return _updates; }
        }

        /// <summary>
        /// Add an update to this Product.
        /// </summary>
        /// <param name="update">The update to Add to this Product.</param>
        internal void AddUpdate(IUpdate update)
        {
            if (update != null)
                _updates.Add(update);

            if (UpdateAdded != null)
                UpdateAdded(this, update);
        }

        /// <summary>
        /// Search for an update with the same UpdateId and replace it by the provided update.
        /// </summary>
        /// <param name="newUpdate">New update which replace the old update.</param>
        internal void RefreshUpdate(IUpdate newUpdate)
        {
            for (int i = 0; i < Updates.Count; i++)
			{
                if (Updates[i].Id.UpdateId == newUpdate.Id.UpdateId)
                {
                    Updates[i] = newUpdate;
                    break;
                }
			} 
        }

        /// <summary>Get the number of update for this Product.
        /// </summary>
        /// <returns>Number of updates.</returns>
        internal int UpdatesCount
        {
            get { return _updates.Count; }
        }

        /// <summary>
        /// Remove an update from the list.
        /// </summary>
        /// <param name="updateToRemove">Update to remove.</param>
        internal void RemoveUpdate(IUpdate updateToRemove)
        {
            if (updateToRemove != null && _updates.Contains(updateToRemove))
                _updates.Remove(updateToRemove);

            if (UpdatesCount == 0)
                if (NoMoreUpdatesForThisProduct != null)
                    NoMoreUpdatesForThisProduct(this);
        }

        /// <summary>
        /// Remove all update from the list.
        /// </summary>
        internal void ClearUpdateList()
        {
            _updates.Clear();

            if (NoMoreUpdatesForThisProduct != null)
                NoMoreUpdatesForThisProduct(this);
        }

        public override string ToString()
        {
            return ProductName;
        }

        public delegate void NoMoreUpdatesForThisProductEventHandler(Product productWithoutUpdate);
        public event NoMoreUpdatesForThisProductEventHandler NoMoreUpdatesForThisProduct;

        public delegate void UpdateAddedEventHandler(Product updatedProduct, IUpdate updateAdded);
        public event UpdateAddedEventHandler UpdateAdded;
    }
}
