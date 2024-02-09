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

        //public int AddOrder(OrderRequestModel model, ref int documentId, ref string errorMessage)
        //{
        //    int result = ResultException;

        //    int creationDate = model.Date?.Creation > DateTime.MinValue ? ConvertToClarionDate(model.Date.Creation) : ConvertToClarionDate(DateTime.Now);
        //    int realizationDate = model.Date?.Realization > DateTime.MinValue ? ConvertToClarionDate(model.Date.Realization) : 0;
        //    int expirationDate = model.Date?.Expiration > DateTime.MinValue ? ConvertToClarionDate(model.Date.Expiration) : 0;

        //    try
        //    {
        //        var doc = new XLDokumentZamNagInfo_20232
        //        {
        //            Wersja = ApiVersion,
        //            Tryb = 2, //wsadowy
        //            Typ = model.DocumentType == 1 ? (int)OrderType.ZS : (int)OrderType.ZZ,
        //            FormaPl = model.Payment?.Type > 0 ? model.Payment.Type : 20,
        //            Akronim = model.Contractor.Main,
        //            AkronimDocelowego = model.Contractor.Target ?? string.Empty,
        //            DokumentObcy = model.Name ?? string.Empty,
        //            DataWystawienia = creationDate,
        //            DataRealizacji = realizationDate,
        //            DataWaznosci = expirationDate,
        //            FlagaNB = "N",
        //            RezerwujZasoby = -1, //z definicji dokumentu
        //            TerminPlatnosci = model.Payment?.Delay ?? 0,
        //            Opis = model.Description,
        //            Waluta = model.Payment?.Currency ?? string.Empty,
        //            ExpoNorm = model.Contractor.ExpoNorm,
        //            Magazyn = model.WarehouseCode,
        //            SposobDst = model.DeliveryMethod,
        //            ZamSeria = model.Series ?? string.Empty,
        //        };

        //        if (model.Contractor.PayerId > 0)
        //        {
        //            doc.KnPNumer = model.Contractor.PayerId;
        //            doc.KnPTyp = (int)DatabaseClient.ObjectType.Contractor;
        //        }

        //        if (model.ShippingAddressId > 0)
        //        {
        //            doc.AdwNumer = model.ShippingAddressId;
        //            doc.AdwTyp = (int)DatabaseClient.ObjectType.ShippingAddress;
        //        }

        //        result = cdn_api.cdn_api.XLNowyDokumentZam(_sessionId, ref documentId, doc);
        //        if (result != 0)
        //        {
        //            errorMessage = GetErrorDescription(ErrorType.NowyDokumentZam, result);
        //            var message = $"XLNowyDokumentZam: {result} - {errorMessage}";

        //            LogErrorEvent?.Invoke(message);

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogErrorEvent?.Invoke(ex.Message);
        //        gLog.Write(ex.ToString());
        //    }

        //    return result;
        //}

        //public int AddOrderItem(OrderItemModel model, int documentId, ref string errorMessage)
        //{
        //    int result = ResultException;

        //    try
        //    {
        //        var doc = new XLDokumentZamElemInfo_20232
        //        {
        //            Wersja = ApiVersion,
        //            Towar = model.Code,
        //            Ilosc = model.Count.ToString(),
        //            CenaKatalogowa = (model.Price >= 0) ? model.Price.ToString() : string.Empty,
        //            CenaSpr = model.PriceOrdinalId, //0 - domyslna dla kontrahenta
        //            Rabat = (model.Discount >= 0) ? model.Discount.ToString() : string.Empty,
        //            Waluta = model.Currency,
        //            Cecha = model.Feature,
        //        };

        //        //if (unit != null)
        //        //{
        //        //    doc.Ilosc = (item.Count * unit.Numerator / unit.Denominator).ToString();
        //        //    doc.JmZ = unit.Name;
        //        //    doc.PrzeliczL = unit.Numerator;
        //        //    doc.PrzeliczM = unit.Denominator;
        //        //    doc.TypJm = 1; //dziesietny
        //        //    doc.IgnorujJmTwr = 1;
        //        //}

        //        result = cdn_api.cdn_api.XLDodajPozycjeZam(documentId, doc);
        //        if (result != 0)
        //        {
        //            errorMessage = $"{model.Code} - {GetErrorDescription(ErrorType.DodajPozycjeZam, result)}";
        //            var message = $"XLDodajPozycjeZam: {result} - Error: {errorMessage}";

        //            LogErrorEvent?.Invoke(message);

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogErrorEvent?.Invoke(ex.Message);
        //        gLog.Write(ex.ToString());
        //    }

        //    return result;
        //}

        //public int CloseOrder(bool error, ref int documentId, ref string errorMessage)
        //{
        //    int result = ResultException;

        //    try
        //    {
        //        int save = 0;
        //        int delete = 1;
        //        int confirm = 2;

        //        bool saveToBuffer = true;// order.IsPayed == false;

        //        var doc = new XLZamkniecieDokumentuZamInfo_20232
        //        {
        //            Wersja = ApiVersion,
        //            TrybZamkniecia = error ? delete : (saveToBuffer ? save : confirm)
        //        };

        //        result = cdn_api.cdn_api.XLZamknijDokumentZam(documentId, doc);
        //        if (result != 0)
        //        {
        //            errorMessage = GetErrorDescription(ErrorType.ZamknijDokumentZam, result);
        //            var message = $"XLZamknijDokumentZam: {result} - {errorMessage}";

        //            LogErrorEvent?.Invoke(message);

        //            documentId = -1;
        //        }

        //        documentId = doc.ZamNumer;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogErrorEvent?.Invoke(ex.Message);
        //        gLog.Write(ex.ToString());
        //    }

        //    return result;
        //}
    }
}
