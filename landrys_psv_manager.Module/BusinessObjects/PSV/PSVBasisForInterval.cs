//-----------------------------------------------------------------------
// <copyright file="D:\Users\dlandry\source\repos\landrys-psv-manager\landrys_psv_manager.Module\BusinessObjects\PSV\PSVBasisForInterval.cs" company="David W. Landry III">
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

namespace landrys_psv_manager.Module.BusinessObjects.PSV
{
    [DefaultClassOptions, CreatableItem(false)]
    [DefaultProperty(nameof(Name))]
    [NavigationItem("Lookup Lists")]
    [XafDisplayName("PSV Basis for Interval")]
    public class PSVBasisForInterval : XPObject
    {
        public PSVBasisForInterval(Session session) : base(session) { }
        public override void AfterConstruction() => base.AfterConstruction();

        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField("RuleRequiredField for PSVBasisForInterval.Name", DefaultContexts.Save)]
        [RuleUniqueValue("RuleUniqueField for PSVBasisForInterval.Name", DefaultContexts.Save)]
        public string Name { get => name; set => SetPropertyValue(nameof(Name), ref name, value); }
    }
}