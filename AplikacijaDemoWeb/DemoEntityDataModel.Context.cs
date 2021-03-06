﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplikacijaDemoWeb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Text;

    public partial class DemoEntities : DbContext
    {
        public DemoEntities()
            : base("name=DemoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int DodajLogZapis(Nullable<System.DateTime> vrijeme, Nullable<int> akcija, string korisnik, string podaci)
        {
            var vrijemeParameter = vrijeme.HasValue ?
                new ObjectParameter("Vrijeme", vrijeme) :
                new ObjectParameter("Vrijeme", typeof(System.DateTime));
    
            var akcijaParameter = akcija.HasValue ?
                new ObjectParameter("Akcija", akcija) :
                new ObjectParameter("Akcija", typeof(int));
    
            var korisnikParameter = korisnik != null ?
                new ObjectParameter("Korisnik", korisnik) :
                new ObjectParameter("Korisnik", typeof(string));
    
            var podaciParameter = podaci != null ?
                new ObjectParameter("Podaci", podaci) :
                new ObjectParameter("Podaci", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajLogZapis", vrijemeParameter, akcijaParameter, korisnikParameter, podaciParameter);
        }
    
        public virtual int DodajPrimatelja(string imePrezime, string brojMobitela)
        {
            var imePrezimeParameter = imePrezime != null ?
                new ObjectParameter("ImePrezime", imePrezime) :
                new ObjectParameter("ImePrezime", typeof(string));
    
            var brojMobitelaParameter = brojMobitela != null ?
                new ObjectParameter("BrojMobitela", brojMobitela) :
                new ObjectParameter("BrojMobitela", typeof(string));
    
            int result = 0;
            try
            {
                result = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajPrimatelja", imePrezimeParameter, brojMobitelaParameter);
            }
            catch (Exception e)
            {
                var ex = e.GetBaseException();
                //TODO
                //switch ()
                //{
                //    case unchecked((int)0x80131904):
                //        return -1;
                //        break;
                //    default:
                //        throw e;
                //}
            }

            this.DodajLogZapis(DateTime.Now, (int) Const.Akcije.SPREMI_PRIMATELJA, "", (new StringBuilder(brojMobitela)).Append("::").Append(imePrezime).ToString());

            return result;
        }
    
        public virtual ObjectResult<DohvatiPrimatelje_Result> DohvatiPrimatelje()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DohvatiPrimatelje_Result>("DohvatiPrimatelje");
        }
    
        public virtual int UpisiPoruku(Nullable<System.DateTime> datum, string primatelj, string brojMobitela, string nazivDatoteke)
        {
            var datumParameter = datum.HasValue ?
                new ObjectParameter("Datum", datum) :
                new ObjectParameter("Datum", typeof(System.DateTime));
    
            var primateljParameter = primatelj != null ?
                new ObjectParameter("Primatelj", primatelj) :
                new ObjectParameter("Primatelj", typeof(string));
    
            var brojMobitelaParameter = brojMobitela != null ?
                new ObjectParameter("BrojMobitela", brojMobitela) :
                new ObjectParameter("BrojMobitela", typeof(string));
    
            var nazivDatotekeParameter = nazivDatoteke != null ?
                new ObjectParameter("NazivDatoteke", nazivDatoteke) :
                new ObjectParameter("NazivDatoteke", typeof(string));

            int result = 0;

            result = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpisiPoruku", datumParameter, primateljParameter, brojMobitelaParameter, nazivDatotekeParameter);

            this.DodajLogZapis(DateTime.Now, (int)Const.Akcije.POSLANA_PORUKA, "", (new StringBuilder(nazivDatoteke)).Append("::").Append(datum.ToString()).Append("::").Append(brojMobitela).Append("::").Append(primatelj).ToString());

            return result;
        }
    }
}
