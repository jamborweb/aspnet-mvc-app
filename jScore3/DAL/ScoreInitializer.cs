using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using jScore3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace jScore3.DAL
{
    public enum Woj
    {
        Dolnośląskie,
        KujawskoPomorskie,
        Lubelskie,
        Lubuskie,
        Łódzkie,
        Małopolskie,
        Mazowieckie,
        Opolskie,
        Podkarpackie,
        Podlaskie,
        Pomorskie,
        Śląskie,
        Świętokrzyskie,
        WarmińskoMazurskie,
        Wielkopolskie,
        Zachodniopomorskie
    };
    public class ScoreInitializer : DropCreateDatabaseIfModelChanges<ScoreContext>
    //DropCreateDatabaseAlways<ScoreContext>

    {
        protected override void Seed(ScoreContext context)
        {

            //base.Seed(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Userek"));

            var user = new ApplicationUser { UserName = "admin@jscore.com" };
            string password = "Password.12345";
            userManager.Create(user, password);
            userManager.AddToRole(user.Id, "Admin");

            var user2 = new ApplicationUser { UserName = "userek@jscore.com" };
            string password2 = "Password.12345";
            userManager.Create(user2, password2);
            userManager.AddToRole(user2.Id, "Userek");

            var kluby = new List<Klub>
            {
                new Klub { Nazwa = "Śląsk Wrocław", Adres = "al. Śląska 1, Wrocław", KodPocztowy = "54-145", Wojewodztwo = Woj.Dolnośląskie.ToString(), Barwy = "zielono-biało-czerwone", DataPowstania = new DateTime(1947,02,18), Herb = "slaskh.jpg"},
                new Klub { Nazwa = "Legia Warszawa", Adres = "ul. Łazienkowska 3, Warszawa", KodPocztowy = "00-449", Wojewodztwo = Woj.Mazowieckie.ToString(), Barwy = "czerwono-biało-zielono-czarne", DataPowstania = new DateTime(1916,01,01), Herb = "legiah.jpg"},
                new Klub { Nazwa = "Lech Poznań", Adres = "ul. Bułgarska 17, Poznań", KodPocztowy = "60-320", Wojewodztwo = Woj.Wielkopolskie.ToString(), Barwy = "niebiesko-białe", DataPowstania = new DateTime(1920,08,03), Herb = "poznanh.jpg"},
                new Klub { Nazwa = "Jagiellonia Białystok", Adres = "ul. Legionowa 28, Białystok", KodPocztowy = "15-281", Wojewodztwo = Woj.Podlaskie.ToString(), Barwy = "żółto-czerwone", DataPowstania = new DateTime(1920,05,30), Herb="jagah.jpg"}
            };
            kluby.ForEach(p => context.Kluby.Add(p));
            context.SaveChanges();



            var sezony = new List<Sezon>
            {
                new Sezon { Nazwa = "2017/2018"},
                new Sezon { Nazwa = "2018/2019"}
            };
            sezony.ForEach(ss => context.Sezony.Add(ss));
            context.SaveChanges();

            var stadiony = new List<Stadion>
            {
                new Stadion {Nazwa = "Stadion Wrocław", Pojemnosc = 42771, Miasto = "Wrocław", Zdjecie = "slask.jpg"},
                new Stadion {Nazwa = "INEA Stadion", Pojemnosc = 42837, Miasto = "Poznań", Zdjecie = "inea.jpg"},
                new Stadion {Nazwa = "Stadion Energa Gdańsk", Pojemnosc = 41620, Miasto = "Gdańsk", Zdjecie = "energa.jpg"},
                new Stadion {Nazwa = "Stadion Miejski Legii Warszawa", Pojemnosc = 30967, Miasto = "Warszawa", Zdjecie = "legia.jpg"}
            };
            stadiony.ForEach(s => context.Stadiony.Add(s));
            context.SaveChanges();

            var zawodnicy = new List<Zawodnik>
            {
                new Zawodnik { Imie = "Marcin", Nazwisko = "Kozłowski", DataUrodzenia = new DateTime(1920,05,30), KlubId = 1, KrajPochodzenia = "Polska", NumerKoszulki = 20, Waga = 65.0, Wzrost = 175.0},
                new Zawodnik { Imie = "Bartosz", Nazwisko = "Bereszyński", DataUrodzenia = new DateTime(1980,05,30), KlubId = 2, KrajPochodzenia = "Polska", NumerKoszulki = 11, Waga = 63.0, Wzrost = 179.0, Zdjecie = "beres.jpg"},
                new Zawodnik { Imie = "Kuba", Nazwisko = "Błaszczykowski", DataUrodzenia = new DateTime(1989,05,30), KlubId = 1, KrajPochodzenia = "Polska", NumerKoszulki = 16, Waga = 70.0, Wzrost = 165.0, Zdjecie = "blaszczyk.jpg"},
                new Zawodnik { Imie = "Sławomir", Nazwisko = "Peszko", DataUrodzenia = new DateTime(1980,05,30), KlubId = 2, KrajPochodzenia = "Polska", NumerKoszulki = 5, Waga = 69.0, Wzrost = 178.0, Zdjecie = "peszko.jpg"},
                new Zawodnik { Imie = "Dawid", Nazwisko = "Kapustka", DataUrodzenia = new DateTime(1990,05,30), KlubId = 1, KrajPochodzenia = "Polska", NumerKoszulki = 22, Waga = 70.0, Wzrost = 185.0},
                /*Lech Poznan id3*/
                new Zawodnik { Imie = "Matus", Nazwisko = "Putnocky", DataUrodzenia = new DateTime(1984,11,01), KlubId = 3, KrajPochodzenia = "Słowacja", NumerKoszulki = 30, Waga = 63.0, Wzrost = 179.0},
                new Zawodnik { Imie = "Vernon", Nazwisko = "De Marco", DataUrodzenia = new DateTime(1992,11,01), KlubId = 3, KrajPochodzenia = "Argentyna", NumerKoszulki = 3, Waga = 73.0, Wzrost = 175.0},
                new Zawodnik { Imie = "Rafal", Nazwisko = "Janicki", DataUrodzenia = new DateTime(1992,11,01), KlubId = 3, KrajPochodzenia = "Polska", NumerKoszulki = 26, Waga = 86.0, Wzrost = 189.0, Zdjecie="janicki.jpg"},
                new Zawodnik { Imie = "Maciej", Nazwisko = "Orlowski", DataUrodzenia = new DateTime(1994,11,01), KlubId = 3, KrajPochodzenia = "Polska", NumerKoszulki = 28, Waga = 69.0, Wzrost = 189.0},
                new Zawodnik { Imie = "Marcin", Nazwisko = "Wasielewski", DataUrodzenia = new DateTime(1994,11,01), KlubId = 3, KrajPochodzenia = "Polska", NumerKoszulki = 37, Waga = 69.0, Wzrost = 189.0},
                new Zawodnik { Imie = "Pedro", Nazwisko = "Tiba", DataUrodzenia = new DateTime(1988,11,01), KlubId = 3, KrajPochodzenia = "Portugalia", NumerKoszulki = 25, Waga = 69.0, Wzrost = 189.0},
                new Zawodnik { Imie = "Łukasz", Nazwisko = "Trałka", DataUrodzenia = new DateTime(1984,11,01), KlubId = 3, KrajPochodzenia = "Polska", NumerKoszulki = 6, Waga = 69.0, Wzrost = 189.0},
                new Zawodnik { Imie = "Tomasz", Nazwisko = "Cywka", DataUrodzenia = new DateTime(1988,11,01), KlubId = 3, KrajPochodzenia = "Polska", NumerKoszulki = 19, Waga = 69.0, Wzrost = 189.0},
                new Zawodnik { Imie = "Maciej", Nazwisko = "Makuszewski", DataUrodzenia = new DateTime(1989,11,01), KlubId = 3, KrajPochodzenia = "Polska", NumerKoszulki = 17, Waga = 69.0, Wzrost = 189.0},
                new Zawodnik { Imie = "Christian", Nazwisko = "Gytkjaer", DataUrodzenia = new DateTime(1989,11,01), KlubId = 3, KrajPochodzenia = "Dania", NumerKoszulki = 32, Waga = 69.0, Wzrost = 189.0},
                new Zawodnik { Imie = "Joao", Nazwisko = "Amaral", DataUrodzenia = new DateTime(1991,11,01), KlubId = 3, KrajPochodzenia = "Portugalia", NumerKoszulki = 22, Waga = 69.0, Wzrost = 189.0},
                /*Legia Warszawa id2*/
                new Zawodnik { Imie = "Arkadiusz", Nazwisko = "Malarz", DataUrodzenia = new DateTime(1980,05,30), KlubId = 2, KrajPochodzenia = "Polska", NumerKoszulki = 1, Waga = 63.0, Wzrost = 179.0, Zdjecie="malarz.jpg"},
                new Zawodnik { Imie = "Artur", Nazwisko = "Jedrzejczyk", DataUrodzenia = new DateTime(1987,05,30), KlubId = 2, KrajPochodzenia = "Polska", NumerKoszulki = 55, Waga = 63.0, Wzrost = 179.0},
                new Zawodnik { Imie = "Mateusz", Nazwisko = "Wieteska", DataUrodzenia = new DateTime(1997,05,30), KlubId = 2, KrajPochodzenia = "Polska", NumerKoszulki = 4, Waga = 63.0, Wzrost = 179.0},
                new Zawodnik { Imie = "Inaki", Nazwisko = "Astiz", DataUrodzenia = new DateTime(1983,05,30), KlubId = 2, KrajPochodzenia = "Hiszpania", NumerKoszulki = 34, Waga = 63.0, Wzrost = 179.0},
                new Zawodnik { Imie = "Mateusz", Nazwisko = "Hołownia", DataUrodzenia = new DateTime(1998,05,30), KlubId = 2, KrajPochodzenia = "Polska", NumerKoszulki = 5, Waga = 63.0, Wzrost = 179.0},
                new Zawodnik { Imie = "Miguel", Nazwisko = "Cafu", DataUrodzenia = new DateTime(1993,05,30), KlubId = 2, KrajPochodzenia = "Portugalia", NumerKoszulki = 26, Waga = 63.0, Wzrost = 179.0},
                new Zawodnik { Imie = "Domagoj", Nazwisko = "Antolic", DataUrodzenia = new DateTime(1990,05,30), KlubId = 2, KrajPochodzenia = "Chorwacja", NumerKoszulki = 7, Waga = 63.0, Wzrost = 179.0},
                new Zawodnik { Imie = "Kasper", Nazwisko = "Hamalainen", DataUrodzenia = new DateTime(1986,05,30), KlubId = 2, KrajPochodzenia = "Finlandia", NumerKoszulki = 22, Waga = 63.0, Wzrost = 179.0},
                new Zawodnik { Imie = "Bartosz", Nazwisko = "Jamborski", DataUrodzenia = new DateTime(1995,09,30), KlubId = 2, KrajPochodzenia = "Polska", NumerKoszulki = 6, Waga = 75.0, Wzrost = 174.0, Zdjecie="jambor.jpg"},
                new Zawodnik { Imie = "Carlitos", Nazwisko = "Lopez", DataUrodzenia = new DateTime(1990,05,30), KlubId = 2, KrajPochodzenia = "Hiszpania", NumerKoszulki = 27, Waga = 69.0, Wzrost = 178.0},
                new Zawodnik { Imie = "Kante", Nazwisko = "Kante", DataUrodzenia = new DateTime(1990,05,30), KlubId = 2, KrajPochodzenia = "Gwinea", NumerKoszulki = 19, Waga = 69.0, Wzrost = 178.0}

            };
            zawodnicy.ForEach(z => context.Zawodnicy.Add(z));
            context.SaveChanges();

            var mecze = new List<Mecz>
            {
                new Mecz {Data = new DateTime(2018,01,01), GolGosc = 2, GolGospodarz = 1, GoscKlubId = 1, GospodarzKlubId = 2, Kolejka = 1, LiczbaWidzow = 18456, SezonId = 1, StadionId = 1},
                new Mecz {Data = new DateTime(2018,02,01), GolGosc = 6, GolGospodarz = 0, GoscKlubId = 2, GospodarzKlubId = 3, Kolejka = 1, LiczbaWidzow = 16000, SezonId = 1, StadionId = 2},
                new Mecz {Data = new DateTime(2018,03,01), GolGosc = 2, GolGospodarz = 2, GoscKlubId = 3, GospodarzKlubId = 4, Kolejka = 1, LiczbaWidzow = 32500, SezonId = 1, StadionId = 3},
                new Mecz {Data = new DateTime(2018,04,01), GolGosc = 3, GolGospodarz = 2, GoscKlubId = 1, GospodarzKlubId = 2, Kolejka = 1, LiczbaWidzow = 160, SezonId = 1, StadionId = 1}
                //new Mecz {Data = new DateTime(2018,05,01), GolGosc = 0, GolGospodarz = 1, GoscKlubId = 1, GospodarzKlubId = 2, Kolejka = 1, LiczbaWidzow = 160, SezonId = 1, StadionId = 1},
                //new Mecz {Data = new DateTime(2018,06,01), GolGosc = 1, GolGospodarz = 1, GoscKlubId = 1, GospodarzKlubId = 2, Kolejka = 1, LiczbaWidzow = 160, SezonId = 1, StadionId = 1},
            };
            mecze.ForEach(mm => context.Mecze.Add(mm));
            context.SaveChanges();


            var komentarze = new List<Komentarz>
            {
                new Komentarz {MeczId = 2, Tresc = "Super"},
                new Komentarz {MeczId = 2, Tresc = "Najlepszy"}
            };
            komentarze.ForEach(kk => context.Komentarze.Add(kk));
            context.SaveChanges();

            var pozycje = new List<Pozycja>
            {
                /*Lech Poznan id3*/
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Bramkarz", ZawodnikId = 6},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Lewy Obrońca", ZawodnikId = 7},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Środkowy Obrońca", ZawodnikId = 8},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Środkowy Obrońca", ZawodnikId = 9},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Prawy Obrońca", ZawodnikId = 10},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Lewy Pomocnik", ZawodnikId = 11},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Środkowy Pomocnik", ZawodnikId = 12},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Środkowy Pomocnik", ZawodnikId = 13},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Prawy Pomocnik", ZawodnikId = 14},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Napastnik", ZawodnikId = 15},
                new Pozycja {KlubId = 3,MeczId = 2, Nazwa = "Napastnik", ZawodnikId = 16},
                /*Legia Warszawa id2*/
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Bramkarz", ZawodnikId = 17},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Lewy Obrońca", ZawodnikId = 18},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Środkowy Obrońca", ZawodnikId = 19},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Środkowy Obrońca", ZawodnikId = 20},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Prawy Obrońca", ZawodnikId = 21},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Lewy Pomocnik", ZawodnikId = 22},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Środkowy Pomocnik", ZawodnikId = 23},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Środkowy Pomocnik", ZawodnikId = 24},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Prawy Pomocnik", ZawodnikId = 25},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Napastnik", ZawodnikId = 26},
                new Pozycja {KlubId = 2,MeczId = 2, Nazwa = "Napastnik", ZawodnikId = 27},
            };
            pozycje.ForEach(poz => context.Pozycje.Add(poz));
            context.SaveChanges();



        }
    }
}