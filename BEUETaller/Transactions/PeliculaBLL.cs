using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUETaller.Transactions
{
    public class PeliculaBLL
    {
        public static void Create(Pelicula p)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Pelicula.Add(p);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static Pelicula Get(int? id)
        {
            Entities db = new Entities();
            return db.Pelicula.Find(id);
        }

        public static void Update(Pelicula pelicula)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Pelicula.Attach(pelicula);
                        db.Entry(pelicula).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static void Delete(int? id)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Pelicula pelicula = db.Pelicula.Find(id);
                        db.Entry(pelicula).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static List<Pelicula> List()
        {
            Entities db = new Entities();
            return db.Pelicula.ToList();
        }


        private static List<Pelicula> GetPeliculas(string criterio)
        {
            //Ejemplo: criterio = 'quin'
            //Posibles resultados => Quintana, Quintero, Pulloquinga, Quingaluisa...
            Entities db = new Entities();
            return db.Pelicula.Where(x => x.categoria.ToLower().Contains(criterio)).ToList();
        }

        private static Pelicula GetPelicula(string nombre)
        {
            Entities db = new Entities();
            return db.Pelicula.FirstOrDefault(x => x.nombre == nombre);
        }
    }
}