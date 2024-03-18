using cdn_api;
using InterProcessCommunication.gTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService.Database;

namespace WinService.CdnApi
{
    public partial class CdnApiClient
    {
        public enum OrderType : uint
        {
            ZZ = 5,
            ZS = 6
        }

        public int AddTestOrder(ref int documentId, ref string errorMessage)
        {
            int result = ResultException;

            int creationDate = ConvertToClarionDate(DateTime.Now);
            int realizationDate = ConvertToClarionDate(DateTime.Now);
            int expirationDate = ConvertToClarionDate(DateTime.Now);

            try
            {
#if DEBUG
                var contractorCode = "K1";
                var description = "Test QWMS";                
#endif
                var doc = new XLDokumentZamNagInfo_20232
                {
                    Wersja = ApiVersion,
                    Tryb = 2, //wsadowy
                    Typ = (int)OrderType.ZS,
                    FormaPl = 20,
                    Akronim = contractorCode,
                    DataWystawienia = creationDate,
                    DataRealizacji = realizationDate,
                    DataWaznosci = expirationDate,
                    FlagaNB = "N",
                    RezerwujZasoby = -1, //z definicji dokumentu
                    Opis = description,                    
                };
                
                result = cdn_api.cdn_api.XLNowyDokumentZam(_sessionId, ref documentId, doc);
                if (result != 0)
                {
                    errorMessage = GetErrorDescription(ErrorType.NowyDokumentZam, result);
                    var message = $"XLNowyDokumentZam: {result} - {errorMessage}";

                    LogErrorEvent?.Invoke(message);

                    return result;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                LogErrorEvent?.Invoke(ex.Message);
                gLog.Write(ex.ToString());
            }

            return result;
        }

        public int AddTestOrderItem(int documentId, ref string errorMessage)
        {
            int result = ResultException;

            try
            {
#if DEBUG
                var code = "T1";                
#endif

                var doc = new XLDokumentZamElemInfo_20232
                {
                    Wersja = ApiVersion,
                    Towar = code,
                    Ilosc = "1",
                    CenaSpr = 0, //0 - domyslna dla kontrahenta                    
                };

                result = cdn_api.cdn_api.XLDodajPozycjeZam(documentId, doc);
                if (result != 0)
                {
                    errorMessage = $"{code} - {GetErrorDescription(ErrorType.DodajPozycjeZam, result)}";
                    var message = $"XLDodajPozycjeZam: {result} - Error: {errorMessage}";

                    LogErrorEvent?.Invoke(message);

                    return result;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                LogErrorEvent?.Invoke(ex.Message);
                gLog.Write(ex.ToString());
            }

            return result;
        }

        public int CloseTestOrder(bool error, ref int documentId, ref string errorMessage)
        {
            int result = ResultException;

            try
            {
                int save = 0;
                int delete = 1;
                int confirm = 2;

                bool saveToBuffer = true;// order.IsPayed == false;

                var doc = new XLZamkniecieDokumentuZamInfo_20232
                {
                    Wersja = ApiVersion,
                    TrybZamkniecia = error ? delete : (saveToBuffer ? save : confirm)
                };

                result = cdn_api.cdn_api.XLZamknijDokumentZam(documentId, doc);
                if (result != 0)
                {
                    errorMessage = GetErrorDescription(ErrorType.ZamknijDokumentZam, result);
                    var message = $"XLZamknijDokumentZam: {result} - {errorMessage}";

                    LogErrorEvent?.Invoke(message);

                    documentId = -1;
                }

                documentId = doc.ZamNumer;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                LogErrorEvent?.Invoke(ex.Message);
                gLog.Write(ex.ToString());
            }

            return result;
        }
    }
}
