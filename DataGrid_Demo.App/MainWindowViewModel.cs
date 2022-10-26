using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataGrid_Demo.DB.DataBase;
using DataGrid_Demo.DB.Models;

namespace DataGrid_Demo.App;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private Db _db;

    private Product _product;
    public Product SelectedProduct
    {
        get => _product; 
        set => SetField(ref _product, value);
    }
    
    public ObservableCollection<Product> Products { get; set; }

    public Command AddCommand { get; set; }
    public Command DelCommand { get; set; }
    public Command SaveCommand { get; set; }
    public Command CreateCommand { get; set; }

    public MainWindowViewModel()
    {
        _db = new Db();
        //_db.CreateDb();
        Products = new ObservableCollection<Product>(_db.Products);

        AddCommand = new Command(Add, _ => true);
        DelCommand = new Command(Del, _ => true);
        SaveCommand = new Command(Save, _ => true);
        CreateCommand = new Command(_ =>
        {
            _db.CreateDb();
            Products.Clear();
        }, _ => true);
    }

    private void Add(object? o)
    {
        _db.Products.Add(SelectedProduct);
        _db.SaveChanges();
    }

    private void Del(object? o)
    {
        _db.Products.Remove(SelectedProduct);
        _db.SaveChanges();
        Products.Remove(SelectedProduct);
    }

    private void Save(object? o)
    {
        _db.Products.Update(SelectedProduct);
        _db.SaveChanges();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}