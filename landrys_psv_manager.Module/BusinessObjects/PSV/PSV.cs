//-----------------------------------------------------------------------
// <copyright file="D:\Users\dlandry\source\repos\landrys-psv-manager\landrys_psv_manager.Module\BusinessObjects\PSV\PSV.cs" company="David W. Landry III">
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
using landrys_psv_manager.Module.BusinessObjects.SiteInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace landrys_psv_manager.Module.BusinessObjects.PSV
{
    [DefaultClassOptions, CreatableItem(false)]
    [DefaultProperty(nameof(TagNumber))]
    [NavigationItem("PSV's")]
    [RuleCombinationOfPropertiesIsUnique("RuleCombinationOfPropertiesIsUnique for PSV.Unit and PSV.TagNumber", DefaultContexts.Save, "TagNumber, Unit")]
    public class PSV : XPObject
    {
        public PSV(Session session) : base(session) { }
        public override void AfterConstruction() => base.AfterConstruction();


        FileData sizingScenariosFile;
        FileData testReportsFile;
        FileData calculationFile;
        FileData pidFile;
        FileData specSheetFile;
        bool hasProcessCalc;
        bool hasSpecSheet;
        string remarks;
        string drawingRF;
        DateTime dateNextPMDue;
        PSVBasisForInterval intervalBasis;
        int pMInterval;
        string lastWorkOrderNumber;
        DateTime dateLastPMDone;
        string overPressure;
        string devBackPressure;
        string conBackPressure;
        string reliefTemperature;
        string normalTemperature;
        string springSet;
        string normalPressure;
        string valveCoefficient;
        string minReseat;
        string maxBlowdown;
        string vaporPressure;
        string latentHeatVap;
        string percentFlashing;
        string steamQuality;
        string compressibility;
        string specificHeatRatio;
        string viscosity;
        string sGorMW;
        string valveArea;
        string requiredArea;
        string valveCapacity;
        string requiredCapacity;
        string fluid;
        string bASOther;
        string aCCOther;
        bool fire;
        PSVIndustryCode code;
        bool gAG;
        bool lever;
        string trimOther;
        string nozzle;
        string bellows;
        string spring;
        string guideRing;
        string seatDisc;
        string connection;
        string size;
        PSVMaterial material;
        string bonnetStuds;
        string bonnet;
        PSVSeatType seatType;
        string serialNumber;
        string model;
        PSVManufacturer manufacturer;
        bool criticalItem;
        string stockNumber;
        string functionalLocation;
        bool fieldIDAssigned;
        Plant plant;
        string service;
        Unit unit;
        string tagNumber;
        InstrumentArea instrumentArea;


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField("RuleRequiredField for PSV.TagNumber", DefaultContexts.Save)]
        public string TagNumber { get => tagNumber; set => SetPropertyValue(nameof(TagNumber), ref tagNumber, value); }


        [RuleRequiredField("RuleRequiredField for PSV.Unit", DefaultContexts.Save)]
        [Association("Unit-PSVs")]
        public Unit Unit { get => unit; set => SetPropertyValue(nameof(Unit), ref unit, value); }

        [Association("InstrumentArea-PSVs")]
        public InstrumentArea InstrumentArea { get => instrumentArea; set => SetPropertyValue(nameof(InstrumentArea), ref instrumentArea, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Service { get => service; set => SetPropertyValue(nameof(Service), ref service, value); }

        [Association("Plant-PSVs")]
        [VisibleInListView(false)]
        [DataSourceCriteria("Unit.Oid = '@This.Unit.Oid'")]
        public Plant Plant { get => plant; set => SetPropertyValue(nameof(Plant), ref plant, value); }

        [XafDisplayName("Field ID Assigned")]
        [CaptionsForBoolValues("Yes", "No")]
        [VisibleInListView(false)]
        public bool FieldIDAssigned { get => fieldIDAssigned; set => SetPropertyValue(nameof(FieldIDAssigned), ref fieldIDAssigned, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Func Loc")]
        public string FunctionalLocation { get => functionalLocation; set => SetPropertyValue(nameof(FunctionalLocation), ref functionalLocation, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Stock No")]
        public string StockNumber { get => stockNumber; set => SetPropertyValue(nameof(StockNumber), ref stockNumber, value); }

        [CaptionsForBoolValues("Yes", "No")]
        public bool CriticalItem { get => criticalItem; set => SetPropertyValue(nameof(CriticalItem), ref criticalItem, value); }

        [Association("PSVManufacturer-PSVs")]
        [VisibleInListView(false)]
        public PSVManufacturer Manufacturer { get => manufacturer; set => SetPropertyValue(nameof(Manufacturer), ref manufacturer, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Model { get => model; set => SetPropertyValue(nameof(Model), ref model, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string SerialNumber { get => serialNumber; set => SetPropertyValue(nameof(SerialNumber), ref serialNumber, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string DrawingRF { get => drawingRF; set => SetPropertyValue(nameof(DrawingRF), ref drawingRF, value); }

        [VisibleInListView(false)]
        public PSVSeatType SeatType { get => seatType; set => SetPropertyValue(nameof(SeatType), ref seatType, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Bonnet { get => bonnet; set => SetPropertyValue(nameof(Bonnet), ref bonnet, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string BonnetStuds { get => bonnetStuds; set => SetPropertyValue(nameof(BonnetStuds), ref bonnetStuds, value); }

        [VisibleInListView(false)]
        public PSVMaterial Material { get => material; set => SetPropertyValue(nameof(Material), ref material, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Size { get => size; set => SetPropertyValue(nameof(Size), ref size, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Connection { get => connection; set => SetPropertyValue(nameof(Connection), ref connection, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string SeatDisc { get => seatDisc; set => SetPropertyValue(nameof(SeatDisc), ref seatDisc, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string GuideRing { get => guideRing; set => SetPropertyValue(nameof(GuideRing), ref guideRing, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Spring { get => spring; set => SetPropertyValue(nameof(Spring), ref spring, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Bellows { get => bellows; set => SetPropertyValue(nameof(Bellows), ref bellows, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Nozzle { get => nozzle; set => SetPropertyValue(nameof(Nozzle), ref nozzle, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        [XafDisplayName("Trim - Other")]
        public string TrimOther { get => trimOther; set => SetPropertyValue(nameof(TrimOther), ref trimOther, value); }

        [VisibleInListView(false)]
        [CaptionsForBoolValues("Yes", "No")]
        public bool Lever { get => lever; set => SetPropertyValue(nameof(Lever), ref lever, value); }

        [VisibleInListView(false)]
        [CaptionsForBoolValues("Yes", "No")]
        [XafDisplayName("GAG")]
        public bool GAG { get => gAG; set => SetPropertyValue(nameof(GAG), ref gAG, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("ACC - Other")]
        public string ACCOther { get => aCCOther; set => SetPropertyValue(nameof(ACCOther), ref aCCOther, value); }

        [VisibleInListView(false)]
        public PSVIndustryCode Code { get => code; set => SetPropertyValue(nameof(Code), ref code, value); }

        [VisibleInListView(false)]
        [CaptionsForBoolValues("Yes", "No")]
        public bool Fire { get => fire; set => SetPropertyValue(nameof(Fire), ref fire, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("BAS - Other")]
        public string BASOther { get => bASOther; set => SetPropertyValue(nameof(BASOther), ref bASOther, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Fluid { get => fluid; set => SetPropertyValue(nameof(Fluid), ref fluid, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RequiredCapacity { get => requiredCapacity; set => SetPropertyValue(nameof(RequiredCapacity), ref requiredCapacity, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ValveCapacity { get => valveCapacity; set => SetPropertyValue(nameof(ValveCapacity), ref valveCapacity, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RequiredArea { get => requiredArea; set => SetPropertyValue(nameof(RequiredArea), ref requiredArea, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ValveArea { get => valveArea; set => SetPropertyValue(nameof(ValveArea), ref valveArea, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("SG / MW")]
        public string SGorMW { get => sGorMW; set => SetPropertyValue(nameof(SGorMW), ref sGorMW, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Viscosity { get => viscosity; set => SetPropertyValue(nameof(Viscosity), ref viscosity, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SpecificHeatRatio { get => specificHeatRatio; set => SetPropertyValue(nameof(SpecificHeatRatio), ref specificHeatRatio, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Compressibility { get => compressibility; set => SetPropertyValue(nameof(Compressibility), ref compressibility, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SteamQuality { get => steamQuality; set => SetPropertyValue(nameof(SteamQuality), ref steamQuality, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("% Flash")]
        public string PercentFlashing { get => percentFlashing; set => SetPropertyValue(nameof(PercentFlashing), ref percentFlashing, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LatentHeatVap { get => latentHeatVap; set => SetPropertyValue(nameof(LatentHeatVap), ref latentHeatVap, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Pv")]
        public string VaporPressure { get => vaporPressure; set => SetPropertyValue(nameof(VaporPressure), ref vaporPressure, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string MaxBlowdown { get => maxBlowdown; set => SetPropertyValue(nameof(MaxBlowdown), ref maxBlowdown, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string MinReseat { get => minReseat; set => SetPropertyValue(nameof(MinReseat), ref minReseat, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Valve Cv")]
        public string ValveCoefficient { get => valveCoefficient; set => SetPropertyValue(nameof(ValveCoefficient), ref valveCoefficient, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string NormalPressure { get => normalPressure; set => SetPropertyValue(nameof(NormalPressure), ref normalPressure, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SpringSet { get => springSet; set => SetPropertyValue(nameof(SpringSet), ref springSet, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string NormalTemperature { get => normalTemperature; set => SetPropertyValue(nameof(NormalTemperature), ref normalTemperature, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ReliefTemperature { get => reliefTemperature; set => SetPropertyValue(nameof(ReliefTemperature), ref reliefTemperature, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ConBackPressure { get => conBackPressure; set => SetPropertyValue(nameof(ConBackPressure), ref conBackPressure, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string DevBackPressure { get => devBackPressure; set => SetPropertyValue(nameof(DevBackPressure), ref devBackPressure, value); }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string OverPressure { get => overPressure; set => SetPropertyValue(nameof(OverPressure), ref overPressure, value); }

        [XafDisplayName("PM Done")]
        public DateTime DateLastPMDone { get => dateLastPMDone; set => SetPropertyValue(nameof(DateLastPMDone), ref dateLastPMDone, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Last WO")]
        public string LastWorkOrderNumber { get => lastWorkOrderNumber; set => SetPropertyValue(nameof(LastWorkOrderNumber), ref lastWorkOrderNumber, value); }

        [XafDisplayName("PM Interval")]
        public int PMInterval { get => pMInterval; set => SetPropertyValue(nameof(PMInterval), ref pMInterval, value); }

        public PSVBasisForInterval IntervalBasis { get => intervalBasis; set => SetPropertyValue(nameof(IntervalBasis), ref intervalBasis, value); }

        [XafDisplayName("PM Due")]
        public DateTime DateNextPMDue { get => dateNextPMDue; set => SetPropertyValue(nameof(DateNextPMDue), ref dateNextPMDue, value); }

        [Size(500)]
        public string Remarks { get => remarks; set => SetPropertyValue(nameof(Remarks), ref remarks, value); }

        [CaptionsForBoolValues("Yes", "No")]
        public bool HasSpecSheet { get => hasSpecSheet; set => SetPropertyValue(nameof(HasSpecSheet), ref hasSpecSheet, value); }

        [CaptionsForBoolValues("Yes", "No")]
        public bool HasProcessCalc { get => hasProcessCalc; set => SetPropertyValue(nameof(HasProcessCalc), ref hasProcessCalc, value); }

        [VisibleInListView(false)]
        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData SpecSheetFile
        {
            get => specSheetFile;
            set => SetPropertyValue(nameof(SpecSheetFile), ref specSheetFile, value);
        }

        [VisibleInListView(false)]
        [ModelDefault("PropertyEditor", "PdfViewerPropertyEditor")]
        public FileData SpecSheet => SpecSheetFile;

        [VisibleInListView(false)]
        [XafDisplayName("P&ID File")]
        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData PidFile
        {
            get => pidFile;
            set => SetPropertyValue(nameof(PidFile), ref pidFile, value);
        }

        [VisibleInListView(false)]
        [ModelDefault("PropertyEditor", "PdfViewerPropertyEditor")]
        [XafDisplayName("P&ID")]
        public FileData PID => pidFile;

        [VisibleInListView(false)]
        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData CalculationFile
        {
            get => calculationFile;
            set => SetPropertyValue(nameof(CalculationFile), ref calculationFile, value);
        }

        [VisibleInListView(false)]
        [ModelDefault("PropertyEditor", "PdfViewerPropertyEditor")]
        public FileData Calculations => CalculationFile;

        [VisibleInListView(false)]
        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData TestReportsFile
        {
            get => testReportsFile;
            set => SetPropertyValue(nameof(TestReportsFile), ref testReportsFile, value);
        }

        [VisibleInListView(false)]
        [ModelDefault("PropertyEditor", "PdfViewerPropertyEditor")]
        public FileData TestReports => TestReportsFile;

        [VisibleInListView(false)]
        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData SizingScenariosFile
        {
            get => sizingScenariosFile;
            set => SetPropertyValue(nameof(SizingScenariosFile), ref sizingScenariosFile, value);
        }

        [VisibleInListView(false)]
        [ModelDefault("PropertyEditor", "PdfViewerPropertyEditor")]
        public FileData SizingScenarios => SizingScenariosFile;
    }
}