# 📡 MyNetworkScanner, avagy ki a franc lopja a wifit? (C#)

Egy gyors, aszinkron és többszálú (multithreaded) hálózati szkenner alkalmazás, amely feltérképezi a helyi hálózatot (LAN), azonosítja az aktív eszközöket, és megpróbálja feloldani a hosztneveiket.


## 🚀 Funkciók

-   **Gyors szkennelés:** Párhuzamosan vizsgálja a teljes alhálózatot (254 IP cím) 1-2 másodperc alatt.
-   **Pingelés:** ICMP csomagok küldése az eszközök elérhetőségének ellenőrzésére.
-   **DNS Névfeloldás:** Megpróbálja lekérdezni az eszközök hálózati nevét (Hostname).
-   **Késleltetés mérése:** Megjeleníti a válaszidőt (ping) ezredmásodpercben.
-   **Automatikus IP detektálás:** A program felismeri a gép saját IP tartományát.

## 🛠️ Technológiai Háttér

A projekt **.NET (Core)** alapokon nyugszik, és az alábbi megoldásokat használja:

-   **Nyelv:** C#
-   **Hálózat:** `System.Net.NetworkInformation` (Ping), `System.Net` (DNS)
-   **Aszinkronitás:** `async` / `await` minták a blokkolásmentes futásért.
-   **Párhuzamosítás:** `Task.WhenAll` használata a 254 ping kérés egyidejű futtatásához.

### Architektúra (Clean Code)

A kód felépítése követi a **Separation of Concerns** elvét:

1.  **Model (`NetworkDevice`):** Csak az adatokat tárolja (IP, Hostname, Latency).
2.  **Interface (`INetworkScanner`):** Definiálja a működést, lehetővé téve a későbbi bővítést vagy tesztelést.
3.  **Service (`NetworkScannerService`):** Tartalmazza az üzleti logikát (pingelés, hibakezelés).
4.  **UI (`Program.cs`):** A felhasználói interakcióért és az eredmények megjelenítéséért felel.

## 💻 Hogyan működik?

A hagyományos, szinkron szkennerek egyesével pingelik a címeket (1...2...3), ami perceket vehet igénybe.
Ez az alkalmazás a **Task Parallel Library (TPL)** segítségével elindítja mind a 254 ping kérést szinte egyszerre, majd megvárja, amíg mindegyik befejeződik.

**Kódrészlet:**
```csharp
// Minden IP címhez indítunk egy Task-ot
var tasks = new List<Task<NetworkDevice>>();
for (int i = 1; i < 255; i++)
{
    tasks.Add(CheckIpAddressAsync(currentIp));
}

// Megvárjuk az összeset egyszerre
var results = await Task.WhenAll(tasks);
