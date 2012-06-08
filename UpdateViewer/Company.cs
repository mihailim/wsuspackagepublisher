using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateViewer
{
    internal class Company
    {
        private string _companyName = "";
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();

        /// <summary>
        /// Create a new instance of Company with the name 'companyName'.
        /// </summary>
        /// <param name="companyName">Name of the Company.</param>
        internal Company(string companyName)
        {
            CompanyName = companyName;
        }

        /// <summary>
        /// Get or Set the name of the Company
        /// </summary>
        internal string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _companyName = value;
            }
        }

        /// <summary>
        /// Return the list of all products for this Company.
        /// </summary>
        internal Dictionary<string, Product> Products
        {
            get { return _products; }
        }

        /// <summary>
        /// Add a Product to this Company
        /// </summary>
        /// <param name="productName">Name of the Product, must be unique for the Company</param>
        internal void AddProduct(string productName)
        {
            if (!string.IsNullOrEmpty(productName) && !_products.ContainsKey(productName))
                _products.Add(productName, new Product(productName));
        }

        /// <summary>
        /// Remove a Product in this Company.
        /// </summary>
        /// <param name="productName">Name of the Product to remove.</param>
        internal void RemoveProduct(string productName)
        {
            if (!string.IsNullOrEmpty(productName) && _products.ContainsKey(productName))
            {
                _products[productName].ClearUpdateList();
                _products.Remove(productName);
            }
        }

        /// <summary>
        /// Get the number of Product for this Company
        /// </summary>
        /// <returns>Number of Product.</returns>
        internal int GetProductsCount()
        {
            return _products.Count;
        }

        /// <summary>
        /// Remove all Product for this Company.
        /// </summary>
        internal void ClearProductsList()
        {
            _products.Clear();
        }
    }
}
