//-----------------------------------------------------------------------
// <copyright file="D:\Users\dlandry\source\repos\landrys-psv-manager\landrys_psv_manager.Module\BusinessObjects\SiteInfo\Plant.cs" company="David W. Landry III">
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
    [DefaultProperty(nameof(PlantID))]
    [DefaultListViewOptions(true, NewItemRowPosition.Bottom)]
    public class Plant : XPObject
    {
        public Plant(Session session) : base(session) { }
        public override void AfterConstruction() => base.AfterConstruction();


        Unit unit;
        InstrumentArea instrumentArea;
        string plantID;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField("RuleRequiredField for Plant.Plant_ID", DefaultContexts.Save)]
        [RuleUniqueValue("RuleUniqueValue for Plant.Plant_ID", DefaultContexts.Save)]
        [XafDisplayName("Plant")]
        public string PlantID { get => plantID; set => SetPropertyValue(nameof(PlantID), ref plantID, value); }

        [Association("Unit-Plants")]
        [RuleRequiredField("RuleRequiredField for Plant.Unit", DefaultContexts.Save)]
        public Unit Unit { get => unit; set => SetPropertyValue(nameof(Unit), ref unit, value); }

        public InstrumentArea InstrumentArea { get => instrumentArea; set => SetPropertyValue(nameof(InstrumentArea), ref instrumentArea, value); }

        [Association("Plant-PSVs")]
        public XPCollection<PSV.PSV> PSVs { get { return GetCollection<PSV.PSV>(nameof(PSVs)); } }
    }
}