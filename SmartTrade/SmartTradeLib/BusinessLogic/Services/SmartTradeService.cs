﻿using SmartTradeLib.Entities;
using SmartTradeLib.Persistence;
using System.Collections.Concurrent;

namespace SmartTradeLib.BusinessLogic;

public class SmartTradeService : ISmartTradeService
{
    private readonly IDAL _dal;
    public SmartTradeService()
    {
        _dal = new EntityFrameworkDAL(new SmartTradeContext());
    }
    public void SaveChanges()
    {
        _dal.Commit();
    }

    public void RemoveAll()
    {
        _dal.RemoveAllData();
    }

    public void AddAdmin(Admin admin)
    {
        _dal.Insert<Admin>(admin);
        _dal.Commit();
    }

    public void AddCostumer(Consumer costumer)
    {
        _dal.Insert<Consumer>(costumer);
        _dal.Commit();
    }

    public void AddSeller(Seller seller)
    {
        _dal.Insert<Seller>(seller);
        _dal.Commit();
    }

    public void AddPost(Post post)
    {
        _dal.Insert<Post>(post);
        _dal.Commit();
    }

    public void AddAlert(Alert alert)
    {
        _dal.Insert<Alert>(alert);
        _dal.Commit();
    }

    public void registerConsumer(string email, string password, string name, string lastNames, string dni, DateTime birthDate, Address billingAddress, Address address)
    {
        if (_dal.GetWhere<Consumer>(x => x.Email == email).Any() || _dal.GetWhere<Consumer>(x => x.DNI == dni).Any())
        {
            throw new Exception("Usuario existente");
        }
        else
        {
            _dal.Insert<Consumer>(new Consumer(email, password, name, lastNames, dni, birthDate, billingAddress, address));
            _dal.Commit();

        }
    }

    public void registerSeller(string email, string password, string name, string lastNames, string dni, string companyName, string iban)
    {
        if (_dal.GetWhere<Seller>(x => x.Email == email).Any() || _dal.GetWhere<Seller>(x => x.DNI == dni).Any() || _dal.GetWhere<Seller>(x => x.IBAN == iban).Any())
        {
            throw new Exception("Usuario existente");
        }
        else
        {
            _dal.Insert<Seller>(new Seller(email, password, name, lastNames, dni, companyName, iban));
            _dal.Commit();

        }
    }
}