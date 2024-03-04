using MauiAppListaCompras.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    

namespace MauiAppListaCompras.Helpers
{
    internal class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _coon;
        public SQLiteDatabaseHelper(string path)
        {
            _coon = new SQLiteAsyncConnection(path);
            _coon.CreateTableAsync<Produto>().Wait();
        }

        public Task <int> Insert(Produto p)
        {
            return _coon.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET descricao=?, " + "Quantidade=?, Preco=? WHERE id=?";
            return _coon.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _coon.Table<Produto>().ToListAsync();
        }

        public Task<int> Delete(int id) 
        {
            return _coon.Table<Produto>().DeleteAsync(
                i => i.Id == id);
        }

        public Task<List <Produto>> Seach(string q)
        {
            string sql = "SELECT * FROM Produto WHERE" +
                "descricao LIKE '%" + q + "%'";

            return _coon.QueryAsync<Produto>(sql);
        }

    }

}
