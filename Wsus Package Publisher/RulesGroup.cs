﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class RulesGroup : GenericRule
    {
        private Dictionary<Guid, GenericRule> _frmRules = new Dictionary<Guid, GenericRule>();
        private Guid _guid = Guid.NewGuid();
        private GroupLogicalOperator _groupType = GroupLogicalOperator.And;

        internal RulesGroup()
        {
            InitializeComponent();
        }

        internal void AddFrm(GenericRule frmToAdd)
        {
            _frmRules.Add(Guid.NewGuid(), frmToAdd);
        }

        internal override string GetXmlFormattedRule()
        {
            string result = "";

            switch (GroupType)
            {
                case GroupLogicalOperator.And:
                    result += "<lar:And>\r\n";
                    break;
                case GroupLogicalOperator.Or:
                    result += "<lar:Or>\r\n";
                    break;
                case GroupLogicalOperator.None:
                    break;
                default:
                    break;
            }

            foreach (GenericRule rule in FormList.Values)
            {
                result += rule.GetXmlFormattedRule();
            }

            switch (GroupType)
            {
                case GroupLogicalOperator.And:
                    result += "</lar:And>\r\n";
                    break;
                case GroupLogicalOperator.Or:
                    result += "</lar:Or>\r\n";
                    break;
                case GroupLogicalOperator.None:
                    break;
                default:
                    break;
            }

            return result;
        }

        #region (Properties - Propiétés)

        internal Guid GetGuid
        {
            get { return _guid; }
        }

        internal Dictionary<Guid, GenericRule> FormList
        {
            get { return _frmRules; }
            set { _frmRules = value; }
        }

        internal override GroupLogicalOperator GroupType
        {
            get {return _groupType;}
            set { _groupType = value; }
        }

        internal override GenericRule.ObjectType TypeOfObject
        {
            get { return GenericRule.ObjectType.Group; }
        }

        internal override string RuleType
        {
            get { return ""; }
        }

        #endregion
    }
}