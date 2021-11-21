using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Ple.Response;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces.Sale;
using Fiby.Cloud.Web.DTO.Modules.Sale.Request.Serie;
using Fiby.Cloud.Web.Client.Extensions.Sunat;
using System.Xml.Serialization;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.IO.Compression;
using System.ServiceModel;
using System.Net;
using servicioFacturacion2021;
using System.ServiceModel.Channels;

namespace Fiby.Cloud.Web.Client.Areas.Sale.Controllers
{
    [Area("Sale")]
    public class GeneratorSaleController : Controller
    {

        private readonly IPleService _pleService;
        private readonly ISerieService _serieService;

        public GeneratorSaleController(IPleService pleService,
                                        ISerieService serieService)
        {
            _pleService = pleService;
            _serieService = serieService;
        }

        public async Task<IActionResult> Index()
        {
            SerieDTORequest serieDTORequest = new SerieDTORequest();
            serieDTORequest.Description = serieDTORequest.Description == null ? string.Empty : serieDTORequest.Description;

            ViewBag.listaSerie = await _serieService.GetSerieAll(serieDTORequest);

            return View();
        }

        //[HttpPost]
        public async Task<FileResult> GetPleAll(string mes)
        {
            //pleDTORequest.SequenceCUO = pleDTORequest.Description == null ? string.Empty : pleDTORequest.Description;

            TextWriter tw = new StreamWriter(Path.Combine(Path.GetTempPath(), "plantilla.txt"), true);

            //var model = await _pleService.GetPleAll(pleDTORequest);

            PLE14100DTORequest pLE14100DTORequest = new PLE14100DTORequest();
            pLE14100DTORequest.CODIGO = 0;
            pLE14100DTORequest.MesLista = "01/" + mes + "/2021";

            var model = await _pleService.GetPleAllNew(pLE14100DTORequest);

            for (int i = 0; i < model.Count; i++)
            {
                tw.WriteLine(model[i].Line);
            }

            tw.Close();

            //return File(Path.Combine(Path.GetTempPath(), "plantilla.txt"), "text/plain");

            byte[] byteArray = System.IO.File.ReadAllBytes(Path.Combine(Path.GetTempPath(), "plantilla.txt"));

            //var reportName = "Plev1";

            //Response.Headers.Add("Content-Disposition", String.Format("attachment; filename={0}", $"{reportName}_{DateTime.Now.ToString()}.txt"));
            //return new FileContentResult(byteArray, "text/plain");

            //String text = "Hola mundo" + Environment.NewLine + "Hoy es Martes";
            //byte[] result = Encoding.ASCII.GetBytes(text);
            return File(byteArray, "text/plain", "ple.txt");
        }


        [HttpPost]
        public async Task<string> RegistrarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest)
        {
            string resultado = string.Empty;
            pLE14100DTORequest.IdEmpresa = Int16.Parse(User.Identity.CompanyId());
            try
            {
                resultado = await _pleService.RegistrarPle14100DPorMes(pLE14100DTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<JsonResult> BuscarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest, int indicadorInicio)
        {
            pLE14100DTORequest.IdEmpresa = Int16.Parse(User.Identity.CompanyId());

            if (indicadorInicio == 1)
            {
                pLE14100DTORequest.MesLista = DateTime.Now.ToString("dd/MM/yyyy");
            }

            var model = await _pleService.GetPlePLE14100All(pLE14100DTORequest);
            return Json(model);
        }

        [HttpPost]
        public async Task<string> GenerarXML()
        {
            string resultado = string.Empty;
            PleDTORequest pleDTORequest = new PleDTORequest();
            resultado = await _pleService.GetPleAll(pleDTORequest);

            #region TODO

            

            //InvoiceType Factura = new InvoiceType();

            ////CABECERA XML
            //Factura.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            //Factura.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            //Factura.Ccts = "urn:un:unece:uncefact:documentation:2";
            //Factura.Ds = "http://www.w3.org/2000/09/xmldsig#";
            //Factura.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
            //Factura.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2";
            //Factura.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";
            //UBLExtensionType[] uBLExtensions = new UBLExtensionType[11];
            //UBLExtensionType uBLExtension = new UBLExtensionType();
            //uBLExtensions[0] = uBLExtension;
            //Factura.UBLExtensions = uBLExtensions;


            ////VERSIONES UBL
            //Factura.UBLVersionID = new UBLVersionIDType();
            //Factura.UBLVersionID.Value = "2.1";//TRAER DE LA DB EMPRESA
            //Factura.CustomizationID = new CustomizationIDType();
            //Factura.CustomizationID.Value = "2.0"; //TRAER DE LA DB EMPRESA


            ////SERIE Y NUMERO
            //Factura.ID = new IDType();
            //Factura.ID.Value = "F001" + "-" + "00000100"; //TRAER DE BD 

            ////Fecha de Emision
            //Factura.IssueDate = new IssueDateType();
            //Factura.IssueDate.Value = DateTime.Now;

            //Factura.IssueTime = new IssueTimeType();
            //string hora = Convert.ToDateTime(DateTime.Now).ToString("HH:mm:ss");
            //Factura.IssueTime.Value = Convert.ToDateTime(hora);

            ////Fecha de Vencimiento
            //Factura.DueDate = new DueDateType();
            //Factura.DueDate.Value = DateTime.Now;

            ////TIPO DE FACTURA
            //InvoiceTypeCodeType invoiceTypeCodeType = new InvoiceTypeCodeType();
            //invoiceTypeCodeType.listID = "0101"; //FACTURA VENTA INTERNA
            //invoiceTypeCodeType.listAgencyName = "PE:SUNAT";
            //invoiceTypeCodeType.listName = "Tipo de Documento";
            //invoiceTypeCodeType.name = "Tipo de Operacion";
            //invoiceTypeCodeType.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01";
            //invoiceTypeCodeType.listSchemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo51";
            //invoiceTypeCodeType.Value = "01"; //TRAER DE DB TIPO COMPROBANTE 
            //Factura.InvoiceTypeCode = invoiceTypeCodeType;

            ////LEYENDA
            //NoteType leyenda = new NoteType();
            //leyenda.languageLocaleID = "1000"; //TABLA 52
            //leyenda.Value = "MONTO EN SOLES";
            //List<NoteType> notas = new List<NoteType>();
            //notas.Add(leyenda);
            //Factura.Note = notas.ToArray();

            ////TIPO DE MONEDA
            //DocumentCurrencyCodeType moneda = new DocumentCurrencyCodeType() {
            //    listID = "ISO 4217 Alpha",
            //    listName = "Currency",
            //    listAgencyName = "United Nations Economic Commission for Europe",
            //    Value = "PEN"
            //};

            //Factura.DocumentCurrencyCode = moneda;

            ////CANTIDAD DE PRODUCTOS
            //LineCountNumericType numeroProductos = new LineCountNumericType();
            //numeroProductos.Value = 1;
            //Factura.LineCountNumeric = numeroProductos;

            ////DATOS DE LA EMPRESA EMISORA
            //SignatureType Firma = new SignatureType();
            //SignatureType[] Firmas = new SignatureType[2];
            //PartyType partySign = new PartyType();
            //PartyIdentificationType partyIdentification = new PartyIdentificationType();
            //PartyIdentificationType[] partyIdentifications = new PartyIdentificationType[2];
            //IDType idFirma = new IDType();
            //idFirma.Value = "20606961805"; //RUC DE EMISOR
            //Firma.ID = idFirma;

            //partyIdentification.ID = idFirma;
            //partyIdentifications[0] = partyIdentification;
            //partySign.PartyIdentification = partyIdentifications;
            //Firma.SignatoryParty = partySign;

            //NoteType Nota = new NoteType();
            //NoteType[] Notas = new NoteType[2];
            //Nota.Value = "Elaborado por Sistema Fiby Web";
            //Notas[0] = Nota;
            //Firma.Note = Notas;

            ////RAZON SOCIAL
            //PartyNameType partyName = new PartyNameType();
            //PartyNameType[] partyNames = new PartyNameType[2];
            //NameType1 RazonSocialFirma = new NameType1();
            //RazonSocialFirma.Value = "FIBY OS SERVICE E.I.R.L.";
            //partyName.Name = RazonSocialFirma;
            //partyNames[0] = partyName;
            //partySign.PartyName = partyNames;

            ////CODIGO DE DOCUMENTO IDENTIDAD EMISOR
            //SupplierPartyType empresa = new SupplierPartyType();
            //PartyType party = new PartyType();
            //List<PartyIdentificationType> partyidentifications = new List<PartyIdentificationType>();
            //PartyIdentificationType partyidentification = new PartyIdentificationType();
            //IDType idEmpresa = new IDType();
            //idEmpresa.schemeID = "6";
            //idEmpresa.schemeName = "Documento de Identidad";
            //idEmpresa.schemeAgencyName = "PE:SUNAT";
            //idEmpresa.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            //idEmpresa.Value = "20606961805";
            //partyidentification.ID = idEmpresa;
            //partyidentifications.Add(partyidentification);
            //party.PartyIdentification = partyidentifications.ToArray();

            //PartyNameType partyname = new PartyNameType();
            //List<PartyNameType> partynames = new List<PartyNameType>();
            //NameType1 nameEmisor = new NameType1();
            //nameEmisor.Value = "FIBY";
            //partyname.Name = nameEmisor;
            //partynames.Add(partyname);
            //party.PartyName = partynames.ToArray();

            ////ESTABLECIMIENTOS ANEXOS
            //List<PartyLegalEntityType> partylegals = new List<PartyLegalEntityType>();
            //PartyLegalEntityType partylegal = new PartyLegalEntityType();
            //RegistrationNameType registronombre = new RegistrationNameType();
            //registronombre.Value = "FIBY OS SERVICE E.I.R.L.";
            //partylegal.RegistrationName = registronombre;

            ////UBIGEO
            //AddressType direccion = new AddressType();
            //IDType iddireccion = new IDType();
            //iddireccion.schemeAgencyName = "PE:INEI";
            //iddireccion.schemeName = "Ubigeos";
            //iddireccion.Value = "110102"; //UBIGEO DE LA EMPRESA EMISORA
            //direccion.ID = iddireccion;
            //AddressTypeCodeType anexo = new AddressTypeCodeType();
            //anexo.listAgencyName = "PE:SUNAT";
            //anexo.listName = "Establecimientos anexos";
            //anexo.Value = "0000";
            //direccion.AddressTypeCode = anexo;

            ////DIRECCION FISCAL EMPRESA EMISORA
            //CityNameType Departamento = new CityNameType();
            //Departamento.Value = "ICA";// BASE DE DATOS
            //direccion.CityName = Departamento;

            //CountrySubentityType provincia = new CountrySubentityType();
            //provincia.Value = "ICA";
            //direccion.CountrySubentity = provincia;

            //DistrictType distrito = new DistrictType();
            //distrito.Value = "LA TINGUIÑA";
            //direccion.District = distrito;

            //List<AddressLineType> direcciones = new List<AddressLineType>();
            //AddressLineType direccionEmisor = new AddressLineType();
            //LineType datalocal = new LineType();
            //datalocal.Value = "AV. RIO DE JANERIO A.H. EL MANTARO MZA. H LOTE. 1 DPTO. B CRUCE CON AV MANTARO";
            //direccionEmisor.Line = datalocal;
            //direcciones.Add(direccionEmisor);
            //direccion.AddressLine = direcciones.ToArray();
            //CountryType pais = new CountryType();
            //IdentificationCodeType codigoPais = new IdentificationCodeType();
            //codigoPais.listName = "Country";
            //codigoPais.listAgencyName = "United Nations Economic Commission for Europe";
            //codigoPais.listID = "ISO 3166-1";
            //codigoPais.Value = "PE";
            //pais.IdentificationCode = codigoPais;
            //direccion.Country = pais;
            //partylegal.RegistrationAddress = direccion;
            //partylegals.Add(partylegal);
            ////party.PartyIdentification = partyIdentifications;
            //party.PartyName = partynames.ToArray();
            //party.PartyLegalEntity = partylegals.ToArray();
            //empresa.Party = party;
            //Factura.AccountingSupplierParty = empresa;

            ////Empresa receptora (Cliente)
            //CustomerPartyType cliente = new CustomerPartyType();
            //PartyType partyCliente = new PartyType();
            //List<PartyIdentificationType> partyIdentificationClientes = new List<PartyIdentificationType>();
            //PartyIdentificationType partyIdentificationCliente = new PartyIdentificationType();
            //IDType idtipoCliente = new IDType();
            //idtipoCliente.schemeID = "6";
            //idtipoCliente.schemeName = "Documento de Identidad";
            //idtipoCliente.schemeAgencyName = "PE:SUNAT";
            //idtipoCliente.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06";
            //idtipoCliente.Value = "20112302370"; //RUC DEL CLIENTE
            //partyIdentificationCliente.ID = idtipoCliente;
            //partyIdentificationClientes.Add(partyIdentificationCliente);
            //partyCliente.PartyIdentification = partyIdentificationClientes.ToArray();

            //////Razon social cliente
            //List<PartyLegalEntityType> partylegalClientes = new List<PartyLegalEntityType>();
            //PartyLegalEntityType partylegalCliente = new PartyLegalEntityType();
            //RegistrationNameType razonsocialcliente = new RegistrationNameType();
            //razonsocialcliente.Value = "NOVOLIZ S.A."; // RAZON SOCIAL CLIENTE
            //partylegalCliente.RegistrationName = razonsocialcliente;

            //////Direccion del cliente (OPCIONAL)
            //AddressType direccionclienteType = new AddressType();
            //List<AddressLineType> direccionclientes = new List<AddressLineType>();
            //AddressLineType direccioncliente = new AddressLineType();
            //List<LineType> lineas = new List<LineType>();
            //LineType linea = new LineType();
            //linea.Value = "JR. LOS FORESTALES 444 URB. LAS ACACIAS ALT CDRA 05 LOS CONSTRUCTORES LIMA-LIMA-LA MOLINA";
            //direccioncliente.Line = linea;
            //direccionclientes.Add(direccioncliente);
            //direccionclienteType.AddressLine = direccionclientes.ToArray();
            //partylegalCliente.RegistrationAddress = direccionclienteType; //AGREGADO
            //partylegalClientes.Add(partylegalCliente);
            //partyCliente.PartyLegalEntity = partylegalClientes.ToArray();
            //cliente.Party = partyCliente;
            //Factura.AccountingCustomerParty = cliente;

            //#region <cac:PaymentTerms> Forma de pago
            //PaymentTermsType tipopago = new PaymentTermsType();
            //PaymentTermsType[] tipopagos = new PaymentTermsType[2];
            //IDType idpago = new IDType();
            //idpago.Value = "FormaPago";
            //tipopago.ID = idpago;

            //PaymentMeansIDType formapago = new PaymentMeansIDType();
            //PaymentMeansIDType[] formapagos = new PaymentMeansIDType[1];
            //formapago.Value = "Contado";
            //formapagos[0] = formapago;
            //tipopago.PaymentMeansID = formapagos;

            //tipopagos[0] = tipopago;
            //Factura.PaymentTerms = tipopagos;
            //#endregion

            //#region <cac:TaxTotal> IMPUESTOS AL TOTAL
            ////<cac:TaxTotal>
            //////<cbc:TaxAmount
            //TaxTotalType TotalImptos = new TaxTotalType();
            //List<TaxTotalType> TotalImptosLista = new List<TaxTotalType>();

            //TaxAmountType taxAmountImpto = new TaxAmountType();
            //taxAmountImpto.currencyID = "PEN";
            //taxAmountImpto.Value = decimal.Parse("10,53");  //IGV
            //TotalImptos.TaxAmount = taxAmountImpto;
            //////</cbc:TaxAmount>

            //////<cac:TaxSubtotal>
            //List<TaxSubtotalType> subtotales = new List<TaxSubtotalType>();
            //TaxSubtotalType subtotal = new TaxSubtotalType();

            ////////<cbc:TaxableAmount
            //TaxableAmountType taxsubtotal = new TaxableAmountType();
            //taxsubtotal.currencyID = "PEN";
            //taxsubtotal.Value = decimal.Parse("58,48"); //SUBTOTAL
            //subtotal.TaxableAmount = taxsubtotal;
            ////////</cbc:TaxableAmount>

            ////////<cbc:TaxAmount
            //TaxAmountType TotalTaxAmountTotal = new TaxAmountType();
            //TotalTaxAmountTotal.currencyID = "PEN";
            //TotalTaxAmountTotal.Value = decimal.Parse("10,53");  //IGV
            //subtotal.TaxAmount = TotalTaxAmountTotal;
            //subtotales.Add(subtotal);
            //TotalImptos.TaxSubtotal = subtotales.ToArray();
            ////////</cbc:TaxAmount>

            ////////<cac:TaxCategory>
            //TaxCategoryType taxcategoryTotal = new TaxCategoryType();
            //////////<cac:TaxScheme>
            //TaxSchemeType taxScheme = new TaxSchemeType();
            ////////////<cbc:ID
            //IDType idtotal = new IDType();
            //idtotal.schemeName = "Codigo de tributos";
            //idtotal.schemeAgencyName = "PE:SUNAT";
            //idtotal.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";
            //idtotal.Value = "1000";
            //taxScheme.ID = idtotal;
            ////////////</cbc:ID>
            ////////////<cbc:Name>
            //NameType1 nombreImpuesto = new NameType1();
            //nombreImpuesto.Value = "IGV";
            //taxScheme.Name = nombreImpuesto;
            ////////////</cbc:Name>
            ////////////<cbc:TaxTypeCode>
            //TaxTypeCodeType taxtypecodeImpto = new TaxTypeCodeType();
            //taxtypecodeImpto.Value = "VAT";
            //taxScheme.TaxTypeCode = taxtypecodeImpto;
            ////////////<cbc:TaxTypeCode>
            //////////</cac:TaxScheme>
            //taxcategoryTotal.TaxScheme = taxScheme;
            ////////</cac:TaxCategory>
            //subtotal.TaxCategory = taxcategoryTotal;
            //////</cac:TaxSubtotal>
            ////subtotales.Add(subtotal);
            ////TotalImptos.TaxSubtotal = subtotales.ToArray();
            //TotalImptosLista.Add(TotalImptos);
            //Factura.TaxTotal = TotalImptosLista.ToArray();
            ////</cac:TaxTotal>
            //#endregion

            //#region <cac:LegalMonetaryTotal> TOTALES
            //MonetaryTotalType TotalValorVenta = new MonetaryTotalType();
            //LineExtensionAmountType lineExtensionAmount = new LineExtensionAmountType();
            //lineExtensionAmount.currencyID = "PEN";
            //lineExtensionAmount.Value = 58.48m; //SUBTOTAL
            //TotalValorVenta.LineExtensionAmount = lineExtensionAmount;

            //TaxInclusiveAmountType taxInclusiveAmount = new TaxInclusiveAmountType();
            //taxInclusiveAmount.currencyID = "PEN";
            //taxInclusiveAmount.Value = 69; //TOTAL
            //TotalValorVenta.TaxInclusiveAmount = taxInclusiveAmount;

            //AllowanceTotalAmountType allowanceTotalAmount = new AllowanceTotalAmountType();
            //allowanceTotalAmount.currencyID = "PEN";
            //allowanceTotalAmount.Value = 0.00m;
            //TotalValorVenta.AllowanceTotalAmount = allowanceTotalAmount;

            //PrepaidAmountType prepaidAmount = new PrepaidAmountType();
            //prepaidAmount.currencyID = "PEN";
            //prepaidAmount.Value = 0.00m;
            //TotalValorVenta.PrepaidAmount = prepaidAmount;

            //ChargeTotalAmountType chargeTotalAmount = new ChargeTotalAmountType();
            //chargeTotalAmount.currencyID = "PEN";
            //chargeTotalAmount.Value = 0.00m;
            //TotalValorVenta.ChargeTotalAmount = chargeTotalAmount;



            //PayableAmountType payableAmount = new PayableAmountType();
            //payableAmount.currencyID = "PEN";
            //payableAmount.Value = 69; //TOTAL
            //TotalValorVenta.PayableAmount = payableAmount;
            //Factura.LegalMonetaryTotal = TotalValorVenta;

            //#endregion

            //#region <cac:InvoiceLine> PRODUCTOS DE LA FACTURA
            //List<InvoiceLineType> items = new List<InvoiceLineType>();
            //int idtem = 1;
            ////foreach (LdetalleVenta detalle in parametros.Detalles)
            ////{
            //    InvoiceLineType item = new InvoiceLineType();
            //    IDType numeroItem = new IDType();
            //    numeroItem.Value = idtem.ToString();
            //    item.ID = numeroItem;

            //    InvoicedQuantityType cantidad = new InvoicedQuantityType();
            //    cantidad.unitCode = "ZZ";//detalle.Unidad_de_medida;
            //    cantidad.unitCodeListID = "UN/ECE rec 20";
            //    cantidad.unitCodeListAgencyName = "United Nations Economic Commission for Europe";
            //    cantidad.Value = 1;//detalle.cantidad;
            //    item.InvoicedQuantity = cantidad;

            //    LineExtensionAmountType ValorVenta = new LineExtensionAmountType();
            //    ValorVenta.currencyID = "PEN";
            //    ValorVenta.Value = 58.47m; // detalle.Total_a_pagar / (1 + 0.18m); SUBTOTAL DEL PRODUCTO  REAL CON TODO Y DECIMALES
            //    item.LineExtensionAmount = ValorVenta;

            //    PricingReferenceType ValorReferenUnitario = new PricingReferenceType();

            //    List<PriceType> TipoPrecios = new List<PriceType>();
            //    PriceType TipoPrecio = new PriceType();
            //    PriceAmountType PrecioMonto = new PriceAmountType();
            //    PrecioMonto.currencyID = "PEN";
            //    PrecioMonto.Value = 69.00m; //detalle.preciounitario; PRECIO FINAL DEL PRODUCTO  CON TODO Y DECIMALES 
            //    TipoPrecio.PriceAmount = PrecioMonto;

            //    PriceTypeCodeType TipoPrecioCode = new PriceTypeCodeType();
            //    TipoPrecioCode.listName = "Tipo de Precio";
            //    TipoPrecioCode.listAgencyName = "PE:SUNAT";
            //    TipoPrecioCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16";
            //    TipoPrecioCode.Value = "01";
            //    TipoPrecio.PriceTypeCode = TipoPrecioCode;

            //    TipoPrecios.Add(TipoPrecio);

            //    ValorReferenUnitario.AlternativeConditionPrice = TipoPrecios.ToArray();
            //    item.PricingReference = ValorReferenUnitario;

            //    List<TaxTotalType> Totales_Items = new List<TaxTotalType>();
            //    TaxTotalType Totales_Item = new TaxTotalType();
            //    TaxAmountType Total_Item = new TaxAmountType();
            //    Total_Item.currencyID = "PEN";
            //    Total_Item.Value = 10.53m; // detalle.mtoValorVentaItem - (detalle.mtoValorVentaItem / (1.18m)); IGV DEL PRODUCTO CON TODO Y DECIMALES
            //    Totales_Item.TaxAmount = Total_Item;

            //    List<TaxSubtotalType> subtotal_Items = new List<TaxSubtotalType>();
            //    TaxSubtotalType subtotal_Item = new TaxSubtotalType();
            //    TaxableAmountType taxsubtotal_IGVItem = new TaxableAmountType();
            //    taxsubtotal_IGVItem.currencyID = "PEN";
            //    taxsubtotal_IGVItem.Value = 58.47m;// detalle.mtoValorVentaItem / 1.18m; SUBTOTAL DEL PRODUCTO  REAL CON TODO Y DECIMALES
            //    subtotal_Item.TaxableAmount = taxsubtotal_IGVItem;

            //    TaxAmountType TotalTaxAmount_IGVItem = new TaxAmountType();
            //    TotalTaxAmount_IGVItem.currencyID = "PEN";
            //    TotalTaxAmount_IGVItem.Value = 10.53m;// detalle.mtoValorVentaItem - detalle.mtoValorVentaItem / 1.18m; IGV DEL PRODUCTO CON TODO Y DECIMALES
            //    subtotal_Item.TaxAmount = TotalTaxAmount_IGVItem;
            //    subtotal_Items.Add(subtotal_Item);

            //    TaxCategoryType taxcategory_IGVItem = new TaxCategoryType();
            //    PercentType1 porcentaje = new PercentType1();
            //    porcentaje.Value = 18.00m;//detalle.porIgvItem * 100; PORCENTAJE DE IGV
            //    taxcategory_IGVItem.Percent = porcentaje;
            //    subtotal_Item.TaxCategory = taxcategory_IGVItem;

            //    TaxExemptionReasonCodeType ReasonCode = new TaxExemptionReasonCodeType();
            //    ReasonCode.listAgencyName = "PE:SUNAT";
            //    ReasonCode.listName = "Afectacion del IGV";
            //    ReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07";
            //    ReasonCode.Value = "10";
            //    taxcategory_IGVItem.TaxExemptionReasonCode = ReasonCode;

            //    TaxSchemeType taxscheme_IGVItem = new TaxSchemeType();
            //    IDType id2_IGVItem = new IDType();
            //    id2_IGVItem.schemeName = "Codigo de tributos";
            //    id2_IGVItem.schemeAgencyName = "PE:SUNAT";
            //    id2_IGVItem.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo05";
            //    id2_IGVItem.Value = "1000";
            //    taxscheme_IGVItem.ID = id2_IGVItem;

            //    NameType1 nombreImpto_IGVItem = new NameType1();
            //    nombreImpto_IGVItem.Value = "IGV";
            //    taxscheme_IGVItem.Name = nombreImpto_IGVItem;

            //    TaxTypeCodeType nombreImpto_IGVItemInter = new TaxTypeCodeType();
            //    nombreImpto_IGVItemInter.Value = "VAT";
            //    taxscheme_IGVItem.TaxTypeCode = nombreImpto_IGVItemInter;

            //    taxcategory_IGVItem.TaxScheme = taxscheme_IGVItem;

            //    Totales_Item.TaxSubtotal = subtotal_Items.ToArray();
            //    Totales_Items.Add(Totales_Item);
            //    item.TaxTotal = Totales_Items.ToArray();


            //    ItemType itemTipo = new ItemType();
            //    DescriptionType description = new DescriptionType();
            //    List<DescriptionType> descriptions = new List<DescriptionType>();
            //    description.Value = "SERVICIO DE LIMPIEZA 5 HORAS"; //DESCRIPCION DEL SERVICIO
            //    descriptions.Add(description);

            //    ItemIdentificationType codigoProd = new ItemIdentificationType();
            //    IDType id = new IDType();
            //    id.Value = "";//detalle.Codigo; CODIGO Q SIEMPRE DEJO VACIO
            //    codigoProd.ID = id;
            //    itemTipo.Description = descriptions.ToArray();
            //    itemTipo.SellersItemIdentification = codigoProd;

            //    //List<CommodityClassificationType> codSunats = new List<CommodityClassificationType>();
            //    //CommodityClassificationType codSunat = new CommodityClassificationType();
            //    //ItemClassificationCodeType codClas = new ItemClassificationCodeType();
            //    //codClas.listID = "UNSPSC";
            //    //codClas.listAgencyName = "GS1 US";
            //    //codClas.listName = "Item Classification";
            //    //codClas.Value = "25172405";
            //    //codSunat.ItemClassificationCode = codClas;
            //    //codSunats.Add(codSunat);
            //    //itemTipo.CommodityClassification = codSunats.ToArray();


            //    PriceType PrecioProducto = new PriceType();
            //    PriceAmountType PrecioMontoTipo = new PriceAmountType();
            //    PrecioMontoTipo.currencyID = "PEN";
            //    PrecioMontoTipo.Value = 58.47m;//detalle.preciounitario / (detalle.porIgvItem + 1); SUBTOTAL
            //    PrecioProducto.PriceAmount = PrecioMontoTipo;


            //    item.Item = itemTipo;
            //    item.Price = PrecioProducto;
            //    items.Add(item);
            //    idtem += 1;
            ////}
            //Factura.InvoiceLine = items.ToArray();

            //#endregion


            ////AddressType direccionCliente = new AddressType();
            ////IDType iddireccionCliente = new IDType();
            ////iddireccionCliente.schemeAgencyName = "PE:INEI";
            ////iddireccionCliente.schemeName = "Ubigeos";
            ////iddireccionCliente.Value = "150114"; //UBIGEO DE LA EMPRESA EMISORA
            ////direccionCliente.ID = iddireccionCliente;
            ////AddressTypeCodeType anexoCliente = new AddressTypeCodeType();
            ////anexoCliente.listAgencyName = "PE:SUNAT";
            ////anexoCliente.listName = "Establecimientos anexos";
            ////anexoCliente.Value = "0000";
            ////direccionCliente.AddressTypeCode = anexoCliente;

            ////CityNameType DepartamentoCliente = new CityNameType();
            ////DepartamentoCliente.Value = "LIMA";// BASE DE DATOS
            ////direccionCliente.CityName = DepartamentoCliente;

            ////CountrySubentityType provinciaCliente = new CountrySubentityType();
            ////provinciaCliente.Value = "LIMA";
            ////direccionCliente.CountrySubentity = provinciaCliente;

            ////DistrictType distritoCliente = new DistrictType();
            ////distritoCliente.Value = "LA MOLINA";
            ////direccionCliente.District = distritoCliente;

            ////List<AddressLineType> direccionesCliente = new List<AddressLineType>();
            ////AddressLineType direccionReceptor = new AddressLineType();
            ////LineType datalocalCliente = new LineType();
            ////datalocalCliente.Value = "JR. LOS FORESTALES 444 URB. LAS ACACIAS ALT CDRA 05 LOS CONSTRUCTORES";
            ////direccionReceptor.Line = datalocalCliente;
            ////direcciones.Add(direccionEmisor);
            ////direccion.AddressLine = direcciones.ToArray();
            ////CountryType pais = new CountryType();
            ////IdentificationCodeType codigoPais = new IdentificationCodeType();
            ////codigoPais.listName = "Country";
            ////codigoPais.listAgencyName = "United Nations Economic Commission for Europe";
            ////codigoPais.listID = "ISO 3166-1";
            ////codigoPais.Value = "PE";
            ////pais.IdentificationCode = codigoPais;
            ////direccion.Country = pais;
            ////partylegal.RegistrationAddress = direccion;
            ////partylegals.Add(partylegal);
            ////party.PartyIdentification = partyIdentifications;
            ////party.PartyName = partynames.ToArray();
            ////party.PartyLegalEntity = partylegals.ToArray();
            ////empresa.Party = party;
            ////Factura.AccountingSupplierParty = empresa;



            //XmlWriterSettings propiedades = new XmlWriterSettings();
            //propiedades.Indent = true;
            //propiedades.IndentChars = "\t";
            //string rutaxml = @"D:\20606961805-01-F001-00000100.xml";
            //string rutaMover = @"D:\TEST\20606961805-01-F001-00000100.xml";
            //string rutaTEST = @"D:\TEST";
            //string rutaZip = @"D:\20606961805-01-F001-00000100.zip";
            //string rutaCertificado = @"D:\LLAMA-PE-CERTIFICADO-DEMO-20606961805.pfx";
            //string clave = "123456";
            //using (XmlWriter escribir = XmlWriter.Create(rutaxml,propiedades))
            //{
            //    Type serializacion = typeof(InvoiceType);
            //    XmlSerializer crearxml = new XmlSerializer(serializacion);
            //    crearxml.Serialize(escribir, Factura);

            //}

            //FirmarXML(rutaxml, rutaCertificado, clave);
            //System.IO.File.Move(rutaxml, rutaMover);
            
            //ComprimirZip(rutaTEST, rutaZip);
            //Enviardocumento(rutaZip);

            #endregion
            return resultado;
        }

        public string FirmarXML(string cRutaArchivo, string cCertificado, string cClave)
        {

            string file = cRutaArchivo;
            string text = System.IO.File.ReadAllText(file);
            text = text.Replace(@"<ext:UBLExtension />", @"<ext:UBLExtension> <ext:ExtensionContent /></ext:UBLExtension>");
            text = text.Replace("xsi:type=", "");
            text = text.Replace("cbc:SerialIDType", "");
            text = text.Replace("\"\"", "");
            System.IO.File.WriteAllText(file, text);
            string cRpta;
            string tipo = Path.GetFileName(cRutaArchivo);

            string local_typoDocumento = tipo.Substring(12, 2);
            string l_xpath = "";
            string f_certificat = cCertificado;
            string f_pwd = cClave;
            string xmlFile = cRutaArchivo;

            X509Certificate2 MonCertificat = new X509Certificate2(f_certificat, f_pwd);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(xmlFile);
            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = MonCertificat.PrivateKey;
            KeyInfo KeyInfo = new KeyInfo();
            Reference Reference = new Reference();
            Reference.Uri = "";
            Reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(Reference);
            X509Chain X509Chain = new X509Chain();
            X509Chain.Build(MonCertificat);
            X509ChainElement local_element = X509Chain.ChainElements[0];
            KeyInfoX509Data x509Data = new KeyInfoX509Data(local_element.Certificate);
            string subjectName = local_element.Certificate.Subject;
            x509Data.AddSubjectName(subjectName);
            KeyInfo.AddClause(x509Data);
            signedXml.KeyInfo = KeyInfo;
            signedXml.ComputeSignature();
            XmlElement signature = signedXml.GetXml();
            signature.Prefix = "ds";
            signedXml.ComputeSignature();
            foreach (XmlNode node in signature.SelectNodes("descendant-or-self::*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#']"))
            {

                if (node.LocalName == "Signature")
                {
                    XmlAttribute newAttribute = xmlDoc.CreateAttribute("Id");
                    newAttribute.Value = "SignSUNAT";
                    node.Attributes.Append(newAttribute);
                }
            }
            XmlNamespaceManager nsMgr;
            nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
            nsMgr.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2");
            nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            switch (local_typoDocumento)
            {
                case "01":
                case "03" // factura y boleta
               :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
                        l_xpath = "/tns:Invoice/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }

                case "07" // nota de credito
         :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");
                        l_xpath = "/tns:CreditNote/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }

                case "08" // nota de debito
                    :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2");
                        l_xpath = "/tns:DebitNote/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }


            }
            nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            nsMgr.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            nsMgr.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlDoc.SelectSingleNode(l_xpath, nsMgr).AppendChild(xmlDoc.ImportNode(signature, true));
            xmlDoc.Save(xmlFile);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("ds:Signature");
            if (nodeList.Count != 1)
            {
                //MessageBox.Show("Problemas con la firma");
                cRpta = "Problemas con la firma";
            }
            signedXml.LoadXml((XmlElement)nodeList[0]);
            if (signedXml.CheckSignature() == false)
                cRpta = "No se logro firmar el comprobante";
            else
                cRpta = "OK";
            return cRpta;
        }

        public void ComprimirZip(string nombrearchivo, string rutadestino)
        {

            ZipFile.CreateFromDirectory(nombrearchivo, rutadestino);
        }

        public void Enviardocumento(string Archivo)
        {
            string filezip = Archivo;
            string filepath = filezip;
            byte[] bitArray = System.IO.File.ReadAllBytes(filepath);
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                EndpointAddress remoteAddress = new EndpointAddress("https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService");
                billServiceClient servicio = new billServiceClient(binding, remoteAddress);
                ServicePointManager.UseNagleAlgorithm = true;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.CheckCertificateRevocationList = true;
                servicio.ClientCredentials.UserName.UserName = "20606961805MODDATOS";
                servicio.ClientCredentials.UserName.Password = "MODDATOS";

                var elements = servicio.Endpoint.Binding.CreateBindingElements();
                //elements.Find<SecurityBindingElement>().EnableUnsecuredResponse = true;
                servicio.Endpoint.Binding = new CustomBinding(elements);
                servicio.Open();
                filezip = Path.GetFileName(filezip);
                byte[] returByte = servicio.sendBill(filezip, bitArray, "0");
                servicio.Close();
                filezip = Path.GetFileName(filezip);
                FileStream fs = new FileStream(@"D:\" + "R-" + filezip, FileMode.Create);
                fs.Write(returByte, 0, returByte.Length);
                fs.Close();
                //MessageBox.Show("Archivo generado con exito");

                //var respuesta = new EmitirFactura();
                var respuesat = ObtenerRespuestaZIPSunat(@"D:\" + "R-" + filezip);
                var test = 0;
                test = 1;
            }
            catch (FaultException ex)
            {
                ////MessageBox.Show(ex.Code.Name);
            }

        }

        public string[] ObtenerRespuestaZIPSunat(string ruta)
        {
            FileInfo arch = new FileInfo(ruta);
            if (arch.Extension == ".zip")
            {
                return LeerRespuestaCDR(ruta, Path.GetDirectoryName(ruta));
            }
            else
            {
                return null;
            }
        }

        public string[] LeerRespuestaCDR(string ruta, string nomfile)
        {
            string r = "";
            string file = "";
            string[] datos = new string[3];
            try
            {
                using (ZipArchive zip = ZipFile.Open(ruta, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry zentry = null;
                    file = zip.Entries[1].ToString();
                    zentry = zip.GetEntry(file);
                    XmlDocument xd = new XmlDocument();
                    xd.Load(zentry.Open());
                    XmlNodeList xnl = xd.GetElementsByTagName("cbc:Description");
                    foreach (XmlElement item in xnl)
                    {
                        r = item.InnerText;
                    }
                    //MessageBox.Show(r);

                }
            }
            catch (Exception)
            {
            }
            datos[0] = r;
            datos[1] = file;
            datos[2] = nomfile;
            return datos;
        }

    }
}
