# Görev Yönetimi Uygulaması

Bu proje, ASP.NET MVC kullanılarak geliştirilmiş bir görev yönetimi uygulamasıdır. Kullanıcılar, görevlerini oluşturabilir, düzenleyebilir, arşivleyebilir ve durumlarını takip edebilir.

## Başlangıç

1.  Depoyu klonlayın: `git clone https://github.com/AliGurSoftDev/TaskManager.git`
2.  Visual Studio'da çözümü açın.
3.  **Veritabanını Güncelle:**
    * Paket Yöneticisi Konsolu'nu açın (Görünüm -> Diğer Pencereler -> Paket Yöneticisi Konsolu).
    * `Update-Database` komutunu çalıştırın.
4.  Uygulamayı çalıştırın.

**Not:** Bu proje SQL Server LocalDB kullanmaktadır. Eğer LocalDB yüklü değilse, yüklemeniz gerekebilir.

## Özellikler

* Görevleri listeleme ve oluşturma Index ekranından gerçekleştirilir.
* Görevler durum bilgilerine göre filtrelenebilir.
* Görevleri oluşturma AJAX ile sayfa yenilenmeden yapılır.
* Görevleri Düzenleme ve Silme operasyonları ise kendilerine ait sayfalardan gerçekleştirilir.
* Görev Adı alanı zorunludur ve kontrolü yapılmaktadır.
* Girilen tarih geçmiş bir zaman olmamalıdır ve kontolü yapılmaktadır.
* Görevler silinirken kullanıcıya arşive ekleme seçeneği sunulur.
* Arşive eklenen görevler üzerinde değişiklik yapılamaz, sadece arşivden kaldırılabilir.
