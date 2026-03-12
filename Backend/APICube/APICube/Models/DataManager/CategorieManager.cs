using APICube.Models.EntityFramework;
using APICube.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;


namespace APICube.Models.DataManager
{
    public class CategorieManager : IDataRepository<Categorie>
    {
        readonly S401Context? context;

        public CategorieManager() { }

        public CategorieManager(S401Context context)
        {
            this.context = context;
        }
        public async Task AddAsync(Categorie categorie)
        {
            await context.Categories.AddAsync(categorie);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Categorie categorie)
        {

            context?.Categories.Remove(categorie);
            await context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Categorie>>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<ActionResult<Categorie>> GetByIdAsync(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(u => u.Idcategorie == id);
        }

        public async Task UpdateAsync(Categorie categorieToUpdate, Categorie categorie)
        {
            context.Entry(categorieToUpdate).State = EntityState.Modified;
            categorieToUpdate.Idcategorie = categorie.Idcategorie;
            categorieToUpdate.CatIdcategorie = categorie.CatIdcategorie;
            categorieToUpdate.Nomcategorie = categorie.Nomcategorie;
            await context.SaveChangesAsync();
        }
    }
}
