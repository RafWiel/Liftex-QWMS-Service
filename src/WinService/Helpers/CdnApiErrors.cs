using System;
using System.Collections.Generic;
using System.Text;

namespace WinService.Helpers
{
    public class CdnApiErrors
    {
        private Dictionary<ErrorType, ApiError> _table;
        public enum ErrorType : byte
        { 
            XL_Login,
            XL_Logout,
            Article_AddArticle,
            Article_CloseArticle,
            Article_AddPrice,
            Article_ChangePrice,
            Article_AddGroup,
            Contractor_AddContractor,
            Contractor_CloseContractor,
            Document_AddDocument,
            Document_AddItem,
            Document_AddReceipt,            
            Document_CloseDocument,
            Attribute_AddAttribute,
            MagDocument_AddDocument,
            MagDocument_AddItem,
            MagDocument_CloseDocument,
            Order_AddDocument,
            Order_OpenDocument,
            Order_AddItem,
            Order_RemoveItem,
            Order_CloseDocument,
            StockTaking_AddDocument,
            StockTaking_AddItem,
            StockTaking_CloseDocument
        }

        public CdnApiErrors()
        {
            _table = new Dictionary<ErrorType, ApiError>();
            _table.Add(ErrorType.XL_Login, new XL_Login());
            _table.Add(ErrorType.XL_Logout, new XL_Logout());
            _table.Add(ErrorType.Article_AddArticle, new Article_AddArticle());
            _table.Add(ErrorType.Article_CloseArticle, new Article_CloseArticle());
            _table.Add(ErrorType.Article_AddPrice, new Article_AddPrice());
            _table.Add(ErrorType.Article_ChangePrice, new Article_ChangePrice());
            _table.Add(ErrorType.Article_AddGroup, new Article_AddGroup());
            _table.Add(ErrorType.Contractor_AddContractor, new Contractor_AddContractor());
            _table.Add(ErrorType.Contractor_CloseContractor, new Contractor_CloseContractor());
            _table.Add(ErrorType.Document_AddDocument, new Document_AddDocument());
            _table.Add(ErrorType.Document_AddItem, new Document_AddItem());
            _table.Add(ErrorType.Document_AddReceipt, new Document_AddReceipt());
            _table.Add(ErrorType.Document_CloseDocument, new Document_CloseDocument());
            _table.Add(ErrorType.Attribute_AddAttribute, new Attribute_AddAttribute());
            _table.Add(ErrorType.MagDocument_AddDocument, new MagDocument_AddDocument());
            _table.Add(ErrorType.MagDocument_AddItem, new MagDocument_AddItem());
            _table.Add(ErrorType.MagDocument_CloseDocument, new MagDocument_CloseDocument());
            _table.Add(ErrorType.Order_AddDocument, new Order_AddDocument());
            _table.Add(ErrorType.Order_OpenDocument, new Order_OpenDocument());
            _table.Add(ErrorType.Order_AddItem, new Order_AddItem());
            _table.Add(ErrorType.Order_RemoveItem, new Order_RemoveItem());
            _table.Add(ErrorType.Order_CloseDocument, new Order_CloseDocument());
        }

        public string GetError(ErrorType errorType, int error)
        {
            if (_table.ContainsKey(errorType))
                return _table[errorType].GetError(error);
        
            return string.Empty;
        }
    }

    class ApiError
    {
        protected  Dictionary<int, string> _table;

        public ApiError()
        {
            _table = new Dictionary<int, string>();
        }

        public  string GetError(int error)
        { 
            if (_table.ContainsKey(error))
                return _table[error];

            return string.Empty;
        }
    }

    class XL_Login : ApiError
    {
        public XL_Login()
        {
            _table.Add(-8, "Nie podano nazwy bazy");
            _table.Add(-7, "Baza niezarejestrowana w systemie");
            _table.Add(-6, "Nie podano hasła lub brak operatora");
            _table.Add(-5, "Nieprawidłowe hasło");
            _table.Add(-4, "Konto operatora zablokowane");
            _table.Add(-3, "Nie podano nazwy programu (pole ProgramID)");
            _table.Add(-2, "Błąd otwarcia pliku tekstowego, do którego mają być zapisywane komunikaty.");
            _table.Add(-1, "Podano niepoprawną wersję API");
            _table.Add(0, "Sukces");
            _table.Add(1, "Inicjalizacja nie powiodła się");
            _table.Add(2, "Występuje w przypadku, gdy istnieje już jedna instancja programu i nastąpi ponowne logowanie (z tego samego komputera i na tego samego operatora)");
            _table.Add(3, "Występuje w przypadku, gdy istnieje już jedna instancja programu i nastąpi ponowne logowanie z innego komputera i na tego samego operatora, ale operator nie posiada prawa do wielokrotnego logowania");
            _table.Add(5, "Występuje przy pracy terminalowej w przypadku, gdy operator nie ma prawa do wielokrotnego logowania i na pytanie czy usunąć istniejące sesje terminalowe wybrano odpowiedź ‘Nie’.");
            _table.Add(61, "Błąd zakładania nowej sesji");
        }
    }

    class XL_Logout : ApiError
    {
        public XL_Logout()
        {
            _table.Add(-2, "Błąd otwarcia pliku tekstowego, do którego mają być zapisywane komunikaty.");
            _table.Add(-1, "Nie zawołano poprawnie XLLogin");
            _table.Add(0, "Sukces");
            _table.Add(2, "Występuje w przypadku, gdy istnieje już jedna instancja programu i nastąpi ponowne logowanie (z tego samego komputera i na tego samego operatora)");
            _table.Add(3, "Występuje w przypadku, gdy istnieje już jedna instancja programu i nastąpi ponowne logowanie z innego komputera i na tego samego operatora, ale operator nie posiada prawa do wielokrotnego logowania");
            _table.Add(5, "Występuje przy pracy terminalowej w przypadku, gdy operator nie ma prawa do wielokrotnego logowania i na pytanie czy usunąć istniejące sesje terminalowe wybrano odpowiedź ‘Nie’.");
        }
    }

    class Article_AddArticle : ApiError
    {
        public Article_AddArticle()
        {
            _table.Add(1, "Błąd zapisu dokumentu");
            _table.Add(2, "Błąd logoutu");
            _table.Add(11, "Nie znaleziono towaru");
            _table.Add(82, "Nie podano nazwy towaru");
            _table.Add(83, "Jest już towar o takim kodzie");
            _table.Add(85, "Zła grupa dla towaru");
            _table.Add(103, "Zła grupa domyślna dla towaru");
            _table.Add(161, "Błąd pobierania wzorca towaru");
        }            
    }

    class Article_CloseArticle : ApiError
    {
        public Article_CloseArticle()
        {
            _table.Add(1, "Błąd zapisu dokumentu");
            _table.Add(3, "Nie znaleziono rekordu o podanym IDTowaru w tabeli ObiektyObce");
            _table.Add(11, "Nie znaleziono towaru o podanym GIDzie w tabeli TwrKarty");
        }
    }

    class Article_AddPrice : ApiError
    {
        public Article_AddPrice()
        {
            _table.Add(1, "Błąd zapisu ceny");
            _table.Add(3, "Nie znaleziono karty towaru");
            _table.Add(109, "Nie ma takiego numeru ceny");
            _table.Add(218, "Towar ma już taką cenę");
        }
    }

    class Article_ChangePrice : ApiError
    {
        public Article_ChangePrice()
        {
            _table.Add(1, "Nie udała się aktualizacja ceny");
            _table.Add(2, "Błąd przy zakładaniu transakcji");
            _table.Add(11, "Nie znaleziono towaru");
            _table.Add(86, "Nie znaleziono ceny");            
        }
    }

    class Article_AddGroup : ApiError
    {
        public Article_AddGroup()
        {
            _table.Add(2, "Błąd zakładania transakcji");
            _table.Add(11, "Nie podano kodu lub nazwy grupy");
            _table.Add(61, "Niepoprawny numer sesji");
            _table.Add(83, "Istnieje już taka grupa");
            _table.Add(85, "Podano nieistniejącą ścieżkę do grupy lub wystąpił błąd dodawania grupy");
            _table.Add(103, "Błąd dodawania grupy domyślnej");
            _table.Add(143, "Błąd dodawania atrybutów");
            _table.Add(161, "Błąd pobrania wzorca");
            _table.Add(196, "Błąd dodawania wzorca");
            _table.Add(197, "Błąd dodania opisu wzorca");
        }
    }

    class Contractor_AddContractor : ApiError
    {
        public Contractor_AddContractor()
        {
            _table.Add(2, "Wystąpił błąd przy próbie zablokowania tabel dla wykonania transakcji");
            _table.Add(16, "Nie podano akronimu");
            _table.Add(47, "Nie znaleziono banku");
            _table.Add(48, "Istnieje już kontrahent o takim akronimie w systemie");
            _table.Add(52, "Podano niepoprawny NIP. NIP musi składać się z cyfr i kresek");
            _table.Add(53, "Podano niepoprawny kod pocztowy. Dla kontrahenta krajowego kod pocztowy powinien mieć postać ##-###");
            _table.Add(61, "Nie istnieje taki numer sesji, lub sesja jest nieaktywna");
            _table.Add(141, "Anulowano dodawanie kontrahenta");
            _table.Add(155, "Nie udało się założyć konta odbiorcy");
            _table.Add(156, "Nie udało się założyć konta dostawcy");
            _table.Add(157, "Błąd zakładania kont kontrahenta");
        }
    }

    class Contractor_CloseContractor : ApiError
    {
        public Contractor_CloseContractor()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(3, "Nie znaleziono kontrahenta");            
        }
    }

    class Document_AddDocument : ApiError
    {
        public Document_AddDocument()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(2, "Wystąpił błąd przy próbie zablokowania tabel dla wykonania transakcji");
            _table.Add(3, "Nie znaleziono dokumentu związanego");
            _table.Add(8, "Nie znaleziono kontrahenta");
            _table.Add(9, "Nie znaleziono formy płatności");
            _table.Add(10, "Nie znaleziono waluty");
            _table.Add(14, "Nie znaleziono wskazanego zamówienia");
            _table.Add(16, "Brak kontrahenta");
            _table.Add(18, "Wstrzymano transakcje z tym kontrahentem");
            _table.Add(19, "Został przekroczony limit kredytu dla kontrahenta");
            _table.Add(22, "Podany adres jest dla innego kontrahenta niż podany");
            _table.Add(28, "Ten typ dokumentu nie może być dokumentem Avista");
            _table.Add(31, "Podany adres wysyłkowy jest dla innego kontrahenta niż podany");
            _table.Add(32, "Dokumenty PICO mogą być przetwarzane tylko w trybie wsadowym");
            _table.Add(33, "Nie udało się wymusić numeru dokumentu");
            _table.Add(34, "Nie znaleziono magazynu docelowego");
            _table.Add(35, "Nie znaleziono magazynu źródłowego");
            _table.Add(36, "Brak zdefiniowanych magazynów lub podany magazyn nie istnieje");
            _table.Add(37, "Ten typ dokumentu nie może być dokumentem PICO");
            _table.Add(38, "Podana seria nie jest serią dokumentu PICO");
            _table.Add(42, "Taki dokument nie może być generowany na podstawie takiego dokumentu źródłowego");
            _table.Add(43, "Typ zamówienia nie pasuje do typu generowanego dokumentu");
            _table.Add(44, "Tylko potwierdzone zamówienia mogą być przekształcane do dok. handlowych");
            _table.Add(45, "Podano nieistniejący typ dokumentu");
            _table.Add(57, "Nie ustalono rejestru");
            _table.Add(61, "Nie istnieje taki numer sesji, lub sesja jest nieaktywna");
            _table.Add(65, "Pojawia się wtedy, gdy w konfiguracji ustawimy 'sprzedaż tylko z magazynu domyślnego', a każemy sprzedawać z innego niż domyślny");
            _table.Add(66, "Próba wystawienia korekty do dokumentu, który nie może być korygowany");
            _table.Add(67, "Typ korekty nie jest zgodny z typem dokumentu oryginalnego");
            _table.Add(68, "Próba wystawienia korekty do spinacza");
            _table.Add(71, "Oryginał korekty w buforze");
            _table.Add(72, "Oryginał korekty w edycji");
            _table.Add(73, "Nie można wystawić korekty gdyż ostatnia korekta w buforze");
            _table.Add(74, "Nie można wystawić korekty gdyż ostatnia korekta w edycji");
            _table.Add(76, "Magazyn źródłowy jest taki sam jak docelowy a wystawiany dokument MM");
            _table.Add(77, "Nie znaleziono wskazanego zlecenia");
            _table.Add(78, "Typ zlecenia nie pasuje do typu generowanego dokumentu");
            _table.Add(79, "Tylko potwierdzone zlecenia mogą być przekształcane do dok. handlowych");
            _table.Add(80, "Wybrana osoba nie jest upoważniona do odbioru FA VAT");
            _table.Add(81, "Brak licencji");
            _table.Add(90, "Magazyn zablokowany (np. deprecjacja bądź inwentaryzacja)");
            _table.Add(95, "Dokument spinany jest już związany");
            _table.Add(106, "Wystąpił DeadLock");
            _table.Add(131, "Brak praw do wybranego magazynu");
            _table.Add(132, "Brak praw do wystawiania dokumentu");
            _table.Add(134, "Brak praw do modyfikacji dokumentu");
            _table.Add(135, "Brak praw do wybranej serii");
            _table.Add(137, "Brak praw do oryginału korekty");
            _table.Add(138, "Oryginał korekty został anulowany");
            _table.Add(139, "Nieprawidłowe centrum operatora");
            _table.Add(143, "Błąd dodawania atrybutów");
            _table.Add(145, "Akwizytor wymagany");
            _table.Add(146, "Przekroczono limit akwizytora");
            _table.Add(148, "Nie podano kraju (wymagany)");
            _table.Add(149, "Brak takiego kraju");
            _table.Add(150, "Nie podano kodu transportu (wymagany)");
            _table.Add(151, "Brak kodu transportu");
            _table.Add(152, "Nie podano kodu transakcji (wymagany)");
            _table.Add(153, "Brak kodu transakcji");
            _table.Add(158, "Dokument źródłowy ma stan uniemożliwiający realizację");
            _table.Add(163, "Istnieje niezatwierdzona kaucja do tego dokumentu");
            _table.Add(164, "Nie dodano dokumentu");
            _table.Add(165, "Adres płatnika jest dla innego kontrahenta");
            _table.Add(166, "Tego dokumentu spiąć nie można - prawo do spinania po stronie oddziału");
            _table.Add(168, "Tego dokumentu korygować nie można - prawo do korygowania po stronie oddziału");
            _table.Add(175, "Dokument ma nieustalone koszty");
            _table.Add(193, "Na tym dokumencie może być tylko kontrahent, który jest rolnikiem ryczałtowym");
            _table.Add(194, "Brak praw do wykonania zadania procesu np. utworzenia dokumentu");
            _table.Add(207, "Oryginał dokumentu musi być zafiskalizowany");
            _table.Add(208, "Oryginał dokumentu był korygowany");
            _table.Add(209, "Oryginał dokumentu ma już wygenerowany Tax Free");
            _table.Add(214, "Na takim typie dokumentu nie można wybrać takiego rodzaju transakcji");
            _table.Add(215, "Nie znaleziono takiego rejestru bankowego (lub podano rejestr kasowy)");
            _table.Add(217, "Brak takiego rodzaju ceny");
            _table.Add(233, "Ten typ dokumentu może być wystawiany tylko na podstawie zamówienia");
            _table.Add(234, "Nie masz uprawnień do wystawiania tego typu dokumentu");
            _table.Add(235, "Operacja przerwana przez użytkownika");
            _table.Add(236, "Sesja programu wygasła, utworzenie dokumentu nie jest możliwe");
            _table.Add(239, "Brak projektu o podanym identyfikatorze");
            _table.Add(267, "Brak praw do wybranego rejestru");
            _table.Add(268, "Dokument uszkodzony");
            _table.Add(284, "Zły rodzaj kontrahenta, akwizytora lub płatnika dla tego kontekstu dokumentu");
            _table.Add(285, "Dokument może zostać wygenerowany tylko w Oddziale");
            _table.Add(3000, "Utworzenie dokumentu jest niemożliwe, gdyż data bieżąca należy do zamkniętego okresu operacji handlowych");
        }
    }

    class Document_AddItem : ApiError
    {
        public Document_AddItem()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(3, "Nie znaleziono nagłówka dokumentu");
            _table.Add(4, "Dokument nie jest w buforze");
            _table.Add(10, "Nie znaleziono waluty");
            _table.Add(11, "Nie znaleziono towaru");
            _table.Add(12, "Nie podano towaru");
            _table.Add(13, "Nie podano ilości");
            _table.Add(14, "Nie znaleziono wskazanego zamówienia");
            _table.Add(15, "Taki typ dokumentu nie przyjmuje takiego typu pozycji");
            _table.Add(17, "Zabrakło ilości (przy trybie wsadowym musi być w pełni zaspokojona)");
            _table.Add(20, "Ten towar wymaga zakupu od konkretnego (autoryzowanego) kontrahenta");
            _table.Add(21, "Nie wybrano dostawy - ręczny wybór nie jest możliwy");
            _table.Add(23, "Brak dokumentu źródłowego");
            _table.Add(27, "Ten towar wymaga posiadania ważnej koncesji przez kontrahenta");
            _table.Add(29, "Dokument jest anulowany");
            _table.Add(34, "Brak magazynu docelowego");
            _table.Add(35, "Brak magazynu Ÿródłowego");
            _table.Add(36, "Nie znaleziono magazynu");
            _table.Add(39, "Nie podano cechy, a jest ona wymagana przy tym towarze");
            _table.Add(40, "Błąd autonumeracji pozycji");
            _table.Add(41, "Zamówienie w nagłówku jest inne niż podawane dla elementu");
            _table.Add(54, "Nie podano jednostki miary, a dla towaru typu Avista jest to wymagane");
            _table.Add(55, "Na dokumencie źródłowym jest inny towar niż podany");
            _table.Add(56, "Niezgodność kodu towaru z kodem EAN");
            _table.Add(65, "Parametry konfiguracji nie pozwalają na sprzedaż z podanego magazynu");
            _table.Add(69, "Próba wystawienia elementu korekty, gdy nagłówek nie jest korektą");
            _table.Add(70, "Ilość korekty przekracza pozostałą do skorygowania");
            _table.Add(75, "Nie znaleziono wskazanego elementu oryginału (przy generowaniu korekty)");
            _table.Add(77, "Nie znaleziono wskazanego nagłówka/elementu zlecenia");
            _table.Add(101, "Nie udało się wygenerować elementu korekty");
            _table.Add(106, "Wystąpił DeadLock");
            _table.Add(121, "Waluta pozycji niezgodna z nagłówkiem dokumentu eksportowego");
            _table.Add(128, "Cena ma niewłaściwą wartość");
            _table.Add(131, "Brak praw do wybranego magazynu");
            _table.Add(134, "Brak praw do modyfikacji dokumentu");
            _table.Add(160, "Zły identyfikator dostawy (z tej dostawy korzysta inny towar)");
            _table.Add(174, "Dostawa ma nieustalony koszt");
            _table.Add(175, "Ustawienia programu nie pozwalają na zrobienie korekty tego typu");
            _table.Add(204, "Do takiego dokumentu nie można dodawać elementów");
            _table.Add(210, "Nie znaleziono podanego identyfikatora partii");
            _table.Add(211, "Z identyfikatora wskazanej partii korzysta inny towar");
            _table.Add(213, "Taki towar już istnieje na tym dokumencie");
            _table.Add(216, "Skład celny ma ustawiony ręczny wybór dostawy");
            _table.Add(271, "Wartość jednostkowa KGO nie może być większa od ceny sprzedaży");
        }
    }

    class Document_CloseDocument : ApiError
    {
        public Document_CloseDocument()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(3, "Nie znaleziono nagłówka dokumentu");
            _table.Add(4, "Dokument nie jest w buforze");
            _table.Add(7, "Nie udało się zmodyfikować płatności");
            _table.Add(16, "Dokument nie posiada kontrahenta");
            _table.Add(106, "Wystąpił DeadLock");
            _table.Add(114, "Nie można zatwierdzić dokumentu bez elementów");
            _table.Add(3000, "Anulowanie dokumentu z podanymi datami jest niemożliwe, gdyż należą one do zamkniętego okresu operacji handlowych");
        }
    }

    class Attribute_AddAttribute : ApiError
    {
        public Attribute_AddAttribute()
        {
            _table.Add(-1, "Brak sesji");
            _table.Add(2, "Błąd przy zakładaniu logout");
            _table.Add(3, "Nie znaleziono obiektu");
            _table.Add(4, "Nie znalezniono klasy atrybutu");
            _table.Add(5, "Klasa nieprzypisana do definicji obiektu");
            _table.Add(6, "Atrybut juz istnieje w kolejce");
            _table.Add(7, "Błąd ADO Connection");
            _table.Add(8, "Błąd ADO");
            _table.Add(9, "Brak zdefiniowanego obiektu");
        }
    }

    class MagDocument_AddDocument : ApiError
    {
        public MagDocument_AddDocument()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(2, "Wystąpił błąd przy próbie zablokowania tabel dla wykonania transakcji");
            _table.Add(8, "Nie znaleziono kontrahenta");
            _table.Add(16, "Brak kontrahenta");
            _table.Add(22, "Podany adres jest dla innego kontrahenta niż podany");
            _table.Add(23, "Nie znaleziono dokumentu źródłowego");
            _table.Add(24, "Przyjęto wszystkie elementy transakcji");
            _table.Add(25, "Wydano wszystkie elementy transakcji");
            _table.Add(36, "Nie znaleziono magazynu");
            _table.Add(40, "Dokument źródłowy jest w buforze");
            _table.Add(45, "Podano nieistniejący typ dokumentu");
            _table.Add(50, "Dokument źródłowy jest innego typu niż dokument podany");
            _table.Add(61, "Nie istnieje taki numer sesji, lub sesja jest nieaktywna");
            _table.Add(106, "Wystąpił DeadLock");
            _table.Add(131, "Brak praw do wybranego magazynu");
            _table.Add(137, "Brak praw do oryginału dokumentu");
            _table.Add(194, "Brak praw do wykonania zadania procesu np. utworzenia dokumentu");
            _table.Add(236, "Sesja programu wygasła, utworzenie dokumentu nie jest możliwe");
        }
    }

    class MagDocument_AddItem : ApiError
    {
        public MagDocument_AddItem()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(3, "Nie znaleziono nagłówka dokumentu");
            _table.Add(4, "Dokument nie jest w buforze");
            _table.Add(11, "Nie znaleziono towaru");
            _table.Add(12, "Nie podano towaru");
            _table.Add(13, "Nie podano ilości");
            _table.Add(15, "Taki typ dokumentu nie przyjmuje takiego typu pozycji");
            _table.Add(17, "Zabrakło ilości");
            _table.Add(20, "Ten towar wymaga zakupu od konkretnego (autoryzowanego) kontrahenta");
            _table.Add(21, "Ten towar wymaga ręcznego wyboru dostawy");
            _table.Add(23, "Nie znaleziono dokumentu źródłowego");
            _table.Add(29, "Dokument jest anulowany");
            _table.Add(51, "Magazyn na pozycji jest inny niż magazyn w nagłówku");
            _table.Add(56, "Niezgodność kodu towaru z kodem EAN");
            _table.Add(77, "Nie znaleziono nagłówka/elementu dokumentu źródłowego");
            _table.Add(106, "Wystąpił DeadLock");
            _table.Add(136, "Niezgodność źródła między nagłówkiem a elementem");
            _table.Add(177, "Nie ma takiej jednostki składowania");
            _table.Add(178, "Nie ustalono partii towaru");
            _table.Add(179, "Złe ilości subelementów");
            _table.Add(182, "Ten typ dokumentu wymaga ustalenia adresu skąd");
            _table.Add(183, "Ten typ dokumentu wymaga ustalenia adresu dokąd");
            _table.Add(184, "Nieznane położenie");
            _table.Add(185, "Nie można umieścić towaru pod tym położeniem");
            _table.Add(203, "Adres należy do innego magazynu niż magazyn dokumentu");
            _table.Add(205, "Ustalono adres \''skąd\'' taki sam jak adres \''dokąd\''");
            _table.Add(210, "Nie znaleziono podanego identyfikatora partii");
        }
    }

    class MagDocument_CloseDocument : ApiError
    {
        public MagDocument_CloseDocument()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(3, "Nie znaleziono nagłówka dokumentu");
            _table.Add(4, "Dokument nie jest w buforze");
            _table.Add(106, "Wystąpił DeadLock");
            _table.Add(114, "Nie można zatwierdzić dokumentu bez elementów");
            _table.Add(266, "Różnica: MaE_ilosc = SUM(MaP_Ilosc)");
        }
    }

    class Order_AddDocument : ApiError
    {
        public Order_AddDocument()
        {
            _table.Add(1, "Błąd zapisu dokumentu");
            _table.Add(2, "Błąd logoutu");
            _table.Add(3, "Błędny typ");
            _table.Add(4, "Nie podano akronimu ani GIDu kontrahenta i jest tryb wsadowy, albo nie znaleziono w tabeli");
            _table.Add(5, "Błędna forma płatności");
            _table.Add(6, "Błąd zapisu opisu");
            _table.Add(7, "Błąd sesji (nie podano)");
            _table.Add(8, "Błąd podczas operacji na tabeli ObiektyObce");
            _table.Add(14, "Nie podano kwoty ani procentu kwoty do płatności");
            _table.Add(16, "Błąd generowania rezerwacji");
            _table.Add(17, "Błąd otwierania dokumentu. Jest on już modyfikowany");
            _table.Add(18, "Podany adres nie należy do danego kontrahenta");
            _table.Add(19, "Podany adres wysyłkowy nie należy do danego kontrahenta");
            _table.Add(20, "Próba wystawienia zamówienia eksportowego, a w konfiguracji nie ma stawki vat dla eksportu");
            _table.Add(21, "Nie podano kodu ani GIDu magazynu, albo nie znaleziono w tabeli");
            _table.Add(22, "Błąd wyspecyfikowania cechy pozycji");
            _table.Add(23, "Dla zamówienia/oferty sprzedaży wybrano magazyn wewnętrzny");
            _table.Add(24, "Przy ustawionej w konfiguracji opcji - sprzedaż z wybranego magazynu nie można wybrać magazynu <WSZYSTKIE>");
            _table.Add(25, "Podany magazyn nie jest magazynem domyślnym ustawionym w konfiguracji  dla opcji *Sprzedaż z jednego magazynu* i *Wszystkie pozycje z domyślnego magazynu*");
            _table.Add(26, "Ustawienie w konfiguracji opcji Sprzedaż z jednego magazynu oraz Wszystkie pozycje z jednego magazynu lub Wszystkie pozycje z domyślnego magazynu wymusza na pozycjach magazyn taki jak w nagłówku");
            _table.Add(27, "Błędnie podany typ zamówienia - zewnętrzne/wewnętrzne");
            _table.Add(28, "Brak określonego kontrahenta dla podanego magazynu - zamówienia wewnętrzne");
            _table.Add(29, "Błąd zapisu dokumentu duplikacja klucza");
            _table.Add(30, "Nie znaleziono magazynu docelowego dla zamówienia wewnętrznego");
            _table.Add(31, "Podany kontrahent nie jest związany z magazynem");
            _table.Add(32, "W przypadku zamówienia wewnętrznego należy podać magazyn docelowy lub kontrahenta");
            _table.Add(33, "Brak licencji na moduł Zamówienia");
            _table.Add(35, "Wystąpił DeadLock");
            _table.Add(38, "Błędnie podana waluta");
            _table.Add(39, "Błędny kurs waluty");
            _table.Add(40, "Błąd autonumeracji");
            _table.Add(41, "Brak praw wynikająca z definicji dokumentu");
            _table.Add(42, "Brak praw do magazynu wynikająca z definicji dokumentu");
            _table.Add(43, "Brak praw do serii wynikająca z definicji dokumentu");
            _table.Add(44, "Transakcje z kontrahentem wstrzymane");
            _table.Add(45, "Przekroczony limit kontrahenta");
            _table.Add(46, "Dla zamówienia/oferty sprzedaży wybrano magazyn oddziałowy");
            _table.Add(47, "Nieprawidłowy identyfikator centrum operatora");
            _table.Add(48, "Nieprawidłowy typ towaru dla danego zamówienia (w przypadku wewnętrznych nie mogą być usługa,koszt...)");
            _table.Add(49, "Błędna flaga VAT pozycji (niezgodna z nagłówkiem)");
            _table.Add(50, "Błąd dodawania atrybutów");
            _table.Add(51, "Błąd  - wybrany kontrahent archiwalny");
            _table.Add(52, "Błąd zapisu promocji");
            _table.Add(57, "Podano nieistniejący numer rejestru bankowego");
            _table.Add(59, "Nie udało się wymusić numeru dokumentu");
            _table.Add(284, "Zły rodzaj kontrahenta");
        }
    }

    class Order_OpenDocument : ApiError
    {
        public Order_OpenDocument()
        {
            _table.Add(1, "Błąd zapisu dokumentu");
            _table.Add(2, "Błąd logoutu");
            _table.Add(3, "Błędny typ");
            _table.Add(6, "Błąd zapisu opisu");
            _table.Add(7, "Błąd sesji");            
            _table.Add(8, "Błąd podczas operacji na tabeli ObiektyObce");
            _table.Add(9, "Błąd pobierania zamówienia");
            _table.Add(17, "Błąd otwierania dokumentu. Jest on już modyfikowany");
            _table.Add(35, "Wystąpił DeadLock");
            _table.Add(41, "Brak praw wynikająca z definicji dokumentu");
            _table.Add(66, "Brak praw do operacji na potwierdzonych zapytaniach ofertowych");            
        }
    }

    class Order_AddItem : ApiError
    {
        public Order_AddItem()
        {
            _table.Add(1, "Błąd zapisu dokumentu");
            _table.Add(2, "Błąd Logout’u");
            _table.Add(6, "Błąd zapisu opisu");
            _table.Add(8, "Błąd podczas operacji na tabeli ObiektyObce");
            _table.Add(9, "Błąd pobierania zamówienia");        
            _table.Add(10, "Pozycje można dodawać tylko do niepotwierdzonego zamówienia lub oferty");
            _table.Add(11, "Nie podano ilości towaru");
            _table.Add(12, "Nie podano kodu, ani nazwy, ani GID’u towaru");
            _table.Add(13, "Nie udało się pobrać przekazanego towaru");
            _table.Add(15, "Błąd dodawania pozycji – towar wymaga koncesji, a kontrahent jej nie posiada");
            _table.Add(21, "Nie podano kodu, ani GID’u magazynu, albo nie znaleziono w tabeli");
            _table.Add(22, "Błąd wyspecyfikowania cechy pozycji");
            _table.Add(23, "Dla zamówienia/oferty sprzedaży wybrano magazyn wewnętrzny");
            _table.Add(24, "Przy ustawionej w konfiguracji opcji - sprzedaż z wybranego magazynu nie można wybrać magazynu <WSZYSTKIE>");
            _table.Add(26, "Ustawienie w konfiguracji opcji Sprzedaż z jednego magazynu oraz Wszystkie pozycje z jednego magazynu lub Wszystkie pozycje z domyślnego magazynu wymusza na pozycjach magazyn taki jak w nagłówku");
            _table.Add(34, "Podana pozycja receptury nie istnieje");
            _table.Add(35, "Wystąpił DeadLock");
            _table.Add(36, "Podany magazyn ma przypisanego innego kontrahenta niz podany - zamowienia wewnętrzne");
            _table.Add(37, "Ten towar wymaga zakupu od konkretnego (autoryzowanego) kontrahenta");
            _table.Add(38, "Błędnie podana waluta");
            _table.Add(39, "Błędny kurs waluty");
            _table.Add(40, "Błąd autonumeracji");
            _table.Add(41, "Brak praw wynikająca z definicji dokumentu");
            _table.Add(42, "Brak praw do magazynu wynikająca z definicji dokumentu");
            _table.Add(43, "Brak praw do serii wynikająca z definicji dokumentu");
            _table.Add(46, "Dla zamówienia/oferty sprzedaży wybrano magazyn oddziałowy");
            _table.Add(48, "Nieprawidłowy typ towaru dla danego zamówienia ( w przypadku wewnętrznych nie mogą być usługa,koszt...)");
            _table.Add(55, "Pozycja już istnieje na zamowieniu. W definicji dok. zakazano powielania pozycji");
        }
    }

    class Order_RemoveItem : ApiError
    {
        public Order_RemoveItem()
        {
            _table.Add(1, "Błąd usuwania pozycji");
            _table.Add(8, "Błąd podczas operacji na tabeli ObiektyObce");
            _table.Add(9, "Błąd pobierania zamówienia");
            _table.Add(10, "Pozycje można dodawać tylko do niepotwierdzonego zamówienia lub oferty");
            _table.Add(41, "Brak praw wynikająca z definicji dokumentu");            
        }
    }

    class Order_CloseDocument : ApiError
    {
        public Order_CloseDocument()
        {
            _table.Add(1, "Błąd zapisu dokumentu");
            _table.Add(2, "Błąd Logout’u");
            _table.Add(6, "Błąd zapisu opisu");
            _table.Add(8, "Błąd podczas operacji na tabeli ObiektyObce");
            _table.Add(9, "Błąd pobierania zamówienia");        
            _table.Add(16, "Błąd generowania rezerwacji");
            _table.Add(35, "Wystąpił DeadLock");
            _table.Add(40, "Błąd autonumeracji");
            _table.Add(54, "Nie można potwierdzić zamówienia, jeśli nie dodano żadnej pozycji");
            _table.Add(71, "Brak praw do operacji na potwierdzonych zamówieniach");
            _table.Add(73, "Zamknąć może operator z prawem do operacji na potwierdzonych zamówieniach");
            _table.Add(87, "Brak praw do otwarcia potwierdzonego zamówienia");
            _table.Add(88, "Brak praw do otwarcia potwierdzonego zamówienia zakupu");
            _table.Add(89, "Brak praw do otwarcia potwierdzonego zamówienia sprzedaży");
            _table.Add(90, "Brak praw do otwarcia potwierdzonego zamówienia wewnętrznego");
            
        }
    }

    class StockTaking_AddDocument : ApiError
    {
        public StockTaking_AddDocument()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(61, "Podano nieprawidłowy numer sesji");
            _table.Add(143, "Błąd dodawania atrybutów");
            _table.Add(220, "Nie znaleziono nagłówka inwentaryzacji");
            _table.Add(221, "Nie znaleziono arkusza inwentaryzacyjnego");
            _table.Add(222, "Arkusz inwentaryzacyjny nie ma wygenerowanych pozycji");
            _table.Add(223, "Błąd dodania odczytu");
            _table.Add(229, "Nie podano nazwy odczytu inwentaryzacji");
            _table.Add(237, "Inwentaryzacja została już zamknięta");
            _table.Add(290, "Nie udało się pobrać inwentaryzowanych magazynów");
        }
    }

    class StockTaking_AddItem : ApiError
    {
        public StockTaking_AddItem()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(11, "Nie znaleziono towaru");
            _table.Add(13, "Nie podano ilości");
            _table.Add(39, "Nie znaleziono klasy cechy");
            _table.Add(51, "Podany adres magazynowy jest w innym magazynie niż magazyn inwentaryzacji");
            _table.Add(61, "Podano nieprawidłowy numer sesji");
            _table.Add(184, "Nieznane położenie");
            _table.Add(220, "Nie znaleziono nagłówka inwentaryzacji");
            _table.Add(221, "Nie znaleziono arkusza inwentaryzacyjnego");
            _table.Add(224, "Nie znaleziono odczytu inwentaryzacji");
            _table.Add(225, "Nie znaleziono obszaru magazynowego");
            _table.Add(226, "Nie podano położenia a jest ono wymagane");
            _table.Add(227, "Podane położenie jest poza zakresem podanym na arkuszu inwentaryzacyjnym");
            _table.Add(228, "Odczyt inwentaryzacyjny jest zamknięty");
            _table.Add(231, "Wystąpił błąd podczas dodawania/modyfikacji/usuwania pozycji odczytu");
            _table.Add(243, "Błąd pobrania/dodania/usunięcia rekordu tabeli ObiektyObce");
            _table.Add(245, "Nie udało się pobrać rekordu z tabeli InwNagSesje");
            _table.Add(246, "Niepoprawna akcja lub parametry inwentaryzacji nie pozwalają na wykonanie akcji");
            _table.Add(247, "Nie podano identyfikatora pozycji odczytu (dotyczny akcji modyfikacja i usuń)");
            _table.Add(248, "Nie znaleziono wskazanej pozycji odczytu (dotyczny akcji modyfikacja i usuń)");
            _table.Add(293, "Nie znaleziono wskazanego towaru na arkuszu (brak rekordu InwTwr)");            
        }
    }

    class StockTaking_CloseDocument : ApiError
    {
        public StockTaking_CloseDocument()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(61, "Podano nieprawidłowy numer sesji");
            _table.Add(221, "Nie znaleziono arkusza inwentaryzacyjnego");
            _table.Add(224, "Nie znaleziono odczytu inwentaryzacji");
            _table.Add(228, "Odczyt inwentaryzacyjny jest zamknięty");
            _table.Add(230, "Wystąpił błąd podczas zapisu odczytu inwentaryzacji");
            _table.Add(243, "Błąd pobrania/dodania/usunięcia rekordu tabeli ObiektyObce");
            _table.Add(244, "Nie udało się zwolnić z edycji odczytu lub arkusza inwentaryzacyjnego");
            _table.Add(245, "Nie udało się pobrać rekordu z tabeli InwNagSesje");
            _table.Add(290, "Nie udało się pobrać inwentaryzowanych magazynów");
        }
    }

    class Document_AddReceipt : ApiError
    {
        public Document_AddReceipt()
        {
            _table.Add(-1, "API nie zostało aktywowane bądź błąd wersji");
            _table.Add(2, "Wystąpił błąd przy próbie zablokowania tabel dla wykonania transakcji");
            _table.Add(3, "Nie znaleziono nagłówka dokumentu");
            _table.Add(4, "Dokument nie jest w buforze");
            _table.Add(6, "Dokument jest zaksięgowany");
            _table.Add(29, "Dokument jest anulowany");
            _table.Add(92, "Dokument spinany ma inny typ niż spinający");
            _table.Add(93, "Dokument spinany jest anulowany");
            _table.Add(94, "Dokument spinany ma inną flagę (netto/brutto) niż spinający");
            _table.Add(95, "Dokument spinany jest już związany");
            _table.Add(96, "Dokument spinany jest w buforze");
            _table.Add(97, "Dokument spinany jest aktywny");
            _table.Add(98, "Dokument spinany należy do innego spinacza");
            _table.Add(106, "Wystąpił DeadLock");
            _table.Add(120, "Dokument spinany ma inną walutę niż spinający");
            _table.Add(123, "Dokument spinany ma całkowicie lub częściowo rozliczoną płatność");
            _table.Add(124, "Dokument spinany ma predekrety na płatności");
            _table.Add(134, "Brak praw do modyfikacji dokumentu");
            _table.Add(140, "Brak praw do spięcia dokumentu");
            _table.Add(169, "Oryginał dokumentu spinanego nie jest spięty");
            _table.Add(180, "Data spinanego PA jest poza zakresem dat RS");
            _table.Add(181, "Próba spięcia PA do RSK lub PAK do RS");
            _table.Add(195, "Dokument do spięcia nie ma ustawienia Rolnik Ryczałtowy");
            _table.Add(198, "Do RA/RAK może być spięty tylko jeden dokument PA/PAK");
            _table.Add(200, "Próba spięcia PA/PAK do RS/RSK gdy spinacz PA/PAK jest już w rejestrze VAT");
        }
    }

    
}






