//-----------------------------------------------------------------------
// <copyright file="D:\Users\dlandry\source\repos\landrys-psv-manager\landrys_psv_manager.Module\BusinessObjects\SiteInfo\InstrumentArea.cs" company="David W. Landry III">
//     Author: _**David Landry**_
//     *Copyright (c) David W. Landry III. All rights reserved.*
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace landrys_psv_manager.Module.BusinessObjects.SiteInfo
{
    [DefaultClassOptions, CreatableItem(false)]
    [NavigationItem("Site Info")]
    [DefaultProperty(nameof(InstrumentAreaID))]
    public class InstrumentArea : XPObject
    {
        public InstrumentArea(Session session) : base(session) { }
        public override void AfterConstruction() => base.AfterConstruction();

        Unit unit;
        string description;
        string instrumentAreaID;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField("RuleRequiredField for Instrument_Area.Instrument_Area_ID", DefaultContexts.Save)]
        [RuleUniqueValue("RuleUniqueValue for Instrument_Area.Instrument_Area_ID", DefaultContexts.Save)]
        [XafDisplayName("Instrument Area")]
        public string InstrumentAreaID { get => instrumentAreaID; set => SetPropertyValue(nameof(InstrumentAreaID), ref instrumentAreaID, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Description { get => description; set => SetPropertyValue(nameof(Description), ref description, value); }

        [Association("Unit-Instrument_Areas")]
        public Unit Unit { get => unit; set => SetPropertyValue(nameof(Unit), ref unit, value); }

        [Association("InstrumentArea-PSVs")]
        public XPCollection<PSV.PSV> PSVs { get { return GetCollection<PSV.PSV>(nameof(PSVs)); } }
        
    }
}