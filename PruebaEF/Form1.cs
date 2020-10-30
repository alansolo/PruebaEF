using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;

namespace PruebaEF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            List<Tabla_1> tabla1 = new List<Tabla_1>();
            List<Table_2> tabla2 = new List<Table_2>();

            var tablaFusion = new Object();

            Tabla_1 tabla1_a = new Tabla_1();
            tabla1_a.Nombre = "Agregado";
            tabla1_a.Id = 7;


            using (var bd = new PruebaEF())
            {

                using (var tran = new TransactionScope())
                {
                    tran.Complete();
                }
                    //SELECT
                    //tabla1 = (from bdL in bd.Tabla_1
                    //                       //where bdL.Id == 1
                    //                       select bdL).ToList();

                    //tabla2 = (from bdL2 in bd.Table_2
                    //              //where bdL.Id == 1
                    //          select bdL2).ToList();

                    //tablaFusion = (from tf1 in bd.Tabla_1.Where(n => n.Id >= 2)
                    //               from tf2 in bd.Table_2
                    //               where tf1.Id == tf2.Id
                    //               select new { tf1, tf2 }).ToList();



                    for (int j = 0; j <= 100; j++)
                    {
                        var tab = new Tabla_1()
                        {
                            Nombre = "don" + j
                        };

                        //INSERT
                        bd.Tabla_1.Add(tab);
                    }

                //Task<int> k = bd.SaveChangesAsync();

                int i = bd.SaveChanges();


                bd.Table_2.Add(new Table_2() { Id = 1, Nombre="Nom", Descripcion="Descrip" });

                //UPDATE
                bd.Tabla_1.Where(n => n.Nombre == "don").ToList().ForEach(m =>
                {
                    m.Nombre = "Nuevo don";
                });

                //DELETE
                bd.Tabla_1.RemoveRange(bd.Tabla_1.Where(n => n.Id >= 4));                             

                //int i = bd.SaveChanges();


            }


            List<Tabla_1> dos = tabla1.Where(n => n.Id >= 3).ToList();

            decimal total = dos.Sum(n => n.Id);

            decimal max = dos.Max(n => n.Id);

            string nombre = dos.Select(n => n.Nombre).FirstOrDefault();

            var tres = (from dosL in dos
                        select new { dosL.Id,
                                     otro = dosL.Id + 10,
                                     dosL.Nombre }).ToList();

            var cuatro = (from t1 in tabla1.Where(n => n.Id == 1)
                          from t2 in tabla2.Where(n => n.Nombre == "Chevrolet")
                          where t1.Id == t2.Id
                          select new { t1.Id, t1.Nombre, Nombre2 = t2.Nombre, t2.Descripcion }).ToList();


            var cinco = (from u1 in tabla1
                         select new Table_2 { Id = u1.Id, Nombre = u1.Nombre, Descripcion = u1.Nombre + "_desc" }).ToList();

            var seis = tabla1.Where(n => n.Id == 1).ToList();

            var siete = (from a1 in tabla1
                         where a1.Id == 1
                         select a1).ToList();

            var ocho = tabla2.Select(n => n.Nombre).Distinct().ToList();

            var nueve = tabla1.Select(n => n.Id).Except(tabla2.Select(n => n.Id));

            var diez = tabla2.OrderBy(n => n.Id).ThenByDescending(m => m.Nombre).ThenBy(x => x.Id).ToList();

            tabla2.ForEach(n =>
            {
                n.Id = n.Id + 10;

                n.Nombre = n.Nombre + "_A";
            });

            tabla2.Remove(tabla2.FirstOrDefault());

            tabla2.RemoveAll(n => n.Id > 3);

            tabla2.RemoveAt(0);

            tabla2.RemoveRange(0, 1);

            

        }
    }
}
 