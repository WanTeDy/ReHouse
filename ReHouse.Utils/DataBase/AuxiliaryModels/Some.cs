using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting;

namespace ITfamily.Utils.DataBase.AuxiliaryModels
{
    public class Some<T> where T : TypeEntry
    {
        public DbItFamily _context { get; set; }

        public Some()
        {
        }

        private BrainCategory RecursiveLoad(int id)
        {
            var ParentFromDatabase = _context.Categories.Include(x => x.Parent)
                .Include(d => d.Categories.Select(dd => dd.name)).Single(x => x.Id == id);

            foreach (var child in ParentFromDatabase.Categories)
            {
                var childNotLoaded = child;
                var childFullyLoaded = _context.Categories
                    .Include(d => d.Parent)
                    .Include(d => d.name)
                    .Single(d => d.Id == childNotLoaded.Id);

                child.name = childFullyLoaded.name;
                    //Require to set back the value because we want by reference to have everything in the tree
                child.Parent = RecursiveLoad(childFullyLoaded.Parent.Id);
                    //Require to set back the value because we want by reference to have everything in the tree
            }
            return ParentFromDatabase;
        }

        public DbCollectionEntry<BrainCategory, BrainCategory> RecursiveLoad(BrainCategory parent)
        {
            var parentFromDatabase = _context.Entry(parent).Collection(d => d.Categories);
                //Children are loaded, we can loop them now

            foreach (var child in parent.Categories)
            {
                _context.Entry(child).Reference(d => d.name).Load();
                RecursiveLoad(child);
            }
            return parentFromDatabase;
        }

        public DbCollectionEntry<BrainCategory, BrainCategory> RecursiveLoadWithProductsVendors(BrainCategory parent)
        {
            var parentFromDatabase = _context.Entry(parent).Collection(d => d.Categories);
                //Children are loaded, we can loop them now

            foreach (var child in parent.Categories)
            {
                _context.Entry(child).Reference(d => d.name).Load();
                _context.Entry(child).Reference(d => d.BrainProducts).Load();
                _context.Entry(child).Reference(d => d.Vendors).Load();
                RecursiveLoadWithProductsVendors(child);
            }
            return parentFromDatabase;
        }


        public DbCollectionEntry<BrainCategory, BrainCategory> RecursiveLoadWithVendors(BrainCategory parent)
        {
            var parentFromDatabase = _context.Entry(parent).Collection(d => d.Categories);
                //Children are loaded, we can loop them now

            foreach (var child in parent.Categories)
            {
                _context.Entry(child).Reference(d => d.name).Load();
                _context.Entry(child).Reference(d => d.Vendors).Load();
                RecursiveLoadWithVendors(child);
            }
            return parentFromDatabase;
        }

        public T LoadRecursive(T item, Expression<Func<T, ICollection<T>>> nExpression)
        {
            _context//.GetDataStore()
                .Entry(item)
                .Collection(nExpression)
                .Load();

            foreach (var child in nExpression.Compile().Invoke(item))
                LoadRecursive(child, nExpression);
            return item;
        }

        private void FUnc()
        {
            var qryCategories = from q in _context.Categories
                                where q.name == "Open"
                                select q;
            var b = _context.Categories.Include(x=>x.Categories).First(x => x.name == "");
            var parentFromDatabase = _context.Entry(b).Collection(d => d.Categories);

            foreach (var cat in b.Categories)
            {
                if (!_context.Entry(cat).Collection(d=>d.Categories).IsLoaded)
                    _context.Entry(cat).Collection(d => d.Categories).Load();
                // This will only load product groups "once" if need be.
                if (!_context.Entry(cat).Collection(d => d.BrainProducts).IsLoaded)
                    _context.Entry(cat).Collection(d => d.BrainProducts).Load();
            }
        }
    }
}