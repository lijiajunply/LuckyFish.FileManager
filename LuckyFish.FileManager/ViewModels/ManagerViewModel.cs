﻿using System;
using System.Collections.ObjectModel;
using DynamicData;
using FileManager.Lib.FileModels;
using LuckyFish.FileManager.Models;
using LuckyFish.FileManager.Serves;

namespace LuckyFish.FileManager.ViewModels;

public class ManagerViewModel : ViewModelBase
{
    #region Left

    private SettingModel? Model { get; set; }
    public ObservableCollection<CommonModel> Common { get; set; }
    public ObservableCollection<DriveSimpleOperation> Root { get; set; }

    #endregion

    private ViewModelBase? _finder;
    public ViewModelBase? Finder
    {
        get => _finder;
        set => SetField(ref _finder, value);
    }

    public void FilePath(string path)
    {
        if (_finder is FinderViewBase model)
            model.FilePath = path;
        else
        {
            if(Model == null)return;
            var type = Type.GetType($"LuckyFish.FileManager.ViewModels.{Model.FinderTheme}FinderViewModel");
            if(type == null)return;
            var a = (FinderViewBase)Activator.CreateInstance(type)!;
            a.FilePath = path;
            Finder = a;
        }
    }

    public void ReInit()
    {
        if (_finder is FinderViewBase model)
            model.Init();
    }
    
    public void ToHome()
    {
        var a = FileServer.GetDrives();
        var b = FileServer.GetCommonPath();
        Common.Clear();
        Common.Add(b);
        Root.Clear();
        Root.Add(a);
        Finder = new HomeFinderViewModel(a,b);
    }
    
    public ManagerViewModel()
    {
        Model = SettingServer.Read();
        Common = new ObservableCollection<CommonModel>();
        Root = new ObservableCollection<DriveSimpleOperation>();
        ToHome();
    }
}