# 🏥 Pediatri Asistan Nöbet ve Yönetim Sistemi

Bu proje, bir üniversite hastanesinin Pediatri anabilim dalı için geliştirilmiş; asistan nöbetlerinin, öğretim üyesi randevularının ve bölüm içi acil durumların yönetildiği kapsamlı bir web tabanlı otomasyon sistemidir.

Proje, **ASP.NET MVC 5** mimarisi üzerine inşa edilmiş olup, kullanıcı deneyimini artırmak (SPA hissi vermek) amacıyla tüm CRUD işlemleri **AJAX ve Bootstrap Modals** kullanılarak "Single Page" mantığıyla tasarlanmıştır.

---

## 🚀 Öne Çıkan Özellikler

### 🔐 Yönetim Paneli (Admin Dashboard)
* **Dinamik Dashboard:** Anlık asistan sayısı, nöbetçi sayısı ve bekleyen randevuların istatistiksel gösterimi.
* **AJAX Tabanlı CRUD:** Sayfa yenilenmeden; Asistan, Öğretim Üyesi, Bölüm ve Nöbet ekleme/silme/güncelleme işlemleri.
* **Nöbet Yönetimi:** Asistanların nöbetlerinin tarihe ve bölüme göre atanması.
* **Müsaitlik Yönetimi:** Öğretim üyeleri için randevu saatlerinin (Slot) belirlenmesi.
* **Güvenlik:** Yönetici girişlerinde **SHA-256** şifreleme algoritması.

### 👩‍⚕️ Kullanıcı Arayüzü (Ön Yüz)
* **Nöbet Takvimi (FullCalendar):** Asistanların aylık nöbet listesini görsel takvim üzerinde görüntülemesi.
* **Randevu Sistemi:** Öğrencilerin, hocaların açtığı müsait saatlere randevu alabilmesi.
* **Anlık Duyurular:** Bölüm içi acil durumların (Kan ihtiyacı, toplantı vb.) anlık listelenmesi.
* **Akademik Kadro & Bölümler:** Dinamik olarak listelenen doktor ve poliklinik bilgileri.

---

## 🛠️ Kullanılan Teknolojiler

* **Backend:** C#, ASP.NET MVC 5, Entity Framework 6 (Code First)
* **Frontend:** HTML5, CSS3, Bootstrap 5, JavaScript (jQuery)
* **Veritabanı:** MS SQL Server
* **Kütüphaneler & Araçlar:**
    * *FullCalendar.js* (Nöbet Takvimi için)
    * *SimpleDatatables* (Veri listeleme için)
    * *SHA256* (Şifreleme için)

---

## 💻 Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/KULLANICI_ADINIZ/PediatriNobetSistemi.git](https://github.com/KULLANICI_ADINIZ/PediatriNobetSistemi.git)
    ```

2.  **Veritabanı Bağlantısı:**
    `Web.config` dosyasını açın ve `connectionStrings` bölümündeki sunucu adını kendi SQL Server adınıza göre düzenleyin.

3.  **Veritabanını Oluşturun (Code First):**
    Visual Studio'da `Package Manager Console`'u açın ve sırasıyla şu komutları uygulayın:
    ```bash
    update-database
    ```

4.  **Projeyi Başlatın:**
    `CTRL + F5` ile projeyi tarayıcıda açın.

---

## 📷 Ekran Görüntüleri

### 1. Ana Ekran
(Screenshots/ana_ekran.png)

### 2. Yönetim Paneli (Dashboard)
(Screenshots/dashboard.png)

### 3. Nöbet Atama (Modal & AJAX)
(Screenshots/nobet.png)

### 4. Nöbet Takvimi
(Screenshots/takvim.png)


---

## 👤 İletişim

* **Geliştirici:** Mümine Buran
* **LinkedIn:** 


---