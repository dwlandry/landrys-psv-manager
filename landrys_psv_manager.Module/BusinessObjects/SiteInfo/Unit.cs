//-----------------------------------------------------------------------
// <copyright file="D:\Users\dlandry\source\repos\landrys-psv-manager\landrys_psv_manager.Module\BusinessObjects\SiteInfo\Unit.cs" company="David W. Landry III">
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
    [DefaultProperty(nameof(Name))]
    public class Unit : XPObject
    {
        public Unit(Session session) : base(session) { }
        public override void AfterConstruction() => base.AfterConstruction();


        UnitStatus status;
        string name;
        string unitID;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField("RuleRequiredField for Unit.UnitID", DefaultContexts.Save)]
        public string UnitID { get => unitID; set => SetPropertyValue(nameof(UnitID), ref unitID, value); }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField("RuleRequiredField for Unit.Name", DefaultContexts.Save)]
        [XafDisplayName("Unit")]
        public string Name { get => name; set => SetPropertyValue(nameof(Name), ref name, value); }

        [Association("Unit-Plants")]
        public XPCollection<Plant> Plants { get { return GetCollection<Plant>(nameof(Plants)); } }

        [Association("Unit-Instrument_Areas")]
        public XPCollection<InstrumentArea> InstrumentAreas { get { return GetCollection<InstrumentArea>(nameof(InstrumentAreas)); } }

        [Association("Unit-PSVs")]
        public XPCollection<PSV.PSV> PSVs { get { return GetCollection<PSV.PSV>(nameof(PSVs)); } }

        [Association("UnitStatus-Units")]
        public UnitStatus Status { get => status; set => SetPropertyValue(nameof(Status), ref status, value); }
    }

    [DefaultClassOptions, CreatableItem(false)]
    [NavigationItem("Lookup Lists")]
    public class UnitStatus : XPObject
    {
        public UnitStatus(Session session) : base(session) { }


        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleUniqueValue("RuleUniqueValue for UnitStatus.Name", DefaultContexts.Save)]
        [RuleRequiredField("RuleRequiredField for UnitStatus.Name", DefaultContexts.Save)]
        public string Name { get => name; set => SetPropertyValue(nameof(Name), ref name, value); }

        [Association("UnitStatus-Units")]
        public XPCollection<Unit> Units { get { return GetCollection<Unit>(nameof(Units)); } }
    }
}