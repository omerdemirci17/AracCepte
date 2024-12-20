﻿using Microsoft.EntityFrameworkCore.Migrations.Operations;
using AracCepte.Business.Abstract;
using AracCepte.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AracCepte.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace AracCepte.Business.Concrete
{
    public class GenericManager<T>(IRepository<T> _repository) : IGenericService<T> where T : class
    {
        public int TCount()
        {
            return _repository.Count();
        }

        public void TCreate(T entity)
        {
            _repository.Create(entity);
        }
        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Veritabani Hatasi : {dbEx.Message}");
                Console.WriteLine($"inner exeption hatasi: {dbEx.InnerException?.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir Hata olustu: {ex.Message}");
                return false ;
            }
        }

        public void TDelete(int id)
        {
            _repository.Delete(id);
        }

        public int TFilteredCount(Expression<Func<T, bool>> predicate)
        {
            return _repository.FilteredCount(predicate);
        }

        public T TGetByFilter(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetByFilter(predicate);
        }

        public T TGetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<T> TGetFilteredList(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetFilteredList(predicate);
        }

        public List<T> TGetList()
        {
            return _repository.GetList();
        }

        public void TUpdate(T entity)
        {
            _repository.Update(entity);
        }
    }
}
