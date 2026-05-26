# 🏢 SIGORTA KOPRUSU (Insurance Bridge)
> **C# ve MS SQL Server Tabanlı, Gelişmiş Kurumsal Sigorta Yönetim ve Takip Sistemi**

Sigorta Köprüsü, sigorta acenteleri ve müşterileri arasındaki iş akışını tamamen dijitalleştirmek, poliçe yönetimini, müşteri takibini ve finansal verileri tek bir merkezden güvenli bir şekilde yönetmek için geliştirilmiş kurumsal bir **Sigorta Yönetim Sistemi (Insurance Management Software)** yazılımıdır.

Sistem, karmaşık sigorta verilerini optimize edilmiş bir ilişkisel veri tabanında tutarken, kullanıcıya hızlı ve modern bir deneyim sunar. Kurumsal standartlara uygun  bileşenleriyle tasarlanan arayüzü, kullanıcıların en kritik işlemleri en az tıklamayla gerçekleştirmesini sağlar.

---

## 🛠️ Kullanılan Teknolojiler ve Mimari

* **Geliştirme Dili:** C# (.NET)
* **Arayüz Teknolojisi:** .NET MAUI / Windows Forms (Responsive)
* **Veri Tabanı:** MS SQL Server
* **Veri Erişimi / ORM:** ADO.NET / Entity Framework
* **Veri Tabanı Programlama:** T-SQL (Stored Procedures, Triggers, Views)

---

##  Öne Çıkan Özellikler ve Fonksiyonel Mimari

### 1. Optimize Edilmiş İlişkisel Veri Tabanı Mimarisi
Projenin temelini oluşturan SQL Server veri tabanı; *Müşteriler, Poliçeler, Araç/Varlık Bilgileri, Ödemeler ve Sigorta Şirketleri* tablolarını **Primary/Foreign Key** ilişkileriyle kusursuz bir şekilde birbirine bağlar. Veri tekrarını önleyen (Normalization) bu yapı sayesinde sorgular en yüksek performansla çalışır.

### 2. Dinamik Müşteri ve Varlık Yönetimi
Kullanıcılar; müşterilerin kişisel bilgilerini ve sigortalanacak varlıklarını (araç, konut vb.) sisteme dinamik olarak işleyebilir. Gelişmiş arama ve filtreleme algoritmaları sayesinde binlerce kayıt arasından anlık sorgulama yapılabilir.

### 3. Poliçe Oluşturma ve Vadeli Takip Motoru
Sistem; seçilen sigorta türüne, varlığın durumuna ve risk faktörlerine göre dinamik poliçe girdisi üretir. Başlangıç ve bitiş tarihlerini otomatik hesaplayarak süresi yaklaşan poliçeler için uyarı ve takip mekanizmalarını tetikler.

### 4. Finansal Takip ve Ödeme Planı (Cari Hesap)
Her poliçenin ödeme adımları (peşin veya taksitli seçenekler) veri tabanında cari hareket olarak tutulur. Kullanıcı aksiyonlarına ve ödeme durumlarına göre müşterinin borç/alacak bakiyesi anlık olarak güncellenir ve raporlanır.

### 5. Kurumsal Görsel Kimlik (UI/UX)
Resmi ve profesyonel bir uygulama standardı yakalamak adına, arayüz mimarisinde **kurumsal yeşil renk paleti** tercih edilmiştir. Bu sayede ekran karşısında uzun saatler geçiren acente personeli için konforlu bir veri takibi ve kullanıcı deneyimi sağlanmıştır.

---

## 🎯 Proje Sonucu ve Kazanımlar
**Sigorta Köprüsü**, masaüstü ve mobil uygulama geliştirme pratiklerinin güçlü bir veri tabanı yönetimiyle birleştiğinde ne kadar kararlı ve ticari potansiyeli yüksek bir ürüne dönüşebileceğini göstermektedir. Proje; karmaşık sigorta süreçlerini basitleştirerek hata payını minimuma indirmeyi ve kurumsal işletmelerin dijital dönüşümüne tam uyumlu bir altyapı sunmayı başarmıştır.


## 🔮 Gelecek Yol Haritası ve İleri Dönük Hedefler (Roadmap)

Projenin mevcut kararlı sürümünün ardından, sistemi daha akıllı, modüler ve genişletilebilir hale getirmek için planlanan ileri dönük hedefler ve eklenecek modüller şunlardır:

* **Acente ve Alt Acente (Sub-Agency) Modülü:** Farklı sigorta acentelerinin sisteme dahil olabileceği, merkez acentenin alt acentelerin komisyon ve satış süreçlerini yönetebileceği çoklu katmanlı (Multi-tenancy) bir altyapıya geçiş.
* **Otomatik Hatırlatma ve Bildirim Sistemleri (SMS / E-posta):** Poliçe bitiş tarihi yaklaşan veya taksit ödemesi bulunan müşterilere, arka planda çalışan bir Windows Service veya Cron Job vasıtasıyla otomatik SMS ve e-posta bildirimleri gönderilmesi.
* **Web ve Mobil Müşteri Portalı:** .NET MAUI altyapısını tamamen cross-platform (çapraz platform) standartlarına taşıyarak, sigorta sigortalılarının kendi poliçelerini görüntüleyebileceği, hasar ihbarı yapabileceği bir web/mobil müşteri arayüzünün geliştirilmesi.
* **Gelişmiş Raporlama ve BI (İş Zekası) Entegrasyonu:** Grafiksel finansal raporlar, dönemsel kârlılık analizleri ve satış performanslarını dinamik dashboard'lar (görsel paneller) üzerinden sunacak veri analitiği araçlarının sisteme dahil edilmesi.
