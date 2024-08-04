# LHDC UBI Vezénylés szerkesztő

## Beállítások

A beállítások ablakot a Fájl > Beállítások útvonalon érheti el.

A beállításokban a műszakok színeit, illetve számszerű beállításokat tud módosítani.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/settings.png)

## Használata

### Új munkáltatott hozzáadása

Kattintson a képen látható menüpontra (Fájl > Munkáltatott > Hozzáadás), majd vegye fel a munkáltatott adatait.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20employee%201.png)

Munkáltatott adatai:
- Neve
- Neme
- Száma (automatikusan ajánlott sorszám)
- Típusa (munkáltatott / supervisor)
- Utolsó vizsga dátuma (lejárat előtt értesít a program)
- Kártya lejárati dátuma (lejárat előtt értesít a program)
- Sikeres vizsgák (VÉD 1-4, FEL, belső jogosítvány)
- Távolság (statisztika oldalon számoláshoz)
- Kivel tud együtt munkába járni (a program ezzel is tud számolni beosztás generálásakor)

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20employee%202.png)

Együtt tudnak bejárni beállításhoz kattintson a szerkesztés gombra, majd válassza ki a kollégákat. A lista törléséhez kattintson a törlés gombra.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20employee%203.png)

### Munkáltatott szerkesztése

Jobb kattintás a munkáltatott nevére, majd a szerkesztés menüpont kiválasztása.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20employee%204.png)

A továbbiakban az előző bekezdés alapján működik az eljárás.

## Járat hozzáadása

Kattintson a képen látható menüpontra (Fájl > Járat > Hozzáadás), majd vegye fel a járat adatait.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20flight%201.png)

Vagy jobb kattintás a napra és kattintson a járat hozzáadása menüpontra.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20flight%202.png)

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20flight%203.png)

Járat adatai:
- Úticél
- Légitársaság
- Check in kezdés (kattintson a naptár ikonra a dátum beállításához)

Amennyiben a második módszert alkalmazta, a dátum automatikusan be van állítva, csak az időpontot kell módosítani.
Ha a hetente ismétlődő pontot bejelöli, a program a hónapban minden hét adott napjára automatikusan beilleszti a járatot ugyanazzal az időponttal.

## Kérés

### Hozzáadása

Jobb kattintás a munkáltatott nevére, majd kérés hozzáadása (Kérés > Hozzáadás) menüpont kiválasztása.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20request%201.png)

Vagy jobb kattintás a napra (a munkáltatottal megegyező sorban) és kérés hozzáadása (Kérés > Hozzáadás) menüpont kiválasztása.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20request%202.png)

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/add%20request%203.png)

Kérés adatai:
- Szabadnap / szabadság / éjszakás kiválasztása
- Szabadnap esetén fontos / nem fontos
- Kérés napja (kattintson a naptár ikonra a dátum kiválaszásához)

Amennyiben a második módszert alkalmazta, a dátum automatikusan be van állítva.
A nem fontos kérések (alap beállítás szerint) sárga, a fontos kérések piros színnel jelennek meg. A szabadság mindig fontos, illetve az éjszakás mindig nem fontos színekkel jelenik meg.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/requested%20days%201.png)

### Törlése

Jobb kattintás a napra (amelyik kérést törölni szeretné) és a kérés törlése (Kérés > Törlés) menüpont kiválasztása.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/delete%20request%201.png)

A kérés automatikusan törlődni fog. Éjszakás kérés törlése esetén bármelyik napra (éjszakás kezd / végez) lehet kérni a törlést, mind a kettő műszakot törölni fogja a program.

### Több óra a hónapban

Jobb kattintás a munkáltatott nevére, vagy bármelyik napra a munkáltatottal megegyező sorban, majd a legördülő menü (Kérés > Legördülő menü) kiválasztása.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/more%20hours%201.png)

- Normál órák (nincs változás a beosztás generálásában)
- Több óra a hónapban (a munkáltatott több órát fog kapni az átlagnál)

## Betegség

### Bejelentése

Jobb kattintás a munkáltatott nevére, majd a betegség bejelentése (Betegség > Bejelentés) menüpont kiválasztása.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/sick%201.png)

Vagy jobb kattintás a napra (a munkáltatottal megegyező sorban) és betegség bejelentése (Betegség > Bejelentés) menüpont kiválasztása.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/sick%202.png)

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/sick%203.png)

- Dátum (kattintson a naptár ikonra a dátum kiválaszásához)
- Helyettesítő (legördülő menü, csak az aznap készenlétesek vannak felsorolva)

Amennyiben a második módszert alkalmazta, a dátum automatikusan be van állítva.
A bejelentett betegség (alapbeállítás szerint) sárga színnel jelenik meg, a helyettesítő számával együtt. A helyettesítő készenlétes műszakjának helyére automatikusan be lesz írva a beteg műszakja.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/sick%204.png)

### Törlése

Jobb kattintás a napra (amelyik betegséget törölni szeretné) és a betegség törlése (Betegség > Törlés) menüpont kiválasztása.

![](https://github.com/hidvegarpi/DEB/blob/main/IMAGES/sick%205.png)