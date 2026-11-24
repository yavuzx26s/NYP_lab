using System;
using System.Collections.Generic;

public enum CihazTuru
{
    Lamba,
    Klima,
    GuvenlikSistemi
}

public class Cihaz
{
    public int Kimlik { get; set; }
    public string Ad { get; set; }
    public CihazTuru Tur { get; set; }
    public bool AcikMi { get; private set; }

    public Cihaz(int kimlik, string ad, CihazTuru tur)
    {
        Kimlik = kimlik;
        Ad = ad;
        Tur = tur;
        AcikMi = false;
    }

    public void Ac()
    {
        if (AcikMi)
        {
            Console.WriteLine($"'{Ad}' zaten açık.");
        }
        else
        {
            AcikMi = true;
            Console.WriteLine($"'{Ad}' açıldı.");
        }
    }

    public void Kapat()
    {
        if (!AcikMi)
        {
            Console.WriteLine($"'{Ad}' zaten kapalı.");
        }
        else
        {
            AcikMi = false;
            Console.WriteLine($"'{Ad}' kapatıldı.");
        }
    }

    public void BilgileriGoster()
    {
        string durum = AcikMi ? "Açık" : "Kapalı";
        Console.WriteLine($"ID: {Kimlik} - Ad: {Ad} - Tür: {Tur} - Durum: {durum}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Cihaz> cihazlar = new List<Cihaz>();
        cihazlar.Add(new Cihaz(1, "Salon Lambası", CihazTuru.Lamba));
        cihazlar.Add(new Cihaz(2, "Yatak Odası Kliması", CihazTuru.Klima));
        cihazlar.Add(new Cihaz(3, "Giriş Kamerası", CihazTuru.GuvenlikSistemi));

        bool devam = true;

        while (devam)
        {
            Console.WriteLine("--- AKILLI EV CİHAZ YÖNETİMİ ---");
            Console.WriteLine("1. Cihazları Listele");
            Console.WriteLine("2. Yeni Cihaz Ekle");
            Console.WriteLine("3. Cihaz Aç");
            Console.WriteLine("4. Cihaz Kapat");
            Console.WriteLine("5. Çıkış");
            Console.Write("Seçiminiz: ");

            string secim = Console.ReadLine();
            Console.WriteLine();

            switch (secim)
            {
                case "1":
                    CihazlariListele(cihazlar);
                    break;
                case "2":
                    YeniCihazEkle(cihazlar);
                    break;
                case "3":
                    CihazDurumDegistir(cihazlar, true);
                    break;
                case "4":
                    CihazDurumDegistir(cihazlar, false);
                    break;
                case "5":
                    devam = false;
                    Console.WriteLine("çıkılıyor...");
                    break;
                default:
                    Console.WriteLine("Hata: Geçersiz bir seçim yaptınız.");
                    break;
            }
        }
    }

    static void CihazlariListele(List<Cihaz> cihazlar)
    {
        if (cihazlar.Count == 0)
        {
            Console.WriteLine("kayıtlı cihaz bulunmamaktadır.");
            return;
        }

        Console.WriteLine("Kayıtlı Cihazlar");
        for (int i = 0; i < cihazlar.Count; i++)
        {
            cihazlar[i].BilgileriGoster();
        }
    }

    static void YeniCihazEkle(List<Cihaz> cihazlar)
    {
        Console.Write("Cihaz Kimliği : ");
        int kimlik;
        if (!int.TryParse(Console.ReadLine(), out kimlik))
        {
            Console.WriteLine("Hata: Kimlik sayısal bir değer olmalı.");
            return;
        }

        for (int i = 0; i < cihazlar.Count; i++)
        {
            if (cihazlar[i].Kimlik == kimlik)
            {
                Console.WriteLine($"Hata: {kimlik} kimliğine sahip bir cihaz zaten var");
                return;
            }
        }

        Console.Write("Cihaz Adı: ");
        string ad = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(ad))
        {
            Console.WriteLine("Hata: Cihaz adı boş bırakılamaz.");
            return;
        }

        Console.Write("Cihaz Türü Seçiniz (1: Lamba, 2: Klima, 3: GuvenlikSistemi): ");
        string turSecim = Console.ReadLine();
        CihazTuru secilenTur;

        if (turSecim == "1")
        {
            secilenTur = CihazTuru.Lamba;
        }
        else if (turSecim == "2")
        {
            secilenTur = CihazTuru.Klima;
        }
        else if (turSecim == "3")
        {
            secilenTur = CihazTuru.GuvenlikSistemi;
        }
        else
        {
            Console.WriteLine("Hata: Geçersiz cihaz türü seçimi.");
            return;
        }

        cihazlar.Add(new Cihaz(kimlik, ad, secilenTur));
        Console.WriteLine($"'{ad}' sisteme başarıyla eklendi.");
    }

    static void CihazDurumDegistir(List<Cihaz> cihazlar, bool acilsinMi)
    {
        Console.Write("cihazın Kimliği : ");
        int kimlik;
        if (!int.TryParse(Console.ReadLine(), out kimlik))
        {
            Console.WriteLine("Hata: Geçersiz kimlik.");
            return;
        }

        Cihaz bulunanCihaz = null;
        for (int i = 0; i < cihazlar.Count; i++)
        {
            if (cihazlar[i].Kimlik == kimlik)
            {
                bulunanCihaz = cihazlar[i];
                break;
            }
        }

        if (bulunanCihaz == null)
        {
            Console.WriteLine($"Hata: {kimlik} kimliğine sahip bir cihaz bulunamadı");
            return;
        }

        if (acilsinMi)
            bulunanCihaz.Ac();
        else
            bulunanCihaz.Kapat();
    }
}