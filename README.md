# Zadání cvičné praktické maturity
Váš program se bude připojovat k webové službě, získávat z ní data, zobrazovat je a analyzovat. Bude vytvořen buď jako aplikace pro Windows (WinForms nebo WPF) nebo webová aplikace (JS).

# Úkoly
  1. Vyzkoušejte komunikaci s webovou službou na adrese http://maturita.delta-studenti.cz/prakticka/cvicna-tajne-funkce/tajne-funkce.php .
    - požadavky odesílejte jako GET s proměnnými funcName a xVal
    - proměnná funcName může mít dvě řetězcové hodnoty - "funcF" a "funcG"
    - proměnná xVal je reálné číslo
    - pokud je požadavek v pořádku, je vrácen stavový kód 200 a JSON ve tvaru 

    - kde yVal vrací hodnotu dotazované funkce funcName v bodě x = xVal. Vyzkoušejte např. http://maturita.delta-studenti.cz/prakticka/cvicna-tajne-funkce/tajne-funkce.php?funcName=funcG&xVal=-4 .
    - V případě neplatného požadavku je vrácen stavový kód 400 a odpovídající JSON.
  2. Vytvořte textové rozhraní pro tuto službu, tzn. výběrový prvek, ve kterém bude zvolena funkce F nebo G a číselný vstup, do kterého uživatel vepíše číslo x. Po stisku tlačítka "Zjisti hodnotu" vypíše hodnotu dotazované funkce v daném bodě x.
  3. Vytvořte prohlížeč průběhu funkce
    - Uživatel zvolí dotazovanou funkci, hodnoty x_min a x_max a počet bodů (předvolte 300).
    - Program grafickým způsobem vykreslí všechny body. Pro web se nabízí použití canvasu, SVG nebo libovolné kreslicí knihovny, pro WPF lze umisťovat body přímo do canvasu.
  4. Vytvořte analyzátor funkcí na intervalu <-4; 8>
    - V určitém úseku má funkce G nižší hodnotu , než funkce F. Analyzátor najde hranice této oblasti.
    - Analyzátor zjistí, kde má funkce G mezi krajními body intervalu kořen, tzn. její hodnota prochází nulou (s přesností 0.001)
Považujte za dané, že mezi hodnotami -4 a 8 určitě alespoň jednou nulou prochází 
(návod: zvolíme si interval = dva hraniční body a zkoušíme to tak dlouho, než je jeden nad nulou a druhý pod nulou. Pak zkusíme interval rozpůlit a podle hodnoty funkce v polovině vzdálenosti nahradíme buď jeden nebo druhý krajní bod intervalu tak, abychom měli pořád jeden bod nad nulou a druhý pod nulou. Pak znovu půlíme...)
    - Analyzátor najde všechny kořeny na daném intervalu.
    - Analyzátor najde pro funkci G na intervalu lokální maxima a minima (místa, od nichž na obě strany funkce klesá resp. roste). Návod: Zvol dva body blízko sebe a jdi ve směru růstu. Až zjistíš, že při dalším kroku hodnota neroste, dělej něco podobného jako v předchozím bodě
## Další info
  - Zadání je rozsáhlé, aby i dobří programátoři měli celou dobu co dělat. Pakliže nestihnete naprogramovat řešení celé, odevzdáváte jen jeho část. Snažte se splnit „alespoň něco alespoň nějak“, i když přitom zadání nedodržíte zcela přesně.
  - Vzhled není příliš důležitý, dokud jsou snadno odlišitelné ovládací prvky a výstupy, které k nim patří
# Odevzdání
  - V případě projektu Windows odevzdáte zazipované celé řešení (solution) včetně zdrojových kódů i zkompilovaného řešení.
  - V případě webového projektu odevzdáte zazipované funkční řešení, které lze nasadit na server. Pakliže použijete některou z knihoven pro transpilování či preprocessing kódu, budou v zip archivu i původní zdrojové kódy.
Pokud není struktura zcela očividná, přidejte do archivu soubor readme.txt, ve kterém zdokumentujete, kde lze nalézt kterou část řešení.
