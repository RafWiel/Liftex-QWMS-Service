using System;
using System.Collections.Generic;
using System.Text;
using cdn_api;
using System.Linq;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using WinService.Helpers;
using InterProcessCommunication.gTools;

namespace WinService.CdnApi
{   
    public partial class ApiClient
    {
        #region Initialization

        public delegate void LogDelegate(string message);
        public event LogDelegate LogErrorEvent;

        private const int ApiVersion = 20232;
        private const int ResultException = 5001;
        private int _sessionId = 0;
        private CdnApiErrors _apiErrors = new CdnApiErrors();

        [DllImport("ClaRUN.dll")]
        public static extern void AttachThreadToClarion(int flag);

        #endregion

        #region Enums        

        public enum ErrorType : int
        {
            NowyDokument = 1,
            DodajPozycje = 2,
            DodajSubPozycje = 3,
            UsunPozycje = 4,
            DodajVat = 5,
            DodajPlatnosc = 6,
            ZamknijDokument = 7,
            NowyDokumentMag = 8,
            DodajPozycjeMag = 9,
            UsunPozycjeMag = 10,
            ZamknijDokumentMag = 11,
            NowyKontrahent = 12,
            ZamknijKontrahent = 13,
            NowyAdres = 14,
            ZmienAdres = 15,
            ZamknijAdres = 16,
            NowyDokumentImp = 17,
            DodajPozycjeImp = 18,
            UsunPozycjeImp = 19,
            ZamknijDokumentImp = 20,
            NowyDokumentSad = 21,
            DodajPozycjeSad = 22,
            DodajSubPozycjeSad = 23,
            UsunPozycjeSad = 24,
            ZamknijDokumentSad = 25,
            NowyTowar = 26,
            DodajCene = 27,
            ZamknijTowar = 28,
            ZmienCene = 29,
            NowaReceptura = 30,
            DodajSkladnikRecpetury = 31,
            ZamknijRecepture = 32,
            DodajDoSpinacza = 33,
            UsunZeSpinacza = 34,
            SAD2PZI = 35,
            DodajKwoteDodSAD = 36,
            OtworzDokumentHan = 37,
            OtworzDokumentMag = 38,
            OtworzDokumentImp = 39,
            OtworzDokumentSad = 40,
            NowyDokumentZam = 41,
            DodajPozycjeZam = 42,
            ZamknijDokumentZam = 43,
            NowyDokumentZlc = 44,
            DodajPozycjeZlc = 45,
            ZamknijDokumentZlc = 46,
            PrzeliczRabat = 47,
            NoweZlecenieSR = 48,
            DodajPozycjeZleceniaSR = 49,
            ZamknijZlecenieSR = 50,
            OtworzZlecenieSR = 51,
            ModyfikujVat = 52,
            NoweZlecenieProd = 53,
            DodajDoZleceniaProd = 54,
            ZamknijZlecenieProd = 55,
            OtworzZlecenieProd = 56,
            DodajSUBPozycjeMap = 58,
            NowyObiektProd = 61,
            NowaFunkcjaProd = 62,
            NowyObiektFunkcjaProd = 63,
            NowaTechnologia = 64,
            NowaCzynnoscTechnologia = 65,
            NowaFunkcjaCzynnoscTechnologia = 66,
            NowyZasobCzynnoscTechnologia = 67,
            DodajCzynnoscDoProcesuProd = 68,
            DodajZasobDoCzynnosciProd = 69,
            DodajObiektDoCzynnosciProd = 70,
            DodajTerminDoCzynnosciProd = 71,
            ModyfikujPozycje = 72,
            NowaPaczka = 75,
            ZamknijPaczke = 76,
            OtworzPaczke = 77,
            DodajDokumentDoPaczki = 78,
            UsunDokumentZPaczki = 79,
            NowaWysylka = 80,
            ZamknijWysylke = 81,
            OtworzWysylke = 82,
            DodajPaczkeDoWysylki = 83,
            UsunPaczkeZWysylki = 84,
            DodajKosztDoWysylki = 85,
            UsunKosztZWysylki = 86,
            WykonajPodanyWydruk = 87,
            PowiazZasobProd = 88,
            ModyfikujP³atnoœæ = 89,
            DodajCzynnoscSerwis = 90,
            DodajObiektSerwis = 91,
            DodajParametrUrzadzeniaSerwis = 92,
            DodajSkladnikSerwis = 93,
            DodajTerminCzynnosciSerwis = 94,
            DodajUrzadzenieSerwis = 95,
            DodajWlascicielaUrzadzeniaSerwis = 96,
            NoweUrzadzenieSerwis = 97,
            NoweZlecenieSerwis = 98,
            OtworzZlecenieSerwis = 99,
            ZamknijZlecenieSerwis = 100,
            ModyfikujPozycjeMag = 101,
            NowyOdczytInw = 102,
            DodajPozycjeOdczytuInw = 103,
            ZamknijOdczytInw = 104,
            OtworzOdczytInw = 105,
            DodajOpiekunaDoMag = 106,
            DodajZasobDoMag = 107,
            RealizujPozycjeMag = 108,
            NowySrt = 109,
            DodajDokumentSrt = 110,
            DodajPozycjeSrt = 111,
            ZamknijDokumentSrt = 112,
            UsunDokumentSrt = 113,
            UsunPozycjeDokSrt = 114,
            DodajDokumentUML = 115,
            ZamknijDokumentUML = 116,
            OtworzDokumentUML = 117,
            UsunDokumentUML = 118,
            DodajAneksUML = 119,
            DodajPrzedmiotUML = 120,
            UsunPrzedmiotUML = 121,
            ModyfikujPrzedmiotUML = 122,
            DodajRateUML = 123,
            UsunRateUML = 124,
            GenerujRatyUML = 125,
            UsunRalizacjeMag = 126,
            ZamknijRealizacjeMag = 127,
            OtworzDokumentZam = 128,
            DolaczDefinicjeParametruDoRodzajuUrzadzeniaSerwis = 129,
            NowyRodzajUrzadzeniaSerwis = 130
        }

        #endregion

        #region Properties

        public bool IsLoggedIn
        {
            get => _sessionId > 0;
        }

        #endregion

        #region Public methods
        
        public int Login(string keyServer, string dbName, string user, string password)
        {
            int result = ResultException;

            try
            {
                AttachThreadToClarion(1);

                #if !DEBUG
                int version = XL_GetVersion();
                if (version > ApiVersion)
                {
                    var message = $"Incorrect CdnApi version: {version}";

                    LogErrorEvent?.Invoke(message);
                    gLog.Write(message);

                    return 530;
                }
                #endif  

                var login = new XLLoginInfo_20232
                {
                    Baza = dbName,
                    OpeIdent = user,
                    OpeHaslo = password,
                    //PlikLog = "xl_api.log",
                    ProgramID = "WebApi Contracting",
                    SerwerKlucza = keyServer,
                    TrybWsadowy = 1,
                    UtworzWlasnaSesje = 1,
                    Wersja = ApiVersion,
                    Winieta = -1,
                };

                _sessionId = 0;

                result = cdn_api.cdn_api.XLLogin(login, ref _sessionId);
                if (result != 0)
                {
                    var message = $"XLLogin: {result} - {_apiErrors.GetError(CdnApiErrors.ErrorType.XL_Login, result)}";

                    LogErrorEvent?.Invoke(message);
                    
                    _sessionId = 0;
                }
            }
            catch (Exception ex)
            {
                LogErrorEvent?.Invoke(ex.Message);
                gLog.Write(ex.ToString());
            }

            return result;
        }
        
        public int Logout()
        {
            if (IsLoggedIn == false)
                return 0;

            int result = ResultException;

            try
            {
                result = cdn_api.cdn_api.XLLogout(_sessionId);
                if (result != 0)
                {
                    var message = $"XLLogout: {result} - {_apiErrors.GetError(CdnApiErrors.ErrorType.XL_Logout, result)}";

                    LogErrorEvent?.Invoke(message);
                }           
            }
            catch (Exception ex)
            {
                LogErrorEvent?.Invoke(ex.Message);
                gLog.Write(ex.ToString());
            }

            return result;
        }
       
        #endregion

        #region Private methods

        public static int ConvertToComarchTimeStamp(DateTime dateTime)
        {
            return Convert.ToInt32(Math.Abs((dateTime - new DateTime(1990, 1, 1)).TotalSeconds));
        }

        public static DateTime ConvertToDateTime(int clarionDate)
        {
            return (new DateTime(1800, 12, 28) + new TimeSpan(clarionDate, 0, 0, 0));
        }

        public static int ConvertToClarionDate(DateTime dateTime)
        {
            DateTime date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return Convert.ToInt32(Math.Abs((date - new DateTime(1800, 12, 28)).TotalDays));
        }

        public static DateTime ConvertMillisecondsToDateTime(int milliseconds)
        {
            return new DateTime().AddMilliseconds(milliseconds * 10);
        }

        public static int ConvertDateTimeToMilliseconds(DateTime dateTime)
        {
            return (int)(new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second).TotalMilliseconds) / 10;
        }

        private string Hash(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);

            return BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
        }

        public static string GetErrorDescription(ErrorType method, int error)
        {

            var info = new XLKomunikatInfo_20232
            {
                Wersja = ApiVersion,
                Funkcja = (int)method,
                Blad = error
            };

            cdn_api.cdn_api.XLOpisBledu(info);

            return info.OpisBledu;
        }

        private int XL_GetVersion()
        {
            int result = ResultException;

            try
            {
                int version = ApiVersion;
                result = cdn_api.cdn_api.XLSprawdzWersje(ref version);
                if (result != 0)
                {
                    var message = $"XLSprawdzWersje: {result}";

                    LogErrorEvent?.Invoke(message);
                    gLog.Write(message);
                    
                    return version;
                }

                result = version;
            }
            catch (Exception ex)
            {
                LogErrorEvent?.Invoke(ex.Message);
                gLog.Write(ex.ToString());
            }

            return result;
        }

        #endregion
    }
}
