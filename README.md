
# Sharp Shooter - 3D FPS Game 🔫

<img width="981" height="468" alt="image" src="https://github.com/user-attachments/assets/cf2041b5-f887-470b-a158-4833a8a2f435" />


## 📝 Deskripsi Projek
**Sharp Shooter** adalah sebuah game *First-Person Shooter* (FPS) 3D bertema Sci-Fi yang dikembangkan menggunakan **Unity 6.2**. Projek ini fokus pada pengalaman *gunplay* yang responsif, pergerakan karakter yang realistis (*immersive movement*), dan manajemen *game loop* yang utuh.

Projek ini dibuat sebagai portofolio untuk mendemonstrasikan kemampuan implementasi logika C#, NavMesh AI, Unity New Input System, dan UI Management.

## 🎮 Fitur Utama (Key Features)

### 🏃‍♂️ Advanced Player Controller
- **Realistic Movement:** Implementasi *custom friction* untuk mencegah efek "sliding" pada pergerakan karakter.
- **Procedural Head Bobbing:** Sistem kamera dinamis yang bereaksi terhadap keadaan *Idle, Walking, Sprinting,* dan *Landing Impact* (hentakan saat mendarat).
- **Dynamic Camera Tilt:** Kamera miring secara halus saat melakukan *strafing* (gerak kiri/kanan).

### 🔫 Combat & Weapon System
- **ScriptableObject Architecture:** Data senjata (Damage, Fire Rate, Ammo) disimpan secara modular menggunakan ScriptableObjects.
- **3 Tipe Senjata:** Pistol, Assault Rifle (Full Auto), dan Sniper Rifle (dengan fitur Scope/Zoom).
- **Shooting Mechanics:** Menggunakan Raycasting presisi dengan efek *Muzzle Flash*, *Impact VFX*, dan *Recoil*.
- **Pickup System:** Sistem *Respawning Item* dimana senjata/ammo yang diambil akan muncul kembali setelah durasi tertentu menggunakan Coroutine.

### 🤖 AI & Enemy System
- **NavMesh AI:** Musuh (Robot) mengejar player menggunakan pathfinding cerdas.
- **Dynamic Enemy Counter:** Sistem "Absensi" di `GameManager` untuk menghitung jumlah musuh secara *real-time*, mencegah bug "Ghost Enemy" saat musuh jatuh keluar map atau spawn dari portal.
- **Kamikaze Logic:** Musuh meledakkan diri dan memberikan damage area saat menyentuh player.

### ✨ Polish & Game Juice
- **Immersive Feedback:** Efek *Screen Shake*, *Damage Overlay* (Vignette Darah), dan suara *Hurt/Ouch* saat terkena hit.
- **Dramatic Victory:** Efek *Slow Motion* dan transisi audio saat musuh terakhir dikalahkan.
- **Robust Scene Management:** Sistem Loading Screen asinkron (*Async Loading*) dengan Progress Bar yang menghubungkan Lobby, Gameplay, dan Menu.

## 🛠️ Teknologi yang Digunakan
- **Engine:** Unity 6
- **Language:** C#
- **Input:** Unity New Input System Package
- **AI:** Unity NavMesh
- **VCS:** Unity Version Control / Git

## 🕹️ Kontrol (Controls)
| Input | Aksi |
| :--- | :--- |
| **W A S D** | Bergerak |
| **Shift** | Berlari (Sprint) |
| **Space** | Lompat |
| **Mouse Kiri** | Menembak |
| **Mouse Kanan** | Aim / Zoom (Sniper) |

## 📂 Struktur Kode (Code Highlights)
Beberapa implementasi teknis menarik dalam projek ini:

- **GameManager (Singleton Pattern-like):** Bertindak sebagai wasit pusat yang mengatur *Win/Lose condition*, *Time Scale*, dan UI Flow.
- **Static Scene Loader:** Class statis `Loader.cs` yang menangani perpindahan scene melewati *Loading Scene* perantara.
- **Inheritance pada Pickup:** Menggunakan *Base Class* `Pickup` untuk logika respawn, dan *Derived Class* `WeaponPickup`/`AmmoPickup` untuk efek spesifik.

## 📸 Video Demonstration
**Winning Scenario**


https://github.com/user-attachments/assets/2ddb4729-ef2d-4edb-a9b6-bcc2afec106a

**Losing Scenario**


https://github.com/user-attachments/assets/ef7d7db3-4cd1-4986-ba08-18b6ce16bb0f



---
*Dibuat oleh Alif As'ad Ramadhan*
